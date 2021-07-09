namespace ACM.BL
{
    public class OrderItem
    {
        public OrderItem()
        {

        }
        public OrderItem(int orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int Quantity { get; set; }

        // Retrieve one order item
        public OrderItem Retrieve(int orderItemId)
        {
            // Code that retrieves the defined order item

            return new OrderItem();
        }

        // Saves the current order item
        public bool Save()
        {
            // Code that saves the defined order item

            return true;
        }

        // Validates the order item data
        public bool Validate()
        {
            var isValid = true;

            if (Quantity < 0) isValid = false;
            if (ProductId < 0) isValid = false;
            if (PurchasePrice == null) isValid = false;

            return isValid;
        }
    }
}
