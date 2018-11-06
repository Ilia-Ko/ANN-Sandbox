
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.HBox hbox1;

	private global::Gtk.DrawingArea canvas;

	private global::Gtk.VBox vbox1;

	private global::Gtk.Frame frame1;

	private global::Gtk.Alignment GtkAlignment;

	private global::Gtk.VBox vbox2;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView textConfig;

	private global::Gtk.HButtonBox hbuttonbox1;

	private global::Gtk.Button button1;

	private global::Gtk.Button btnEdit;

	private global::Gtk.Button btnSave;

	private global::Gtk.Button button4;

	private global::Gtk.Label GtkLabel3;

	private global::Gtk.Frame frameLearn;

	private global::Gtk.Alignment GtkAlignment1;

	private global::Gtk.VBox vbox3;

	private global::Gtk.HBox hbox3;

	private global::Gtk.Label label2;

	private global::Gtk.ComboBox comboMethod;

	private global::Gtk.CheckButton checkDropOut;

	private global::Gtk.CheckButton checkCrossValid;

	private global::Gtk.CheckButton checkConstrain;

	private global::Gtk.CheckButton checkRegular;

	private global::Gtk.CheckButton checkLearnRate;

	private global::Gtk.HButtonBox hbuttonbox2;

	private global::Gtk.Button btnLearn;

	private global::Gtk.Button button6;

	private global::Gtk.Button button7;

	private global::Gtk.Label GtkLabel5;

	private global::Gtk.Frame frameVisual;

	private global::Gtk.Alignment GtkAlignment2;

	private global::Gtk.VBox vbox6;

	private global::Gtk.HBox hbox8;

	private global::Gtk.Label label11;

	private global::Gtk.ComboBox comboView;

	private global::Gtk.HBox hbox6;

	private global::Gtk.Label label9;

	private global::Gtk.ComboBox comboX;

	private global::Gtk.Label label10;

	private global::Gtk.ComboBox comboY;

	private global::Gtk.HBox hbox7;

	private global::Gtk.CheckButton checkErrTest;

	private global::Gtk.CheckButton checkErrLearn;

	private global::Gtk.ScrolledWindow scrolledwindow1;

	private global::Gtk.VBox vboxAxis;

	private global::Gtk.Label label12;

	private global::Gtk.Label GtkLabel14;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hbox1 = new global::Gtk.HBox();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.canvas = new global::Gtk.DrawingArea();
		this.canvas.Name = "canvas";
		this.hbox1.Add(this.canvas);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.canvas]));
		w1.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 48;
		// Container child vbox1.Gtk.Box+BoxChild
		this.frame1 = new global::Gtk.Frame();
		this.frame1.Name = "frame1";
		this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frame1.Gtk.Container+ContainerChild
		this.GtkAlignment = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment.Name = "GtkAlignment";
		this.GtkAlignment.LeftPadding = ((uint)(12));
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		this.vbox2 = new global::Gtk.VBox();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textConfig = new global::Gtk.TextView();
		this.textConfig.CanFocus = true;
		this.textConfig.Name = "textConfig";
		this.textConfig.Editable = false;
		this.GtkScrolledWindow.Add(this.textConfig);
		this.vbox2.Add(this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
		w3.Position = 0;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbuttonbox1 = new global::Gtk.HButtonBox();
		this.hbuttonbox1.Name = "hbuttonbox1";
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button1 = new global::Gtk.Button();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString("New");
		this.hbuttonbox1.Add(this.button1);
		global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.button1]));
		w4.Expand = false;
		w4.Fill = false;
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.btnEdit = new global::Gtk.Button();
		this.btnEdit.CanFocus = true;
		this.btnEdit.Name = "btnEdit";
		this.btnEdit.UseUnderline = true;
		this.btnEdit.Label = global::Mono.Unix.Catalog.GetString("Edit");
		this.hbuttonbox1.Add(this.btnEdit);
		global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.btnEdit]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.btnSave = new global::Gtk.Button();
		this.btnSave.CanFocus = true;
		this.btnSave.Name = "btnSave";
		this.btnSave.UseUnderline = true;
		this.btnSave.Label = global::Mono.Unix.Catalog.GetString("Save");
		this.hbuttonbox1.Add(this.btnSave);
		global::Gtk.ButtonBox.ButtonBoxChild w6 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.btnSave]));
		w6.Position = 2;
		w6.Expand = false;
		w6.Fill = false;
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button4 = new global::Gtk.Button();
		this.button4.CanFocus = true;
		this.button4.Name = "button4";
		this.button4.UseUnderline = true;
		this.button4.Label = global::Mono.Unix.Catalog.GetString("Load");
		this.hbuttonbox1.Add(this.button4);
		global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.button4]));
		w7.Position = 3;
		w7.Expand = false;
		w7.Fill = false;
		this.vbox2.Add(this.hbuttonbox1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbuttonbox1]));
		w8.Position = 1;
		w8.Expand = false;
		w8.Fill = false;
		this.GtkAlignment.Add(this.vbox2);
		this.frame1.Add(this.GtkAlignment);
		this.GtkLabel3 = new global::Gtk.Label();
		this.GtkLabel3.Name = "GtkLabel3";
		this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Configuration</b>");
		this.GtkLabel3.UseMarkup = true;
		this.frame1.LabelWidget = this.GtkLabel3;
		this.vbox1.Add(this.frame1);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frame1]));
		w11.Position = 0;
		// Container child vbox1.Gtk.Box+BoxChild
		this.frameLearn = new global::Gtk.Frame();
		this.frameLearn.Name = "frameLearn";
		this.frameLearn.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frameLearn.Gtk.Container+ContainerChild
		this.GtkAlignment1 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment1.Name = "GtkAlignment1";
		this.GtkAlignment1.LeftPadding = ((uint)(12));
		// Container child GtkAlignment1.Gtk.Container+ContainerChild
		this.vbox3 = new global::Gtk.VBox();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.label2 = new global::Gtk.Label();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Method:");
		this.hbox3.Add(this.label2);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label2]));
		w12.Position = 0;
		w12.Expand = false;
		w12.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.comboMethod = global::Gtk.ComboBox.NewText();
		this.comboMethod.AppendText(global::Mono.Unix.Catalog.GetString("BackProp"));
		this.comboMethod.AppendText(global::Mono.Unix.Catalog.GetString("Newton"));
		this.comboMethod.AppendText(global::Mono.Unix.Catalog.GetString("QuasiNewton"));
		this.comboMethod.AppendText(global::Mono.Unix.Catalog.GetString("ConjGrad"));
		this.comboMethod.Name = "comboMethod";
		this.comboMethod.Active = 0;
		this.hbox3.Add(this.comboMethod);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.comboMethod]));
		w13.Position = 1;
		w13.Expand = false;
		w13.Fill = false;
		this.vbox3.Add(this.hbox3);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.checkDropOut = new global::Gtk.CheckButton();
		this.checkDropOut.CanFocus = true;
		this.checkDropOut.Name = "checkDropOut";
		this.checkDropOut.Label = global::Mono.Unix.Catalog.GetString("Use DropOut");
		this.checkDropOut.DrawIndicator = true;
		this.checkDropOut.UseUnderline = true;
		this.vbox3.Add(this.checkDropOut);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.checkDropOut]));
		w15.Position = 1;
		w15.Expand = false;
		w15.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.checkCrossValid = new global::Gtk.CheckButton();
		this.checkCrossValid.CanFocus = true;
		this.checkCrossValid.Name = "checkCrossValid";
		this.checkCrossValid.Label = global::Mono.Unix.Catalog.GetString("Cross-Validate");
		this.checkCrossValid.DrawIndicator = true;
		this.checkCrossValid.UseUnderline = true;
		this.vbox3.Add(this.checkCrossValid);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.checkCrossValid]));
		w16.Position = 2;
		w16.Expand = false;
		w16.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.checkConstrain = new global::Gtk.CheckButton();
		this.checkConstrain.CanFocus = true;
		this.checkConstrain.Name = "checkConstrain";
		this.checkConstrain.Label = global::Mono.Unix.Catalog.GetString("Constrain Weights");
		this.checkConstrain.DrawIndicator = true;
		this.checkConstrain.UseUnderline = true;
		this.vbox3.Add(this.checkConstrain);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.checkConstrain]));
		w17.Position = 3;
		w17.Expand = false;
		w17.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.checkRegular = new global::Gtk.CheckButton();
		this.checkRegular.CanFocus = true;
		this.checkRegular.Name = "checkRegular";
		this.checkRegular.Label = global::Mono.Unix.Catalog.GetString("Regularize");
		this.checkRegular.DrawIndicator = true;
		this.checkRegular.UseUnderline = true;
		this.vbox3.Add(this.checkRegular);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.checkRegular]));
		w18.Position = 4;
		w18.Expand = false;
		w18.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.checkLearnRate = new global::Gtk.CheckButton();
		this.checkLearnRate.CanFocus = true;
		this.checkLearnRate.Name = "checkLearnRate";
		this.checkLearnRate.Label = global::Mono.Unix.Catalog.GetString("Smart Scaling");
		this.checkLearnRate.DrawIndicator = true;
		this.checkLearnRate.UseUnderline = true;
		this.vbox3.Add(this.checkLearnRate);
		global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.checkLearnRate]));
		w19.Position = 5;
		w19.Expand = false;
		w19.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hbuttonbox2 = new global::Gtk.HButtonBox();
		this.hbuttonbox2.Name = "hbuttonbox2";
		// Container child hbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
		this.btnLearn = new global::Gtk.Button();
		this.btnLearn.CanFocus = true;
		this.btnLearn.Name = "btnLearn";
		this.btnLearn.UseUnderline = true;
		this.btnLearn.Label = global::Mono.Unix.Catalog.GetString("Learn");
		this.hbuttonbox2.Add(this.btnLearn);
		global::Gtk.ButtonBox.ButtonBoxChild w20 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox2[this.btnLearn]));
		w20.Expand = false;
		w20.Fill = false;
		// Container child hbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
		this.button6 = new global::Gtk.Button();
		this.button6.CanFocus = true;
		this.button6.Name = "button6";
		this.button6.UseUnderline = true;
		this.button6.Label = global::Mono.Unix.Catalog.GetString("Reset");
		this.hbuttonbox2.Add(this.button6);
		global::Gtk.ButtonBox.ButtonBoxChild w21 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox2[this.button6]));
		w21.Position = 1;
		w21.Expand = false;
		w21.Fill = false;
		// Container child hbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
		this.button7 = new global::Gtk.Button();
		this.button7.CanFocus = true;
		this.button7.Name = "button7";
		this.button7.UseUnderline = true;
		this.button7.Label = global::Mono.Unix.Catalog.GetString("Random");
		this.hbuttonbox2.Add(this.button7);
		global::Gtk.ButtonBox.ButtonBoxChild w22 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox2[this.button7]));
		w22.Position = 2;
		w22.Expand = false;
		w22.Fill = false;
		this.vbox3.Add(this.hbuttonbox2);
		global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbuttonbox2]));
		w23.Position = 6;
		w23.Expand = false;
		w23.Fill = false;
		this.GtkAlignment1.Add(this.vbox3);
		this.frameLearn.Add(this.GtkAlignment1);
		this.GtkLabel5 = new global::Gtk.Label();
		this.GtkLabel5.Name = "GtkLabel5";
		this.GtkLabel5.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Learning</b>");
		this.GtkLabel5.UseMarkup = true;
		this.frameLearn.LabelWidget = this.GtkLabel5;
		this.vbox1.Add(this.frameLearn);
		global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frameLearn]));
		w26.Position = 1;
		w26.Expand = false;
		w26.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.frameVisual = new global::Gtk.Frame();
		this.frameVisual.Name = "frameVisual";
		this.frameVisual.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frameVisual.Gtk.Container+ContainerChild
		this.GtkAlignment2 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment2.Name = "GtkAlignment2";
		this.GtkAlignment2.LeftPadding = ((uint)(12));
		// Container child GtkAlignment2.Gtk.Container+ContainerChild
		this.vbox6 = new global::Gtk.VBox();
		this.vbox6.Name = "vbox6";
		this.vbox6.Spacing = 6;
		// Container child vbox6.Gtk.Box+BoxChild
		this.hbox8 = new global::Gtk.HBox();
		this.hbox8.Name = "hbox8";
		this.hbox8.Spacing = 6;
		// Container child hbox8.Gtk.Box+BoxChild
		this.label11 = new global::Gtk.Label();
		this.label11.Name = "label11";
		this.label11.LabelProp = global::Mono.Unix.Catalog.GetString("View:");
		this.hbox8.Add(this.label11);
		global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.label11]));
		w27.Position = 0;
		w27.Expand = false;
		w27.Fill = false;
		// Container child hbox8.Gtk.Box+BoxChild
		this.comboView = global::Gtk.ComboBox.NewText();
		this.comboView.AppendText(global::Mono.Unix.Catalog.GetString("Structure"));
		this.comboView.AppendText(global::Mono.Unix.Catalog.GetString("Outputs"));
		this.comboView.AppendText(global::Mono.Unix.Catalog.GetString("Learning"));
		this.comboView.AppendText(global::Mono.Unix.Catalog.GetString("Error surface"));
		this.comboView.Name = "comboView";
		this.comboView.Active = 0;
		this.hbox8.Add(this.comboView);
		global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.comboView]));
		w28.Position = 1;
		w28.Expand = false;
		w28.Fill = false;
		this.vbox6.Add(this.hbox8);
		global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.hbox8]));
		w29.Position = 0;
		w29.Expand = false;
		w29.Fill = false;
		// Container child vbox6.Gtk.Box+BoxChild
		this.hbox6 = new global::Gtk.HBox();
		this.hbox6.Name = "hbox6";
		this.hbox6.Spacing = 6;
		// Container child hbox6.Gtk.Box+BoxChild
		this.label9 = new global::Gtk.Label();
		this.label9.Name = "label9";
		this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("0X=");
		this.hbox6.Add(this.label9);
		global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.label9]));
		w30.Position = 0;
		w30.Expand = false;
		w30.Fill = false;
		// Container child hbox6.Gtk.Box+BoxChild
		this.comboX = global::Gtk.ComboBox.NewText();
		this.comboX.Name = "comboX";
		this.hbox6.Add(this.comboX);
		global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.comboX]));
		w31.Position = 1;
		w31.Expand = false;
		w31.Fill = false;
		// Container child hbox6.Gtk.Box+BoxChild
		this.label10 = new global::Gtk.Label();
		this.label10.Name = "label10";
		this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("0Y=");
		this.hbox6.Add(this.label10);
		global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.label10]));
		w32.Position = 3;
		w32.Expand = false;
		w32.Fill = false;
		// Container child hbox6.Gtk.Box+BoxChild
		this.comboY = global::Gtk.ComboBox.NewText();
		this.comboY.Name = "comboY";
		this.hbox6.Add(this.comboY);
		global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.comboY]));
		w33.Position = 4;
		w33.Expand = false;
		w33.Fill = false;
		this.vbox6.Add(this.hbox6);
		global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.hbox6]));
		w34.Position = 1;
		w34.Expand = false;
		w34.Fill = false;
		// Container child vbox6.Gtk.Box+BoxChild
		this.hbox7 = new global::Gtk.HBox();
		this.hbox7.Name = "hbox7";
		this.hbox7.Spacing = 6;
		// Container child hbox7.Gtk.Box+BoxChild
		this.checkErrTest = new global::Gtk.CheckButton();
		this.checkErrTest.CanFocus = true;
		this.checkErrTest.Name = "checkErrTest";
		this.checkErrTest.Label = global::Mono.Unix.Catalog.GetString("Test Err");
		this.checkErrTest.DrawIndicator = true;
		this.checkErrTest.UseUnderline = true;
		this.hbox7.Add(this.checkErrTest);
		global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.checkErrTest]));
		w35.Position = 0;
		// Container child hbox7.Gtk.Box+BoxChild
		this.checkErrLearn = new global::Gtk.CheckButton();
		this.checkErrLearn.CanFocus = true;
		this.checkErrLearn.Name = "checkErrLearn";
		this.checkErrLearn.Label = global::Mono.Unix.Catalog.GetString("Learn Err");
		this.checkErrLearn.Active = true;
		this.checkErrLearn.DrawIndicator = true;
		this.checkErrLearn.UseUnderline = true;
		this.hbox7.Add(this.checkErrLearn);
		global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.checkErrLearn]));
		w36.Position = 2;
		this.vbox6.Add(this.hbox7);
		global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.hbox7]));
		w37.Position = 2;
		w37.Expand = false;
		w37.Fill = false;
		// Container child vbox6.Gtk.Box+BoxChild
		this.scrolledwindow1 = new global::Gtk.ScrolledWindow();
		this.scrolledwindow1.CanFocus = true;
		this.scrolledwindow1.Name = "scrolledwindow1";
		this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child scrolledwindow1.Gtk.Container+ContainerChild
		global::Gtk.Viewport w38 = new global::Gtk.Viewport();
		w38.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child GtkViewport.Gtk.Container+ContainerChild
		this.vboxAxis = new global::Gtk.VBox();
		this.vboxAxis.Name = "vboxAxis";
		this.vboxAxis.Spacing = 6;
		// Container child vboxAxis.Gtk.Box+BoxChild
		this.label12 = new global::Gtk.Label();
		this.label12.Name = "label12";
		this.label12.LabelProp = global::Mono.Unix.Catalog.GetString("Invisible Axis:");
		this.vboxAxis.Add(this.label12);
		global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.vboxAxis[this.label12]));
		w39.Position = 0;
		w39.Expand = false;
		w39.Fill = false;
		w38.Add(this.vboxAxis);
		this.scrolledwindow1.Add(w38);
		this.vbox6.Add(this.scrolledwindow1);
		global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.scrolledwindow1]));
		w42.Position = 3;
		this.GtkAlignment2.Add(this.vbox6);
		this.frameVisual.Add(this.GtkAlignment2);
		this.GtkLabel14 = new global::Gtk.Label();
		this.GtkLabel14.Name = "GtkLabel14";
		this.GtkLabel14.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Visualization</b>");
		this.GtkLabel14.UseMarkup = true;
		this.frameVisual.LabelWidget = this.GtkLabel14;
		this.vbox1.Add(this.frameVisual);
		global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frameVisual]));
		w45.Position = 2;
		this.hbox1.Add(this.vbox1);
		global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox1]));
		w46.Position = 1;
		w46.Expand = false;
		w46.Fill = false;
		w46.Padding = ((uint)(6));
		this.Add(this.hbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 2636;
		this.DefaultHeight = 1714;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.button1.Clicked += new global::System.EventHandler(this.ButtonNew);
		this.btnEdit.Clicked += new global::System.EventHandler(this.ButtonEdit);
		this.btnSave.Clicked += new global::System.EventHandler(this.ButtonSave);
		this.button4.Clicked += new global::System.EventHandler(this.ButtonLoad);
		this.comboMethod.Changed += new global::System.EventHandler(this.ComboMethod);
		this.checkDropOut.Toggled += new global::System.EventHandler(this.CheckDropOut);
		this.checkCrossValid.Toggled += new global::System.EventHandler(this.CheckCrossValidate);
		this.checkConstrain.Toggled += new global::System.EventHandler(this.CheckConstrain);
		this.checkRegular.Toggled += new global::System.EventHandler(this.CheckRegularize);
		this.checkLearnRate.Toggled += new global::System.EventHandler(this.CheckSmartRate);
		this.btnLearn.Clicked += new global::System.EventHandler(this.ButtonLearn);
		this.button6.Clicked += new global::System.EventHandler(this.ButtonReset);
		this.button7.Clicked += new global::System.EventHandler(this.ButtonRandom);
		this.comboView.Changed += new global::System.EventHandler(this.ComboView);
		this.comboX.Changed += new global::System.EventHandler(this.ComboAxisX);
		this.comboY.Changed += new global::System.EventHandler(this.ComboAxisY);
		this.checkErrTest.Toggled += new global::System.EventHandler(this.CheckErrTest);
		this.checkErrLearn.Toggled += new global::System.EventHandler(this.CheckErrLearn);
	}
}