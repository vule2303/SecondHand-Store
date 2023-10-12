using Microsoft.AspNetCore.Mvc;
using MVC_Core.Areas.Customer.Repository;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IDanhMucSPRepository _loaiSp;

        public CategoryViewComponent(IDanhMucSPRepository danhMucSPRepository)
        {
            _loaiSp = danhMucSPRepository;
        }
        public IViewComponentResult Invoke()
        {
            var listSp = _loaiSp.GetAllLoaiSP().OrderBy(x => x.Name);
            return View(listSp);
        }
    }
}
