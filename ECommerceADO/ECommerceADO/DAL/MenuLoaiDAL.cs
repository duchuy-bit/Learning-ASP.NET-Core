using ECommerceADO.DataBase;
using ECommerceADO.ViewModels;
using System.Data.SqlClient;

namespace ECommerceADO.DAL
{
    public class MenuLoaiDAL
    {

        DBConnect connect = new DBConnect();

        public List<MenuLoaiVM> getAll(int limit )
        {

            connect.openConnection();

            List<MenuLoaiVM> list = new List<MenuLoaiVM>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"select top " + limit + @" a.MaLoai, a.TenLoai ,count( a.MaLoai) as SoLuong 
                    from Loai a join HangHoa b on a.MaLoai = b.MaLoai
                    group by a.MaLoai,a.TenLoai
                    order by  Soluong desc  ";

                //command.Parameters.AddWithValue("@Limit", limit);

                //Console.WriteLine( limit+ "    "+command.CommandText);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MenuLoaiVM menuLoai = new MenuLoaiVM()
                    {
                        MaLoai = Convert.ToInt32(reader["MaLoai"]),
                        TenLoai = reader["TenLoai"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                    };

                    list.Add(menuLoai);
                }

                connect.closeConnection();
                return list;
            }

        }
    }
}
