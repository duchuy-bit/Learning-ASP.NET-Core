using ConnectSQL.Data;
using ConnectSQL.Helper;
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

        private static int PAGE_SIZE { get; set; } = 3;

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

        public void Delete(Guid id)
        {
            var hanghoaUpdate = _context.HangHoas.SingleOrDefault(obj => obj.MaHH == id);
            if (hanghoaUpdate != null)
            {
                _context.Remove(hanghoaUpdate);
                _context.SaveChanges();
            }
        }

        public List<HangHoaFind> FindHangHoa(string? search, double? from, double? to, string? sortBy, int page)
        {
            Debug.WriteLine("FindHangHoa");
            var allProducts = _context.HangHoas.Include(hh => hh.Loai).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where((hanghoa) => hanghoa.TenHH.Contains(search!));
            }

            if (from.HasValue)
            {
                allProducts = allProducts.Where((hanghoa) => hanghoa.DonGia >= from);
            }

            if (to.HasValue)
            {
                allProducts = allProducts.Where((hanghoa) => hanghoa.DonGia <= to);
            }
            #endregion


            #region SortBy
            // Default sort by Name (TenHH)
            allProducts = allProducts.OrderBy(hh => hh.TenHH);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc":
                        allProducts = allProducts.OrderByDescending(hh => hh.TenHH);
                        break;

                    case "gia_asc":
                        allProducts = allProducts.OrderBy(hh => hh.DonGia);
                        break;

                    case "v":
                        allProducts = allProducts.OrderByDescending(hh => hh.DonGia);
                        break;
                }
            }
            #endregion


            #region Paging
            //allProducts = allProducts.Skip(PAGE_SIZE * (page - 1)).Take(PAGE_SIZE);

            #endregion


            //Debug.WriteLine(allProducts);

            //var result = allProducts.Select((hanghoa) => new HangHoaFind
            //{
            //    MaHH = hanghoa.MaHH,
            //    TenHH = hanghoa.TenHH,
            //    DonGia = hanghoa.DonGia,
            //    TenLoai = hanghoa.Loai.TenLoai,
            //});

            var result = PaginatedList<ConnectSQL.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);


            return result.Select(hanghoa => new HangHoaFind
            {
                MaHH = hanghoa.MaHH,
                TenHH = hanghoa.TenHH,
                DonGia = hanghoa.DonGia,
                TenLoai = hanghoa.Loai.TenLoai,
            }).ToList();
        }

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

            return hanghoas.ToList();
        }

        public HangHoaVM GetById(Guid id)
        {
            var hanghoa = _context.HangHoas.SingleOrDefault(obj => obj.MaHH == id);

            return new HangHoaVM
            {
                MaHH = hanghoa!.MaHH,
                TenHH = hanghoa.TenHH,
                Mota = hanghoa.Mota,
                DonGia = hanghoa.DonGia,
                GiamGia = hanghoa.GiamGia,
                MaLoai = hanghoa.MaLoai
            };
        }

        public void Update(Guid id, HangHoaVM hanghoa)
        {
            var hanghoaUpdate = _context.HangHoas.SingleOrDefault(obj => obj.MaHH == id);
            if (hanghoaUpdate != null)
            {
                if (hanghoa.TenHH != null) hanghoaUpdate.TenHH = hanghoa.TenHH;
                if (hanghoa.Mota != null) hanghoaUpdate.Mota = hanghoa.Mota;
                if (hanghoa.DonGia > 0) hanghoaUpdate.DonGia = hanghoa.DonGia;
                if (hanghoa.GiamGia > 0) hanghoaUpdate.GiamGia = hanghoa.GiamGia;
                if (hanghoa.MaLoai != null) hanghoaUpdate.MaLoai = hanghoa.MaLoai;
                _context.SaveChanges();
            }
        }

    }
}
