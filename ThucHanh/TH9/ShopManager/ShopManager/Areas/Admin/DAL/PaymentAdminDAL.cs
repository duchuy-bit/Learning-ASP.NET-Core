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

        public List<RevenueAdmin> GetTotalRevenue_WithMonth_InYear(int year)
        {
            connect.openConnection();

            List<RevenueAdmin> list = new List<RevenueAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT 
                        MONTH(createAt) AS Month,
                        SUM(total) AS TotalRevenue
                    FROM 
                        payment
                    WHERE 
                        YEAR(createAt) = @year
                    GROUP BY 
                        MONTH(createAt)
                    ORDER BY 
                        Month;";

                command.CommandText = query;
                command.Parameters.AddWithValue("@year", year);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RevenueAdmin revenueAdmin = new RevenueAdmin()
                    {
                        Month = Convert.ToInt32(reader["Month"]),
                        TotalRevenue = Convert.ToInt64(reader["TotalRevenue"])
                    };
                    list.Add(revenueAdmin);
                }
            }
            connect.closeConnection();
            return list;
        }

        public List<PaymentInMonth> GetCountPayment_WithMonth_InYear(int year)
        {
            connect.openConnection();

            List<PaymentInMonth> list = new List<PaymentInMonth>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT 
                        MONTH(createAt) AS Month,
                        count(total) AS CountPayment
                    FROM 
                        payment
                    WHERE 
                        YEAR(createAt) = @year
                    GROUP BY 
                        MONTH(createAt)
                    ORDER BY 
                        Month;";

                command.CommandText = query;
                command.Parameters.AddWithValue("@year", year);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PaymentInMonth paymentInMonth = new PaymentInMonth()
                    {
                        Month = Convert.ToInt32(reader["Month"]),
                        CountPayment = Convert.ToInt64(reader["CountPayment"])
                    };
                    list.Add(paymentInMonth);
                }
            }
            connect.closeConnection();
            return list;
        }


        public PaymentAdmin? GetPaymentById(int id)
        {
            connect.openConnection();
            int count = 0;

            PaymentAdmin payment = new PaymentAdmin();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select  p.id as PaymentId, c.id as CustomerId, p.firstName as FirstName, p.lastName as LastName, 
                        p.email as Email, p.phone as Phone, p.createAt as CreateAt, p.total as Total, c.img as Img
                        from payment p join customer c on c.id = p.customerId
                        where p.id = @id ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count++;
                    payment.Id = Convert.ToInt32(reader["PaymentId"]);
                    payment.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    payment.FirstName = Convert.ToString(reader["firstName"])!;
                    payment.LastName = Convert.ToString(reader["lastName"])!;
                    payment.Phone = Convert.ToString(reader["phone"])!;
                    payment.Email = Convert.ToString(reader["email"])!;
                    payment.Avatar = Convert.ToString(reader["img"])!;
                    payment.Total = Convert.ToInt32(reader["total"]);
                    payment.CreateAt = DateTime.Parse(reader["createAt"].ToString()!);
                }
            }
            connect.closeConnection();
            if (count == 0) return null;
            return payment;
        }


        public List<PaymentDetailAdmin> GetPaymentDetail_ByPaymentId(int paymentId)
        {
            connect.openConnection();

            List<PaymentDetailAdmin> list = new List<PaymentDetailAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select p.id as PaymentId,pr.id as ProductId, pr.title as TitleProduct,
                    pr.img as ProductImg, pd.price as Price, pd.quantity as Quantity, pd.total as TotalDetail
                    from  payment p left join paymentDetail pd on p.id = pd.paymentId
                    left join product pr on pr.id = pd.productId
                    where p.id = @paymentId   ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@paymentId", paymentId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PaymentDetailAdmin paymentDetailAdmin = new PaymentDetailAdmin()
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        PaymentId = Convert.ToInt32(reader["PaymentId"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        TotalDetail = Convert.ToInt32(reader["TotalDetail"]),
                        TitleProduct = reader["TitleProduct"].ToString()!,
                        Img = reader["ProductImg"].ToString()!
                    };

                    list.Add(paymentDetailAdmin);
                }
            }
            connect.closeConnection();
            return list;
        }


        public List<PaymentAdmin> GetPayment_Pagination(int? page, int pageSize, string? search, string? sortOrder)
        {
            connect.openConnection();

            List<PaymentAdmin> list = new List<PaymentAdmin>();

            //Search
            string condition = " where 1=1 ";
            if (!String.IsNullOrEmpty(search))
            {
                condition = condition + " and ( firstName like '%" + search + "%' or lastName like '%"
                    + search + "%' or email like '%" + search + "%' )";
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
                    case "createAt":
                        sortQuery = " ORDER BY createAt ";
                        break;
                    case "createAt_desc":
                        sortQuery = " ORDER BY createAt DESC ";
                        break;
                    case "total":
                        sortQuery = " ORDER BY total ";
                        break;
                    case "total_desc":
                        sortQuery = " ORDER BY total DESC ";
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
	                FROM payment 
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

        public int getCountRowPayment_Pagination(string? search)
        {
            connect.openConnection();
            int count = 0;

            //Search
            string condition = " where 1=1 ";
            if (!String.IsNullOrEmpty(search))
            {
                condition = condition + " and ( firstName like '%" + search + "%' or lastName like '%"
                   + search + "%' or email like '%" + search + "%'  )";
            }

            //Truy vaans
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT Count(*) as CountRow  FROM payment " + condition;

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
