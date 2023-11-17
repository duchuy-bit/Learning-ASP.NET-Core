using ConnectSQL.Models;

namespace ConnectSQL.Service
{
    public interface ILoaiResponsitory
    {
        List<LoaiVM> GetAll();

        LoaiVM GetById(int id);

        LoaiVM Add(LoaiModel loai);

        void Update(int id,LoaiVM loai);

        void Delete(int id);
    }
}
