using System;
using Gtk;
using MultiPerceptron;

public partial class MainWindow : Window {

    enum ViewMode {
        Structure = 0,
        Outputs = 1,
        Learning = 2,
        ErrorSurface = 3
	}

	// data
	bool hasPerceptron, isLearning;
	bool HasPerceptron {
		get { return hasPerceptron; }
		set {
			hasPerceptron = value;
			btnEdit.Sensitive = value;
			btnSave.Sensitive = value;
			frameLearn.Sensitive = value;
			frameVisual.Sensitive = value;
		}
    }
	ViewMode VisualMode { get; set; }
	Config conf;
	Perceptron ann;

	// gui

    // visualization

	// initialize
	void Init() {
		conf.initialized = false;
		HasPerceptron = false;
	}
	// data logic
    void UpdatePerceptron() {
		if (!HasPerceptron && conf.initialized) {
			ann = new Perceptron(conf);
			HasPerceptron = true;
		} else if (HasPerceptron) {
			conf = ann.GetConfiguration();
		}
	}

	// startup
	public MainWindow() : base(WindowType.Toplevel) {
        Build();
		Init();
    }
    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
        Application.Quit();
        a.RetVal = true;
    }

    // GUI.Buttons.Configuration
	protected void ButtonNew(object sender, EventArgs e) {
		ConfigDialog dialog = new ConfigDialog();
        if (dialog.Run() == (int) ResponseType.Ok) {
			conf = dialog.Configuration;
			UpdatePerceptron();
		}
	}
	protected void ButtonEdit(object sender, EventArgs e) {
		if (!hasPerceptron) return;

		ConfigDialog dialog = new ConfigDialog { Configuration = conf };
		if (dialog.Run() == (int) ResponseType.Ok) {
			conf = dialog.Configuration;
			UpdatePerceptron();
		}
	}
    protected void ButtonSave(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		// get file
		FileChooserDialog dialog = new FileChooserDialog(
						"Select a data file:", this, FileChooserAction.Save,
						"Cancel", ResponseType.Cancel,
						"Open", ResponseType.Accept);

        // save
		if (dialog.Run() == (int) ResponseType.Accept)
			Perceptron.Save(dialog.Filename, ann);
	}
	protected void ButtonLoad(object sender, EventArgs e) {
		// get file
		FileChooserDialog dialog = new FileChooserDialog(
						"Select a data file:", this, FileChooserAction.Open,
						"Cancel", ResponseType.Cancel,
						"Open", ResponseType.Accept);

        // load
        if (dialog.Run() == (int) ResponseType.Accept) {
			conf.initialized = false;
			ann = Perceptron.Load(dialog.Filename);
			UpdatePerceptron();
		}

	}
	// GUI.Buttons.Learn
    protected void ButtonLearn(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		if (isLearning) {
			VisualMode = ViewMode.ErrorSurface;
			ann.BreakLearn();
		} else {
			VisualMode = ViewMode.Learning;
			ann.BeginLearn(DataEngine.GetData());
		}
		
        // toggle
		isLearning = !isLearning;
	}
	protected void ButtonReset(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		ann.Reset();
	}
	protected void ButtonRandom(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		ann.Randomize();
	}
    // GUI.ComboBoxes
	protected void ComboMethod(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		ann.LearnMethod = (Perceptron.Method) comboMethod.Active;
	}
	protected void ComboView(object sender, EventArgs e) {
		if (!HasPerceptron) return;

		VisualMode = (ViewMode) comboView.Active;
	}
	protected void ComboAxisX(object sender, EventArgs e) {

	}
	protected void ComboAxisY(object sender, EventArgs e) {

	}
    // GUI.CheckBox
	protected void CheckDropOut(object sender, EventArgs e) {

	}
    protected void CheckCrossValidate(object sender, EventArgs e) {

	}
	protected void CheckConstrain(object sender, EventArgs e) {

	}
	protected void CheckRegularize(object sender, EventArgs e) {

	}
	protected void CheckSmartRate(object sender, EventArgs e) {

	}
	protected void CheckErrTest(object sender, EventArgs e) {

	}
	protected void CheckErrLearn(object sender, EventArgs e) {

	}


}
