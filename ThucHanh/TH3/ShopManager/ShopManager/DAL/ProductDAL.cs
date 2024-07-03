using Microsoft.Data.SqlClient;
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
                    //foreach (var item in reader) {
                    //Console.WriteLine(reader["ProductId"]);
                    //Console.WriteLine(reader["ProductTitle"]);
                    //Console.WriteLine(reader["ProductContent"]);
                    //Console.WriteLine(reader["ProductImg"]);
                    //Console.WriteLine(reader["ProductPrice"]);
                    //Console.WriteLine(reader["ProductDiscount"]);
                    //Console.WriteLine(reader["ProductCreateAt"]);
                    //Console.WriteLine(reader["ProductUpdateAt"]);
                    //Console.WriteLine(reader["CategoryId"]);
                    //Console.WriteLine(reader["CategoryTitle"]);
                    //}
                    //Product product = new Product()
                    //{
                    //    Id = Convert.ToInt32(reader["ProductId"]),
                    //    Title = reader["ProductTitle"].ToString() ?? "",
                    //    Content = reader["ProductContent"].ToString() ?? "",
                    //    Img = reader["ProductImg"].ToString() ?? "",
                    //    Price = Convert.ToInt32(reader["ProductPrice"]),
                    //    Discount = Convert.ToDouble(reader["ProductDiscount"].ToString()),
                    //    CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                    //    UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                    //    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    //    CategoryTitle = Convert.ToInt32(reader["CategoryTitle"]).ToString(),
                    //};

                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader[0]),
                        Title = reader[1].ToString() ?? "",
                        Content = reader[2].ToString() ?? "",
                        Img = reader[3].ToString() ?? "",
                        Price = Convert.ToInt32(reader[4]),
                        Discount = Convert.ToDouble(reader[5].ToString()),
                        CreateAt = DateTime.Parse(reader[6]?.ToString()),
                        UpdateAt = DateTime.Parse(reader[7]?.ToString()),
                        CategoryId = Convert.ToInt32(reader[8]),
                        CategoryTitle = reader[9].ToString(),
                    };

                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }
    }
}
