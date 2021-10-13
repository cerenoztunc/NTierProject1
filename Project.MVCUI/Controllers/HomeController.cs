using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository _apRep;
        public HomeController()
        {
            _apRep = new AppUserRepository();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser yakalanan = _apRep.FirstOrDefault(x => x.UserName == appUser.UserName);
            if(yakalanan == null)
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return View();
            }
            string decrpyted = DantexCrypt.DeCrypt(yakalanan.Password);
            if(appUser.Password == decrpyted && yakalanan.Role == ENTITIES.Enums.UserRole.Admin)
            {
                if (!yakalanan.Active) return AktifKontrol();

                Session["admin"] = yakalanan;
                return RedirectToAction("CategoryList", "Category", new { area = "Admin" });
            }
            else if(yakalanan.Role == ENTITIES.Enums.UserRole.Member && appUser.Password == decrpyted)
            {
                if (!yakalanan.Active) return AktifKontrol();
                Session["member"] = yakalanan;
                return RedirectToAction("ShoppingList", "Shopping");
            }
            ViewBag.Kullanici = "Kullanıcı bulunamadı";
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(AppUser appUser, UserProfile userProfile)
        {
            AppUser yakalanan = _apRep.FirstOrDefault(x => x.Email == appUser.Email);
            TempData["Kullanici"] = "Doğrulama mailinize şifreniz gönderilmiştir ";
            if(yakalanan != null)
            {
                Random r = new Random();

                yakalanan.Password = r.Next(1101, 9999).ToString();
               

                string gonderilecekEmail = "Yeni şifreniz: " + yakalanan.Password + " Şifrenizi en kısa sürede değiştirmelisiniz..";
                MailService.Send(appUser.Email, body: gonderilecekEmail, subject: "Şifre!!");
                appUser.ID = yakalanan.ID;
                appUser.Password = DantexCrypt.Crypt(yakalanan.Password.ToString());
                appUser.Profile = userProfile;
                appUser.UserName = yakalanan.UserName;
                appUser.Active = yakalanan.Active;
                
                _apRep.Update(appUser);
                return RedirectToAction("Login");
            }
            else 
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return RedirectToAction("Login");
            }
            
        }

        private ActionResult AktifKontrol()
        {
            ViewBag.Kullanici = "Lütfen hesabınızı aktif hale getirin";
            return View("Login");
        }
    }
}