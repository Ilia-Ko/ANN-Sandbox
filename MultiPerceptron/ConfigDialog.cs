using System;
using Gtk;

namespace MultiPerceptron {

	public struct Config {
    
	    internal bool initialized;
		// common
		internal string name;
		internal int numHidLayers;
		internal int numInputs, numHiddens, numOutputs;
		internal int func;
		// learning
		internal double learnRate, momentum, stopDW;
		internal double dropOutProb;
		internal double crossValRatio;
		internal double constrainModulo;
		internal double regularAspect;
		internal double smartScaleFactor;

	}

	public partial class ConfigDialog : Dialog {

		Config configuration;
        internal Config Configuration {
			get { return configuration; }
			set {
				ResetTo(value);
			}
		}

		internal ConfigDialog() {
            Build();
        }
        void ResetTo(Config config) {
			configuration = config;

			entryName.Text = config.name;
			spinLayers.Value = config.numHidLayers;
			spinInput.Value = config.numInputs;
			spinHidden.Value = config.numHiddens;
			spinOutput.Value = config.numOutputs;
			comboActivFunc.Active = config.func / 2;
			entryA.Text = Convert.ToString(Activators.A);
			entryB.Text = Convert.ToString(Activators.B);
			hscaleLearnRate.Value = config.learnRate;
			hscaleMomentum.Value = config.momentum;
			hscaleStopDW.Value = config.stopDW;
			hscaleDropOut.Value = config.dropOutProb;
			hscaleCrossRatio.Value = config.crossValRatio;
			hscaleConstrain.Value = config.constrainModulo;
			hscaleRegular.Value = config.regularAspect;
			hscaleSmartScale.Value = config.smartScaleFactor;
		}

		protected void ButtonOK(object sender, EventArgs e) {
            // update conf
			configuration.name = entryName.Text;
			configuration.numHidLayers = (int) spinLayers.Value;
			configuration.numInputs = (int) spinInput.Value;
			configuration.numHiddens = (int) spinHidden.Value;
			configuration.numOutputs = (int) spinOutput.Value;
			configuration.func = comboActivFunc.Active * 2;
			Activators.A = Convert.ToDouble(entryA.Text);
			Activators.B = Convert.ToDouble(entryB.Text);
			configuration.learnRate = hscaleLearnRate.Value;
			configuration.momentum = hscaleMomentum.Value;
			configuration.stopDW = hscaleStopDW.Value;
			configuration.dropOutProb = hscaleDropOut.Value;
			configuration.crossValRatio = hscaleCrossRatio.Value;
			configuration.constrainModulo = hscaleConstrain.Value;
			configuration.regularAspect = hscaleRegular.Value;
			configuration.smartScaleFactor = hscaleSmartScale.Value;
            // update data engine
			DataEngine.DoDecorrelation = checkCorrel.Active;
			DataEngine.DoStandartization = checkStandard.Active;
			DataEngine.FilePath = entryFile.Text;
		}
		protected void ButtonFile(object sender, EventArgs e) {
			FileChooserDialog dialog = new FileChooserDialog(
						"Select a data file:", this, FileChooserAction.Open,
						"Cancel", ResponseType.Cancel,
						"Open", ResponseType.Accept);

			if (dialog.Run() == (int) ResponseType.Accept)
				entryFile.Text = dialog.Filename;
			else
				entryFile.Text = "none";
		}
		protected void EntryEdited(object sender, EventArgs e) {
			Entry entry = (Entry) sender;
            if (!double.TryParse(entry.Text, out double a)) entry.Text = "1.0";
		}

	}

}
