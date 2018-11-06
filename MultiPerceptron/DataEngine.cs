using MathNet.Numerics.LinearAlgebra;

namespace MultiPerceptron {

    public struct DataPoint {

		public Vector<double>[] x;
		public Vector<double>[] y;

        public DataPoint(Vector<double>[] x, Vector<double>[] y) {
			this.x = x;
			this.y = y;
		}

	}

	static class DataEngine {

        // data.file
		static string filePath;
		internal static string FilePath {
			get { return filePath; }
			set {
				if (!filePath.Equals(value)) {
					filePath = value;
					LoadData();
				}
			}
		}
		// data.points
		static DataPoint[] points;
		static int size;
		// preprocessor.decorrelation
		static bool decorrelated;
        static bool doDecorrelation;
        internal static bool DoDecorrelation {
			get { return doDecorrelation; }
			set {
				doDecorrelation = value;
				if (value && !decorrelated) Decorrelate(points);
			}
		}
		// preprocessor.whitening
		static bool whitened;
		static bool doWhitening;
		internal static bool DoWhitening {
			get { return doWhitening; }
			set {
				doWhitening = value;
				if (value && !whitened) Whiten(points);
			}
		}

        // data
        static void LoadData() {
            // TODO: open file and read data points
			size = 3;
			points = new DataPoint[size];

			// preprocess
			decorrelated = false;
			whitened = false;
			if (doDecorrelation) {
				Decorrelate(points);
				decorrelated = true;
			}
			if (doWhitening) {
				Whiten(points);
				whitened = true;
			}
		}
		internal static DataPoint[] GetData() {
			return points;
		}
		// preprocessor
		static void Decorrelate(DataPoint[] correlated) {
			// TODO: complete decorrelation code
		}
		static void Whiten(DataPoint[] unwhitened) {
			// TODO: complete whitening code
		}


	}

}
