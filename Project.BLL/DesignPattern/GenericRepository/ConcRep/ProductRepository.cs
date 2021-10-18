using Project.BLL.DesignPattern.GenericRepository.BaseRep;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPattern.GenericRepository.ConcRep
{
    public class ProductRepository:BaseRepository<Product>
    {
        public ProductRepository()
        {

        }
        //public override void Update(Product item)
        //{
        //    Product toBeUpdated = Find(item.ID);
        //    item.ImagePath = toBeUpdated.ImagePath;
        //    base.Update(item);
        //}
    }
}
