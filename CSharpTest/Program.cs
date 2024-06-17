// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using UTCSharpTest.Models;

Console.WriteLine($@"Command list: (use semicolon ';' to separate command and parameters, use comma ',' to separate multiple parameters)
  show shopping cart                              - display all products in shopping cart
  add shoppingcart; id,name,price                 - add one product into shopping cart
  remove shopping cart; product id                - remove one product from shopping cart
  sum shopping cart                               - get total price from shopping cart
  filter shopping cart; product name              - search product in shopping cart by name
  filter shopping cart price; mix price,max price - filter product in shopping cart by price range
  save shopping cart                              - export shopping cart into file, located in bin/Debug/net6.0/ShoppingCart.csv or bin/Release/net6.0/ShoppingCart.csv
  load shopping cart                              - import shopping cart from file, if existing
  discount shopping cart; product id, amount(%)   - with % or not will apply different type discount
  
  add order; id,name,price,quantity   - add one product into order
  show order                          - display all products in order
  sum order                           - get total price from order

Please input command:
");


void OutputFormattedJSON(object? obj)
{
    Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented) + "\n\n");
}

ShoppingCart cart = new ShoppingCart();
Order order = new Order();

// Question 9
while (true)
{ 
    string? command = Console.ReadLine();

    if (command != null)
    {
        string[] commands = command.Trim().Split(";");

        string[] paramLine;

        switch (commands[0])
        {
            case "show shoppingcart":
            case "show shopping cart":
                OutputFormattedJSON(cart.DisplayAllProducts());
                break;

            case "add shoppingcart":
            case "add shopping cart":
                paramLine = commands[1].Split(",");
                try 
                {
                    cart.AddProduct(int.Parse(paramLine[0].Trim()), paramLine[1].Trim(), double.Parse(paramLine[2].Trim()));
                    Console.WriteLine("success");
                }
                catch (Exception e)
                {
                    string msg = "";
                    if (e.Message.Contains("DuplicateProductException"))
                    {
                        msg += " duplicated product ID !";
                    }
                    else
                    {
                        msg += " invalid ID or price !";
                    }
                    Console.WriteLine($"failed, reason: {msg}");
                }
                Console.WriteLine("Products in shopping cart:");
                OutputFormattedJSON(cart.DisplayAllProducts());
                break;

            case "remove shoppingcart":
            case "remove shopping cart":
                try
                {
                    cart.RemoveProduct(int.Parse(commands[1].Trim()));
                    Console.WriteLine("success");
                }
                catch
                {
                    Console.WriteLine($"failed, reason: invalid ID");
                }
                OutputFormattedJSON(cart.DisplayAllProducts());
                break;

            case "sum shoppingcart":
            case "sum shopping cart":
                Console.WriteLine($"total price in shopping cart: {cart.GetTotalPrice().ToString("0.00")}");
                break;

            case "filter shoppingcart":
            case "filter shopping cart":
                OutputFormattedJSON(cart.FliterProductByName(commands[1].Trim()));
                break;

            case "filter shoppingcart price":
            case "filter shopping cart price":
                paramLine = commands[1].Split(",");
                try
                {
                    OutputFormattedJSON(cart.FliterProductByPrice(int.Parse(paramLine[0].Trim()), int.Parse(paramLine[1].Trim())));
                }
                catch 
                {
                    Console.WriteLine($"failed, reason: invalid price");
                }
                break;

            case "export shoppingcart":
            case "export shopping cart":
            case "save shoppingcart":
            case "save shopping cart":
                try
                {
                    cart.ExportToFile();
                    Console.WriteLine("ShoppingCart.csv file saved");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"export file error: {e}");
                }
                break;

            case "import shoppingcart":
            case "import shopping cart":
            case "load shoppingcart":
            case "load shopping cart":
                OutputFormattedJSON(cart.ImportFromFile());
                break;

            case "discount shoppingcart":
            case "discount shopping cart":
                paramLine = commands[1].Split(",");
                try
                {
                    if (paramLine[1].Contains("%"))
                    {
                        OutputFormattedJSON(cart.DiscountProductPercentage(int.Parse(paramLine[0].Trim()), double.Parse(paramLine[1].Trim().Replace("%", ""))));
                    }
                    else
                    {
                        OutputFormattedJSON(cart.DiscountProductFixed(int.Parse(paramLine[0].Trim()), double.Parse(paramLine[1].Trim())));
                    }
                }
                catch
                {
                    Console.WriteLine($"failed, reason: invalid product id / discount amount");
                }
                break;

            case "add order":
                paramLine = commands[1].Split(",");
                try
                {
                    order.AddProduct(new Product()
                    {
                        Id = int.Parse(paramLine[0].Trim()),
                        Name = paramLine[1].Trim(),
                        Price = double.Parse(paramLine[2].Trim()),
                    }, int.Parse(paramLine[3].Trim()));
                    Console.WriteLine("success");
                }
                catch
                {
                    Console.WriteLine("failed, invalid Id/price/quantity");
                }
                Console.WriteLine("Products in order:");
                OutputFormattedJSON(order.DisplayOrderDetails());
                break;

            case "show order":
                OutputFormattedJSON(order.DisplayOrderDetails());
                break;

            case "sum order":
                Console.WriteLine($"total price in order: {order.GetOrderTotalPrice().ToString("0.00")}");
                break;

            default:
                Console.WriteLine("command not found!");
                break;
        }
    }
}
