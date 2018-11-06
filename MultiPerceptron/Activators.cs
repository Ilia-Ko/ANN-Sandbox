using System;
using System.Collections.Generic;

namespace MultiPerceptron {

	static class Activators {

		internal static double A = 1.0, B = 1.0;
		internal static Dictionary<int, Func<double, double>> dict;

		static Activators() {
			dict = new Dictionary<int, Func<double, double>>(8) {
				{ 0, Sigm },
				{ 1, Sigmdfdx },
				{ 2, TanH },
				{ 3, TanHdfdx },
				{ 4, PRELU },
				{ 5, PRELUdfdx },
				{ 6, ELU },
				{ 7, ELUdfdx }
			};
		}

		static double Sigm(double x)        => A / (1.0 + Math.Exp(-B * x));
		static double Sigmdfdx(double y)    => B / A * y * (A - y);
		static double TanH(double x)        => A * Math.Tanh(B * x);
		static double TanHdfdx(double y)    => B * (A - y * y / A);
		static double PRELU(double x)       => 0.5 * (A + B - Math.Sign(x) * (A - B)) * x;
		static double PRELUdfdx(double y)   => 0.5 * (A + B - Math.Sign(y) * (A - B));
		static double ELU(double x)         => x > 0.0 ? B * x : A * (Math.Exp(x) - 1.0);
		static double ELUdfdx(double y)     => y > 0.0 ? B : y + A;

	}

}
