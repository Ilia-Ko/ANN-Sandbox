using System;

namespace Rosenblatt {

	public struct DataPoint {

		const double eps = 0.1;

		internal readonly int dimension;
		internal readonly double[] x;
		internal readonly int y;

		public DataPoint(int dimensionality, double[] input, int result) {
			dimension = dimensionality;
			if (input.Length != dimension) throw new Exception("Dimension mismatch.");
			x = input;
			y = Math.Sign(result);
		}

		internal double MakeCorrection(int i) {
			return x[i] * y;
		}

		internal bool IsVisible(int a1, int a2, int a3, double[] sector) {
			int A1 = Math.Min(a1, Math.Min(a2, a3));
			int A3 = Math.Max(a1, Math.Max(a2, a3));
			int A2 = a1 + a2 + a3 - A1 - A3;

			int i;

			for (i = 0; i < A1; i++) if (Math.Abs(x[i] - sector[i]) > eps) return false;
			for (i = A1 + 1; i < A2; i++) if (Math.Abs(x[i] - sector[i]) > eps) return false;
			for (i = A2 + 1; i < dimension; i++) if (Math.Abs(x[i] - sector[i]) > eps) return false;
			return true;
		}

	}

	public class Rosenblatt {

		readonly double[] w;
		readonly int numInputs;

		public Rosenblatt(int inputs) {
			numInputs = inputs;
			w = new double[inputs + 1];
		}

		public void ResetToZero() {
			for (int i = 0; i <= numInputs; i++)
				w[i] = 0.0;
		}

		public int Classify(double[] x) {
			if (x.Length != numInputs) throw new Exception($"Dimension of x[{x.Length}] does not match" +
				$"dimension of w[{numInputs}].");

			double field = 0.0;
			for (int i = 0; i < numInputs; i++)
				field += w[i] * x[i];
			field -= w[numInputs];

			return Math.Sign(field);
		}

		public void Learn(DataPoint[] samples, int maxIters) {
			if (numInputs != samples[0].dimension) throw new Exception("Dimension mismatch.");
			ResetToZero();

			int sampleIndex = 0;
			int iters = 0;

			do {
				// adjust weights
				for (int i = 0; i < numInputs; i++)
					w[i] += samples[sampleIndex].MakeCorrection(i);
				w[numInputs] -= samples[sampleIndex].y;

				// check if ready
				sampleIndex = -1;
				for (int i = 0; i < samples.Length; i++)
					if (Classify(samples[i].x) * samples[i].y <= 0) {
						sampleIndex = i;
						break;
					}
			} while (sampleIndex > -1 && ++iters < maxIters);
		}

	}

}
