using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("File.txt");

            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);
            foreach(var car in cars)
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
            => File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    //.Select(line => Car.ParseFromCvs(line))
                    .ToCar()
                    .ToList();
                    
    }
}
