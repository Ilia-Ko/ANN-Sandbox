using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Gtk;
using Cairo;
using Rosenblatt;

using MathNet.Spatial.Euclidean;

public partial class MainWindow : Window {

	// neuro + data
	bool isInitialized;
	Rosenblatt.Rosenblatt perceptron;
	int dimensionality;
	List<DataPoint> points;
	Random random;
	double[] randPlane;
	// visual
	ImageSurface surface;
	HScale[] scales;
	double[] sector;
	int axis1, axis2, axis3;

	// 3D Render System
	const double INIT_DIST = 3.0;
	const double INIT_ROT = 0.0, Y_ANG = Math.PI / 3.0;
	const double INIT_CAM_DIST = 6.0;
	Plane proj;
	UnitVector3D xAxis, yAxis;
	bool mode3D = true;

	// window
	public MainWindow() : base(WindowType.Toplevel) {
		Build();
		isInitialized = false;
		random = new Random();
		// visual
		surface = new ImageSurface(Format.RGB24, 2000, 2000);
		root.ExposeEvent += (sender, e) => {
			Context gc = Gdk.CairoHelper.Create(root.GdkWindow);
			gc.SetSourceSurface(surface, 0, 0);
			gc.Paint();
			gc.Dispose();
		};
		root.AddEvents((int)Gdk.EventMask.ButtonPressMask);

		// geometry
		double dx = Math.Sin(INIT_ROT) * Math.Sin(Y_ANG);
		double dy = Math.Cos(Y_ANG);
		double dz = Math.Cos(INIT_ROT) * Math.Sin(Y_ANG);
		UnitVector3D norm = new UnitVector3D(dx, dy, dz);
		Point3D rootPoint = norm.ScaleBy(INIT_DIST).ToPoint3D();
		proj = new Plane(rootPoint, norm);
		yAxis = proj.Project(new UnitVector3D(0, 1, 0)).Direction;
		xAxis = yAxis.CrossProduct(norm);
	}
	protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
		surface.Dispose();
		Application.Quit();
		a.RetVal = true;
	}

	// data
	protected void BtnDataSave(object sender, EventArgs e) {
		if (!isInitialized) {
			ShowDlg("There is nothing to save.", MessageType.Info);
			return;
		}

		// get file
		FileChooserDialog dialog = new FileChooserDialog(
					"Select a data file:", this, FileChooserAction.Save,
					"Cancel", ResponseType.Cancel,
					"Open", ResponseType.Accept);

		// write file
		if (dialog.Run() == (int)ResponseType.Accept) {
			StreamWriter writer;
			writer = File.CreateText(dialog.Filename);
			WriteDataFile(writer);
			writer.Flush();
			writer.Close();
		}
		dialog.Destroy();
	}
	protected void BtnDataChoose(object sender, EventArgs e) {
		// get file
		FileChooserDialog dialog = new FileChooserDialog(
					"Select a data file:", this, FileChooserAction.Open,
					"Cancel", ResponseType.Cancel,
					"Open", ResponseType.Accept);

		// read file
		if (dialog.Run() == (int) ResponseType.Accept) {
			isInitialized = false;
			ReadDataFile(dialog.Filename);
			// make perceptron
			perceptron = new Rosenblatt.Rosenblatt(dimensionality);
			// update visual
			UpdateAxisAndCombo();
			// update controls
			lblQuality.Text = $"Quality: {TestPerceptron():N}";
			RenderRoot();
		}

		// finalize
		dialog.Destroy();
	}
	protected void BtnDataLearn(object sender, EventArgs e) {
		if (!isInitialized) {
			ShowDlg("Choose some data first.", MessageType.Info);
			return;
		}

		perceptron.Learn(points.ToArray(), 1728);
		lblQuality.Text = $"Quality: {TestPerceptron():N}";
		RenderRoot();
	}
	protected void ButtonRandom(object sender, EventArgs e) {
		if (!isInitialized) return;

		// randomize hyperplane
        if (randPlane == null) {
			randPlane = new double[dimensionality + 1];
			for (int i = 0; i <= dimensionality; i++)
				randPlane[i] = (random.NextDouble() - 0.5) * 2.0;
		}

		// randomize points
		int num = (int) (12 * Math.Pow(2, dimensionality));
        for (int i = 0; i < num; i++) {
			double[] vec = new double[dimensionality];
			double sum = randPlane[dimensionality];
            for (int j = 0; j < dimensionality; j++) {
				vec[j] = (random.NextDouble() - 0.5) * 2.0;
				sum += randPlane[j] * vec[j];
			}
			points.Add(new DataPoint(dimensionality, vec, Math.Sign(sum)));
		}
		RenderRoot();
	}

    // visual
	protected void BtnVisualRedraw(object sender, EventArgs e) {
		RenderRoot();
	}
	protected void ComboFirstAxis(object sender, EventArgs e) {
		if (dimensionality < 3 || (dimensionality == 3 && mode3D)) return;

		int code = comboAxis1.Active;
		if (code == axis1) return;
		if (code == axis2 || code == axis3) {
			comboAxis1.Active = axis1;
			return;
		}

		scales[axis1].Sensitive = true;
		axis1 = comboAxis1.Active;
		scales[axis1].Sensitive = false;
		RenderRoot();
	}
	protected void ComboSecondAxis(object sender, EventArgs e) {
		if (dimensionality < 3 || (dimensionality == 3 && mode3D)) return;

		int code = comboAxis2.Active;
		if (code == axis2) return;
		if (code == axis1 || code == axis3) {
			comboAxis1.Active = axis2;
			return;
		}

		scales[axis2].Sensitive = true;
		axis2 = comboAxis2.Active;
		scales[axis2].Sensitive = false;
		RenderRoot();
	}
    protected void ComboThirdAxis(object sender, EventArgs e) {
		if (dimensionality < 3 || (dimensionality == 3 && mode3D)) return;

		int code = comboAxis3.Active;
		if (code == axis3) return;
		if (code == axis1 || code == axis2) {
			comboAxis3.Active = axis3;
			return;
		}

		scales[axis3].Sensitive = true;
		axis3 = comboAxis3.Active;
		scales[axis3].Sensitive = false;
		RenderRoot();
	}
	protected void HScaleRotate(object sender, EventArgs e) {
		double ang = hscaleRot.Value * Math.PI;
		double dx = Math.Sin(ang) * Math.Sin(Y_ANG);
		double dy = Math.Cos(Y_ANG);
		double dz = Math.Cos(ang) * Math.Sin(Y_ANG);
		UnitVector3D norm = new UnitVector3D(dx, dy, dz);
		Point3D rootPoint = norm.ScaleBy(hscaleDist.Value).ToPoint3D();
		proj = new Plane(rootPoint, norm);
		RenderRoot();
	}
	protected void HScaleDistance(object sender, EventArgs e) {
		UnitVector3D norm = proj.Normal;
		Point3D rootPoint = norm.ScaleBy(hscaleDist.Value).ToPoint3D();
		proj = new Plane(rootPoint, norm);
		RenderRoot();
	}
	protected void HScaleCamera(object sender, EventArgs e) {
		RenderRoot();
	}
    protected void Toggle3DMode(object sender, EventArgs e) {
		mode3D = toggle3D.Active;
		hscaleRot.Sensitive = mode3D;
		hscaleDist.Sensitive = mode3D;
		hscaleCamera.Sensitive = mode3D;
		root.Sensitive = !mode3D;
		RenderRoot();
	}
	protected void RootButtonPressed(object sender, ButtonPressEventArgs args) {
		if (!isInitialized || mode3D) return;

		int btn = (int)args.Event.Button;
		double x = args.Event.X * 2.0 / surface.Width - 1.0;
		double y = 1.0 - args.Event.Y * 2.0 / surface.Height;

		double[] coords = new double[dimensionality];
		if (dimensionality > 2) Array.Copy(sector, coords, dimensionality);
		coords[axis1] = x;
		coords[axis2] = y;

		DataPoint point = new DataPoint(dimensionality, coords, btn - 2);
		points.Add(point);
		RenderRoot();
	}
	void ShowDlg(string message, MessageType type) {
		MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
													type, ButtonsType.Close, message);
		dialog.Run();
		dialog.Destroy();
	}

	// data logic
	void ReadDataFile(string name) {
		string[] lines = File.ReadAllLines(name);
		int size = 0;

		// parse dimensionality
		if (lines[0].StartsWith("dim="))
			dimensionality = Convert.ToInt32(lines[0].Substring(4));
		else throw new Exception("No dim line in data file!");

		// parse number of points
		if (lines[1].StartsWith("num="))
			size = Convert.ToInt32(lines[1].Substring(4));
		else throw new Exception("No num line in data file!");

		// parse data
		points = new List<DataPoint>();
		for (int i = 2; i < lines.Length; i++) {
			// get coords
			string[] coords = lines[i].Split('\t');
			double[] x = new double[dimensionality];
			for (int j = 1; j <= dimensionality; j++)
				x[j-1] = Convert.ToDouble(coords[j]);
			// make a data point
			points.Add(new DataPoint(dimensionality, x, Convert.ToInt32(coords[0])));
		}

		axis1 = 0;
		axis2 = 1;
		axis3 = 2;
		isInitialized = true;
		randPlane = null;
	}
	void WriteDataFile(StreamWriter writer) {
		// write header
		StringBuilder content = new StringBuilder();
		content.AppendLine($"dim={dimensionality}");
		content.AppendLine($"num={points.Count}");

		// write data
		foreach (DataPoint sample in points) {
			content.Append(sample.y);
			for (int i = 0; i < dimensionality; i++) {
				content.Append('\t');
				content.Append(sample.x[i]);
			}
			content.AppendLine();
		}

		// write file
		writer.Write(content);
	}
	double TestPerceptron() {
		if (!isInitialized) {
			ShowDlg("Choose some data first.", MessageType.Info);
			return 0.0;
		}

		int correct = 0;

		foreach (DataPoint point in points)
			if (perceptron.Classify(point.x) == point.y) correct++;

		return correct / (double) points.Count;
	}

	// visual logic
	void UpdateAxisAndCombo() {
		comboAxis1.Clear();
		comboAxis2.Clear();
		comboAxis3.Clear();

		// fill combos
		CellRendererText maker = new CellRendererText();
		comboAxis1.PackStart(maker, false);
		comboAxis1.AddAttribute(maker, "text", 0);
		comboAxis2.PackStart(maker, false);
		comboAxis2.AddAttribute(maker, "text", 0);
		comboAxis3.PackStart(maker, false);
		comboAxis3.AddAttribute(maker, "text", 0);
		for (int i = 0; i < dimensionality; i++) {
			comboAxis1.AppendText($"axis A{i}");
			comboAxis2.AppendText($"axis A{i}");
			comboAxis3.AppendText($"axis A{i}");
		}

		// clear old scales
		if (scales?.Length > 0)
			foreach (HScale scale in scales) {
				scale.Visible = false;
				axisBox.Remove(scale);
				scale.Destroy();
			}
		// create new scales
		if (dimensionality > 2) {
			sector = new double[dimensionality];
			// fill axisBox
			scales = new HScale[dimensionality];
			for (int i = 0; i < dimensionality; i++) {
				scales[i] = new HScale(-1, +1, 0.001) { TooltipText = $"Axis A{i}", Value = 0.0 };
				scales[i].ValueChanged += (sender, e) => {
					HScale me = (HScale)sender;
					int index = Convert.ToInt32(me.TooltipText.Substring(6));
					sector[index] = scales[index].Value;
					RenderRoot();
				};
				scales[i].Visible = true;
				scales[i].Sensitive = (i != axis1) && (i != axis2) && (i != axis3);
				axisBox.Add(scales[i]);
			}
		}

		// setup
		axis1 = 0;
		axis2 = 1;
		axis3 = 2;
		comboAxis1.Active = 0;
		comboAxis2.Active = 1;
		comboAxis3.Active = 2;
	}
	void RenderRoot() {
		Context gc = new Context(surface);
		double w = surface.Width;
		double h = surface.Height;

		// clear
		gc.SetSourceRGB(1.0, 1.0, 1.0);
		gc.Paint();

		// coordinates
		gc.Scale(w / 2.0, h / 2.0);
		gc.Translate(1.0, 1.0);

		if (mode3D) {
			if (isInitialized && dimensionality > 2) {
				// coordinate system
				Point3D rootProj = proj.Project(new Point3D(0, 0, 0));
				Point3D camera = rootProj + proj.Normal.ScaleBy(hscaleCamera.Value);
				yAxis = proj.Project(new UnitVector3D(0, 1, 0)).Direction;
				xAxis = yAxis.CrossProduct(proj.Normal);

				// render axis and planes
				Vector3D xs = Project(new Point3D(-1, 0, 0), camera);
				Vector3D xe = Project(new Point3D(+1, 0, 0), camera);
				Vector3D ys = Project(new Point3D(0, -1, 0), camera);
				Vector3D ye = Project(new Point3D(0, +1, 0), camera);
				Vector3D zs = Project(new Point3D(0, 0, -1), camera);
				Vector3D ze = Project(new Point3D(0, 0, +1), camera);
				// draw
				gc.LineWidth = 0.003;
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

				// render points
				gc.NewPath();
				foreach (DataPoint point in points) {
					if (!point.IsVisible(axis1, axis2, axis3, sector)) continue;
					// project and translate
					double[] vec0 = new double[3];
					vec0[0] = point.x[axis1];
					vec0[1] = point.x[axis2];
					vec0[2] = point.x[axis3];
					Point3D p0 = new Point3D(vec0);
					Point3D p1 = Project(p0, camera).ToPoint3D();
					Vector3D dp = rootProj.VectorTo(p1);
					// draw point
					PointD p = TranslateToCS(dp);
					gc.Arc(p.X, p.Y, 0.006, 0.0, 6.3);
					gc.SetSourceRGB(point.y, 0, 1 - point.y);
					gc.Fill();
				}
				gc.ClosePath();

				{
					// render plane
					//// get root 1
					//double[] vec1 = new double[dimensionality];
					//if (dimensionality > 3) Array.Copy(sector, vec1, dimensionality);
					//vec1[axis2] = 0.0;
					//vec1[axis3] = 0.0;
					//double l = -1.0, r = +1.0;
					//vec1[axis1] = -1.0;
					//int ly = perceptron.Classify(vec1);
					//vec1[axis1] = +1.0;
					//int ry = perceptron.Classify(vec1);
					//int iter = 0;
					//            while (l < r && iter < 1728) {
					//	double c = (l + r) / 2.0;
					//	vec1[axis1] = c;
					//	int y = perceptron.Classify(vec1);
					//	if (ly * y > 0) {
					//		l = c;
					//		ly = y;
					//	} else if (ry * y > 0) {
					//		r = c;
					//		ry = y;
					//	}
					//	iter++;
					//}
					//Point3D pPlane1 = new Point3D(l, 0.0, 0.0);
					//// get root 2
					//vec1[axis1] = 0.0;
					//vec1[axis3] = 0.0;
					//l = -1.0; r = +1.0;
					//vec1[axis2] = -1.0;
					//ly = perceptron.Classify(vec1);
					//vec1[axis2] = +1.0;
					//ry = perceptron.Classify(vec1);
					//iter = 0;
					//while (l < r && iter < 1728) {
					//	double c = (l + r) / 2.0;
					//	vec1[axis2] = c;
					//	int y = perceptron.Classify(vec1);
					//	if (ly * y > 0) {
					//		l = c;
					//		ly = y;
					//	} else if (ry * y > 0) {
					//		r = c;
					//		ry = y;
					//	}
					//	iter++;
					//}
					//Point3D pPlane2 = new Point3D(0.0, l, 0.0);
					//// get root 3
					//vec1[axis1] = 0.0;
					//vec1[axis2] = 0.0;
					//l = -1.0; r = +1.0;
					//vec1[axis3] = -1.0;
					//ly = perceptron.Classify(vec1);
					//vec1[axis3] = +1.0;
					//ry = perceptron.Classify(vec1);
					//iter = 0;
					//while (l < r && iter < 1728) {
					//	double c = (l + r) / 2.0;
					//	vec1[axis3] = c;
					//	int y = perceptron.Classify(vec1);
					//	if (ly * y > 0) {
					//		l = c;
					//		ly = y;
					//	} else if (ry * y > 0) {
					//		r = c;
					//		ry = y;
					//	}
					//	iter++;
					//}
					//Point3D pPlane3 = new Point3D(0.0, 0.0, l);
					//// render it
					//Plane plane = new Plane(pPlane1, pPlane2, pPlane3);
					//double a = 2.0;
					//Point3D i1 = plane.IntersectionWith(new Ray3D(new Point3D(-a, -a, -a), new UnitVector3D(1, 0, 0)));
					//Point3D i2 = plane.IntersectionWith(new Ray3D(new Point3D(-a, -a, -a), new UnitVector3D(1, 0, 0)));
					//Point3D i3 = plane.IntersectionWith(new Ray3D(new Point3D(-a, -a, -a), new UnitVector3D(1, 0, 0)));
					//PointD pi1 = TranslateToCS(Project(i1, camera));
					//PointD pi2 = TranslateToCS(Project(i2, camera));
					//PointD pi3 = TranslateToCS(Project(i3, camera));
					//Console.WriteLine($"{0}\t{1}", pi1.X, pi1.Y);
					//Console.WriteLine($"{0}\t{1}", pi2.X, pi2.Y);
					//Console.WriteLine($"{0}\t{1}", pi3.X, pi3.Y);
					//Console.WriteLine();
					//gc.NewPath();
					//gc.SetSourceRGBA(0.0, 1.0, 0.0, 0.6);
					//gc.MoveTo(pi1);
					//gc.LineTo(pi2);
					//gc.LineTo(pi3);
					//gc.ClosePath();
					//gc.Fill();
				}

				// render plane
				double pw = 0.003;
				int num = 48;
				gc.NewPath();
				gc.SetSourceRGB(0.0, 0.0, 0.0);
				for (int ix = -num; ix <= num; ix++) {
					double x = ix / (double)num;
					for (int iy = -num; iy <= num; iy++) {
						double y = iy / (double)num;
						int prevRes = 0;
						for (int iz = -num; iz <= num; iz++) {
							double z = iz / (double)num;
							double[] vec2 = new double[dimensionality];
							if (dimensionality > 3) Array.Copy(sector, vec2, dimensionality);
							vec2[axis1] = x;
							vec2[axis2] = y;
							vec2[axis3] = z;
							int res = perceptron.Classify(vec2);
							if (res == prevRes) continue;
                            if (prevRes == 0) {
								prevRes = res;
								continue;
							}
							prevRes = res;
							// project and translate
							double[] pointVec = { x, y, z };
							Point3D p0 = new Point3D(pointVec);
							Point3D p1 = Project(p0, camera).ToPoint3D();
							Vector3D dp = rootProj.VectorTo(p1);
							// draw point
							PointD p = TranslateToCS(dp);
							gc.Rectangle(p.X - pw / 2.0, p.Y - pw / 2.0, pw, pw);
							gc.Fill();
						}
					}
				}
			}
		} else {
			// axis + net
			gc.LineWidth = 0.003;
			gc.SetSourceRGB(0.0, 0.0, 0.0);
			// horizontal
			gc.MoveTo(-1.0, 0.0);
			gc.LineTo(+1.0, 0.0);
			// vertical
			gc.MoveTo(0.0, -1.0);
			gc.LineTo(0.0, +1.0);
			gc.Stroke();
			// net
			gc.LineWidth = 0.001;
			for (int i = -6; i <= 6; i++) {
				double a = i / 6.0;
				gc.MoveTo(a, -1.0);
				gc.LineTo(a, +1.0);
				gc.MoveTo(-1.0, a);
				gc.LineTo(+1.0, a);
			}
			gc.Stroke();

			// points
			if (isInitialized) {
				// render points
				if (dimensionality > 2) {
					foreach (DataPoint point in points)
						if (point.IsVisible(axis1, axis2, axis3, sector)) {
							gc.Arc(point.x[axis1], -point.x[axis2], 0.01, 0.0, 6.3);
							gc.SetSourceRGB(point.y, 0, 1 - point.y);
							gc.Fill();
						}
				} else {
					foreach (DataPoint point in points) {
						gc.Arc(point.x[0], -point.x[1], 0.01, 0.0, 6.3);
						gc.SetSourceRGB(point.y, 0.0, 1.0 - point.y);
						gc.Fill();
					}
				}
				// render classification fields
				double pw = 0.006;
				int num = 36;
				for (int x = -num; x <= num; x++)
					for (int y = -num; y <= num; y++) {
						double px = x / (double)num;
						double py = y / (double)num;
						// classify
						double[] vec = new double[dimensionality];
						if (dimensionality > 2) Array.Copy(sector, vec, dimensionality);
						vec[axis1] = px;
						vec[axis2] = py;
						int result = perceptron.Classify(vec);
						// render
						gc.Rectangle(px - pw / 2.0, -py - pw / 2.0, pw, pw);
						gc.SetSourceRGBA(result, 0.0, 1.0 - result, 0.5);
						gc.Fill();
					}
			}
		}

		// update
		gc.Dispose();
		root.QueueDraw();
	}
    Vector3D Project(Point3D p, Point3D camera) {
		Line3D line = new Line3D(p, camera);
		Point3D i = proj.IntersectionWith(line) ?? new Point3D(0, 0, 0);
		return i.ToVector3D();
	}
	PointD TranslateToCS(Vector3D dp) {
		double x = dp.ProjectOn(xAxis).Length * Math.Sign(dp.DotProduct(xAxis));
		double y = dp.ProjectOn(yAxis).Length * Math.Sign(dp.DotProduct(yAxis));
		return new PointD(x, -y);
	}

}
