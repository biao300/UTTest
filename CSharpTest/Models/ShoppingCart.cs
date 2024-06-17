using CSharpTest.Helper;
using UTCSharpTest.Helper;

namespace UTCSharpTest.Models
{
    public class DuplicateProductException: Exception
    {
        
    }

    // Question 2
    public class ShoppingCart
    {
        private List<Product> Products { get; set; }

        public ShoppingCart()
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
        }

        public List<Product> AddProduct(int id, string name, double price)
        {
            Product? p = Products.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                Products.Add(new Product()
                {
                    Id = id,
                    Name = name,
                    Price = Math.Round(price, 2)
                });
                return Products;
            }
            else
            {
                Console.WriteLine($"Product id={id} already exists in shopping cart.");
                // Question 3
                throw new DuplicateProductException();
            }
        }

        public List<Product> RemoveProduct(int id)
        {
            Product? p = Products.FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                Products.Remove(p);
            }
            else
            {
                Console.WriteLine("Product id not found, nothing to remove.");
            }
            return Products;
        }

        public List<Product> DisplayAllProducts()
        {
            return Products;
        }

        // Question 4
        public double GetTotalPrice()
        {
            double ret = 0.0d;
            foreach (Product p in Products)
            {
                ret += p.Price;
            }
            return Math.Round(ret, 2);
        }

        // Question 6
        public List<Product> FliterProductByName(string name)
        {
            return Products.FindAll(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Product> FliterProductByPrice(float minPrice, float maxPrice)
        {
            if (maxPrice >= minPrice)
            {
                return Products.FindAll(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            else
            {
                Console.WriteLine("maximum price must larger than minimum price");
                return new List<Product>();
            }
        }

        // Question 8
        public void ExportToFile()
        {
            string fileName = "ShoppingCart.csv";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                fi.Delete();
            }

            // header
            Utility.WriteToFile(fileName, "Id,Name,Price");

            foreach (Product p in Products)
            {
                Utility.WriteToFile(fileName, $"\"{p.Id}\",\"{p.Name}\",\"{p.Price.ToString("0.00")}\"");
            }
        }

        public List<Product> ImportFromFile()
        {
            string fileName = "ShoppingCart.csv";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                Products.Clear();

                using (StreamReader sr = new StreamReader(fileName))
                {
                    int line_num = 0;
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine();
                        if (line != null && line_num > 0)
                        {
                            var cols = Utility.ProcessLine(line, ',');
                            try
                            {
                                AddProduct(int.Parse(cols[0]), cols[1], float.Parse(cols[2]));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"add line {line} failed, reason: ", e);
                            }
                        }
                        line_num++;
                    }
                }
            }
            else
            {
                Console.WriteLine("file not found!");
            }

            return Products;
        }

        // Question 10
        public List<Product> DiscountProductPercentage(int id, double percentage)
        {
            Product? p = Products.FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                p.Price = DiscountManager.ApplyDiscountPercentage(p.Price, percentage);
            }
            else
            {
                Console.WriteLine("product not found!");
            }
            return Products;
        }

        public List<Product> DiscountProductFixed(int id, double amount)
        {
            Product? p = Products.FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                p.Price = DiscountManager.ApplyDiscountFixed(p.Price, amount);
            }
            else
            {
                Console.WriteLine("product not found!");
            }
            return Products;
        }
    }
}
