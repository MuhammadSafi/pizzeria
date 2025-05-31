using pizzeria.Entity;

namespace pizzeria.Validation
{
    public class RefineOrder
    {
        public List<Order>? orders { get; set; }
        public string? InvalidOrderId { get; set; }
    }
}
