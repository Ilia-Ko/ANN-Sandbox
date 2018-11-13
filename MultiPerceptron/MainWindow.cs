using System;
using Gtk;
using Cairo;
using MultiPerceptron;
using MathNet.Spatial.Euclidean;
using MathNet.Numerics.LinearAlgebra;

public partial class MainWindow : Window
{

	enum ViewMode
	{
		Structure = 0,
		Outputs = 1,
		Learning = 2,
		ErrorSurface = 3
	}

	const int DISCRET_LEVEL = 24;
	const int MAX_DIMENSION = 6;

	// data
	bool hasPerceptron, isLearning;
	bool HasPerceptron
	{
		get { return hasPerceptron; }
		set
		{
			hasPerceptron = value;
			btnEdit.Sensitive = value;
			btnSave.Sensitive = value;
			frameLearn.Sensitive = value;
			frameVisual.Sensitive = value;
		}
	}
	Config conf;
	Perceptron ann;
    // perceptron.structure
	Point2D[][] structPoints;
	Line2D[] structLines;
	// visualization
	ViewMode VisualMode { get; set; }
	ImageSurface surf;
	RenderSystem3DProjection rs3DP;
	RenderSystem2D rs2D;
	RenderSystem3DTransform rs3DT;
	HScale[] scales;
	double[] sector;

	// initialize
	void Init()
	{
		// data
		conf.initialized = false;
		HasPerceptron = false;

		// visualization
		surf = new ImageSurface(Format.ARGB32, 1024, 1024);
		rs3DP = new RenderSystem3DProjection(surf)
		{
			DrawAxis = true,
			DrawPlanes = true
		};
		rs2D = new RenderSystem2D(surf)
		{
			DrawAxis = true,
			Root = new Point2D(-1, -1)
		};
		rs3DT = new RenderSystem3DTransform(surf)
		{
			DrawAxis = true
		};
		scales = new HScale[0];
		sector = new double[0];
	}

