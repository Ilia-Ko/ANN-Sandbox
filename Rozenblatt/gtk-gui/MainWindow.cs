
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.HBox hbox1;

	private global::Gtk.DrawingArea root;

	private global::Gtk.VBox vbox1;

	private global::Gtk.Frame frame1;

	private global::Gtk.Alignment GtkAlignment;

	private global::Gtk.VButtonBox vbuttonbox1;

	private global::Gtk.Button button3;

	private global::Gtk.Button button1;

	private global::Gtk.Button button2;

	private global::Gtk.Button button4;

	private global::Gtk.Label GtkLabel4;

	private global::Gtk.Frame frame2;

	private global::Gtk.Alignment GtkAlignment1;

	private global::Gtk.VBox vbox2;

	private global::Gtk.Label lblQuality;

	private global::Gtk.Button button5;

	private global::Gtk.ComboBox comboAxis1;

	private global::Gtk.ComboBox comboAxis2;

	private global::Gtk.ComboBox comboAxis3;

	private global::Gtk.Frame frame3;

	private global::Gtk.VBox axisBox;

	private global::Gtk.Label GtkLabel6;

	private global::Gtk.Frame frame4;

	private global::Gtk.Alignment GtkAlignment2;

	private global::Gtk.VBox vbox3;

	private global::Gtk.ToggleButton toggle3D;

	private global::Gtk.HScale hscaleDist;

	private global::Gtk.HScale hscaleRot;

	private global::Gtk.HScale hscaleCamera;

	private global::Gtk.Label GtkLabel8;

	private global::Gtk.Label GtkLabel9;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Rosenblatt");
		this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-connect", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.AllowShrink = true;
		this.Decorated = false;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hbox1 = new global::Gtk.HBox();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 12;
		this.hbox1.BorderWidth = ((uint)(12));
		// Container child hbox1.Gtk.Box+BoxChild
		this.root = new global::Gtk.DrawingArea();
		this.root.Name = "root";
		this.hbox1.Add(this.root);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.root]));
		w1.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 12;
		this.vbox1.BorderWidth = ((uint)(12));
		// Container child vbox1.Gtk.Box+BoxChild
		this.frame1 = new global::Gtk.Frame();
		this.frame1.Name = "frame1";
		this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
		this.frame1.BorderWidth = ((uint)(12));
		// Container child frame1.Gtk.Container+ContainerChild
		this.GtkAlignment = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment.Name = "GtkAlignment";
		this.GtkAlignment.LeftPadding = ((uint)(12));
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		this.vbuttonbox1 = new global::Gtk.VButtonBox();
		this.vbuttonbox1.Name = "vbuttonbox1";
		this.vbuttonbox1.Spacing = 12;
		this.vbuttonbox1.BorderWidth = ((uint)(12));
		this.vbuttonbox1.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(3));
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button3 = new global::Gtk.Button();
		this.button3.CanFocus = true;
		this.button3.Name = "button3";
		this.button3.UseUnderline = true;
		this.button3.Label = global::Mono.Unix.Catalog.GetString("Save");
		this.vbuttonbox1.Add(this.button3);
		global::Gtk.ButtonBox.ButtonBoxChild w2 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.button3]));
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button1 = new global::Gtk.Button();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString("Choose");
		this.vbuttonbox1.Add(this.button1);
		global::Gtk.ButtonBox.ButtonBoxChild w3 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.button1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button2 = new global::Gtk.Button();
		this.button2.CanFocus = true;
		this.button2.Name = "button2";
		this.button2.UseUnderline = true;
		this.button2.Label = global::Mono.Unix.Catalog.GetString("Learn");
		this.vbuttonbox1.Add(this.button2);
		global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.button2]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button4 = new global::Gtk.Button();
		this.button4.CanFocus = true;
		this.button4.Name = "button4";
		this.button4.UseUnderline = true;
		this.button4.Label = global::Mono.Unix.Catalog.GetString("Random");
		this.vbuttonbox1.Add(this.button4);
		global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.button4]));
		w5.Position = 3;
		w5.Expand = false;
		w5.Fill = false;
		this.GtkAlignment.Add(this.vbuttonbox1);
		this.frame1.Add(this.GtkAlignment);
		this.GtkLabel4 = new global::Gtk.Label();
		this.GtkLabel4.Name = "GtkLabel4";
		this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Data</b>");
		this.GtkLabel4.UseMarkup = true;
		this.frame1.LabelWidget = this.GtkLabel4;
		this.vbox1.Add(this.frame1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frame1]));
		w8.Position = 0;
		w8.Expand = false;
		w8.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.frame2 = new global::Gtk.Frame();
		this.frame2.Name = "frame2";
		this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
		this.frame2.BorderWidth = ((uint)(12));
		// Container child frame2.Gtk.Container+ContainerChild
		this.GtkAlignment1 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment1.Name = "GtkAlignment1";
		this.GtkAlignment1.LeftPadding = ((uint)(12));
		// Container child GtkAlignment1.Gtk.Container+ContainerChild
		this.vbox2 = new global::Gtk.VBox();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 12;
		this.vbox2.BorderWidth = ((uint)(12));
		// Container child vbox2.Gtk.Box+BoxChild
		this.lblQuality = new global::Gtk.Label();
		this.lblQuality.Name = "lblQuality";
		this.lblQuality.LabelProp = global::Mono.Unix.Catalog.GetString("Quality: 0.0%");
		this.vbox2.Add(this.lblQuality);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.lblQuality]));
		w9.Position = 0;
		w9.Expand = false;
		w9.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.button5 = new global::Gtk.Button();
		this.button5.CanFocus = true;
		this.button5.Name = "button5";
		this.button5.UseUnderline = true;
		this.button5.Label = global::Mono.Unix.Catalog.GetString("Redraw");
		this.vbox2.Add(this.button5);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.button5]));
		w10.Position = 1;
		w10.Expand = false;
		w10.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.comboAxis1 = global::Gtk.ComboBox.NewText();
		global::Gtk.Tooltips w11 = new Gtk.Tooltips();
		w11.SetTip(this.comboAxis1, "Axis OX", "Axis OX");
		this.comboAxis1.Name = "comboAxis1";
		this.vbox2.Add(this.comboAxis1);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.comboAxis1]));
		w12.Position = 2;
		w12.Expand = false;
		w12.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.comboAxis2 = global::Gtk.ComboBox.NewText();
		w11.SetTip(this.comboAxis2, "Axis OY", "Axis OY");
		this.comboAxis2.Name = "comboAxis2";
		this.vbox2.Add(this.comboAxis2);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.comboAxis2]));
		w13.Position = 3;
		w13.Expand = false;
		w13.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.comboAxis3 = global::Gtk.ComboBox.NewText();
		w11.SetTip(this.comboAxis3, "Axis OZ", "Axis OZ");
		this.comboAxis3.Name = "comboAxis3";
		this.vbox2.Add(this.comboAxis3);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.comboAxis3]));
		w14.Position = 4;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.frame3 = new global::Gtk.Frame();
		this.frame3.Name = "frame3";
		this.frame3.ShadowType = ((global::Gtk.ShadowType)(0));
		this.frame3.BorderWidth = ((uint)(12));
		// Container child frame3.Gtk.Container+ContainerChild
		this.axisBox = new global::Gtk.VBox();
		this.axisBox.Name = "axisBox";
		this.axisBox.Spacing = 12;
		this.axisBox.BorderWidth = ((uint)(12));
		this.frame3.Add(this.axisBox);
		this.GtkLabel6 = new global::Gtk.Label();
		this.GtkLabel6.Name = "GtkLabel6";
		this.GtkLabel6.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Axis:</b>");
		this.GtkLabel6.UseMarkup = true;
		this.frame3.LabelWidget = this.GtkLabel6;
		this.vbox2.Add(this.frame3);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.frame3]));
		w16.Position = 5;
		// Container child vbox2.Gtk.Box+BoxChild
		this.frame4 = new global::Gtk.Frame();
		this.frame4.Name = "frame4";
		this.frame4.ShadowType = ((global::Gtk.ShadowType)(0));
		this.frame4.BorderWidth = ((uint)(12));
		// Container child frame4.Gtk.Container+ContainerChild
		this.GtkAlignment2 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
		this.GtkAlignment2.Name = "GtkAlignment2";
		this.GtkAlignment2.LeftPadding = ((uint)(12));
		// Container child GtkAlignment2.Gtk.Container+ContainerChild
		this.vbox3 = new global::Gtk.VBox();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.toggle3D = new global::Gtk.ToggleButton();
		this.toggle3D.CanFocus = true;
		this.toggle3D.Name = "toggle3D";
		this.toggle3D.UseUnderline = true;
		this.toggle3D.Active = true;
		this.toggle3D.Label = global::Mono.Unix.Catalog.GetString("3D Mode");
		this.vbox3.Add(this.toggle3D);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.toggle3D]));
		w17.Position = 0;
		w17.Expand = false;
		w17.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hscaleDist = new global::Gtk.HScale(null);
		w11.SetTip(this.hscaleDist, "Distance from Zero", "Distance from Zero");
		this.hscaleDist.CanFocus = true;
		this.hscaleDist.Name = "hscaleDist";
		this.hscaleDist.Adjustment.Lower = 2D;
		this.hscaleDist.Adjustment.Upper = 12D;
		this.hscaleDist.Adjustment.PageIncrement = 2D;
		this.hscaleDist.Adjustment.StepIncrement = 0.2D;
		this.hscaleDist.Adjustment.Value = 3D;
		this.hscaleDist.DrawValue = true;
		this.hscaleDist.Digits = 2;
		this.hscaleDist.ValuePos = ((global::Gtk.PositionType)(2));
		this.vbox3.Add(this.hscaleDist);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hscaleDist]));
		w18.Position = 1;
		w18.Expand = false;
		w18.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hscaleRot = new global::Gtk.HScale(null);
		w11.SetTip(this.hscaleRot, "Rotation", "Rotation");
		this.hscaleRot.CanFocus = true;
		this.hscaleRot.Name = "hscaleRot";
		this.hscaleRot.Adjustment.Lower = -1D;
		this.hscaleRot.Adjustment.Upper = 1D;
		this.hscaleRot.Adjustment.PageIncrement = 10D;
		this.hscaleRot.Adjustment.StepIncrement = 0.02D;
		this.hscaleRot.DrawValue = true;
		this.hscaleRot.Digits = 2;
		this.hscaleRot.ValuePos = ((global::Gtk.PositionType)(2));
		this.vbox3.Add(this.hscaleRot);
		global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hscaleRot]));
		w19.Position = 2;
		w19.Expand = false;
		w19.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hscaleCamera = new global::Gtk.HScale(null);
		w11.SetTip(this.hscaleCamera, "Camera distance", "Camera distance");
		this.hscaleCamera.CanFocus = true;
		this.hscaleCamera.Name = "hscaleCamera";
		this.hscaleCamera.Adjustment.Lower = 1D;
		this.hscaleCamera.Adjustment.Upper = 24D;
		this.hscaleCamera.Adjustment.PageIncrement = 10D;
		this.hscaleCamera.Adjustment.StepIncrement = 0.2D;
		this.hscaleCamera.Adjustment.Value = 6D;
		this.hscaleCamera.DrawValue = true;
		this.hscaleCamera.Digits = 2;
		this.hscaleCamera.ValuePos = ((global::Gtk.PositionType)(2));
		this.vbox3.Add(this.hscaleCamera);
		global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hscaleCamera]));
		w20.PackType = ((global::Gtk.PackType)(1));
		w20.Position = 3;
		w20.Expand = false;
		w20.Fill = false;
		this.GtkAlignment2.Add(this.vbox3);
		this.frame4.Add(this.GtkAlignment2);
		this.GtkLabel8 = new global::Gtk.Label();
		this.GtkLabel8.Name = "GtkLabel8";
		this.GtkLabel8.LabelProp = global::Mono.Unix.Catalog.GetString("<b>3D View</b>");
		this.GtkLabel8.UseMarkup = true;
		this.frame4.LabelWidget = this.GtkLabel8;
		this.vbox2.Add(this.frame4);
		global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.frame4]));
		w23.PackType = ((global::Gtk.PackType)(1));
		w23.Position = 6;
		w23.Expand = false;
		w23.Fill = false;
		this.GtkAlignment1.Add(this.vbox2);
		this.frame2.Add(this.GtkAlignment1);
		this.GtkLabel9 = new global::Gtk.Label();
		this.GtkLabel9.Name = "GtkLabel9";
		this.GtkLabel9.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Visual</b>");
		this.GtkLabel9.UseMarkup = true;
		this.frame2.LabelWidget = this.GtkLabel9;
		this.vbox1.Add(this.frame2);
		global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frame2]));
		w26.Position = 1;
		w26.Expand = false;
		w26.Fill = false;
		this.hbox1.Add(this.vbox1);
		global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox1]));
		w27.Position = 1;
		w27.Expand = false;
		w27.Fill = false;
		this.Add(this.hbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 2741;
		this.DefaultHeight = 1713;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.root.ButtonPressEvent += new global::Gtk.ButtonPressEventHandler(this.RootButtonPressed);
		this.button3.Clicked += new global::System.EventHandler(this.BtnDataSave);
		this.button1.Clicked += new global::System.EventHandler(this.BtnDataChoose);
		this.button2.Clicked += new global::System.EventHandler(this.BtnDataLearn);
		this.button4.Clicked += new global::System.EventHandler(this.ButtonRandom);
		this.button5.Clicked += new global::System.EventHandler(this.BtnVisualRedraw);
		this.comboAxis1.Changed += new global::System.EventHandler(this.ComboFirstAxis);
		this.comboAxis2.Changed += new global::System.EventHandler(this.ComboSecondAxis);
		this.comboAxis3.Changed += new global::System.EventHandler(this.ComboThirdAxis);
		this.toggle3D.Toggled += new global::System.EventHandler(this.Toggle3DMode);
		this.hscaleDist.ValueChanged += new global::System.EventHandler(this.HScaleDistance);
		this.hscaleRot.ValueChanged += new global::System.EventHandler(this.HScaleRotate);
		this.hscaleCamera.ValueChanged += new global::System.EventHandler(this.HScaleCamera);
	}
}
