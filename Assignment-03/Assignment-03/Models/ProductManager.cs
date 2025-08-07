using Assignment_03.Factories;
using Assignment_03.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_03.Models
{
    public class ProductManager : IProductRepository
    {
        // Inventory
        public static List<Product> products = new List<Product>() { };
        public ProductManager() { 
        
        generateProducts();
        }
        public List<Product> GetByCategory(string category)
        {
            List<Product> filtered=products.Where(product=>product.GetProduct().getType()==category).ToList();
            return filtered;

        }
        public List<Product> GetProductByID(string id) {
            List<Product> filtered = products.Where(product => product.GetProduct().getID() == id).ToList();
            return filtered;
        }
        public List<Product> GetAllProducts(string category)
        {
            
            return products;

        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            List<Product> filtered = products.Where(product => product.GetProduct().getQuantity() <=threshold).ToList();
            return filtered;
        }
        void generateProducts()
        {
            for (int i = 0; i < 5; i++)
            {
                Product product_1 = new Clothing("dress shirt");
                Product product_2 = new Clothing("coat");
                Product product_3 = new Electronics("fridge");
                Product product_4 = new Electronics("computer");
                Product product_5 = new Electronics("air conditioner");
                Product product_6 = new HomeGarden("apple garden");
                Product product_7 = new HomeGarden("mango garden");
                Product product_8 = new Book("math book");
                Product product_9 = new Book("english book");
                products.Add(product_1);
                products.Add(product_2);
                products.Add(product_3);
                products.Add(product_4);
                products.Add(product_5);
                products.Add(product_6);
                products.Add(product_7);
                products.Add(product_8);
                products.Add(product_9);


            }
        }



    }


}
