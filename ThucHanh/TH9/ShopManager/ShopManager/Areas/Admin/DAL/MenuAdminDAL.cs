using Microsoft.Data.SqlClient;
using ShopManager.Areas.Admin.Models;
using ShopManager.DAL;
using ShopManager.Database;
using ShopManager.Models;

namespace ShopManager.Areas.Admin.DAL
{
    public class MenuAdminDAL
    {
        DBConnect connect = new DBConnect();

        public List<MenuAdmin> GetAllMenu()
        {
            connect.openConnection();

            List<MenuAdmin> list = new List<MenuAdmin>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from menu  order by menuIndex ";

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MenuAdmin MenuAdmin = new MenuAdmin()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? "",
                        ParentId = reader["ParentId"] != DBNull.Value ? Convert.ToInt32(reader["ParentId"]) : null,
                        MenuUrl = reader["MenuUrl"]?.ToString() ?? null,
                        MenuIndex = Convert.ToInt32(reader["MenuIndex"]),
                        IsVisible = Convert.ToInt32(reader["isVisible"]) == 1
                    };
                    list.Add(MenuAdmin);
                }
            }
            connect.closeConnection();
            return list;
        }


        public bool updateMenuVisible(int id, bool value)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update menu
                    set isVisible = @isVisible 
                    where Id = @id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@isVisible", value ? 1 : 0);


                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool updateMenuIndex(int id, int newMenuIndex)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update menu
                    set menuIndex = @menuIndex 
                    where Id = @id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@menuIndex", newMenuIndex);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool AddNewMenu(MenuAdmin menuAdd, bool isSubmenu)
        {
            connect.openConnection();

            int id = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"insert into menu(title, parentId,  menuUrl, menuIndex, isVisible)
                    values( @title, @parentId, @menuUrl, @menuIndex, @isVisible); ";

                command.CommandText = query;

                command.Parameters.AddWithValue("@title", menuAdd.Title);
                command.Parameters.AddWithValue("@parentId", isSubmenu? menuAdd.ParentId:  DBNull.Value );
                command.Parameters.AddWithValue("@menuUrl", menuAdd.MenuUrl);
                command.Parameters.AddWithValue("@menuIndex", menuAdd.MenuIndex);
                command.Parameters.AddWithValue("@isVisible", true);

                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }

        public MenuAdmin GetMenuAdminById(int id)
        {
            connect.openConnection();

            MenuAdmin menuAdmin = new MenuAdmin();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from menu where Id = " + id;

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    menuAdmin.Id = id;
                    menuAdmin.Title = reader["Title"].ToString() ?? "";
                    menuAdmin.ParentId = reader["ParentId"] != DBNull.Value ? Convert.ToInt32(reader["ParentId"]) : null;
                    menuAdmin.MenuUrl = reader["MenuUrl"]?.ToString() ?? null;
                    menuAdmin.MenuIndex = Convert.ToInt32(reader["MenuIndex"]);
                    menuAdmin.IsVisible = Convert.ToInt32(reader["isVisible"]) == 1;
                }
            }
            connect.closeConnection();
            return menuAdmin;
        }

        public bool UpdateMenuById(int id, MenuAdmin menuUpdate)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"update menu
                    set title = @title, menuUrl = @menuUrl
                    where Id = @id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id); 
                command.Parameters.AddWithValue("@title", menuUpdate.Title);
                command.Parameters.AddWithValue("@menuUrl", menuUpdate.MenuUrl);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }

        public bool DeleteMenuById(int id)
        {
            connect.openConnection();

            int isSuccess = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"DELETE FROM menu WHERE id = @id;";

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);

                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
    }
}
