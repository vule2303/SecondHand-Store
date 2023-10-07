using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;

namespace MVC_Core.Areas.Customer.Repository
{
    public class DanhMucSPRepository : IDanhMucSPRepository
    {
        private readonly S2HandDbContext _context;

        public DanhMucSPRepository(S2HandDbContext context) {
            _context = context;
        }
        public Category Add(Category loaiSp)
        {
            _context.Categories.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public Category Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllLoaiSP()
        {
            return _context.Categories;
        }

        public Category GetLoaiSP(int id)
        {
            return _context.Categories.Find(id);
        }

     

        public Category Update(Category loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        Category IDanhMucSPRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
