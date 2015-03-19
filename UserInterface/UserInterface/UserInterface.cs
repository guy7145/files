using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.EnumCollection;

namespace UserInterface
{
    class UserInterface
    {
        static void introSequence()
        {
            Console.WriteLine("**********************************************");
            Console.WriteLine("* Welcome to EasyN'Greasy Management System! *");
            Console.WriteLine("**********************************************");
            Console.WriteLine("What would you like to do?");

            Method m = 0;
            for (int i = 0; i < Enum.GetNames(typeof(Method)).Length; i++)
            {
                Console.WriteLine((int)m + ". " + m);
                m++;
            }

            bool exit = false;
            while (!exit)

                switch ((Method)Console.Read())
                {
                    case (Method.Exit): exit = true;
                        break;
                    case (Method.Login): Console.WriteLine(loginSequence());
                        break;
                    case (Method.PrintUsers): printUsers();
                        break;
                    case (Method.PrintEmployees): EmployeeRequest();
                        break;
                    case (Method.Register): regNewUser();
                        break;
                    case (Method.RegisterEmployee): regNewEmployee();
                        break;
                    case (Method.DeleteUser): delUser();
                        break;
                    case (Method.DeleteEmployee): delEmployee();
                        break;
                    default: break;
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
            SqlCommand cmd = new SqlCommand("INSERT INTO Table_2 (firstName,lastName,salary,birthday) VALUES ('" + fName + "','" + lName + "','" + salary + "','" + bDay + "');", conn);
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
                Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1) + " makes " + reader.GetInt32(2));
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
            string user = Console.ReadLine();
            Console.WriteLine("Password: ");
            string pass = Console.ReadLine();
            SqlConnection conn = new SqlConnection("Server=LENOVO;Database=master;Integrated Security=true;User=sa;Password=saLogin");
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Table_1 (username,pw) VALUES ('" + user + "','" + pass + "');", conn);
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
            SqlCommand cmd = new SqlCommand("DELETE FROM Table_1 WHERE username='" + user + "';", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
    }
}
