using System;
using Cairo;
using MathNet.Spatial.Euclidean;

namespace MultiPerceptron {

    public class RenderSystem {

		const double CAM = 1.0;
		const double INIT_RADIUS = 3.0;
		const double INIT_PHI = Math.PI / 6.0;
		const double INIT_THETA = Math.PI / 4.0;

		// render options
		Surface Surface { get; set; }
		bool DrawAxis { get; set; }
		bool DrawPlanes { get; set; }
		// position
		double phi, theta, radius;
        double AnglePhi {
			get { return phi; }
			set {
				phi = value;
				double x = radius * Math.Sin(theta) * Math.Cos(phi);
				double y = radius * Math.Sin(theta) * Math.Sin(phi);
				double z = radius * Math.Cos(theta);
				Vector3D root = new Vector3D(x, y, z);
				ProjectionPlane = new Plane(root.ToPoint3D(), root.Normalize());
			}
		}
		double AngleTheta {
			get { return theta; }
			set {
				theta = value;
				double x = radius * Math.Sin(theta) * Math.Cos(phi);
				double y = radius * Math.Sin(theta) * Math.Sin(phi);
				double z = radius * Math.Cos(theta);
				Vector3D root = new Vector3D(x, y, z);
				ProjectionPlane = new Plane(root.ToPoint3D(), root.Normalize());
			}
		}
		double Radius {
			get { return radius; }
			set {
				radius = value;
				UnitVector3D root = projPlane.Normal;
				ProjectionPlane = new Plane(root.ScaleBy(radius).ToPoint3D(), root);
			}
		}
		// projection
        Plane projPlane;
		UnitVector3D hAxis, vAxis;
        Plane ProjectionPlane {
			get { return projPlane; }
			set {
				projPlane = value;
				UnitVector3D Oy = new UnitVector3D(0, 1, 0);
				hAxis = projPlane.Project(new Ray3D(projPlane.RootPoint, Oy)).Direction;
				vAxis = projPlane.Normal.CrossProduct(hAxis);
			}
		}

		public RenderSystem() {
			projPlane = new Plane();
			Radius = INIT_RADIUS;
			AnglePhi = INIT_PHI;
			AngleTheta = INIT_THETA;
        }

        public void Render(Vector3D[] points, double w, double h) {
            // setup
			Context gc = new Context(Surface);
			gc.Scale(w / 2.0, h / 2.0);
			gc.Translate(1.0, 1.0);
			gc.SetSourceRGB(1.0, 1.0, 1.0);
			gc.Paint();

			// rendering
			if (DrawPlanes) RenderPlanes(gc);
			if (DrawAxis) RenderAxis(gc);
			RenderPoints(gc, points);

            // finalize
			gc.Dispose();
		}
        void RenderPlanes(Context gc) {

		}
        void RenderAxis(Context gc) {

		}
        void RenderPoints(Context gc, Vector3D[] points) {

		}

	}

}
