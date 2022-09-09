using BlazorEcommerce.Shared;
using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public async Task AddToCart(CartItem cartItem)
        {
            List<CartItem>? cart = await GetCart();

            if(!cart.Any(p=>p.ProductId == cartItem.ProductId && p.ProductTypeId == cartItem.ProductTypeId))
            {
                cartItem.Quantity = 1;
                cart.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            await _localStorage.SetItemAsync("cart", cart);

            OnChange.Invoke();
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

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);

            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();

            return cartProducts.Data;
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            var cart = await GetCart();

            if(cart is null)
            {
                return;
            }

            var cartItem = cart
                .Find(p => p.ProductId == productId && p.ProductTypeId == productTypeId);

            if(cartItem is not null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync<List<CartItem>>("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQuantity(CartProductResponse product)
        {
            var cart = await GetCart();

            if (cart is null)
            {
                return;
            }

            var cartItem = cart
                .Find(p => p.ProductId == product.ProductId && p.ProductTypeId == product.ProductTypeId);

            if (cartItem is not null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync<List<CartItem>>("cart", cart);
            }
        }
    }
}
