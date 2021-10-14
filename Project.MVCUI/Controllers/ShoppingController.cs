using PagedList;
using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;
        public ShoppingController()
        {
            _odRep = new OrderDetailRepository();
            _oRep = new OrderRepository();
            _cRep = new CategoryRepository();
            _pRep = new ProductRepository();
        }
        public ActionResult ShoppingList(int? page, int? categoryID) //nullable int verilmesinin sebebi aslında buradaki int'in kacıncı sayfada olundugunu temsil edecek olmasıdır. Ancak birisi direkt alışveriş sayfasına ulastıgında hangi sayfada oldugu verisi alamayacağından dolayı bu sekilde de (yani sayfa numarası gönderilmeden de) bu action'ın calısabilmesini istiyoruz
        {
            PaginationVM pavm = new PaginationVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page??1,9):_pRep.Where(x=>x.CategoryID == categoryID).ToPagedList(page??1,9),
                Categories = _cRep.GetActives()
            };
            if (categoryID != null) TempData["catID"] = categoryID;

            return View();
        }
    }
}