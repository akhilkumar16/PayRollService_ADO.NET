﻿using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            static void AddRecordInput()
            {
                Console.WriteLine("Welcome to Employee Payroll Services Using ADO.NET Problem");

                EmployeeRepo repository = new EmployeeRepo();
                //UC2
             
                repository.GetAllEmployeeData();              
                Console.WriteLine();
            }
            EmployeeRepo repository = new EmployeeRepo();
                EmployeePayroll employee = new EmployeePayroll();

                employee.EmployeeName = "a";
                employee.PhoneNumber = 1234567899;
                employee.Address = "telangana";
                employee.Department = "abc";
                employee.Gender = 'M';
                employee.Deductions = 200;
                employee.TaxablePay = 250;
                employee.Tax = 240;
                employee.NetPay = 200000;
                employee.StartDate = DateTime.Now;
                employee.City = "Varanasi";
                employee.Country = "India";

                Console.WriteLine(repository.AddEmployee(employee) ? "Record Successfully Inserted On Table" : "Failed");
            }
        }
    }
}
