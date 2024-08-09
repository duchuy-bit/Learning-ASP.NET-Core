using Microsoft.Data.SqlClient;
using ShopManager.Areas.Admin.Models;
using ShopManager.Database;
using ShopManager.Models;

namespace ShopManager.Areas.Admin.DAL
{
    public class PaymentAdminDAL
    {
        DBConnect connect = new DBConnect();

        public List<PaymentAdmin> GetAllPayment()
        {
            connect.openConnection();

            List<PaymentAdmin> list = new List<PaymentAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from payment ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PaymentAdmin payment = new PaymentAdmin()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CustomerId = Convert.ToInt32(reader["customerId"]),
                        FirstName = Convert.ToString(reader["firstName"])!,
                        LastName = Convert.ToString(reader["lastName"])!,
                        Phone = Convert.ToString(reader["phone"])!,
                        Email = Convert.ToString(reader["email"])!,
                        Total = Convert.ToInt32(reader["total"]),
                        CreateAt = DateTime.Parse(reader["createAt"].ToString()!),
                    };
                    list.Add(payment);
                }
            }
            connect.closeConnection();
            return list;
        }
        public bool AddNew(CategoryAdmin categoryAdd)
        {
            connect.openConnection();

            int id = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"insert into category(title, content, createAt, updateAt)
                    values(@title, @content, @createAt, @updateAt); ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@title", categoryAdd.Title);
                command.Parameters.AddWithValue("@content", categoryAdd.Content);
                command.Parameters.AddWithValue("@createAt", categoryAdd.CreateAt);
                command.Parameters.AddWithValue("@updateAt", categoryAdd.UpdateAt);

                Console.WriteLine("command Insert Category: " + command.CommandText);

                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }

        public CategoryAdmin getCategoryById(int id)
        {
            connect.openConnection();

            CategoryAdmin category = new CategoryAdmin();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from category where Id = " + id;

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    category.Id = id;
                    category.Title = reader["Title"].ToString() ?? "";
                    category.Content = reader["Content"].ToString() ?? "";
                    category.CreateAt = DateTime.Parse(reader["CreateAt"]?.ToString());
                    category.UpdateAt = DateTime.Parse(reader["UpdateAt"]?.ToString());
                }
            }
            connect.closeConnection();
            return category;
        }

        public bool updateCategoryById(int id, CategoryAdmin categoryUpdate)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update category
                    set title = @title, content = @content,  updateAt = @updateAt
                    where Id = @id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", categoryUpdate.Title);
                command.Parameters.AddWithValue("@content", categoryUpdate.Content);
                command.Parameters.AddWithValue("@updateAt", categoryUpdate.UpdateAt);

                Console.WriteLine("command update Category: " + command.CommandText);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool deleteCategoryById(int id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"DELETE FROM category WHERE id = @id;";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);

                Console.WriteLine("command delete Category: " + command.CommandText);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
    }
}
