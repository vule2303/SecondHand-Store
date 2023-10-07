using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Customer.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly S2HandDbContext _context;

        public BrandRepository(S2HandDbContext context) { _context = context; }
        public Brand Add(Brand Id)
        {
            _context.Brands.Add(Id);
            _context.SaveChanges();
            return Id;
        }

        public Brand Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Brand Delete(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAllLoaiSP()
        {
            return _context.Brands;
        }

        public Brand GetLoaiSP(int id)
        {
            return _context.Brands.Find(id);
        }

        public Brand Update(Brand Id)
        {
            _context.Update(Id);
            _context.SaveChanges();
            return Id;
        }
    }
}
