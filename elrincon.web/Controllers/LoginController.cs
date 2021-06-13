using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using elrincon.web.Manager;

namespace elrincon.web.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult GirisYap(string user, string password)
        {
            ElDbTable loginKontrol = ExecuteTable("select * from el_kullanicilar where kullanici_adi like '" + user + "' and kullanici_sifre like '" + password + "'");
            if (loginKontrol.Rows.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(user, true);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Login");//todo: json result  
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}