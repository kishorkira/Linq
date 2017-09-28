using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{Id=1,Name="Ram"},
                new Employee{Id=2,Name="Adi"},
                new Employee{Id=5,Name="Sri"}


            };

            IEnumerable<Employee> sales = new List<Employee>
            {
                new Employee{Id=3,Name="Raj"},
                new Employee{Id=4,Name="Sri"}

            };
            Console.WriteLine(sales.Count());
            IEnumerator<Employee> enumerator = sales.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
            //Named function
            foreach(var employee in developers.Where(NameLength3)){
                Console.WriteLine(employee.Name);
            }
            //delegate
            foreach (var employee in developers.Where(delegate (Employee employee)
            {
                return employee.Name.Length == 3;
            }))
            {
                Console.WriteLine(employee.Name);
            }
            //Lambda
            foreach (var employee in developers
                                    .Where( e => e.Name.Length == 3))
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static bool NameLength3(Employee emp)
        {
            return emp.Name.Length == 3;
        }
    }
}
