using Microsoft.Data.SqlClient;
using ShopManager.Database;
using ShopManager.Models;

namespace ShopManager.DAL
{
    public class MenuDAL
    {
        DBConnect connect = new DBConnect();
        public List<MenuItem> GetAllMenu()
        {
            connect.openConnection();

            List<MenuItem> list = new List<MenuItem>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from menu  order by menuIndex ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //var parentId = DbNu;
                    MenuItem MenuItem = new MenuItem()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? "",
                        ParentId = reader["ParentId"] != DBNull.Value ? Convert.ToInt32(reader["ParentId"]) : null,
                        MenuUrl = reader["MenuUrl"]?.ToString() ?? null,
                        MenuIndex = Convert.ToInt32(reader["MenuIndex"]),
                        isVisible = Convert.ToInt32(reader["isVisible"]) == 1
                    };

                    list.Add(MenuItem);
                }
            }
            connect.closeConnection();
            return list;
        }
    }
}
