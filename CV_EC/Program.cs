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
using System.IO;
using System.Xml.Schema;
using Program;

namespace CV_EC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@" pstart, prev, nT, X(1), N, K?");
            var commandParams = Console.ReadLine();
            Console.WriteLine(@" Output file name?");
            var outputFilename = Console.ReadLine();
            var simulationParameters = commandParams.Split(',');

            if ( simulationParameters.Length != 6 )
                Console.WriteLine("Not enough parameters supplied, ending.");
            else
            {
                try
                {
                    var pStart = double.Parse(simulationParameters[0]);
                    var prev = double.Parse(simulationParameters[1]);
                    var nT = int.Parse(simulationParameters[2]);
                    var h1 = double.Parse((simulationParameters[3]));
                    var Count = int.Parse(simulationParameters[4]);
                    var tempInKelvin = double.Parse(simulationParameters[5]);

                    Console.WriteLine(@" CV_EC:");
                    Console.WriteLine($@" nT        = {nT} per p-unit");
                    Console.WriteLine($@" pstart    = {pStart:10.3}");
                    Console.WriteLine($@" pstart    = {prev:10.3}");
                    Console.WriteLine($@" X(1)      = {h1:12.5}");
                    Console.WriteLine($@" N         = {Count}");
                    Console.WriteLine($@" hcr K     = {tempInKelvin}");

                    var dT = 1.0f / nT;
                    var xlim = 6 * Math.Sqrt(2 * Math.Abs(prev - pStart));

                    Console.WriteLine($@" Xlim      = {xlim}");
                    Console.WriteLine(@" 2-point G and boundary cond.");

                    using (var fs = new FileStream(string.Format(".\\{0}", outputFilename), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        try
                        {
                            fs.Seek(0, SeekOrigin.Begin);
                            CVSimulator.Run(pStart, prev, nT, h1, Count, tempInKelvin, fs);
                        }
                        finally
                        {
                            fs.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }
    }
}
