*/
    CVSim -- A functional, efficient and user-friendly Analytical Chemistry 
    application for performing simulations in a laboratory environment. 

    Copyright (C) 2017, Dylan Parsons

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
/*

using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace Program.Unit.Test
{
    public class StepTest
    {
        /*[Ignore("Need to do the simulate and gamma function first")]*/
        [Test]
        public void WhenSteppingThroughCVSimulator_ThenSimulationGeneratesFields()
        {
            // Potl (V) Background (A) Current (A)
            // -1.501	1.37331E-06	1.17494E-06

            const double pstart = -1.501;
            const double prev = -1.502;
            const int nT = 1000;
            const double H1 = 1; // = X1 initially
            const int N = 2;
            const double K = 0;
            DenseVector a1;
            double a2;
            DenseVector ak;
            DenseVector a3;
            DenseVector adA;
            DenseVector adB;
            var gamma = 0.015; // This is fake! Need the real function value
            var dT = 1.0 / nT;
            DenseVector G;
            DenseVector bdA;
            DenseVector bdB;

            var cA = new DenseVector(N+2);
            var cB = new DenseVector(N+2);
            DenseVector X = new DenseVector(N + 2);

            var p = -1.501;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            for (var i = 2; i < N + 1; i++)
                X[i] = H1 * (Math.Pow(gamma, i) - 1) / (gamma - 1);
            
            cvSimulator.Precalc(X,
                N, gamma, 
                K,
                out a1,
                out a2,
                out a3,
                out ak,
                out adA,
                out adB);

            var fakeFileStream = new FakeFileStream();

            var dp = -dT;

            G = new DenseVector(Convert.ToInt32(2 * N));

            p = pstart;
            var j = 0;

            for (var iSweep = 0; iSweep < 2; iSweep++)
            {
                for(var iT = 1; iT <= cvSimulator.NStep; iT++)
                {
                    p += dp;
                    cvSimulator.Step(cA, cB, N, p, a1, a2, a3, ak, adA, adB, out bdA, out bdB);
                    j++;
                    G[j] = (cA[1] - cA[0]) / H1;

                    var output = $"{p,-12:F6}{G[j],-12:F6}";

                    var buffer = Encoding.Default.GetBytes(output);
                    fakeFileStream.Write(buffer, 0, output.Length);
                }
                dp = -dp;
            }
            
            Assert.That(fakeFileStream.Count, Is.EqualTo(2));
        }

    }
}
