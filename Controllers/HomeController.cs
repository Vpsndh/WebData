using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebData.Extension;
using WebData.Models;

namespace WebData.Controllers
{
    public class HomeController : Controller
    {
        private readonly XtbDbContext _context;

        public HomeController(XtbDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult Index(string? returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountID");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Areas = "Admin" });
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login model, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Account kh = _context.Accounts
                        .Include(a => a.Roles)
                        .SingleOrDefault(p => p.UserName.ToLower() == model.UserN.ToLower().Trim());
                    if (kh == null)
                    {
                        ViewBag.Error = "Thông tin đăng nhập không chính xác";
                        return View();
                    }
                    string pass = (model.Passwd.Trim()).ToMD5();
                    if (kh.Password.Trim() != pass)
                    {
                        ViewBag.Error = "Thông tin đăng nhập không chính xác";
                        return View();
                    }
                    string Accidstr = kh.AccountId.ToString();
                    HttpContext.Session.SetString("Accid", Accidstr);
                    var taikhoanID = HttpContext.Session.GetString("Accid");
                    var userclaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, kh.FullName),
                        new Claim(ClaimTypes.Role, kh.Roles.RolesName),
                        new Claim("AccountID", kh.AccountId.ToString()),
                        new 
                        Claim("RoleID", kh.RolesId.ToString())
                    };
                    var grandmaIdentity = new ClaimsIdentity(userclaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    await HttpContext.SignInAsync(userPrincipal);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("admin");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}