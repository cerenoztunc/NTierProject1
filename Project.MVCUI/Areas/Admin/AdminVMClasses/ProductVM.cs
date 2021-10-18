using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Admin.AdminVMClasses
{
    public class ProductVM //PaginationVM ile neredeyse aynı görevi yapıyor gibi gözükebilir. Ancak çok benzer görevleri yapmalarına rağmen PaginationVM sadece alışveriş tarafında kullanılıcak ve sayfalandırmayı yapacak bir VM iken ProductVM sadece Admin tarafında kullanılması için tasarlanmış bir VM classtır.
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public object ImagePath { get; internal set; }
    }
}