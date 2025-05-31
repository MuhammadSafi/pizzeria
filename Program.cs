using pizzeria.Business;
using pizzeria.Entity;
using pizzeria.Validation;
using System.Text;

var orderValidator = new OrderValidator();
var JValidator = new JsonValidator();
var validProducts = new List<Product>();
var validIngredients = new List<ProductIngredient>();
var sb = new StringBuilder();

try
{
    //Load Json Object 
    var orderJson = JsonLoader.ReadJson(FilePath.OrderPath);
    var productJson = JsonLoader.ReadJson(FilePath.ProductPath);
    var ingdJson = JsonLoader.ReadJson(FilePath.IngredientsPath);
    
    // Validate Order Json Object
    var orderError = JValidator.IsValidJsonFormat(orderJson);
    var productError = JValidator.IsValidJsonFormat(productJson);
    var ingdError = JValidator.IsValidJsonFormat(ingdJson);

    if (orderError.Count() == 0 && productError.Count() == 0 && ingdError.Count() == 0)
    {
        // Load JSON data
        var orders = JsonLoader.LoadFromJson<Order>(FilePath.OrderPath);
        var products = JsonLoader.LoadFromJson<Product>(FilePath.ProductPath);
        var ingredients = JsonLoader.LoadFromJson<ProductIngredient>(FilePath.IngredientsPath);

        //Convert Dictionary to List for faster lookup
        var ingredientDict = ingredients.ToDictionary(p => p.ProductId, p => p.Ingredients);

        //Checking if order list is not null
        if (orders == null || !orders.Any())
        {
            Console.WriteLine("No orders found in file.");
            return;
        }

        // Get order with Invalid Order Id
        var refineOrders = orderValidator.GetValidOrder(orders, products);

        if (refineOrders.orders?.Count == 0)
        {
            Console.WriteLine("No valid order exists.");
            return;
        }

        //Get Valid Product associated with Order
        foreach (var product in products)
        {
            if (ValidatePizzeriaOrder.IsValidProduct(product, ingredientDict))
            {
                validProducts.Add(product);
            }
        }
        if (validProducts.Count == 0)
        {
            Console.WriteLine("No valid product exists to proceed with the order.");
            return;
        }

        //Validating Ingredients amount if it all have valid values 
        foreach (var ingredient in ingredients)
        {
            foreach (var item in ingredient.Ingredients)
            {
                if (!ValidatePizzeriaOrder.IsValidIngredient(item))
                {
                    Console.WriteLine($"Product: {ingredient.ProductId} of the ingredient: '{item.Name}' doesnot have valid value: '{item.Amount}.'");
                    return;
                }
            }
        }

        //Processing Order it it has all valid oders ,products and ingredients
        OrderProcessor.PrintOrderSummaries(refineOrders?.orders, validProducts, ingredients, refineOrders?.InvalidOrderId.ToString());
    }
    else
    {
        //Can display the error return from IsValidJsonFormat for each order, products, Ingredients
        Console.WriteLine("Error in Json File");
    }

}
catch (Exception ex)
{
    Console.WriteLine($"Error processing file: {ex.Message}");
}