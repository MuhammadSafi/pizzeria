using pizzeria.Entity;

namespace pizzeria.Validation
{
    public static class ValidatePizzeriaOrder
    {
        public static bool IsValidOrder(Order order, Dictionary<string, Product> productDict)
        {
            if (string.IsNullOrWhiteSpace(order.OrderId)) return false;
            if (!productDict.ContainsKey(order.ProductId)) return false;
            if (order.Quantity <= 0) return false;
            if (string.IsNullOrWhiteSpace(order.DeliveryAddress)) return false;
            if (order.CreatedAt == new DateTime()) return false;
            if (order.DeliveryAt == new DateTime()) return false;
            if (order.DeliveryAt < order.CreatedAt) return false;
            return true;
        }
        public static bool IsValidProduct(Product product, Dictionary<string, List<Ingredient>> ingredientDict)
        {
            if (string.IsNullOrWhiteSpace(product.ProductId)) return false;
            if (string.IsNullOrWhiteSpace(product.ProductName)) return false;
            if (product.Price <= 0) return false;
            if (!ingredientDict.ContainsKey(product.ProductId)) return false;

            return true;
        }
        public static bool IsValidIngredient(Ingredient ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient.Name)) return false;
            if (ingredient.Amount <= 0) return false;
            return true;
        }
    }
}
