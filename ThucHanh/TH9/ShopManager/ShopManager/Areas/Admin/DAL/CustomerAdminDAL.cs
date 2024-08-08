﻿using Microsoft.Data.SqlClient;
using ShopManager.Areas.Admin.Models;
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
