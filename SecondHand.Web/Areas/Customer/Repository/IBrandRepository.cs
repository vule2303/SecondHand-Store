using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Customer.Repository
{
    public interface IBrandRepository
    {
        Brand Add(Brand Id);
        Brand Update(Brand Id);
        Brand Delete(int id);
        Brand GetLoaiSP(int id);

        IEnumerable<Brand> GetAllLoaiSP();
    }
}
