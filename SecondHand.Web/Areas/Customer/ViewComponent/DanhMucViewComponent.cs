using Microsoft.AspNetCore.Mvc;
using MVC_Core.Areas.Customer.Repository;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    
    public class DanhMucViewComponent : ViewComponent
    {
        private readonly IDanhMucSPRepository _loaiSp;

        public DanhMucViewComponent(IDanhMucSPRepository danhMucSPRepository) {
            _loaiSp = danhMucSPRepository;
        }
        public IViewComponentResult Invoke()
        {
            var listSp = _loaiSp.GetAllLoaiSP().OrderBy(x => x.Name);
            return View(listSp);
        }

    }
}
