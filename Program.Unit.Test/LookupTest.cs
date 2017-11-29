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
