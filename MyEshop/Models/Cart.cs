namespace MyEshop.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public int OrderId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public void addItem(CartItem item)
        {
            if (CartItems.Exists(i => i.Id == item.Id))
            {
                CartItems.Find(i => i.Item.Id == item.Item.Id).Quantity += 1;
            }
            else
            {
                CartItems.Add(item);
            }


        }

        public void removeItem(int id)
        {
            var item = this.CartItems.Where(i => i.Item.Id == id).FirstOrDefault();

            if (item.Quantity <= 1)
            {
                CartItems.Remove(item);
            }
            else
            {
                item.Quantity -= 1;
            }
        }
    }
}
