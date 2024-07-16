﻿using Microsoft.Data.SqlClient;
using ShopManager.Database;
using ShopManager.Models;

namespace ShopManager.DAL
{
    public class ProductDAL
    {
        DBConnect connect = new DBConnect();

        public List<Product> getAll()
        {
            connect.openConnection();

            List<Product> list = new List<Product>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                //string query = @"select * from product a join category b
                //    on a.categoryId = b.Id";

                string query = @"select a.id as ProductId, a.title as ProductTitle,  
                    a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                    a.discount as ProductDiscount, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                    b.id as CategoryId, b.title as CategoryTitle
                    from product a join category b on a.categoryId = b.Id
                    ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Discount = Convert.ToDouble(reader["ProductDiscount"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };

                    //Product product = new Product()
                    //{
                    //    Id = Convert.ToInt32(reader[0]),
                    //    Title = reader[1].ToString() ?? "",
                    //    Content = reader[2].ToString() ?? "",
                    //    Img = reader[3].ToString() ?? "",
                    //    Price = Convert.ToInt32(reader[4]),
                    //    Discount = Convert.ToDouble(reader[5].ToString()),
                    //    CreateAt = DateTime.Parse(reader[6]?.ToString()),
                    //    UpdateAt = DateTime.Parse(reader[7]?.ToString()),
                    //    CategoryId = Convert.ToInt32(reader[8]),
                    //    CategoryTitle = reader[9].ToString(),
                    //};

                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }


        public bool AddNew(ProductForm productNew)
        {
            connect.openConnection();

            int id = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"insert into product (title, content, img, price, discount, createAt, updateAt, categoryId)
                    values (@title, @content, @img, @price, @discount, @createAt, @updateAt, @categoryId); ";

                command.CommandText = query;

                

                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                //command.Parameters.AddWithValue("@img", "okok");
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@discount", productNew.Discount);
                command.Parameters.AddWithValue("@createAt", productNew.CreateAt);
                command.Parameters.AddWithValue("@updateAt", productNew.UpdateAt);
                command.Parameters.AddWithValue("@categoryId", productNew.CategoryId);

                Console.WriteLine("command Insert Product: " + command.CommandText);

                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }

        public Product GetProductById(int Id)
        {
            connect.openConnection();
            Product product = new Product();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select a.id as ProductId, a.title as ProductTitle,  
                    a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                    a.discount as ProductDiscount, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                    b.id as CategoryId, b.title as CategoryTitle
                    from product a join category b on a.categoryId = b.Id
                    where a.Id = @Id  ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", Id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Discount = Convert.ToDouble(reader["ProductDiscount"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };
                }
            }
            connect.closeConnection();
            return product;
        }

        public bool UpdateProduct(ProductForm productNew, int Id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update product
                    set title = @title, content = @content, img = @img, price = @price, discount = @discount,  updateAt = @updateAt, categoryId = @categoryId
                    where id = @id
                    ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@discount", productNew.Discount);
                command.Parameters.AddWithValue("@updateAt", productNew.UpdateAt);
                command.Parameters.AddWithValue("@categoryId", productNew.CategoryId);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool DeleteProduct(int Id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"DELETE FROM product 
                    WHERE id = @id ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", Id);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
    }
}
