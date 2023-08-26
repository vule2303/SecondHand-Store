using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Server
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Category/Brand.cshtml");
        }
        public IActionResult GetData()
        {
            var data = new[] {
                new { Id = 1, Name = "Brand 1" },
                new { Id = 2, Name = "Brand 2" },
                // Add more data items as needed
            };

            return Json(data);
        }

        [HttpPost]
        public IActionResult Update(int id, string name)
        {
            // Replace this with actual data update logic
            return Json(new { success = true, message = "Data updated successfully." });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Replace this with actual data delete logic
            return Json(new { success = true, message = "Data deleted successfully." });
        }
    }
}
