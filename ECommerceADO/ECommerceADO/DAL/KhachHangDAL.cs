using ECommerceADO.DataBase;
using ECommerceADO.Helper;
using ECommerceADO.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace ECommerceADO.DAL
{
    public class KhachHangDAL
    {
        DBConnect connect = new DBConnect();

        public bool DangKy(RegisterVM register, string hinhUpload)
        {

            connect.openConnection();

            Console.WriteLine("Mat Khau  " + register.MatKhau);

            KhachHangVM khachHang = new KhachHangVM();
            //khachHang = model;
            khachHang.RandomKey = MyUtil.GenerateRandomKey();
            khachHang.MatKhau = register.MatKhau.ToMd5Hash(khachHang.RandomKey);

            khachHang.HieuLuc = true; // Sẽ xử lý khi dùng Mail để Active

            Console.WriteLine(khachHang.RandomKey+ "  "+ khachHang.MatKhau);
            Console.WriteLine("   "+"admin".ToMd5Hash(khachHang.RandomKey));



            List<MenuLoaiVM> list = new List<MenuLoaiVM>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"insert into KhachHang (MaKH, MatKhau, HoTen, GioiTinh, DiaChi, DienThoai, Email, Hinh, HieuLuc, VaiTro, RandomKey)
                    values (@MaKH, @MatKhau, @HoTen, @GioiTinh, @DiaChi, @DienThoai, @Email, @Hinh, @HieuLuc, @VaiTro, @RandomKey)"
                ;

                //@MaKH, @MatKhau, @HoTen, @GioiTinh, @DiaChi, @DienThoai, @Email, @Hinh, @HieuLuc, @VaiTro, @RandomKey

                command.Parameters.AddWithValue("@MaKH", register.MaKH);
                command.Parameters.AddWithValue("@MatKhau", khachHang.MatKhau);
                command.Parameters.AddWithValue("@HoTen", register.HoTen);
                command.Parameters.AddWithValue("@GioiTinh", register.GioiTinh);
                command.Parameters.AddWithValue("@DiaChi", register.DiaChi);
                command.Parameters.AddWithValue("@DienThoai", register.DienThoai);
                command.Parameters.AddWithValue("@Email", register.Email);
                command.Parameters.AddWithValue("@Hinh", hinhUpload);
                command.Parameters.AddWithValue("@HieuLuc", true);
                command.Parameters.AddWithValue("@VaiTro", 0);
                command.Parameters.AddWithValue("@RandomKey", khachHang.RandomKey);

                Console.WriteLine("   command " + command.CommandText);

                var id = command.ExecuteNonQuery();


                connect.closeConnection();
                return id >0;
            }

        }

        public KhachHangVM? DangNhap(LoginVM userLogin)
        {

            connect.openConnection();
            KhachHangVM khachHang = new KhachHangVM();
            int dem = 0;


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"Select * from KhachHang where MaKH = '" + userLogin.Username + @"'";
                ;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dem++;
                    khachHang = new KhachHangVM()
                    {
                        MaKH = reader["MaKH"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        DienThoai = reader["DienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        //GioiTinh = Convert.ToInt16( reader["GioiTinh"]),
                        HieuLuc = Convert.ToInt16( reader["HieuLuc"]) == 1? true: false,
                        Hinh = reader["Hinh"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        NgaySinh= DateTime.Parse(reader["NgaySinh"].ToString()),
                        RandomKey = reader["RandomKey"].ToString(),
                        vaiTro = Convert.ToInt32( reader["VaiTro"]),
                    };

                    
                }


                connect.closeConnection();
                if (dem == 0)
                {
                    return null;
                }
                return khachHang;
            }

        }
    }
}
