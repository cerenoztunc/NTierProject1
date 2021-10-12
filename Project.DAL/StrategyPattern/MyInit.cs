﻿using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    //Bogus kütüphanesi bize hazır Data sunar
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        { 
            AppUser au = new AppUser();
            au.UserName = "crn";
            au.Password = DantexCrypt.Crypt("123");
            au.Email = "n.ceren097@gmail.com";
            au.Role = ENTITIES.Enums.UserRole.Admin;
            context.AppUsers.Add(au);
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();
                context.AppUsers.Add(ap);
            }
            context.SaveChanges();
            for (int i = 2; i < 12; i++)
            {
                UserProfile up = new UserProfile();
                up.ID = i; //Birebir ilişki olduğundan üst tarafta olusturulan AppUserların ID'leri ile burası eşleşmeli. Bu yüzden döngünün iterasyonunu 2'den başlattık
                up.FirstName = new Name("tr").FirstName();
                up.LastName = new Name("tr").LastName();
                context.Profiles.Add(up);
            }
            context.SaveChanges();
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(10);
                for (int j = 0; j < 30; j++)
                {
                    Product p = new Product();
                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.UnitInStock = 100;
                    p.ImagePath = new Images().Nightlife();
                    c.Products.Add(p);
                }
                context.Categories.Add(c);
                context.SaveChanges();


            }

        }

    }
}
