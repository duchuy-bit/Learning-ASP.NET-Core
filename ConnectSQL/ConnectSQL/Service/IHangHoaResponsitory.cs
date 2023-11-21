using ConnectSQL.Models;

namespace ConnectSQL.Service
{
    public interface IHangHoaResponsitory
    {
        List<HangHoaVM> GetAll();

        //HangHoaVM GetById(Guid id);

        HangHoaVM Add(HangHoaVM hanghoa);

        //void Update(Guid id, HangHoaModel hanghoa);

        //void Delete(Guid id);
    }
}
