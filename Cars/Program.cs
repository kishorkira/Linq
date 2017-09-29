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
            var cars = ProcessCarFile("fuel.csv");
            var manufacturers = ProcessManufacturerFile("manufacturers.csv");

            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);
            var query2 = manufacturers.OrderBy(c => c.Headquarters)
                           .ThenBy(c => c.Name);
            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
            Console.WriteLine("----Manufacturers----");
            foreach (var manufacturer in query2)
            {
                Console.WriteLine($"{manufacturer.Headquarters,-12} : {manufacturer.Name} ");
            }
        }

        private static List<Manufacturer> ProcessManufacturerFile(string file)
            => File.ReadAllLines(file)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToManufacturer()
                    .ToList();

        private static List<Car> ProcessCarFile(string file)
            => File.ReadAllLines(file)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    //.Select(line => Car.ParseFromCvs(line))
                    .ToCar()
                    .ToList();
                    
    }
}
