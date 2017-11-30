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
using NUnit.Framework;

namespace Program.Unit.Test
{
    public class LookupTest
    {
        [Test]
        public void WhenlookingUpValueFromTable_ThenReturnsCorrectly()
        {
            var reader = new ExperimentReader();
            var potential = 720;
            var entry = reader[potential];

            Assert.That(entry.Potential, Is.EqualTo(-1.65));
        }


        [Test]
        public void WhenlookingUpValueFromTableWithoutData_ThenThgrowsException()
        {
            var reader = new ExperimentReader();
            var potential = 1000;
            

            Assert.Throws<ArgumentOutOfRangeException>(delegate {
                var entry = reader[potential];
            });
        }
    }

    
}
