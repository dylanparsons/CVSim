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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Program.Unit.Test
{
    public class EEFactorTest
    {
        [Test]
        public void WhenWeNeedAVariableWidthColumnCallingEEFac_ThenItReturnsGamma()
        {
            // For small numbers, get the gamma
            var N = 3;
            var limitX = 9;
            var startX = 1;

            var gammaStrategy = new GammaStrategy();

            Assert.That( gammaStrategy.Calculate(startX, N, limitX), Is.EqualTo( 3 ) );
        }


        [Test]
        public void WhenWeNeed100VariableWidthColumnsCallingEEFac_ThenItReturnsGamma()
        {
            // For small numbers, get the gamma
            var N = 100;
            var limitX = 9;
            var startX = 1;

            var gammaStrategy = new GammaStrategy();
            var expected = Math.Pow(9, 1.0d / 99.0d);

            Assert.That(gammaStrategy.Calculate(startX, N, limitX), Is.EqualTo(expected));
        }
    }
}
