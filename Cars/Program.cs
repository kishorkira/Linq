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

            var query = from car in cars
                        join manufacturer in manufacturers
                        on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Name,
                            car.Combined,
                            manufacturer.Headquarters
                        };
            var query2 = cars
                         .Join(manufacturers,
                                c => c.Manufacturer,
                                m => m.Name,
                                (c, m) => new
                                {
                                    c.Name,
                                    c.Combined,
                                    m.Headquarters
                                })
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name);

            foreach(var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name,-20} {car.Headquarters,-12}: {car.Combined}");
            }
            Console.WriteLine("**********************");
            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Name,-20} {car.Headquarters,-12} : {car.Combined}");
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
