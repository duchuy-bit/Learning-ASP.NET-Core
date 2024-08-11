using Microsoft.Data.SqlClient;
using ShopManager.Areas.Admin.Models;
using ShopManager.Database;

namespace ShopManager.Areas.Admin.DAL
{
    public class ProductAdminDAL
    {

        DBConnect connect = new DBConnect();

        public List<ProductAdmin> getAll()
        {
            connect.openConnection();

            List<ProductAdmin> list = new List<ProductAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select a.id as ProductId, a.title as ProductTitle,  
                    a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                    a.rate as ProductRate, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                    b.id as CategoryId, b.title as CategoryTitle
                    from product a join category b on a.categoryId = b.Id
                    ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // ================== Cách 1 ===============
                    ProductAdmin product = new ProductAdmin()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };

                    // ================= Cách 2 ===============
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

        public bool AddNew(ProductFormAdmin productNew)
        {
            connect.openConnection();

            int id = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"insert into product (title, content, img, price, rate, createAt, updateAt, categoryId)
                    values (@title, @content, @img, @price, @rate, @createAt, @updateAt, @categoryId); ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@rate", productNew.Rate);
                command.Parameters.AddWithValue("@createAt", productNew.CreateAt);
                command.Parameters.AddWithValue("@updateAt", productNew.UpdateAt);
                command.Parameters.AddWithValue("@categoryId", productNew.CategoryId);

                Console.WriteLine("command Insert Product: " + command.CommandText);

                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }
        public ProductAdmin GetProductById(int Id)
        {
            connect.openConnection();
            ProductAdmin product = new ProductAdmin();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select a.id as ProductId, a.title as ProductTitle,  
                    a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                    a.rate as ProductRate, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                    b.id as CategoryId, b.title as CategoryTitle
                    from product a join category b on a.categoryId = b.Id
                    where a.Id = @Id  ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", Id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    product = new ProductAdmin()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
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
        public bool UpdateProduct(ProductFormAdmin productNew, int Id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update product
                    set title = @title, content = @content, img = @img, price = @price, rate = @rate,  updateAt = @updateAt, categoryId = @categoryId
                    where id = @id
                    ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@rate", productNew.Rate);
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

        // Lấy số lượng kết quả sau khi truy vấn có thêm điều kiện (Search)
        public int getCountRow_Pagination(int pageIndex, int pageSize, string? searchString)
        {
            connect.openConnection();

            // khai báo biến lưu số lượng Row truy vấn được
            int count = 0;

            //Nếu chuỗi tìm kiếm có dữ liệu thì thêm câu lệnh SQL WHERE
            string condition = "";
            if (searchString != "" && searchString != null)
            {
                condition = @" Where a.title Like '%" + searchString + "%' or a.content like '%" + searchString + "%' ";
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                //Câu truy vấn
                string query = @" 
                    SELECT Count(*) as CountRow
                    FROM product a join category b on a.categoryId = b.Id ";

                // nối thêm điều kiện, Nếu điều kiện không có thì condition=""
                // query = query + ""; thì vẫn là query -> không ảnh hưởng
                query = query + condition;

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

        // Lấy danh sách sản phầm theo phân trang (số trang hiện tại, số hàng trong 1 trang, tìm kiếm)
        public List<ProductAdmin> getProduct_Pagination(int pageIndex, int pageSize, string? searchString, string sortOrder)
        {
            connect.openConnection();

            List<ProductAdmin> list = new List<ProductAdmin>();

            //Nếu chuỗi tìm kiếm có dữ liệu thì thêm câu lệnh SQL WHERE
            string condition = "";
            if (searchString != "" && searchString != null)
            {
                condition = @" Where a.title Like '%" + searchString + "%' or a.content like '%" + searchString + "%' ";
            }

            //Truy vấn Sắp xếp
            string sortQuery = " ORDER BY a.id ";
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "id_desc":
                        sortQuery = " ORDER BY a.id DESC ";
                        break;
                    case "title":
                        sortQuery = " ORDER BY a.title ";
                        break;
                    case "title_desc":
                        sortQuery = " ORDER BY a.title DESC ";
                        break;
                    case "price":
                        sortQuery = " ORDER BY a.price ";
                        break;
                    case "price_desc":
                        sortQuery = " ORDER BY a.price DESC ";
                        break;
                    case "rate":
                        sortQuery = " ORDER BY a.rate ";
                        break;
                    case "rate_desc":
                        sortQuery = " ORDER BY a.rate DESC ";
                        break;

                    default:
                        sortQuery = " ORDER BY a.id ";
                        break;
                }
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                //Truy vấn lồng nhau
                // ROW_NUMBER() OVER(ORDER BY a.id asc) AS RowNumber tạo thêm 1 cột để lưu index (tương tự số thứ tự)
                // sau đó sử dụng câu lệnh BETWEEN(start, end) để lấy dữ liệu có RowNumber (index ở trên): 
                //      start <= RowNumber <= end
                string query = @" 
                        SELECT * FROM (
                         SELECT ROW_NUMBER() OVER( " + sortQuery + @") AS RowNumber,
                             a.id as ProductId, a.title as ProductTitle,  
                             a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                             a.rate as ProductRate, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                             b.id as CategoryId, b.title as CategoryTitle
                             FROM product a join category b on a.categoryId = b.Id "
                         + condition +
                        @") TableResult
                        WHERE TableResult.RowNumber BETWEEN( @PageIndex -1) * @PageSize + 1 
                         AND @PageIndex * @PageSize ";


                command.CommandText = query;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductAdmin product = new ProductAdmin()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };

                    list.Add(product);
                }
            }
            connect.closeConnection();

            return list;
        }


        // Lấy số lượng kết quả sau khi truy vấn có thêm điều kiện (Search) Theo Category
        public int getCountRow_Pagination_Category(int? idCategory, int pageIndex, int pageSize, string? searchString)
        {
            connect.openConnection();

            // khai báo biến lưu số lượng Row truy vấn được
            int count = 0;



            //Nếu chuỗi tìm kiếm có dữ liệu thì thêm câu lệnh SQL WHERE
            string condition = "where 1=1 ";
            // kieemr tra category
            if (idCategory.HasValue)
            {
                condition = @" and a.categoryId = " + idCategory;
            }

            //Kieerm tra timf kiem
            if (searchString != "" && searchString != null)
            {
                condition = condition + " and ( " +  @"a.title Like '%" + searchString + @"%' or a.content like '%" + searchString + @"%' ) ";
            }


            //SQL QUERY
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                //Câu truy vấn
                string query = @" 
                    SELECT Count(*) as CountRow
                    FROM product a join category b on a.categoryId = b.Id ";

                // nối thêm điều kiện, Nếu điều kiện không có thì condition=""
                // query = query + ""; thì vẫn là query -> không ảnh hưởng
                query = query + condition;

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

        // Lấy danh sách sản phầm theo phân trang (số trang hiện tại, số hàng trong 1 trang, tìm kiếm) theo Category
        public List<ProductAdmin> getProduct_Pagination_Category(int? idCategory, int pageIndex, int pageSize, string? searchString, string sortOrder)
        {
            connect.openConnection();

            List<ProductAdmin> list = new List<ProductAdmin>();

            //Nếu chuỗi tìm kiếm có dữ liệu thì thêm câu lệnh SQL WHERE
            string condition = "where 1=1 ";
            // kieemr tra category
            if (idCategory.HasValue)
            {
                condition = @" and a.categoryId = " + idCategory;
            }

            //Kieerm tra timf kiem
            if (searchString != "" && searchString != null)
            {
                condition = condition + " and ( " + @"a.title Like '%" + searchString + @"%' or a.content like '%" + searchString + @"%' ) ";

            }

            //Truy vấn Sắp xếp
            string sortQuery = " ORDER BY a.id ";
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "id_desc":
                        sortQuery = " ORDER BY a.id DESC ";
                        break;
                    case "title":
                        sortQuery = " ORDER BY a.title ";
                        break;
                    case "title_desc":
                        sortQuery = " ORDER BY a.title DESC ";
                        break;
                    case "price":
                        sortQuery = " ORDER BY a.price ";
                        break;
                    case "price_desc":
                        sortQuery = " ORDER BY a.price DESC ";
                        break;
                    case "rate":
                        sortQuery = " ORDER BY a.rate ";
                        break;
                    case "rate_desc":
                        sortQuery = " ORDER BY a.rate DESC ";
                        break;

                    default:
                        sortQuery = " ORDER BY a.id ";
                        break;
                }
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                //Truy vấn lồng nhau
                // ROW_NUMBER() OVER(ORDER BY a.id asc) AS RowNumber tạo thêm 1 cột để lưu index (tương tự số thứ tự)
                // sau đó sử dụng câu lệnh BETWEEN(start, end) để lấy dữ liệu có RowNumber (index ở trên): 
                //      start <= RowNumber <= end
                string query = @" 
                        SELECT * FROM (
                         SELECT ROW_NUMBER() OVER( " + sortQuery + @") AS RowNumber,
                             a.id as ProductId, a.title as ProductTitle,  
                             a.content as ProductContent, a.img as ProductImg, a.price as ProductPrice,
                             a.rate as ProductRate, a.createAt as ProductCreateAt, a.updateAt as ProductUpdateAt, 
                             b.id as CategoryId, b.title as CategoryTitle
                             FROM product a join category b on a.categoryId = b.Id "
                         + condition +
                        @") TableResult
                        WHERE TableResult.RowNumber BETWEEN( @PageIndex -1) * @PageSize + 1 
                         AND @PageIndex * @PageSize ";


                command.CommandText = query;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductAdmin product = new ProductAdmin()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };

                    list.Add(product);
                }
            }
            connect.closeConnection();

            return list;
        }

    }
}
