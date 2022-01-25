using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ado.net");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeePayroll employee = new EmployeePayroll();

            employee.EmployeeId = 1;
            employee.EmployeeName = "a";
            employee.PhoneNumber = 1234567899;
            employee.Address = "telamgana";
            employee.Department = " abc";
            employee.Gender = 'M';
            employee.Deductions = 200;
            employee.TaxablePay = 250;
            employee.Tax = 240;
            employee.NetPay = 200000;
            employee.StartDate = DateTime.Now;
        }
    }
}
