using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace MultiPerceptron {

    [Serializable]
    public class Perceptron {
    
        [Serializable]
        public enum LearningConcept {
            BackProp = 0,
            Newton = 1,
            QuasiNewton = 2,
            ConjGrad = 3
		}

        const double RND_BOUND = 1.0 / 3.0;

		// learning
		[NonSerialized] Matrix<double>[] weights;
		double[][,] weightsSeries;
		[NonSerialized] List<double> learnHistory;
		public LearningConcept LearnMethod { get; set; }
		// interaction
		Config conf;
		readonly IContinuousDistribution distrib;
		readonly string filePath = "none";
		public Delegate OnLearnHistoryAppend { get; set; }

		public Perceptron(Config configuration) {
            // create random
			Random source = new MersenneTwister(RandomSeed.Robust(), true);
			distrib = new ContinuousUniform(-RND_BOUND, +RND_BOUND, source);
            // init
			conf = configuration;
			Construct(true);
	    }
		void Construct(bool zeroInit) {
            // TODO: repair a bug (Maybe, last matrix is null? Or just index disorder?)
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
			if (zeroInit) weights[conf.numHidLayers + 1] = CreateMatrix.Dense<double>(next, prev);
			else weights[conf.numHidLayers + 1] = CreateMatrix.Random<double>(next, prev, distrib);
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
			return Response(weights, input, conf.func);
		}
        public void BeginLearn(DataPoint[] trainSet) {
			learnHistory = new List<double>();
            // TODO: learning management
	    }
        public void BreakLearn() {
            // TODO: learning management
		}

		// utils
		public static void Save(string dataFilePath, Perceptron perceptron)
		{
			// prepare weights
			perceptron.weightsSeries = new double[perceptron.weights.Length][,];
            for (int i = 0; i < perceptron.weights.Length; i++)
                perceptron.weightsSeries[i] = perceptron.weights[i].ToArray();

            // serialize
			FileStream stream = File.OpenWrite(dataFilePath);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, perceptron);
			stream.Flush();
			stream.Close();
		}
        public static Perceptron Load(string dataFilePath) {
            // deserialize
			FileStream stream = File.OpenRead(dataFilePath);
			BinaryFormatter formatter = new BinaryFormatter();
			Perceptron perceptron = (Perceptron) formatter.Deserialize(stream);
			stream.Close();

			// init weights
			perceptron.weights = new Matrix<double>[perceptron.weightsSeries.Length];
			for (int i = 0; i < perceptron.weights.Length; i++)
				perceptron.weights[i] = CreateMatrix.DenseOfArray(perceptron.weightsSeries[i]);
			return perceptron;
		}
		public static Vector<double> Response(Matrix<double>[] weights, Vector<double> input, int func)
		{
			if (input.Count != weights[0].ColumnCount)
				throw new Exception("Dimension mismatch.");

			Vector<double> temp = CreateVector.DenseOfVector(input);
			var fx = Activators.dict[func];

			for (int i = 0; i < weights.Length; i++)
			{
				temp = weights[i] * temp;
				for (int j = 0; j < temp.Count; j++)
					temp[j] = fx(temp[j]);
			}

			return temp;
		}
		public static double Risk(Matrix<double>[] weights, int func, DataPoint[] trainSet)
		{
			double sum = 0.0;
            foreach (DataPoint point in trainSet)
			{
				Vector<double> response = Response(weights, point.x, func);
				sum += response.Subtract(point.y).L2Norm();
			}
			return 0.5 * sum / trainSet.Length;
		}
		public Config GetConfiguration() {
			return conf;
		}
        public Matrix<double>[] GetWeights()
		{
			return weights;
		}
		public double[] GetLearningHistory()
		{
			return learnHistory.ToArray();
		}
        public int GetNumOfWeights()
		{
			int num = 0;
            foreach (Matrix<double> matrix in weights)
			    num += matrix.ColumnCount * matrix.RowCount;
			return num;
		}

	}

}
