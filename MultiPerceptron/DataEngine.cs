using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra;

namespace MultiPerceptron {

    public struct DataPoint {

		public Vector<double> x;
		public Vector<double> y;

        public DataPoint(Vector<double> x, Vector<double> y) {
			this.x = x;
			this.y = y;
		}

	}

	static class DataEngine {

        // data.file
		static string filePath = "none";
		internal static string FilePath {
			get { return filePath; }
			set {
				if (!filePath.Equals(value) && File.Exists(filePath)) {
					filePath = value;
					LoadData();
				}
			}
		}
		// data.points
		static DataPoint[] points;
		static int size;
		// preprocessor.normalization
		static double xNorm, yNorm;
		// preprocessor.decorrelation (y = Ax + B)
		static bool decorrelated;
        static bool doDecorrelation;
        internal static bool DoDecorrelation {
			get { return doDecorrelation; }
			set {
				doDecorrelation = value;
				if (value && !decorrelated) Decorrelate(points);
			}
		}
		static Matrix<double> A;
		static Vector<double> B;
		// preprocessor.standartization
		static bool standartized;
		static bool doStandartization;
		internal static bool DoStandartization {
			get { return doStandartization; }
			set {
				doStandartization = value;
				if (value && !standartized) Standartize(points);
			}
		}
		static Vector<double> xMean, yMean;
		static double xDev, yDev;

        // data
        static void LoadData() {
			// read file & parse header
			string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 4) {
				filePath = "none";
				return;
			}
			if (!lines[0].StartsWith("DatLen")) {
				filePath = "none";
				return;
			}
			size = Convert.ToInt32(lines[0].Substring(7));
            if (!lines[1].StartsWith("InpDim")) {
				filePath = "none";
				return;
			}
			int inpDim = Convert.ToInt32(lines[1].Substring(7));
			if (!lines[2].StartsWith("OutDim")) {
				filePath = "none";
				return;
			}
			int outDim = Convert.ToInt32(lines[2].Substring(7));

            // read points
            if (lines.Length != 3 + size) {
				filePath = "none";
				return;
			}
			points = new DataPoint[size];
            for (int i = 0; i < size; i++) {
                // get numbers
				string[] numbers = lines[i+3].Split(' ', '|', '\t');
				if (numbers.Length != inpDim + outDim) {
					Console.WriteLine($"WARNING: incomplete line #{i+3} in data file '{filePath}' was ignored.");
					continue;
				}
                // parse numbers
				double[] input = new double[inpDim];
				double[] output = new double[outDim];
				for (int j = 0; j < inpDim; j++)
					input[j] = Convert.ToDouble(numbers[j]);
				for (int j = inpDim; j < numbers.Length; j++)
					output[j] = Convert.ToDouble(numbers[j]);
                // create data point
				Vector<double> x = CreateVector.DenseOfArray(input);
				Vector<double> y = CreateVector.DenseOfArray(output);
				points[i] = new DataPoint(x, y);
			}

			// preprocess
			decorrelated = false;
			standartized = false;
			if (doDecorrelation) {
				Decorrelate(points);
				decorrelated = true;
			}
			Normalize(points);
			if (doStandartization) {
				Standartize(points);
				standartized = true;
			}
		}
		internal static DataPoint[] GetData() {
			return points;
		}
		// preprocessor
        static void Normalize(DataPoint[] p) {
			// find the longest vectors
			int maxInp = 0, maxOut = 0;
			xNorm = p[maxInp].x.L2Norm();
			yNorm = p[maxOut].y.L2Norm();
			for (int i = 0; i < size; i++) {
				double xLen = p[i].x.L2Norm();
				double yLen = p[i].y.L2Norm();
				if (xLen > xNorm) {
					maxInp = i;
					xNorm = xLen;
				}
				if (yLen > yNorm) {
					maxOut = i;
					yNorm = yLen;
				}
			}
            // scale all vectors by longest
            for (int i = 0; i < size; i++) {
				p[i].x /= xNorm;
				p[i].y /= yNorm;
			}
		}
		static void Decorrelate(DataPoint[] p) {
			// compute some params
			Vector<double> sumX = p[0].x.Clone();
			Vector<double> sumY = p[0].y.Clone();
			double sumX2 = p[0].x.DotProduct(p[0].x);
			Matrix<double> sumYX = p[0].y.OuterProduct(p[0].x);
            for (int i = 1; i < size; i++) {
				sumX += p[i].x;
				sumY += p[i].y;
				sumX2 += p[i].x.DotProduct(p[i].x);
				sumYX += p[i].y.OuterProduct(p[i].x);
			}

			// compute D, Da and Db
			double D = size * sumX2 - sumX.DotProduct(sumX);
			Matrix<double> Da = size * sumYX - sumY.OuterProduct(sumX);
			Vector<double> Db = sumY * sumX2 - sumYX * sumX;

			// compute LMS coefficients
			A = Da / D;
			B = Db / D;

            // subtract linear function
            for (int i = 0; i < size; i++)
				p[i].y -= A * p[i].x + B;
		}
		static void Standartize(DataPoint[] p) {
			// compute mean
			xMean = p[0].x.Clone();
			yMean = p[0].y.Clone();
            for (int i = 1; i < size; i++) {
				xMean += p[0].x;
				yMean += p[0].y;
			}
			xMean /= p.Length;
			yMean /= p.Length;

			// compute standart deviation
			xDev = 0.0;
            yDev = 0.0;
			Vector<double> tmp;
			for (int i = 0; i < size; i++) {
				tmp = p[i].x - xMean;
				xDev += tmp.DotProduct(tmp);
				tmp = p[i].y - yMean;
				yDev += tmp.DotProduct(tmp);
			}
			xDev /= size - 1;
			yDev /= size - 1;
			xDev = Math.Sqrt(xDev);
			yDev = Math.Sqrt(yDev);

            // standartize
            for (int i = 0; i < size; i++) {
				p[0].x = (p[0].x - xMean) / xDev;
				p[0].y = (p[0].y - yMean) / yDev;
			}
		}

	}

}
