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

            var gjQuery = from manufacturer in manufacturers
                          join car in cars on manufacturer.Name equals car.Manufacturer
                          into carGroup
                          orderby manufacturer.Name
                          select new
                          {
                              Manufacturer =manufacturer,
                              Cars = carGroup
                          };
            var gjQuery2 = manufacturers
                            .GroupJoin(cars,
                                       m => m.Name,
                                       c => c.Manufacturer,
                                       (m, g) => new
                                       {
                                           Manufacturer = m,
                                           Cars = g
                                       })
                             .OrderBy(g => g.Manufacturer.Name);

            foreach (var group in gjQuery2)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }


            var gQuery = from car in cars
                         group car by car.Manufacturer.ToUpper()
                         into manufacturer
                         orderby manufacturer.Key
                         select manufacturer;
            var gQuery2 = cars
                            .GroupBy(c => c.Manufacturer.ToUpper())
                            .OrderBy(g => g.Key);

            var query = from car in cars
                        join manufacturer in manufacturers
                        on new { car.Manufacturer, car.Year }
                           equals
                           new { Manufacturer = manufacturer.Name, manufacturer.Year }
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Name,
                            car.Combined,
                            manufacturer.Headquarters
                        };
            var query2 = cars
                         .Join(manufacturers,
                                c => new { c.Manufacturer, c.Year },
                                m => new { Manufacturer = m.Name, m.Year },
                                (c, m) => new
                                {
                                    c.Name,
                                    c.Combined,
                                    m.Headquarters
                                })
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name);

            //foreach(var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Name,-20} {car.Headquarters,-12}: {car.Combined}");
            //}
            //Console.WriteLine("**********************");
            //foreach (var car in query2.Take(10))
            //{
            //    Console.WriteLine($"{car.Name,-20} {car.Headquarters,-12} : {car.Combined}");
            //}

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
