using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;
namespace MVC_Core.Areas.Customer.Repository
{
    public interface IDanhMucSPRepository
    {
        Category Add(Category loaiSp);
        Category Update(Category loaiSp);
        Category Delete(int id);
        Category GetLoaiSP(int loaiSp);

        IEnumerable<Category> GetAllLoaiSP();
    }
}
