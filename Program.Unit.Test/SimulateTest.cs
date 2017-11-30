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
    public class SimulateTest
    {
        [Test]
        public void WhenSimulating6TimePeriods_ThenHeaderEntriesAreCreated()
        {
            // Potl (V) Background (A) Current (A)
            // -1.501	1.37331E-06	1.17494E-06

            const double pstart = -1.501;
            const double prev = -1.502;
            const int nT = 1000;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;
            DenseVector a1;
            double a2;
            DenseVector ak;
            DenseVector a3;
            DenseVector adA;
            DenseVector adB;
            var gamma = 0.015; // This is fake! Need the real function value
            DenseVector X;
            
            var cvSimulator = CVSimulator.Create( new SimulatorConfiguration
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

            cvSimulator.Precalc(X,
                N, gamma, 
                K,
                out a1,
                out a2,
                out a3,
                out ak,
                out adA,
                out adB);

            var fileStrean = new FakeFileStream();

            cvSimulator.WriteHeader(fileStrean);

            Assert.That(fileStrean.Count, Is.EqualTo(6));
        }

        [Test]
        public void WhenWritingOneStep_ThenAStepIsAddedToHeaderForBothSweepDirections()
        {
            const double pstart = -1.501;
            const double prev = -1.502;
            const int nT = 1000;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 293.15;
            var fileStrean = new FakeFileStream();

            CVSimulator.Run( pstart,  prev, nT,  H1, N,  K, fileStrean);
            Assert.That(fileStrean.Count, Is.EqualTo(8));
        }
        
        [Test]
        public void WhenWritingBetween1Pt5And2_With500Steps_Then1006RecordsWritten()
        {
            const double pstart = -1.500;
            const double prev = -2;
            const int nT = 1000;
            const double H1 = 0.01; // = X1 initially
            const int N = 500;
            const double K = 293.15;
            var fileStrean = new FakeFileStream();

            CVSimulator.Run(pstart, prev, nT, H1, N, K, fileStrean);
            Assert.That(fileStrean.Count, Is.EqualTo(1006));
        }

    }
}
