
namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this products does not exist.";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }


            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductListAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>> 
            { 
                Data= await _context.Products.Where(p=>p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync() 
            };

            return response;

        }
    }
}
