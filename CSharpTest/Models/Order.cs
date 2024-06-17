namespace UTCSharpTest.Models
{
    public class OrderDetail
    { 
        public Product? product { get; set; }
        public int quantity;
    }

    // Qeustion 7
    public class Order
    {
        private List<OrderDetail> orderDetails;

        public Order()
        {
            orderDetails = new List<OrderDetail>();
        }

        public List<OrderDetail> AddProduct(Product product, int quantity)
        {
            if (product != null)
            {
                orderDetails.Add(new OrderDetail()
                {
                    product = product,
                    quantity = quantity
                });
            }
            return orderDetails;
        }

        public double GetOrderTotalPrice()
        {
            double ret = 0.0;

            foreach (OrderDetail item in orderDetails) 
            {
                if (item.product != null)
                {
                    ret += item.product.Price * item.quantity;
                }
            }

            return Math.Round(ret, 2);
        }


        public List<OrderDetail> DisplayOrderDetails()
        {
            return orderDetails;
        }
    }
}
