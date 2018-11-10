using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace MultiPerceptron {

    [Serializable]
    public class Perceptron {
    
        public enum Method {
            BackProp = 0,
            Newton = 1,
            QuasiNewton = 2,
            ConjGrad = 3
		}

        const double RND_BOUND = 1.0 / 3.0;

		Config conf;
		Matrix<double>[] weights;
		public Method LearnMethod { get; set; }
		readonly IContinuousDistribution distrib;
		readonly string filePath = "none";

		public Perceptron(Config configuration) {
            // create random
			Random source = new MersenneTwister(RandomSeed.Robust(), true);
			distrib = new ContinuousUniform(-RND_BOUND, +RND_BOUND, source);
            // init
			conf = configuration;
			Construct(true);
	    }
		void Construct(bool zeroInit) {
			weights = new Matrix<double>[conf.numHidLayers + 2];
			int prev, next;

			prev = conf.numInputs;
            for (int i = 0; i < conf.numHidLayers; i++) {
				next = conf.numHiddens;
				if (zeroInit) weights[i] = CreateMatrix.Dense<double>(next, prev);
                else weights[i] = CreateMatrix.Random<double>(next, prev, distrib);
				prev = next;
			}
			next = conf.numOutputs;
			if (zeroInit) weights[conf.numHidLayers] = CreateMatrix.Dense<double>(next, prev);
			else weights[conf.numHidLayers] = CreateMatrix.Random<double>(next, prev, distrib);
		}

		// neuro
        public void Reset() {
			if (File.Exists(filePath)) {
				Perceptron old = Load(filePath);
				old.weights.CopyTo(weights, 0);
			} else {
				Construct(true);
			}
			
		}
        public void Randomize() {
			Construct(false);
		}
		public Vector<double> Response(Vector<double> input) {
			if (input.Count != weights[0].ColumnCount)
                throw new Exception("Dimension mismatch.");

			Vector<double> temp = CreateVector.DenseOfVector(input);
			var fx = Activators.dict[conf.func];

            for (int i = 0; i <= conf.numHidLayers + 1; i++) {
				temp = weights[i] * temp;
				for (int j = 0; j < temp.Count; j++)
					temp[j] = fx(temp[j]);
			}

			return temp;
		}
        public void BeginLearn(DataPoint[] trainSet) {
            // TODO: learning management
	    }
        public void BreakLearn() {
            // TODO: learning management
		}

		// utils
		public static void Save(string dataFilePath, Perceptron perceptron) {
			FileStream stream = File.OpenWrite(dataFilePath);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, perceptron);
			stream.Flush();
			stream.Close();
		}
        public static Perceptron Load(string dataFilePath) {
			FileStream stream = File.OpenRead(dataFilePath);
			BinaryFormatter formatter = new BinaryFormatter();
			Perceptron perceptron = (Perceptron) formatter.Deserialize(stream);
			stream.Close();
			return perceptron;
		}
		public Config GetConfiguration() {
			return conf;
		}

	}

}
