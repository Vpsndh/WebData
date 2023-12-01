using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebData.Models;

namespace WebData.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly XtbDbContext _context;

        public HomeController(XtbDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page, int CatId = 0)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            List<Post> lsPosts = new List<Post>();
            if (CatId != 0)
            {
                lsPosts = _context.Posts
                    .AsNoTracking().Where(x => x.CatId == CatId)
                    .Include(x => x.Cat)
                    .OrderByDescending(x => x.PostId).ToList();
            }
            else
            {
                lsPosts = _context.Posts
                    .AsNoTracking()
                    .Include(x => x.Cat)
                    .OrderByDescending(x => x.PostId).ToList();
            }
            PagedList<Post> models = new PagedList<Post>(lsPosts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentsPage = pageNumber;
            ViewBag.CurrentsCat = CatId;
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(models);
        }
        public  IActionResult Filtter(int CatId = 0)
        {
            var url = $"/Admin?CatId={CatId}";
            if (CatId == 0)
            {
                url = $"/Admin";
            }
            return Json(new {status = "success", redirectUrl = url});
        }

    }
}
