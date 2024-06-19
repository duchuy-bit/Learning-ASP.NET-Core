using CRUDWithADO.Net.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUDWithADO.Net.DAL
{
    public class Employee_DAL
    {
        //SqlConnection _connection = null;
        SqlConnection _connection = new SqlConnection("Data Source=ADMIN\\DUCHUY;Initial Catalog=CRUDWithADO.Net_Database;Integrated Security=True;TrustServerCertificate=False;Encrypt=false");
        SqlCommand _command = null;

        public List<Employee> GetAll()
        {
            System.Diagnostics.Debug.WriteLine("ALO");
            Console.WriteLine("hjejejejje");

            _connection.Open();

            List<Employee> employeeList = new List<Employee>();



            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"select * from Employees";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    employee.Email = reader["Email"].ToString();
                    employee.Salary = Convert.ToDouble(reader["Salary"]);


                    employeeList.Add(employee);
                }
            }

            _connection.Close();
            return employeeList;
        }

        public bool InsertEmployee(Employee employee)
        {
            int id = 0;

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = 
                  @"insert into 
                        Employees(FirstName,LastName,DateOfBirth,Email,Salary)
                        values   (@firstName, @lastName, @dateOfBirth, @email, @salary )                
                  ";

                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@dateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@email", employee.Email);
                command.Parameters.AddWithValue("@salary", employee.Salary) ;

                id = command.ExecuteNonQuery();
            }

                _connection.Close();

            return id >0 ;
        }

        public Employee getEmployeeById(int id)
        {
            Employee employee = new Employee();

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"select * from Employees where Id = @Id";

                command.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                foreach (DataRow reader in dataTable.Rows)
                {                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    employee.Email = reader["Email"].ToString();
                    employee.Salary = Convert.ToDouble(reader["Salary"]);
                }
            }

            _connection.Close();


            return employee;
        }

        public bool UpdateEmployee(int id, Employee employee)
        {
            int isSuccess = 0;

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText =
                  @"update  Employees
                    set FirstName = @firstName,LastName=@lastName,DateOfBirth=@dateOfBirth,Email= @email,Salary=@salary          
                    where Id = @Id
                  ";

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@dateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@email", employee.Email);
                command.Parameters.AddWithValue("@salary", employee.Salary);

                isSuccess = command.ExecuteNonQuery();
            }

            _connection.Close();

            return isSuccess > 0;
        }


        public bool DeleteEmployee(int id)
        {
            int isSucces = 0;

            _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"delete from Employees where Id = @Id";

                command.Parameters.AddWithValue("@Id", id);

                isSucces = command.ExecuteNonQuery();
            }

            _connection.Close();


            return isSucces >0 ;
        }




        //public static IConfiguration Configuration { get; set; }

        //public string GetConnectionString()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(
        //        Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        //    Configuration = builder.Build();
        //    return Configuration.GetConnectionString("DefaultConnection");
        //}

        //public List<Employee> GetAll()
        //{
        //    List<Employee> employeeList = new List<Employee>();
        //    using (_connection = new SqlConnection(GetConnectionString()))
        //    {
        //        _command = _connection.CreateCommand();
        //        _command.CommandType = System.Data.CommandType.Text;
        //        _command.CommandText = "Select * from Employees";

        //        _connection.Open();

        //        SqlDataReader reader = _command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Employee employee = new Employee();
        //            employee.Id = Convert.ToInt32(reader["Id"]);
        //            employee.FirstName = reader["FirstName"].ToString();
        //            employee.LastName = reader["LastName"].ToString();
        //            employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
        //            employee.Email = reader["Email"].ToString();
        //            employee.Salary = Convert.ToDouble(reader["Salary"]);

        //            employeeList.Add(employee);
        //        }
        //        _connection.Close();

        //    }
        //    return employeeList;

        //}
    }
}
