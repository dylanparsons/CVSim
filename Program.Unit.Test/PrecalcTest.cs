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
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace Program.Unit.Test
{
    public class PrecalcTest
    {
        [Test]
        public void WhenPrecalcConstants_ThenConstantResults()
        {
            // Arrange the test data (use a "fake" data, inc gamma until EE_FACT comes about)
            
            // Fake data(normally read from file)
            DenseVector X;
            int N = 2;
            int nT = 100;
            double gamma = 0.015;
            double H1 = 0.01;
            double dT = 1.0 / nT;
            DenseVector a1 = new DenseVector(N+1);
            double a2;
            DenseVector ak = new DenseVector(N + 1); 
            DenseVector a3 = new DenseVector(N + 1);
            double K = 0;
            DenseVector adA;
            DenseVector adB;

            const double pstart = -1.5;
            const double prev = 3;

            var fortranFunctions = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            var X_list = new List<double>(N);

            // Initialise it to zero 

            X_list.AddRange(new double[] {0,0,0});

            for (var i = 2; i < N + 1; i++ )
                X_list.Add( H1 * (Math.Pow(gamma, i) - 1) / (gamma - 1));

            X = X_list.ToArray();

            fortranFunctions.Precalc(X, 
                N, gamma, 
                K, 
                out a1, 
                out a2, 
                out a3, 
                out ak, 
                out adA, 
                out adB);

            Assert.That(Math.Abs(a2 - 66.666668157), Is.LessThan(0.000002));
            Assert.That(Math.Abs(adA[1] + 66.691596514), Is.LessThan(0.000002));
            Assert.That(Math.Abs(adB[1] + 66.691596514), Is.LessThan(0.000002));
        }

        [Test]
        public void WhenPrecalcConstants_ThenDTIsCalculated()
        {
            // Arrange the test data (use a "fake" data, inc gamma until EE_FACT comes about)

            // Fake data(normally read from file)
            DenseVector X;
            int N = 2;
            int nT = 100;
            var gamma = 0.015;
            var H1 = 0.01;
            DenseVector a1 = new DenseVector(N + 1);
            double a2;
            var ak = new DenseVector(N + 1);
            var a3 = new DenseVector(N + 1);
            double K = 0;
            DenseVector adA;
            DenseVector adB;

            const double pstart = -1.5;
            const double prev = 3;

            var fortranFunctions = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            var X_list = new List<double>(N);

            // Initialise it to zero 

            X_list.AddRange(new double[] { 0, 0, 0 });

            for (var i = 2; i < N + 1; i++)
                X_list.Add(H1 * (Math.Pow(gamma, i) - 1) / (gamma - 1));

            X = X_list.ToArray();

            fortranFunctions.Precalc(X,
                N, gamma, 
                K,
                out a1,
                out a2,
                out a3,
                out ak,
                out adA,
                out adB);

            Assert.That(fortranFunctions.dt, Is.EqualTo(Math.Round(1.0000f/nT, 2)));
        }
    }
}
