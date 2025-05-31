using pizzeria.Entity;
using System.Text;

namespace pizzeria.Validation
{
    public class OrderValidator
    {
        public RefineOrder GetValidOrder(List<Order> orders, List<Product> products)
        {
            var validOrders = new List<Order>();
            var rf = new RefineOrder();
            var productDict = products.ToDictionary(p => p.ProductId, p => p);
            StringBuilder sb = new StringBuilder();

            foreach (var order in orders)
            {
                if (ValidatePizzeriaOrder.IsValidOrder(order, productDict))
                {
                    validOrders.Add(order);
                }
                else
                {
                    sb.Append($" - OrderId-{order.OrderId}\n");
                }
            }
            rf.orders = validOrders;
            rf.InvalidOrderId = sb.ToString();

            return rf;
        }
    }
}