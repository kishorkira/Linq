using System;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    public class CarXML
    {
        public static void CreateXML()
        {
            var records =Program.ProcessCarFile("fuel.csv");

            var document = new XDocument();
            var cars = new XElement("Cars",
                                    records
                                    .Select
                                    (r => 
                                    new XElement("Car",
                                                 new XAttribute("Name", r.Name),
                                                 new XAttribute("Manufacturer", r.Manufacturer),
                                                 new XAttribute("Displacement", r.Displacement),
                                                 new XAttribute("Combined", r.Combined),
                                                 new XAttribute("Cylinders", r.Cylinders),
                                                 new XAttribute("City", r.City),
                                                 new XAttribute("Highway", r.Highway),
                                                 new XAttribute("Year", r.Year)
                                                 )
                                    ));
            document.Add(cars);
            document.Save("fuel.xml");
        }

        public static void QueryXML()
        {
            var document = XDocument.Load("fuel.xml");

            var query = document.Element("Cars").Elements("Car")
                        .Where(e => e.Attribute("Manufacturer")?.Value == "BMW") ;

            foreach(var car in query)
            {
                Console.WriteLine($"{car.Attribute("Name").Value} : {car.Attribute("Combined").Value}");
            }
        }
    }
}