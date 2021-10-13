using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository _apRep;
        ProfileRepository _proRep;
        public RegisterController()
        {
            _apRep = new AppUserRepository();
            _proRep = new ProfileRepository();
        }

        

        public ActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(AppUser appUser, UserProfile userProfile)
        {
            

            appUser.Password = DantexCrypt.Crypt(appUser.Password);

            if(_apRep.Any(x=>x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmış";
                return View();
            }
            else if(_apRep.Any(x=>x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Email zaten kayıtlı";
                return View();
            }
            string gonderilecekEmail = "Tebrikler..Hesabınız oluşturulmuştur..Hesabınızı aktive etmek için https://localhost:44375/Register/Activation/"+appUser.ActivationCode+" linkine tıklayabilirsiniz";

            MailService.Send(appUser.Email, body: gonderilecekEmail, subject: "Hesap aktivasyon!!");
            _apRep.Add(appUser); //öncelikle bu eklenmeli.cünkü AppUser'ın ID'si il başta oluşmalı.Cünkü bizim kurduğumuz birebir ilişkide APpUser zorunlu olan Profile ise obsiyonel alandır. dolayısıyla ilk başta AppUser'ın ID'si savechanges ile olusmalı ki sonra Profile rahatca eklenebilsin

            if(!string.IsNullOrEmpty(userProfile.FirstName.Trim()) || !string.IsNullOrEmpty(userProfile.LastName.Trim()))
            {
                userProfile.ID = appUser.ID;
                _proRep.Add(userProfile);
            }
            return View("RegisterOK");
        }

        public ActionResult Activation(Guid id)
        {
            AppUser aktifEdilecek = _apRep.FirstOrDefault(x => x.ActivationCode == id);
            if(aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _apRep.Update(aktifEdilecek);
                TempData["HesapAktifMi"] = "Hesabınız aktif hale getirildi";
                return RedirectToAction("Login", "Home");
            }
            TempData["HesapAktifMi"] = "Hesabınız bulunamadı";
            return RedirectToAction("Login", "Home");
        }
        public ActionResult RegisterOK()
        {
            return View();
        }
    }
}