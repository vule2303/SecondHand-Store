using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Text;

namespace MVC_Core.Areas.Customer.Controllers
{
	[Area("Customer")]

	public class SearchController : Controller
	{
		private readonly S2HandDbContext _context;
		public IActionResult Index()
		{
			return View();
		}
		public SearchController(S2HandDbContext context)
		{
			_context = context;
		}


		public async Task<IActionResult> Search(string searchTerm)
		{

			IQueryable<Product> query = _context.Products;
			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(p => p.Name.Contains(searchTerm));
			}

			var products = await query
				.Include(p => p.Brand)
				.Include(p => p.Category)
				.Include(p => p.productGallery)
				.ToListAsync();

			return View("Index", products);
		}

	}
}
