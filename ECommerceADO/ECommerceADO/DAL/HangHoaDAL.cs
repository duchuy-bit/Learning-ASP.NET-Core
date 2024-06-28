using ECommerceADO.DataBase;
using ECommerceADO.ViewModels;
using System.Data.SqlClient;

namespace ECommerceADO.DAL
{
    public class HangHoaDAL
    {

        DBConnect connect = new DBConnect();

        public List<HangHoaVM> getAll(int? idLoai)
        {

            Console.WriteLine(idLoai+"  jdakldjsakl");

            connect.openConnection();

            List<HangHoaVM> list = new List<HangHoaVM>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from HangHoa join Loai on HangHoa.MaLoai = Loai.MaLoai ";

                if (idLoai.HasValue)
                {
                    query = query + "where Loai.MaLoai = " + idLoai;
                }

                command.CommandText = query;

                    

                

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HangHoaVM menuLoai = new HangHoaVM()
                    {
                        MaHH = Convert.ToInt32(reader["MaHH"]),
                        TenHH = reader["TenHH"].ToString() ?? "",
                        DonGia = Double.Parse(reader["DonGia"].ToString())  ,
                        Hinh = reader["Hinh"].ToString(),
                        MotaNgan = reader["MoTaDonVi"].ToString(),
                        TenLoai = reader["TenLoai"].ToString()
                    };

                    list.Add(menuLoai);
                }

                connect.closeConnection();
                return list;
            }

        }

        public List<HangHoaVM> Search(string? search)
        {

            connect.openConnection();

            List<HangHoaVM> list = new List<HangHoaVM>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from HangHoa join Loai on HangHoa.MaLoai = Loai.MaLoai ";

                if (search != null)
                {
                    query = query + "where HangHoa.TenHH like '%" + search + "%'";
                }

                command.CommandText = query;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HangHoaVM menuLoai = new HangHoaVM()
                    {
                        MaHH = Convert.ToInt32(reader["MaHH"]),
                        TenHH = reader["TenHH"].ToString() ?? "",
                        DonGia = Double.Parse(reader["DonGia"].ToString()),
                        Hinh = reader["Hinh"].ToString(),
                        MotaNgan = reader["MoTaDonVi"].ToString(),
                        TenLoai = reader["TenLoai"].ToString()
                    };

                    list.Add(menuLoai);
                }
            }

            connect.closeConnection();
            return list;
        }

        public HangHoaCTVM? GetById(int Id)
        {
            connect.openConnection();
            HangHoaCTVM menuLoai = new HangHoaCTVM();
            int dem = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"select * from HangHoa join Loai on HangHoa.MaLoai = Loai.MaLoai 
                    where HangHoa.MaHH = "+ Id;

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine(reader.FieldCount);

                

                while (reader.Read())
                {
                    dem++;
                    menuLoai = new HangHoaCTVM()
                    {
                        MaHH = Convert.ToInt32(reader["MaHH"]),
                        TenHH = reader["TenHH"].ToString() ?? "",
                        DonGia = Double.Parse(reader["DonGia"].ToString()),
                        Hinh = reader["Hinh"].ToString(),
                        MotaNgan = reader["MoTaDonVi"].ToString(),
                        TenLoai = reader["TenLoai"].ToString(),
                        ChiTiet = reader["Mota"].ToString(),
                        DiemDanhGia = 4.5,
                        SoLuongTon = 10,
                    };
                }
            }

            connect.closeConnection();
            if (dem == 0)
            {
                return null;
            }
            return menuLoai;
        }

    }
}
