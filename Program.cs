using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanShoppingCart
{
    internal class Program
    {
        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public Product(string name, double price)
            {
                Name = name;
                Price = price;
            }
        }

        public class Category
        {
            public string Name { get; set; }

            public List<Product> products;

            public Category(string name)
            {
                Name = name;
                products = new List<Product>();
            }  

            public void AddProduct(Product product)
            {
                products.Add(product);
            }

            public void RemoveProduct(Product product)
            {
                if (products.Contains(product))
                {
                    products.Remove(product);
                    Console.WriteLine($"Product has been removed: ({product.Name})");
                }
                else
                {
                    Console.WriteLine($"Product is not found: ({product.Name})");
                }
            }

            public List<Product> GetProducts()
            {
                return products;
            }
        }

        public class ShoppingCart
        {
            public List<Product> products;
            public List<Category> categories;

            public ShoppingCart()
            {
                products = new List<Product>();
                categories = new List<Category>();
            }

            public void AddProduct(Product product)
            {
                products.Add(product);
                Console.WriteLine($"Product added: {product.Name}");
            }

            public void AddCategory(Category category)
            {
                categories.Add(category);
                Console.WriteLine($"Category added: {category.Name}");
            }

            public List<Category> GetCategories()
            {
                return categories;
            }

            public void RemoveProduct(Product product)
            {
                if (products.Contains(product))
                {
                    products.Remove(product);
                    Console.WriteLine($"Product has been removed: ({product.Name})");
                }
                else
                {
                    Console.WriteLine($"Product is not found: ({product.Name})");
                }
            }

            public double GetTotalCost()
            {
                double total = 0;
                foreach (var product in products)
                {
                    total += product.Price;
                }

                return total;
            }
        }

        public static void Main(string[] args)
        {
            // Lists for products, categories, cart
            List<Product> products = new List<Product>();
            List<Category> categories = new List<Category>();
            ShoppingCart cart = new ShoppingCart();

            // New Products
            Product Crewneck = new Product(" Crewneck T-shirt", 50);
            Product V_neck =  new Product("V-neck T-shirt", 55);
            Product Polo = new Product("Polo shirt", 30);
            Product Graphic = new Product("Graphic tee", 45);

            Product Adidas = new Product("Adidas sneaker", 64);
            Product Nike = new Product("Nike", 80);

            // Adding products to products list
            products.Add(Crewneck);
            products.Add(V_neck);
            products.Add(Polo);
            products.Add(Graphic);

            products.Add(Adidas);
            products.Add(Nike);

            // Adding product to the cart
            cart.AddProduct(Crewneck);

            // New categories
            Category Clothing = new Category("Clothing");
            Category Shoes = new Category("Shoes");

            // Adding product to the category
            Clothing.AddProduct(Crewneck);
            Clothing.AddProduct(V_neck);
            Clothing.AddProduct(Polo); 
            Clothing.AddProduct(Graphic);

            Shoes.AddProduct(Adidas);
            Shoes.AddProduct(Nike);

            // Adding category to the categories lis
            categories.Add(Clothing);
            categories.Add(Shoes);

            int numberOfProducts = products.Count;

            // Menu
            int currentProduct, currentCategory, i;
            string option =  "";
            while (option != "Exit") 
            {
                Console.WriteLine("1. To List Categories, Enter Keyword: Categories");
                Console.WriteLine("2. To List All Products, Enter Keyword: Products");
                Console.WriteLine("3. To List all Items in my Shopping Cart, Enter Keyword: Cart\n");

                Console.Write("> ");
                option = Console.ReadLine();
                
                switch (option)
                {
                    // Categories
                    case "Categories":
                        Console.WriteLine("Categories section was selected.");

                        while (option != "Menu" && option != "Back")
                        {
                            // Listing the categories
                            i = 1;
                            foreach (var category in categories)
                            {
                                Console.WriteLine($"{i++}. {category.Name}");
                            }
                            Console.WriteLine("\nEnter a category number to list products that it contains:");
                            Console.Write("> ");

                            try
                            {
                                option = Console.ReadLine();
                                currentCategory = Convert.ToInt32(option);

                                if (currentCategory>0 && currentCategory <= categories.Count)
                                {
                                    // Category products section
                                    while (option != "Menu" && option != "Back")
                                    {
                                        Console.WriteLine($"{categories[currentCategory - 1].Name} Category.\n");
                                        //Looking for the category
                                        for (i = 0; i < categories.Count; i++)
                                        {
                                            if (i + 1 == currentCategory)
                                            {
                                                //listing the products in the category
                                                i = 1;
                                                foreach (var product in categories[currentCategory - 1].GetProducts())
                                                {
                                                    Console.WriteLine($"{i++}. {product.Name}");
                                                }
                                                Console.Write("\n");
                                                break;
                                            }
                                        }

                                        Console.WriteLine("Enter a product number to add it into shopping cart:");

                                        try
                                        {
                                            Console.Write("> ");
                                            option = Console.ReadLine();
                                            currentProduct = Convert.ToInt16(option);

                                            if (currentProduct>0 && currentProduct <= categories[currentCategory - 1].GetProducts().Count)
                                            {
                                                cart.AddProduct(categories[currentCategory - 1].GetProducts()[currentProduct - 1]);
                                                //Console.WriteLine($"{categories[currentCategory - 1].GetProducts()[currentProduct - 1].Name} has been added into your shopping cart.");
                                                while (option != "Menu" && option != "Back")
                                                {
                                                    Console.WriteLine("Your shopping Cart List:\n");
                                                    i = 1;
                                                    foreach (var product in cart.products)
                                                    {
                                                        Console.WriteLine($"{i++}. {product.Name} - ${product.Price}.");
                                                    }

                                                    Console.WriteLine($"\nTotal cost: {cart.GetTotalCost()}$\n");
                                                    Console.WriteLine("Enter keyword “Menu” to go to Main Menu");
                                                    Console.WriteLine("Enter keyword “Remove” to remove a product from shopping cart ");

                                                    Console.Write("> ");
                                                    option = Console.ReadLine();
                                                    if (option == "Menu")
                                                        break;
                                                    else if (option == "Remove")
                                                    {
                                                        while (option != "Menu" && option != "Back")
                                                        {
                                                            try
                                                            {
                                                                Console.WriteLine("Enter keyword id of a product.");

                                                                Console.Write("> ");
                                                                option = Console.ReadLine();
                                                                currentProduct = Convert.ToInt16(option);

                                                                if (currentProduct > 0 && currentProduct <= cart.products.Count)
                                                                {
                                                                    i = 1;
                                                                    foreach (var product in cart.products)
                                                                    {
                                                                        if (currentProduct == i)
                                                                        {
                                                                            cart.RemoveProduct(product);
                                                                            Console.WriteLine("Product is removed");
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                    Console.WriteLine("Invalid Command.\n");
                                                            }
                                                            catch
                                                            {
                                                                switch (option)
                                                                {
                                                                    case "Menu":
                                                                        break;
                                                                    case "Back":
                                                                        break;
                                                                    default:
                                                                        Console.WriteLine("Invalid Command.\n");
                                                                        break;
                                                                }
                                                            }
                                                        }
                                                        if (option == "Back")
                                                            option = "";
                                                    }
                                                    else if (option == "Back")
                                                        ;
                                                    else
                                                        Console.WriteLine("Invalid Command.\n");
                                                }
                                                if (option == "Back")
                                                    option = "";
                                            }
                                            else
                                                Console.WriteLine("Product number out of range.\n");
                                        }
                                        catch
                                        {
                                            switch (option)
                                            {
                                                case "Menu":
                                                    break;
                                                case "Back":
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid Command.\n");
                                                    break;
                                            }
                                        }
                                    }
                                    if (option =="Back")
                                        option = "";
                                }
                                else
                                    Console.WriteLine("Category number out of range.\n");

                            }
                            catch
                            {
                                switch (option) 
                                {
                                    case "Menu":
                                        break;
                                    case "Back":
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Command.\n");
                                        break;
                                }
                            }
                        }
                        break;

                    case "Products":
                        while (option != "Menu" && option != "Back")
                        {
                            Console.WriteLine("Products Section:\n");

                            i = 1;
                            foreach (var product in products)
                            {
                                Console.WriteLine($"{i++}. {product.Name} - Price ${product.Price}");
                            }

                            Console.WriteLine("Enter a product number to add it into shopping cart:");
                            try
                            {
                                Console.Write("> ");
                                option = Console.ReadLine();
                                currentProduct = Convert.ToInt16(option);

                                while (option != "Menu" && option != "Back")
                                {
                                    if (currentProduct > 0 && currentProduct <= products.Count)
                                    {
                                        cart.AddProduct(products[currentProduct-1]);

                                        while (option != "Menu" && option != "Back")
                                        {
                                            Console.WriteLine("");
                                        }
                                    }
                                    else
                                        Console.WriteLine("Product number out of range.\n");
                                }
                                if (option == "Back")
                                    option = "";

                            }
                            catch
                            {
                                switch (option)
                                {
                                    case "Back":
                                        break;
                                    case "Menu":
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Command.\n");
                                        break;
                                }
                            }
                        }
                        if (option == "Back")
                            option = "";
                        break;
                    case "Cart":
                        Console.WriteLine("Your shopping Cart List:");

                        i = 1;
                        foreach (var product in cart.products)
                        { 
                            Console.WriteLine($"{i++}. {product.Name} - Price {product.Price}$");
                        }
                        Console.WriteLine($"Total cost: {cart.GetTotalCost()}$\n");
                        Console.WriteLine("Enter keyword, “Menu” to go to Main Menu");
                        Console.WriteLine("Enter keyword Remove, to remove a product from shopping cart ");
                        Console.Write("> ");
                        option = Console.ReadLine();
                        if (option == "Menu")
                            break;
                        else if (option == "Remove") 
                        {
                            Console.WriteLine("Enter keyword id of a product.");
                        }
                        else if (option == "Exit")
                        {
                            Environment.Exit(0);
                        }
                        else
                            Console.WriteLine("Invalid Command!\n");
                        Console.Write("\n");
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }

            //Console.WriteLine($"Total - {cart.GetTotalCost()}");
            //Console.WriteLine($"3rd product {products[2].Name} ");
        }
    }
}
