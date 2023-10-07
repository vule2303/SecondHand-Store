using Microsoft.AspNetCore.Mvc;
using MVC_Core.Areas.Customer.Repository;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class BranddViewComponent : ViewComponent
    {
        private readonly IBrandRepository _loaiBrand;

        public BranddViewComponent(IBrandRepository brandRepository)
        {
            _loaiBrand = brandRepository;
        }
        public IViewComponentResult Invoke()
        {
            var listSp = _loaiBrand.GetAllLoaiSP().OrderBy(x => x.Id);
            return View(listSp);
        }
    }
}