	// data logic
	void UpdatePerceptron()
	{
		if (!HasPerceptron && conf.initialized)
		{
			ann = new Perceptron(conf);
			HasPerceptron = true;
		}
		else if (HasPerceptron)
		{
			conf = ann.GetConfiguration();
		}
		else return;

		UpdateCombosAndScales();
		PlotStructure();
		VisualMode = ViewMode.Structure;
		UpdateView();
	}
	void PlotStructure()
	{
		if (!HasPerceptron) return;

		// layout
		double a = 2.0 / (conf.numHidLayers + 4.0);
		double[] b = {
			2.0 / (conf.numInputs + 1.0),
			2.0 / (conf.numHiddens + 1.0),
			2.0 / (conf.numOutputs + 1.0)
		};

		// prepare
		rs2D.PointThickness = 0.01;
		int numNeurons = conf.numInputs + conf.numHidLayers * conf.numHiddens + conf.numOutputs;
		int numLines = conf.numHiddens * (conf.numInputs + conf.numOutputs + conf.numHidLayers * conf.numHiddens);
		structPoints = new Point2D[conf.numHidLayers + 3][];
		structLines = new Line2D[numLines];
		int c = 0;

		// plot inputs & first hidden layer
		structPoints[0] = new Point2D[conf.numInputs];
		structPoints[1] = new Point2D[conf.numHiddens];
		for (int i = 0; i < conf.numInputs; i++)
		{
			Point2D p = new Point2D(a, b[0] * (i + 1));
			structPoints[0][i] = p;
			for (int j = 0; j < conf.numHiddens; j++)
			{
				Point2D q = new Point2D(a + a, b[1] * (j + 1));
				structPoints[1][j] = p;
				structLines[c++] = new Line2D(p, q);
			}
		}
		// plot hiddens
		for (int i = 2; i < conf.numHidLayers + 2; i++)
		{
			structPoints[i] = new Point2D[conf.numHiddens];
			for (int j = 0; j < conf.numInputs; j++)
			{
				Point2D p = new Point2D(a * i, b[1] * (j + 1));
				structPoints[i][j] = p;
				for (int k = 0; k < conf.numHiddens; k++)
				    structLines[c++] = new Line2D(p, structPoints[i - 1][k]);
			}
		}
		// plot outputs
		structPoints[conf.numHidLayers + 2] = new Point2D[conf.numOutputs];
		for (int i = 0; i < conf.numOutputs; i++)
		{
			Point2D p = new Point2D(2.0 - a, b[2] * (i + 1));
			structPoints[conf.numHidLayers + 2][i] = p;
			for (int j = 0; j < conf.numHiddens; j++)
			    structLines[c++] = new Line2D(p, structPoints[conf.numHidLayers + 1][j]);
		}
	}
	void PlotOutputs()
	{
		if (!HasPerceptron) return;

        // precompute
		int ax = comboX.Active;
		int ay = comboY.Active;
		int volume = DISCRET_LEVEL * DISCRET_LEVEL;
		double step = 2.0 / (DISCRET_LEVEL + 1.0);

        // create points
		Vector3D[][] points = new Vector3D[conf.numOutputs][];
        for (int o = 0; o < conf.numOutputs; o++)
		{
			points[o] = new Vector3D[volume];
		}

		// fill points
		int c = 0;
		for (int ix = 1; ix <= DISCRET_LEVEL; ix++)
		{
			sector[ax] = step * ix;
			for (int iy = 1; iy <= DISCRET_LEVEL; iy++)
			{
				sector[ay] = step * iy;
				Vector<double> point = CreateVector.DenseOfArray(sector);
				double[] values = ann.Response(point).ToArray();
                for (int o = 0; o < conf.numOutputs; o++)
				    points[o][c++] = new Vector3D(sector[ax], sector[ay], values[o]);
			}
		}

		// render points
		rs3DP.Render(points, surf.Width, surf.Height);
	}
	void PlotError()
	{
		if (!HasPerceptron) return;

        // fill current matrix of weights
		Matrix<double>[] weights = ann.GetWeights();
		int len = ann.GetNumOfWeights();
        for (int i = 0; i < len; i++)
		{
			int[] w = WrapIndex(i);
			Vector<double> row = weights[w[0]].Row(w[1]);
			row[w[2]] = sector[i];
			weights[w[0]].SetRow(w[1], row);
		}

        // precompute
		int[] wx = WrapIndex(comboX.Active);
		int[] wy = WrapIndex(comboY.Active);
		int volume = DISCRET_LEVEL * DISCRET_LEVEL;
		double step = 2.0 / (DISCRET_LEVEL + 1.0);

		// create points
		Vector3D[][] points = new Vector3D[1][];
		points[0] = new Vector3D[volume];

		// fill points
		int c = 0;
		for (int ix = 1; ix <= DISCRET_LEVEL; ix++)
		{
			double x = ix * step;
			Vector<double> row = weights[wx[0]].Row(wx[1]);
			row[wx[2]] = x;
			weights[wx[0]].SetRow(wx[1], row);
			for (int iy = 1; iy <= DISCRET_LEVEL; iy++)
			{
				double y = iy * step;
				row = weights[wy[0]].Row(wy[1]);
				row[wy[2]] = y;
				weights[wy[0]].SetRow(wy[1], row);
				// compute error
				points[0][c++] = new Vector3D(x, y, Perceptron.Risk(weights, conf.func, DataEngine.GetData()));
			}
		}

		// render points
		rs3DP.Render(points, surf.Width, surf.Height);
	}

