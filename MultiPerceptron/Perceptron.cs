using MathNet.Numerics.LinearAlgebra;

namespace MultiPerceptron {

    public class Perceptron {
    
        public enum Method {
            BackProp = 0,
            Newton = 1,
            QuasiNewton = 2,
            ConjGrad = 3
		}

		Config conf;
		Matrix<double>[] weights;
		public Method LearnMethod { get; set; }

		public Perceptron(Config configuration) {
			conf = configuration;
			Build();
	    }
        public Perceptron(string dataFilePath) {
			// TODO: file init
			Build();
		}
		void Build() {
			weights = new Matrix<double>[conf.numHidLayers + 1];
			int prev, next;

			prev = conf.numInputs;
            for (int i = 0; i < conf.numHidLayers; i++) {
				next = conf.numHiddens;
				weights[i] = CreateMatrix.Dense<double>(next, prev);
				prev = next;
			}
			next = conf.numOutputs;
			weights[conf.numHidLayers] = CreateMatrix.Dense<double>(next, prev);
		}

		// neuro
        public void Reset() {

		}
        public void Randomize() {

		}
		public Vector<double> Response(Vector<double> input) {
			return null;
		}
        public void BeginLearn(DataPoint[] trainSet) {

	    }
        public void BreakLearn() {

		}

		// save
		public void Save(string dataFilePath) {

		}
        public Config GetConfiguration() {
			return conf;
		}

	}
}
