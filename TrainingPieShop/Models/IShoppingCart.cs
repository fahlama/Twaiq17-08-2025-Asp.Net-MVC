namespace TrainingPieShop.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Pie pie);
        int RemoveFromCart(Pie pie);
        void ClearCart();
        List<ShoppingCartItem> GetShoppingCartItems();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
        decimal GetShoppingCartTotal();
    }
}
