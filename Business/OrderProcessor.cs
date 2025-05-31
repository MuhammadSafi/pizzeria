using pizzeria.Entity;

namespace pizzeria.Business
{
    public static class OrderProcessor
    {
        public static void PrintOrderSummaries(List<Order> orders, List<Product> products, List<ProductIngredient> ingredients,string invalidOrders)
        {
            // Convert  list to dictionary for fast lookup
            var groupedOrders = orders.GroupBy(o => o.OrderId);
            var productDict = products.ToDictionary(p => p.ProductId, p => p);
            var ingredientDict = ingredients.ToDictionary(p => p.ProductId, p => p.Ingredients);

            Console.WriteLine($" -------------------------- INVALID ORDER -------------------------------\n");
            Console.WriteLine($" /////////////////////////////////////////////////////////////////////////\n");
            Console.WriteLine($"{invalidOrders}\n");
            Console.WriteLine($" /////////////////////////////////////////////////////////////////////////\n");
            Console.WriteLine($"--------------------------END---------------------------------------------\n");

            Console.WriteLine($"----------------------VALID ORDER SUMMARY---------------------------------\n");

            foreach (var orderGroup in groupedOrders)
            {
                decimal totalPrice = 0;
                decimal sumofIngredients = 0;
                foreach (var item in orderGroup)
                {
                    var product = productDict[item.ProductId];
                    var ingrdients = ingredientDict[item.ProductId];
                    totalPrice += product.Price * item.Quantity;
                    sumofIngredients += ingrdients.Sum(a => a.Amount * item.Quantity); // if Product Quantity is more than 1 than ingredients amount  will be X by Product quantity

                }
                
                Console.WriteLine($" - Order ID: {orderGroup.Select(a => a.OrderId).FirstOrDefault()},Total Price:{totalPrice}");
                Console.WriteLine($" - Order ID: {orderGroup.Select(a => a.OrderId).FirstOrDefault()},Ingredient Required:{sumofIngredients}");
                
            }

            Console.WriteLine($"\n--------------------------END---------------------------------------------\n");
        }
    }
}
