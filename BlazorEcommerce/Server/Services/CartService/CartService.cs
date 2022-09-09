namespace BlazorEcommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>()
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .Include(p => p.Variants)
                    .ThenInclude(p => p.ProductType)
                    .FirstOrDefaultAsync();

                if(product is not null)
                {
                    if (product.Variants is not null && product.Variants.Count > 0)
                    {
                        var productVariant = product.Variants.FirstOrDefault(p => p.ProductTypeId == item.ProductTypeId);

                        if(productVariant is not null)
                        {
                            var cartProduct = new CartProductResponse
                            {
                                ProductId = product.Id,
                                Title = product.Title,
                                ImageUrl = product.ImageUrl,
                                Price = productVariant.Price,
                                ProductType = productVariant.ProductType.Name,
                                ProductTypeId = productVariant.ProductTypeId,
                                Quantity = item.Quantity
                            };

                            result.Data.Add(cartProduct);
                        }
                    }
                }
            }

            return result;
        }
    }
}
