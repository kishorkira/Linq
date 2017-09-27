using System;
using System.Collections.Generic;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{Id=1,Name="Ram"},
                new Employee{Id=2,Name="Adi"}

            };

            IEnumerable<Employee> sales = new List<Employee>
            {
                new Employee{Id=3,Name="Raj"},
                new Employee{Id=4,Name="Sri"}

            };
            Console.WriteLine(sales.Count());
            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }
    }
}
