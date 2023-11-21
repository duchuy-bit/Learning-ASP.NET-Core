using ConnectSQL.Data;
using ConnectSQL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace ConnectSQL.Service
{
    public class HangHoaResponsitory : IHangHoaResponsitory
    {

        private readonly MyDbContext _context;

        public HangHoaResponsitory(MyDbContext context)
        {
            _context = context;
        }


        public HangHoaVM Add(HangHoaVM hanghoaNew)
        {
            var loaiNew = new HangHoa
            {
                TenHH = hanghoaNew.TenHH,
                DonGia = hanghoaNew.DonGia,
                Mota = hanghoaNew.Mota,
                GiamGia = hanghoaNew.GiamGia,
                MaLoai = hanghoaNew?.MaLoai
            };
            _context.Add(loaiNew);
            _context.SaveChanges();
            return new HangHoaVM
            {
                MaHH = loaiNew.MaHH,
                TenHH = hanghoaNew.TenHH,
                Mota = hanghoaNew.Mota,
                DonGia = hanghoaNew.DonGia,
                GiamGia = hanghoaNew.GiamGia,
                MaLoai = hanghoaNew.MaLoai
            };
        }

        //public void Delete(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public List<HangHoaVM> GetAll()
        {
            System.Diagnostics.Debug.WriteLine("HangHoaResponsitory");

            var hanghoas = _context.HangHoas.Select(loai => new HangHoaVM
            {
                MaHH = loai.MaHH,
                TenHH = loai.TenHH,
                Mota = loai.Mota,
                DonGia = loai.DonGia,
                GiamGia = loai.GiamGia,
                MaLoai = loai.MaLoai
            });

            //System.Diagnostics.Debug.WriteLine("alo lao",hanghoas);

            return hanghoas.ToList();
        }

        //public HangHoaVM GetById(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Guid id, HangHoaModel hanghoa)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
