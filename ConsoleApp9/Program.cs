using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp9.Program;

namespace ConsoleApp9
{
    class Program
    {
        public delegate bool ELigibleToPromotion(Employee EmployeeToPromotion);

        static void Main(string[] args)
        {
            Employee empl1 = new Employee()
            {
                ID = 55,
                Name = "Алексей",
                Expirience = 5,
                Salary = 20000
            };

            Employee empl2 = new Employee()
            {
                ID = 56,
                Name = "Михаил",
                Expirience = 2,
                Salary = 10000
            };

            Employee empl3 = new Employee()
            {
                ID = 57,
                Name = "Николай", // Исправлено
                Expirience = 4,
                Salary = 15000
            };

            List<Employee> IsEmployees = new List<Employee>();
            IsEmployees.Add(empl1); // Исправлено
            IsEmployees.Add(empl2);
            IsEmployees.Add(empl3);

            ELigibleToPromotion eligibleToPromotion = Promote;
            Employee.PromoteEmployee(IsEmployees, eligibleToPromotion); // Исправлено
        }

        public static bool Promote(Employee employee)
        {
            if (employee.Salary > 10000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Expirience { get; set; }
            public int Salary { get; set; }

            public static void PromoteEmployee(List<Employee> listEmployees, ELigibleToPromotion IsEmployeeEligible) // Исправлено
            {
                foreach (Employee employee in listEmployees) // Исправлено
                {
                    if (IsEmployeeEligible(employee))
                        Console.WriteLine("Employee {0} Promoted", employee.Name);
                }
            }
        }
    }
}