	// visualization logic
	void UpdateView()
	{
		if (!HasPerceptron) return;

		switch (VisualMode)
		{
			case ViewMode.Structure:
				rs2D.PointThickness = 0.033;
				rs2D.LineThickness = 0.0001;
				rs2D.Render(structPoints, structLines, surf.Width, surf.Height);
				break;
			case ViewMode.Learning:

				break;
			case ViewMode.Outputs:
				PlotOutputs();
				break;
			case ViewMode.ErrorSurface:
				PlotError();
				break;
		}
		UpdateCombosAndScales();
		canvas.QueueDraw();
	}
    void UpdateCombosAndScales()
	{
		if (!HasPerceptron) return;

        switch (VisualMode)
		{
			case ViewMode.Structure:
			case ViewMode.Learning:
				comboX.Sensitive = false;
				comboY.Sensitive = false;
				foreach (HScale scale in scales)
					scale.Visible = false;
				break;
			case ViewMode.Outputs:
			case ViewMode.ErrorSurface:
				if (conf.numInputs > MAX_DIMENSION || ann.GetNumOfWeights() > MAX_DIMENSION) return;
				// clear combos & scales
				comboX.Clear();
				comboY.Clear();
				foreach (HScale scale in scales)
				{
					// scale.Visible = false;
					vboxAxis.Remove(scale);
					scale.Destroy();
				}
				// refill combos & scales
				CellRendererText maker = new CellRendererText();
				comboX.PackStart(maker, false);
				comboX.AddAttribute(maker, "text", 0);
				comboY.PackStart(maker, false);
				comboY.AddAttribute(maker, "text", 0);
                if (VisualMode == ViewMode.Outputs)
				{
                    // number of inputs
					scales = new HScale[conf.numInputs];
					sector = new double[conf.numInputs];
					for (int i = 1; i <= conf.numInputs; i++)
					{
                        // combo
						comboX.AppendText($"Coord {i}");
						comboY.AppendText($"Coord {i}");
                        // scale
						scales[i] = new HScale(-1, +1, 0.001) {
                            TooltipText = $"Coord {i}",
                            Value = 0.0
                        };
						scales[i].ValueChanged += (sender, e) => {
							HScale me = (HScale)sender;
							int index = Convert.ToInt32(me.TooltipText.Substring(6));
							sector[index] = scales[index].Value;
							UpdateView();
						};
						scales[i].Visible = true;
						scales[i].Sensitive = (i != comboX.Active) && (i != comboY.Active);
						vboxAxis.Add(scales[i]);
					}
				}
				else
				{
                    // number of weights
					int len = ann.GetNumOfWeights();
					scales = new HScale[len];
					sector = new double[len];
					Matrix<double>[] weights = ann.GetWeights();
					for (int i = 0; i < len; i++)
					{
						int[] w = WrapIndex(i);
                        // combo
						comboX.AppendText($"Weight {w[0]}-{w[1]}-{w[2]}");
						comboY.AppendText($"Weight {w[0]}-{w[1]}-{w[2]}");
						// scale
						scales[i] = new HScale(-1, +1, 0.001) {
							TooltipText = $"Weight {w[0]}-{w[1]}-{w[2]}",
							Value = weights[w[0]].At(w[1], w[2])
                        };
						scales[i].ValueChanged += (sender, e) => {
							HScale me = (HScale)sender;
							int index = Convert.ToInt32(me.TooltipText.Substring(6));
							sector[index] = scales[index].Value;
							UpdateView();
						};
						scales[i].Visible = true;
						scales[i].Sensitive = (i != comboX.Active) && (i != comboY.Active);
						vboxAxis.Add(scales[i]);
					}
				}
				break;
		}
	}

