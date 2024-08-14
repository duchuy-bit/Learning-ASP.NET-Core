using Microsoft.Data.SqlClient;
using ShopManager.Areas.Admin.Models;
using ShopManager.DAL;
using ShopManager.Database;

namespace ShopManager.Areas.Admin.DAL
{
    public class CustomerAdminDAL
    {
        DBConnect connect = new DBConnect();

        public List<CustomerAdmin> GetAll()
        {
            connect.openConnection();

            List<CustomerAdmin> list = new List<CustomerAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from customer ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerAdmin customer = new CustomerAdmin()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        LastName = reader["Lastname"].ToString() ?? "",
                        FirstName = reader["FirstName"].ToString() ?? "",
                        Address = reader["Address"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["Phone"].ToString() ?? "",
                        DateOfBirth = reader["dateOfBirth"] == DBNull.Value ? null
                            : DateOnly.FromDateTime(DateTime.Parse(reader["dateOfBirth"]!.ToString()!)),
                        Img = reader["Img"].ToString() ?? "",
                        Password = reader["Password"].ToString() ?? "",
                        RegisterAt = DateTime.Parse(reader["registeredAt"].ToString()!),
                        RandomKey = reader["RandomKey"].ToString() ?? "",
                        IsActive = Convert.ToInt32(reader["IsActive"]),
                        Role = Convert.ToInt32(reader["Role"]),
                    };

                    list.Add(customer);
                }
            }
            connect.closeConnection();
            return list;
        }

        public CustomerAdmin? getCustomerByEmail(string email)
        {
            connect.openConnection();

            CustomerAdmin customer = new CustomerAdmin();
            int dem = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from customer where customer.email = @email";

                command.CommandText = query;
                command.Parameters.AddWithValue("@email", email);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dem++;
                    customer.Id = Convert.ToInt32(reader["Id"]);
                    customer.FirstName = reader["firstName"].ToString()!;
                    customer.LastName = reader["lastName"].ToString()!;
                    customer.Address = reader["address"].ToString()!;
                    customer.Phone = reader["phone"].ToString()!;
                    customer.Email = reader["email"].ToString()!;
                    customer.Img = reader["img"].ToString()!;
                    customer.RegisterAt = DateTime.Parse(reader["registeredAt"].ToString()!);
                    customer.UpdateAt = DateTime.Parse(reader["updateAt"].ToString()!);
                    customer.DateOfBirth = reader["dateOfBirth"] == DBNull.Value
                            ? null
                            : DateOnly.FromDateTime(DateTime.Parse(reader["dateOfBirth"].ToString()!));
                    customer.Password = reader["password"].ToString()!;
                    customer.RandomKey = reader["randomKey"].ToString()!;
                    customer.IsActive = Convert.ToInt32(reader["IsActive"]);
                    customer.Role = Convert.ToInt32(reader["Role"]);
                }
            }
            connect.closeConnection();
            if (dem == 0) return null;
            return customer;
        }
        public bool AddNewCustomer(CustomerAdmin customerAdd)
        {
            connect.openConnection();

            int id = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"insert into customer( firstName, lastName, address, phone, email, img, registeredAt, dateOfBirth, password, randomKey, isActive, role )
                    values( @firstName, @lastName, @address, @phone, @email, @img, @registeredAt, @dateOfBirth, @password, @randomKey, @isActive, @role ); ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@firstName", customerAdd.FirstName);
                command.Parameters.AddWithValue("@lastName", customerAdd.LastName);
                command.Parameters.AddWithValue("@address", customerAdd.Address);
                command.Parameters.AddWithValue("@phone", customerAdd.Phone);
                command.Parameters.AddWithValue("@email", customerAdd.Email);
                command.Parameters.AddWithValue("@img", customerAdd.Img);
                command.Parameters.AddWithValue("@registeredAt", customerAdd.RegisterAt);
                command.Parameters.AddWithValue("@dateOfBirth",
                    customerAdd.DateOfBirth == null ? DBNull.Value : customerAdd.DateOfBirth);
                command.Parameters.AddWithValue("@password", customerAdd.Password);
                command.Parameters.AddWithValue("@randomKey", customerAdd.RandomKey);
                command.Parameters.AddWithValue("@isActive", customerAdd.IsActive);
                command.Parameters.AddWithValue("@role", customerAdd.Role);

                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }

        public CustomerAdmin? GetCustomerById(int id)
        {
            connect.openConnection();

            CustomerAdmin customer = new CustomerAdmin();
            int dem = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from customer where customer.id = @Id";

                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dem++;
                    customer.Id = Convert.ToInt32(reader["Id"]);
                    customer.FirstName = reader["firstName"].ToString()!;
                    customer.LastName = reader["lastName"].ToString()!;
                    customer.Address = reader["address"].ToString()!;
                    customer.Phone = reader["phone"].ToString()!;
                    customer.Email = reader["email"].ToString()!;
                    customer.Img = reader["img"].ToString()!;
                    customer.RegisterAt = DateTime.Parse(reader["registeredAt"].ToString()!);
                    customer.UpdateAt = reader["updateAt"] == DBNull.Value
                            ? null
                            : DateTime.Parse(reader["updateAt"].ToString()!);
                    customer.DateOfBirth = reader["dateOfBirth"] == DBNull.Value
                            ? null
                            : DateOnly.FromDateTime(DateTime.Parse(reader["dateOfBirth"].ToString()!));
                    customer.Password = reader["password"].ToString()!;
                    customer.RandomKey = reader["randomKey"].ToString()!;
                    customer.IsActive = Convert.ToInt32(reader["IsActive"]);
                    customer.Role = Convert.ToInt32(reader["Role"]);
                }
            }
            connect.closeConnection();
            if (dem == 0) return null;
            return customer;
        }

        public bool UpdateCustomerById(int id, CustomerAdmin customerUpdate)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                //firstName, lastName, address, phone, email, img, dateOfBirth, password, randomKey, isActive, role

                string query = @"update customer
                    set firstName = @firstName, lastName = @lastName,  address = @address, phone = @phone, email = @email, 
                    img = @img, dateOfBirth = @dateOfBirth, isActive = @isActive, role = @role, updateAt = @updateAt
                    where Id = @id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", customerUpdate.FirstName);
                command.Parameters.AddWithValue("@lastName", customerUpdate.LastName);
                command.Parameters.AddWithValue("@address", customerUpdate.Address);
                command.Parameters.AddWithValue("@phone", customerUpdate.Phone);
                command.Parameters.AddWithValue("@email", customerUpdate.Email);
                command.Parameters.AddWithValue("@img", customerUpdate.Img);
                command.Parameters.AddWithValue("@dateOfBirth",
                    customerUpdate.DateOfBirth == null ? DBNull.Value : customerUpdate.DateOfBirth);
                command.Parameters.AddWithValue("@isActive", customerUpdate.IsActive);
                command.Parameters.AddWithValue("@role", customerUpdate.Role);
                command.Parameters.AddWithValue("@updateAt", customerUpdate.UpdateAt);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool DeleteCustomerById(int id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"DELETE FROM customer WHERE id = @id;";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);


                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }


        public List<CustomerAdmin> GetCustomer_Pagination(int? page, int pageSize, string? filter, string? search, string? sortOrder)
        {
            connect.openConnection();

            List<CustomerAdmin> list = new List<CustomerAdmin>();

            //Search
            string condition = " where 1=1 ";
            if (!String.IsNullOrEmpty(search))
            {
                condition = condition + " and ( firstName like '%" + search + "%' or lastName like '%"
                    + search + "%' or email like '%" + search + "%' )";
            }
            if (!String.IsNullOrEmpty(filter))
            {
                condition = condition + " and ( " + filter + " ) ";
            }

            //Sort
            //Truy vấn Sắp xếp
            string sortQuery = " ORDER BY id ";
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "id_desc":
                        sortQuery = " ORDER BY id DESC ";
                        break;
                    case "firstName":
                        sortQuery = " ORDER BY firstName ";
                        break;
                    case "firstName_desc":
                        sortQuery = " ORDER BY firstName DESC ";
                        break;
                    case "lastName":
                        sortQuery = " ORDER BY lastName ";
                        break;
                    case "lastName_desc":
                        sortQuery = " ORDER BY lastName DESC ";
                        break;
                    case "registeredAt":
                        sortQuery = " ORDER BY registeredAt ";
                        break;
                    case "registeredAt_desc":
                        sortQuery = " ORDER BY registeredAt DESC ";
                        break;

                    default:
                        sortQuery = " ORDER BY id ";
                        break;
                }
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT * FROM (
	                SELECT ROW_NUMBER() OVER ( " + sortQuery + @" ) AS RowNumber , *  
	                FROM customer 
                    " + condition + @"
	                )TableResult
                WHERE TableResult.RowNumber BETWEEN( @PageIndex -1) * @PageSize + 1 
                AND @PageIndex * @PageSize  ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@PageIndex", page);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerAdmin category = new CustomerAdmin()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        LastName = reader["Lastname"].ToString() ?? "",
                        FirstName = reader["FirstName"].ToString() ?? "",
                        Address = reader["Address"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["Phone"].ToString() ?? "",
                        DateOfBirth = reader["dateOfBirth"] == DBNull.Value ? null
                            : DateOnly.FromDateTime(DateTime.Parse(reader["dateOfBirth"]!.ToString()!)),
                        Img = reader["Img"].ToString() ?? "",
                        Password = reader["Password"].ToString() ?? "",
                        RegisterAt = DateTime.Parse(reader["registeredAt"].ToString()!),
                        RandomKey = reader["RandomKey"].ToString() ?? "",
                        IsActive = Convert.ToInt32(reader["IsActive"]),
                        Role = Convert.ToInt32(reader["Role"]),
                    };

                    list.Add(category);
                }
            }
            connect.closeConnection();
            return list;
        }

        public int getCountRowCustomer_Pagination(string? filter, string? search)
        {
            connect.openConnection();
            int count = 0;

            List<CustomerAdmin> list = new List<CustomerAdmin>();

            //Search
            string condition = " where 1=1 ";
            if (!String.IsNullOrEmpty(search))
            {
                condition = condition + " and ( firstName like '%" + search + "%' or lastName like '%"
                    + search + "%' or email like '%" + search + "%' )";
            }
            if (!String.IsNullOrEmpty(filter))
            {
                condition = condition + " and ( " + filter + " ) ";
            }

            //Truy vaans
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT Count(*) as CountRow  FROM customer " + condition;

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = Convert.ToInt32(reader["CountRow"]);
                }
            }
            connect.closeConnection();
            return count;
        }
    }
}
