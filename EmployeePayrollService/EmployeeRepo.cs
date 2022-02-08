using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Payroll_Service_ADO_NET;Integrated Security=True";

        SqlConnection con = new SqlConnection(ConnectionString);
        //public bool AddEmployee(EmployeePayroll model)
        //{
        //    try
        //    {
        //        using (this.con)
        //        {
        //            SqlCommand command = new SqlCommand("SpAddEmployees", this.con);
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
        //            command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
        //            command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
        //            command.Parameters.AddWithValue("@Address", model.Address);
        //            command.Parameters.AddWithValue("@Department", model.Department);
        //            command.Parameters.AddWithValue("@Gender", model.Gender);
        //            command.Parameters.AddWithValue("@Deductions", model.Deductions);
        //            command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
        //            command.Parameters.AddWithValue("@Tax", model.Tax);
        //            command.Parameters.AddWithValue("@NetPay", model.NetPay);
        //            command.Parameters.AddWithValue("@StartDate", model.StartDate);
        //            command.Parameters.AddWithValue("@EmployeeSalary", model.EmployeeSalary);
        //            command.Parameters.AddWithValue("@SalaryId", model.SalaryId);

        //            this.con.Open();
        //            var result = command.ExecuteNonQuery();
        //            this.con.Close();
        //            if (result != 0)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        this.con.Close();
        //    }
        //}
        public void GetAllEmployeeData()
        {

            EmployeePayroll EmployeePayroll = new EmployeePayroll(); //Creating Employee model class object
            try
            {
                using (this.con)
                {

                    string query = "select * from dbo.employee_payroll"; // Query to get all the data from table./*TableName:-dbo.payroll_service*/

                    this.con.Open(); //open connection

                    SqlCommand command = new SqlCommand(query, con); //accept query and connection

                    SqlDataReader reader = command.ExecuteReader(); // Execute sqlDataReader to fetching all records

                    if (reader.HasRows)     // Checking datareader has rows or not.               
                    {
                        while (reader.Read()) //using while loop for read multiple rows.
                        {
                            EmployeePayroll.EmployeeId = reader.GetInt32(0);
                            EmployeePayroll.EmployeeName = reader.GetString(1);
                            EmployeePayroll.Address = reader.GetString(3);
                            EmployeePayroll.Department = reader.GetString(4);
                            EmployeePayroll.Deductions = reader.GetDouble(7);
                            EmployeePayroll.TaxablePay = reader.GetDouble(8);
                            EmployeePayroll.Tax = reader.GetDouble(9);
                            EmployeePayroll.NetPay = reader.GetDouble(10);
                            EmployeePayroll.StartDate = reader.GetDateTime(11);
                            EmployeePayroll.City = reader.GetString(12);
                            EmployeePayroll.Country = reader.GetString(13);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", EmployeePayroll.EmployeeId,
                                EmployeePayroll.EmployeeName, EmployeePayroll.PhoneNumber, EmployeePayroll.Address, EmployeePayroll.Department,
                                EmployeePayroll.Gender,EmployeePayroll.Deductions, EmployeePayroll.TaxablePay,
                                EmployeePayroll.Tax, EmployeePayroll.NetPay, EmployeePayroll.StartDate, EmployeePayroll.City, EmployeePayroll.Country);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Record Not found on Table ");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.con.Close(); //Always ensuring the closing of the connection
            }

        }

        public bool AddEmployee(EmployeePayroll employee) //insert record to the table
        {

            try
            {
                using (this.con)
                {
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", this.con);   //Creating a stored Procedure for adding employees into database

                    command.CommandType = CommandType.StoredProcedure; //Command type is a class to set as stored procedure

                    // Adding values from employeeModel to stored procedure                     
                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Deductions", employee.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", employee.Tax);
                    command.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    command.Parameters.AddWithValue("@City", employee.City);
                    command.Parameters.AddWithValue("@Country", employee.Country);
                    con.Open();
                    var result = command.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // UC3:- Ability to update the salary i.e. the base pay for Employee

        public bool UpdateBasicPay(string EmployeeName, double BasicPay)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string query = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Payroll_Service_ADO_NET;Integrated Security=True";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@inputBasicPay", BasicPay);
                    command.Parameters.AddWithValue("@inputEmployeeName", EmployeeName);
                    var result = command.ExecuteNonQuery(); 
                    Console.WriteLine("Record Update Successfully");
                    con.Close();
                    GetAllEmployeeData(); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        //uc4//
        public double UpdatedSalaryFromDatabase(string EmployeeName)
        {

            string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Payroll_Service_ADO_NET;Integrated Security=True";

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                using (con)
                {
                    string query = @"update dbo.employee_payroll set BasicPay=@inputBasicPay where EmployeeName=@inputEmployeeName";
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    command.Parameters.AddWithValue("@inputEmployeeName", EmployeeName);
                    return (double)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void EmployeesFromForDateRange(string Date)
        {
            EmployeePayroll employeemodel = new EmployeePayroll(); //Creating Employee model class object

            try
            {
                using (con)
                {
                    con.Open(); //open connection
                    string query = $@"select * from dbo.employee_payroll where StartDate between cast('{Date}' as date) and cast(getdate() as date)";
                    SqlCommand command = new SqlCommand(query, con); 

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)                
                    {
                        
                        while (reader.Read()) 
                        {
                            employeemodel.EmployeeId = reader.GetInt32(0);
                            employeemodel.EmployeeName = reader.GetString(1);
                            employeemodel.Address = reader.GetString(3);
                            employeemodel.Department = reader.GetString(4);
                            employeemodel.Deductions = reader.GetDouble(7);
                            employeemodel.TaxablePay = reader.GetDouble(8);
                            employeemodel.Tax = reader.GetDouble(9);
                            employeemodel.NetPay = reader.GetDouble(10);
                            employeemodel.StartDate = reader.GetDateTime(11);
                            employeemodel.City = reader.GetString(12);
                            employeemodel.Country = reader.GetString(13);
                            Console.WriteLine("{0},{1},{3},{4},{7},{8},{9},{10},{11},{12},{13}",
                                employeemodel.EmployeeName, employeemodel.PhoneNumber, employeemodel.Address, employeemodel.Department,
                                employeemodel.Gender,employeemodel.Deductions, employeemodel.TaxablePay,
                                employeemodel.Tax, employeemodel.NetPay, employeemodel.StartDate, employeemodel.City, employeemodel.Country);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{Date} Record Not found on The Table ");
                    }
                    reader.Close(); //close                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.con.Close();
            }
        }
    }
}