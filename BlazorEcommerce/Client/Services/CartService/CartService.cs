using BlazorEcommerce.Shared;
using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task AddToCart(CartItem cartItem)
        {
            List<CartItem>? cart = await GetCart();

            cart.Add(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
        }

        public async  Task<List<CartItem>> GetCartItems()
        {
            return await GetCart();
        }

        private async Task<List<CartItem>> GetCart()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                cart = new();
            }

            return cart;
        }
    }
}
