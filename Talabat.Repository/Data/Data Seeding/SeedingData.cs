using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Data_Seeding
{
    public static class SeedingData
    {
        public async static Task SeedData(StoreContext context)
        {
            if (context.DeliveryMethods.Count() == 0)
            {
                var dm = File.ReadAllText("../Talabat.Repository/Data/Data Seeding/delivery.json");
                var dmstring = JsonSerializer.Deserialize<List<DeliveryMethod>>(dm);
                if (dmstring.Count() > 0)
                {
                    foreach (var item in dmstring)
                    {
                        context.DeliveryMethods.Add(item); // more clear and best in performance
                        //context.Set<DeliveryMethod>().Add(item); // more flexibelity while u need code more generic
                    }
                    await context.SaveChangesAsync();
                }
            }
            if(context.Brand.Count() == 0) 
            {
            var BrandData = File.ReadAllText("../Talabat.Repository/Data/Data Seeding/brands.json");
            var brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (brand?.Count() > 0)
                {
                    brand = brand.Select(p => new ProductBrand() { Name = p.Name }).ToList();
                    foreach (var item in brand)
                    {
                        context.Set<ProductBrand>().Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.Category.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../Talabat.Repository/Data/Data Seeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count() > 0)
                {
                    categories = categories.Select(p => new ProductCategory() { Name = p.Name }).ToList();
                    foreach (var item in categories)
                    {
                        context.Set<ProductCategory>().Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.product.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/Data Seeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count() > 0)
                {
                 
                    foreach (var item in products)
                    {
                        context.Set<Product>().Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
