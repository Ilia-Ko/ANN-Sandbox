using System;
using Cairo;
using MathNet.Spatial.Euclidean;

namespace MultiPerceptron
{

	public class RenderSystem3DProjection
	{

		const double AXIS_THICKNESS = 0.002;
		const double CAM = 1.0;
		const double INIT_RADIUS = 3.0;
		const double INIT_PHI = Math.PI / 6.0;
		const double INIT_THETA = Math.PI / 4.0;
		readonly static Color DEF_COLOUR = new Color(0.0, 0.8, 0.6);

		// render options
		public Surface Surface { get; set; }
		public bool DrawAxis { get; set; } = true;
		public bool DrawPlanes { get; set; } = true;
		public double PointThickness { get; set; } = 0.003;
		public Color[] GroupColours { get; set; }
		// position
		double phi, theta, radius = INIT_RADIUS;
		double AnglePhi
		{
			get { return phi; }
			set
			{
				phi = value;
				double x = radius * Math.Sin(theta) * Math.Cos(phi);
				double y = radius * Math.Sin(theta) * Math.Sin(phi);
				double z = radius * Math.Cos(theta);
				Vector3D root = new Vector3D(x, y, z);
				ProjectionPlane = new Plane(root.ToPoint3D(), root.Normalize());
			}
		}
		double AngleTheta
		{
			get { return theta; }
			set
			{
				theta = value;
				double x = radius * Math.Sin(theta) * Math.Cos(phi);
				double y = radius * Math.Sin(theta) * Math.Sin(phi);
				double z = radius * Math.Cos(theta);
				Vector3D root = new Vector3D(x, y, z);
				ProjectionPlane = new Plane(root.ToPoint3D(), root.Normalize());
			}
		}
		double Radius
		{
			get { return radius; }
			set
			{
				radius = value;
				UnitVector3D root = projPlane.Normal;
				ProjectionPlane = new Plane(root.ScaleBy(radius).ToPoint3D(), root);
			}
		}
		// projection
		Plane projPlane;
		UnitVector3D hAxis, vAxis;
		Plane ProjectionPlane
		{
			get { return projPlane; }
			set
			{
				projPlane = value;
				UnitVector3D Oy = new UnitVector3D(0, 1, 0);
				hAxis = projPlane.Project(new Ray3D(projPlane.RootPoint, Oy)).Direction;
				vAxis = projPlane.Normal.CrossProduct(hAxis);
			}
		}

		public RenderSystem3DProjection(Surface surface)
		{
			Surface = surface;
			projPlane = new Plane();
			AnglePhi = INIT_PHI;
			AngleTheta = INIT_THETA;
			Radius = INIT_RADIUS;
		}

