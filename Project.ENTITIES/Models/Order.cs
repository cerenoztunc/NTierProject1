using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order:BaseEntity
    {
        public string ShippedAddress { get; set; }
        public decimal TotalPrice { get; set; } //Sipariş işlemlerinin içerisindeki bilgileri daha rahat yakalamak için açılan proplardan bir tanesidir. Bu prop spesifik bir parça olarak bize hız kazandıracak bir durum yaratacaktır. 
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? AppUserID { get; set; }

        //Relational prop
        public virtual AppUser AppUser { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
