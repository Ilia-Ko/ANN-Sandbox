using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace MultiPerceptron {

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
        public Perceptron(string dataFilePath) {
            // create random
			Random source = new MersenneTwister(RandomSeed.Robust(), true);
			distrib = new ContinuousUniform(-RND_BOUND, +RND_BOUND, source);
			// init
			filePath = dataFilePath;
			conf = new Config();
			Construct(true);
			Read(filePath);
		}
		void Construct(bool zeroInit) {
			weights = new Matrix<double>[conf.numHidLayers + 1];
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
			if (File.Exists(filePath))
				Read(filePath);
			else
				Construct(true);
		}
        public void Randomize() {
			Construct(false);
		}
		public Vector<double> Response(Vector<double> input) {
            // TODO: forward prop
			return null;
		}
        public void BeginLearn(DataPoint[] trainSet) {
            // TODO: learning management
	    }
        public void BreakLearn() {
            // TODO: learning management
		}

		// utils
		public void Save(string dataFilePath) {
            // TODO: write file
		}
        void Read(string dataFilePath) {
			// TODO: read file
		}
		public Config GetConfiguration() {
			return conf;
		}

	}

}
