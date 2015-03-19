using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Class1
    {
        static void Main(string[] args)
        {
            introSequence();

        }
        static void introSequence()
        {
            Console.WriteLine("**********************************************");
            Console.WriteLine("* Welcome to EasyN'Greasy Management System! *");
            Console.WriteLine("**********************************************");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Login = 1");
            Console.WriteLine("2. Print users = 2");
            Console.WriteLine("3. Print Employees = 3");
            Console.WriteLine("4. Register = 4");
            Console.WriteLine("5. Register Employee = 5");
            Console.WriteLine("6. Delete User = 6");
            Console.WriteLine("7. Delete Employee = 7");
            Console.WriteLine("8. Exit = e");
            bool exit = false;
            while(!exit)
            switch (char.Parse(Console.ReadLine()))
            {
                case('1'):
                    loginSequence();
                    break;
                case('2'):
                    printUsers();
                    break;
                case('3'):
                    EmployeeRequest();
                    break;
                case('4'):
                    regNewUser();
                    break;
                case('5'):
                    regNewEmployee();
                    break;
                case('6'):
                    delUser();
                    break;
                case('7'):
                    delEmployee();
                    break;
                case('e'):
                    exit = true;
                    break;
                default:
                    break;
            }

        }
        private static void delEmployee()
        {
            Console.WriteLine("Employee name to delete: ");
            string fName = Console.ReadLine();
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Table_2 WHERE firstName='" + fName + "';", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
        private static void regNewEmployee()
        {
            Console.WriteLine("First name: ");
            string fName = Console.ReadLine();
            Console.WriteLine("Last name: ");
            string lName = Console.ReadLine();
            Console.WriteLine("Salary: ");
            string salary = Console.ReadLine();
            Console.WriteLine("Birthday: ");
            string bDay = Console.ReadLine();
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Table_2 (firstName,lastName,salary,birthday) VALUES ('" + fName + "','" + lName + "','"+salary+"','"+bDay+"');", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
        static void EmployeeRequest()
        {
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT firstName,lastName,salary FROM Table_2;", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1)+" makes "+reader.GetInt32(2));
            }
            reader.Close();
            conn.Close();
        }
        static bool loginSequence()
        {
            Console.WriteLine("Username: ");
            string user = Console.ReadLine();
            Console.WriteLine("Password: ");
            string pw = Console.ReadLine();

            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT username,pw FROM Table_1;", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(0).Equals(user))
                    if (reader.GetString(1).Equals(pw))
                        return true;
            }
            reader.Close();
            conn.Close();
            return false;
        }
        static void printUsers()
        {
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT username,pw FROM Table_1;", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + ", " + reader.GetString(1));
            }
            reader.Close();
            conn.Close();
            
        }
        static void regNewUser()
        {
            Console.WriteLine("Username: ");
            string user=Console.ReadLine();
            Console.WriteLine("Password: ");
            string pass = Console.ReadLine();
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Table_1 (username,pw) VALUES ('"+user+"','"+pass+"');", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
        static void delUser()
        {
            Console.WriteLine("Username to delete: ");
            string user = Console.ReadLine();
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Table_1 WHERE username='"+user+"';", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
    }
    
}