	// startup
	public MainWindow() : base(WindowType.Toplevel)
	{
		Build();
		Init();
	}
	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	// GUI.Buttons.Configuration
	protected void ButtonNew(object sender, EventArgs e)
	{
		ConfigDialog dialog = new ConfigDialog();
		if (dialog.Run() == (int)ResponseType.Ok)
		{
			conf = dialog.Configuration;
			conf.initialized = true;
			UpdatePerceptron();
		}
		dialog.Destroy();
	}
	protected void ButtonEdit(object sender, EventArgs e)
	{
		if (!hasPerceptron) return;

		ConfigDialog dialog = new ConfigDialog { Configuration = conf };
		if (dialog.Run() == (int)ResponseType.Ok)
		{
			conf = dialog.Configuration;
			UpdatePerceptron();
		}
		dialog.Destroy();
	}
	protected void ButtonSave(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		// get file
		FileChooserDialog dialog = new FileChooserDialog(
						"Select a data file:", this, FileChooserAction.Save,
						"Cancel", ResponseType.Cancel,
						"Open", ResponseType.Accept);

		// save
		if (dialog.Run() == (int)ResponseType.Accept)
			Perceptron.Save(dialog.Filename, ann);
		dialog.Destroy();
	}
	protected void ButtonLoad(object sender, EventArgs e)
	{
		// get file
		FileChooserDialog dialog = new FileChooserDialog(
						"Select a data file:", this, FileChooserAction.Open,
						"Cancel", ResponseType.Cancel,
						"Open", ResponseType.Accept);

		// load
		if (dialog.Run() == (int)ResponseType.Accept)
		{
			conf.initialized = false;
			ann = Perceptron.Load(dialog.Filename);
			UpdatePerceptron();
		}
		dialog.Destroy();
	}
	// GUI.Buttons.Learn
	protected void ButtonLearn(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		if (isLearning)
		{
			VisualMode = ViewMode.ErrorSurface;
			ann.BreakLearn();
		}
		else
		{
			VisualMode = ViewMode.Learning;
			ann.BeginLearn(DataEngine.GetData());
		}

		// toggle
		isLearning = !isLearning;
		UpdateView();
	}
	protected void ButtonReset(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		ann.Reset();
		UpdateView();
	}
	protected void ButtonRandom(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		ann.Randomize();
		UpdateView();
	}
	// GUI.ComboBoxes
	protected void ComboMethod(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		ann.LearnMethod = (Perceptron.LearningConcept)comboMethod.Active;
	}
	protected void ComboView(object sender, EventArgs e)
	{
		if (!HasPerceptron) return;

		VisualMode = (ViewMode)comboView.Active;
		UpdateView();
	}
	protected void ComboAxisX(object sender, EventArgs e)
	{
		UpdateView();
	}
	protected void ComboAxisY(object sender, EventArgs e)
	{
		UpdateView();
	}
	// GUI.CheckBox
	protected void CheckDropOut(object sender, EventArgs e)
	{

	}
	protected void CheckCrossValidate(object sender, EventArgs e)
	{

	}
	protected void CheckConstrain(object sender, EventArgs e)
	{

	}
	protected void CheckRegularize(object sender, EventArgs e)
	{

	}
	protected void CheckSmartRate(object sender, EventArgs e)
	{

	}
	protected void CheckErrTest(object sender, EventArgs e)
	{
		UpdateView();
	}
	protected void CheckErrLearn(object sender, EventArgs e)
	{
		UpdateView();
	}
	protected void CheckDrawAxis(object sender, EventArgs e)
	{
		rs2D.DrawAxis = rs3DP.DrawAxis = checkDrawAxis.Active;
		UpdateView();
	}
	protected void CheckDrawPlanes(object sender, EventArgs e)
	{
		rs3DP.DrawPlanes = checkDrawPlanes.Active;
		UpdateView();
	}


    // utils
    int[] WrapIndex(int index)
	{
		int[] res = new int[3];
		int[] numWeightsPerLayer = {
		    conf.numInputs * conf.numHiddens,
            conf.numHiddens * conf.numHiddens
		};
		if (index < numWeightsPerLayer[0])
		{
			res[0] = 0;
			res[1] = index / conf.numInputs;
			res[2] = index % conf.numInputs;
		}
		else
		{
			index -= numWeightsPerLayer[0];
			res[0] = 1 + index / numWeightsPerLayer[1];
			index %= numWeightsPerLayer[1];
            res[1] = index / conf.numHiddens;
            res[2] = index % conf.numHiddens;
		}
		return res;
	}
    int UnwrapIndex(int[] index)
	{
		int res = 0;
		int[] numWeightsPerLayer = {
			conf.numInputs * conf.numHiddens,
			conf.numHiddens * conf.numHiddens
		};
		if (index[0] == 0)
		{
			res = conf.numInputs * index[1] + index[2];
		}
		else
		{
			res = numWeightsPerLayer[0];
			res += numWeightsPerLayer[1] * index[0];
            res += conf.numHiddens * index[1];
			res += index[2];
		}
		return res;
	}

}