		public void Render(Vector3D[][] points, double w, double h)
		{
			// setup
			Context gc = new Context(Surface);
			gc.Scale(w / 2.0, h / 2.0);
			gc.Translate(1.0, 1.0);
			gc.SetSourceRGBA(1, 1, 1, 1);
			gc.Paint();

			Point3D root = projPlane.Project(new Point3D(0, 0, 0));
			Point3D camera = root + projPlane.Normal.ScaleBy(CAM);

			// rendering
			if (DrawPlanes) RenderPlanes(gc, camera);
			if (DrawAxis) RenderAxis(gc, camera);
            for (int g = 0; g < points.Length; g++)
			{
				gc.SetSourceColor(GroupColours?[g] ?? DEF_COLOUR);
				RenderPoints(gc, camera, root, points[g]);
			}
			

			// finalize
			gc.Dispose();
		}
		void RenderPlanes(Context gc, Point3D camera)
		{
			// yz
			gc.NewPath();
			gc.MoveTo(TranslateToCS(Project(new Point3D(0, -1, -1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(0, +1, -1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(0, +1, +1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(0, -1, +1), camera)));
			gc.ClosePath();
			gc.SetSourceRGBA(0.0, 1.0, 1.0, 0.3);
			gc.Fill();
			// xz
			gc.NewPath();
			gc.MoveTo(TranslateToCS(Project(new Point3D(-1, 0, -1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(-1, 0, +1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(+1, 0, +1), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(+1, 0, -1), camera)));
			gc.ClosePath();
			gc.SetSourceRGBA(1.0, 0.0, 1.0, 0.3);
			gc.Fill();
			// xy
			gc.NewPath();
			gc.MoveTo(TranslateToCS(Project(new Point3D(-1, -1, 0), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(-1, +1, 0), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(+1, +1, 0), camera)));
			gc.LineTo(TranslateToCS(Project(new Point3D(+1, -1, 0), camera)));
			gc.ClosePath();
			gc.SetSourceRGBA(1.0, 1.0, 0.0, 0.3);
			gc.Fill();
		}
		void RenderAxis(Context gc, Point3D camera)
		{
			// render axis and planes
			Vector3D xs = Project(new Point3D(-1, 0, 0), camera);
			Vector3D xe = Project(new Point3D(+1, 0, 0), camera);
			Vector3D ys = Project(new Point3D(0, -1, 0), camera);
			Vector3D ye = Project(new Point3D(0, +1, 0), camera);
			Vector3D zs = Project(new Point3D(0, 0, -1), camera);
			Vector3D ze = Project(new Point3D(0, 0, +1), camera);
			// draw
			gc.LineWidth = AXIS_THICKNESS;
			gc.SetFontSize(0.05);
			// x
			gc.NewPath();
			gc.MoveTo(TranslateToCS(xs));
			gc.LineTo(TranslateToCS(xe));
			gc.SetSourceRGB(1.0, 0.0, 0.0);
			gc.ShowText("x");
			gc.Stroke();
			// y
			gc.NewPath();
			gc.MoveTo(TranslateToCS(ys));
			gc.LineTo(TranslateToCS(ye));
			gc.SetSourceRGB(0.0, 1.0, 0.0);
			gc.ShowText("y");
			gc.Stroke();
			// z
			gc.NewPath();
			gc.MoveTo(TranslateToCS(zs));
			gc.LineTo(TranslateToCS(ze));
			gc.SetSourceRGB(0.0, 0.0, 1.0);
			gc.ShowText("z");
			gc.Stroke();
		}
		void RenderPoints(Context gc, Point3D camera, Point3D root, Vector3D[] points)
		{
			gc.NewPath();
			foreach (Vector3D point in points)
			{
				// project and translate
				double[] vec0 = { point.X, point.Y, point.Z };
				Point3D p0 = new Point3D(vec0);
				Point3D p1 = Project(p0, camera).ToPoint3D();
				Vector3D dp = root.VectorTo(p1);
				// draw point
				PointD p = TranslateToCS(dp);
				gc.Arc(p.X, p.Y, PointThickness, 0.0, 6.3);
				gc.Fill();
			}
			gc.ClosePath();
		}
		Vector3D Project(Point3D p, Point3D camera)
		{
			Line3D line = new Line3D(p, camera);
			Point3D i = projPlane.IntersectionWith(line) ?? new Point3D(0, 0, 0);
			return i.ToVector3D();
		}
		PointD TranslateToCS(Vector3D dp)
		{
			double x = dp.ProjectOn(hAxis).Length * Math.Sign(dp.DotProduct(hAxis));
			double y = dp.ProjectOn(vAxis).Length * Math.Sign(dp.DotProduct(vAxis));
			return new PointD(x, -y);
		}

	}

	public class RenderSystem2D
	{

		const double AXIS_THICKNESS = 0.002;
		readonly static Color DEF_COLOUR = new Color(0.0, 0.8, 0.6);

		// render options
		public Surface Surface { get; set; }
		public bool DrawAxis { get; set; } = true;
		public Point2D Root { get; set; } = new Point2D(0, 0);
		public double PointThickness { get; set; } = 0.003;
		public double LineThickness { get; set; } = AXIS_THICKNESS;
		public Color[] GroupColours { get; set; }

        public RenderSystem2D(Surface surface)
		{
			Surface = surface;
		}

		public void Render(Point2D[][] points, Line2D[] lines, double w, double h)
		{
			// setup
			Context gc = new Context(Surface);
			gc.Scale(w / 2.0, h / 2.0);
			gc.Translate(1.0 + Root.X, 1.0 + Root.Y);
			gc.SetSourceRGBA(1, 1, 1, 1);
			gc.Paint();

            // render
			if (DrawAxis) RenderAxis(gc);
			RenderLines(gc, lines);
            for (int g = 0; g < points.Length; g++)
			{
				gc.SetSourceColor(GroupColours?[g] ?? DEF_COLOUR);
				RenderPoints(gc, points[g]);
			}
		}
		void RenderAxis(Context gc)
		{
			gc.LineWidth = AXIS_THICKNESS;
			gc.SetSourceRGBA(0, 0, 0, 1);
			gc.NewPath();

			// Ox
			gc.MoveTo(-1.0 + Root.X, 0.0);
			gc.LineTo(+1.0 + Root.X, 0.0);
			// Oy
			gc.MoveTo(0.0, -1.0 + Root.Y);
			gc.MoveTo(0.0, +1.0 + Root.Y);

			gc.Stroke();
		}
		void RenderPoints(Context gc, Point2D[] points)
		{
			gc.NewPath();
            foreach (Point2D point in points)
			{
				gc.Arc(point.X, -point.Y, PointThickness / 2.0, 0.0, 6.3);
			}
			gc.Fill();
		}
		void RenderLines(Context gc, Line2D[] lines)
		{
			gc.SetSourceRGBA(0, 0, 0, 1);
			gc.LineWidth = LineThickness;
			gc.NewPath();
            foreach (Line2D line in lines)
			{
				gc.MoveTo(Cast(line.StartPoint));
				gc.LineTo(Cast(line.EndPoint));
			}
			gc.Stroke();
		}
        PointD Cast(Point2D point)
		{
			return new PointD(point.X, point.Y);
		}

	}

	public class RenderSystem3DTransform
	{

		readonly static Color DEF_COLOUR = new Color(0.0, 0.8, 0.6);

		// render options
		public Surface Surface { get; set; }
		public bool DrawAxis { get; set; } = true;
		public bool DrawPolygons { get; set; } = false;
		public double PointThickness { get; set; } = 0.003;
		public Color[] GroupColours { get; set; }

        public RenderSystem3DTransform(Surface surface)
		{
			Surface = surface;
		}

		public void Render(Vector3D[][] points, double w, double h)
		{

		}
		void RenderAxis(Context gc)
		{

		}
		void RenderPoints(Context gc, Vector3D[] points)
		{

		}

	}

}
