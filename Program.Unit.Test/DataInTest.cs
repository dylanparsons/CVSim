using System;
using Microsoft.Win32.SafeHandles;
using Moq;
using NUnit.Framework;

namespace Program.Unit.Test
{
    public class DataInTest
    {
        [Test]
        public void WhenSetttingParameters_ThenParametersSetInNewObject()
        {
            const double pstart = -1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            const double expectedXlim = 18.00;

            Assert.That(cvSimulator.Xlim, Is.EqualTo(expectedXlim));
        }

        [Test]
        public void WhenFlushingCVSimulator_ThenFileIsWritten()
        {
            const double pstart = -1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });
            
            var fakeFileStream = new FakeFileStream();

            cvSimulator.WriteHeader(fakeFileStream);

            Assert.That(fakeFileStream.WasWrittenTo, Is.True);
        }


        [Test]
        public void WhenFlushingCVSimulator_ThenAllLinesCorrectlyWritter()
        {
            const double pstart = -1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            var fakeFileStream = new FakeFileStream();

            cvSimulator.WriteHeader(fakeFileStream);

            Assert.That(fakeFileStream.AreAllParametersCorrect, Is.True);
        }

        [Test]
        public void WhenPRevIsMinus1Point5AndPRevIs3_ThenXLimSetTo18()
        {
            const double pstart = -1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            Assert.That(cvSimulator.Xlim, Is.EqualTo(18.000));
        }


        [Test]
        public void WhenPRevIs1Point5AndPRevIsMinus3_ThenXLimSetTo18()
        {
            const double pstart = 1.5;
            const double prev = -3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            Assert.That(cvSimulator.Xlim, Is.EqualTo(18.000));
        }


        [Test]
        public void WhenPRevIs1Point5AndPRevIs3_ThenXLimSetTo18()
        {
            const double pstart = 1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            Assert.That(Math.Abs(cvSimulator.Xlim - 10.3923048), Is.LessThan(0.0000008));
        }


        [Test]
        public void WhenPRevIs1Point5AndPRevIs3_ThenNStepSetTo18()
        {
            const double pstart = 1.5;
            const double prev = 3;
            const int nT = 100;
            const double H1 = 1; // = X1 initially
            const int N = 1;
            const double K = 0;

            var cvSimulator = CVSimulator.Create(new SimulatorConfiguration
            {
                PStart = pstart,
                Prev = prev,
                nT = nT,
                H1 = H1,
                N = N,
                K = K
            });

            Assert.That(cvSimulator.NStep, Is.EqualTo(150));
        }

    }
}
