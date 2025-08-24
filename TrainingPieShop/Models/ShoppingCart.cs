
using Microsoft.EntityFrameworkCore;
using TrainingPieShop.Migrations;

namespace TrainingPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ShopDBContext _dbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public ShoppingCart(ShopDBContext dbContext)
        {
                _dbContext = dbContext;
        }
        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(
                s=>s.Pie.PieId == pie.PieId && s.ShopingCartId== ShoppingCartId
                );
            if(shoppingCartItem==null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShopingCartId = ShoppingCartId,
                    Pie= pie,
                    Amount=1
                };
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }else
            {
                shoppingCartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShoppingCartItems.Where(s => s.ShopingCartId == ShoppingCartId);
            _dbContext.ShoppingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _dbContext.ShoppingCartItems.Where(c => c.ShopingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
           var total=_dbContext.ShoppingCartItems.Where(s=>s.ShopingCartId==ShoppingCartId)
                .Select(s=>s.Pie.Price * s.Amount).Sum();
            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(
                s=>s.Pie.PieId==pie.PieId && s.ShopingCartId == ShoppingCartId
                );
            var localAmount =0;
            if(shoppingCartItem!=null)
            {
                if(shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                    localAmount= shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();
            return localAmount;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            ShopDBContext context = services.GetService<ShopDBContext>() ?? throw new Exception("Error Initialization");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
    }
}
