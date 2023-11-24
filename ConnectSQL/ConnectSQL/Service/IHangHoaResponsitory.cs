using ConnectSQL.Models;

namespace ConnectSQL.Service
{
    public interface IHangHoaResponsitory
    {
        List<HangHoaVM> GetAll();

        HangHoaVM GetById(Guid id);

        HangHoaVM Add(HangHoaVM hanghoa);

        void Update(Guid id, HangHoaVM hanghoa);

        void Delete(Guid id);

        List<HangHoaFind> FindHangHoa(string? search, double? from, double? to, string? sortBy, int page );
    }
}
