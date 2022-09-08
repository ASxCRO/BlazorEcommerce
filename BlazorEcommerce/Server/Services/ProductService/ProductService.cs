
namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Include(x => x.Variants)
                .Where(p=>p.Featured)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(x => x.Variants)
                .ThenInclude(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

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
                Data = await _context.Products
                .Include(x=>x.Variants)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>> 
            { 
                Data= await _context.Products
                .Where(p=>p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .Include(x => x.Variants)
                .ToListAsync() 
            };

            return response;

        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _context.Products
                .Where(p => p.Title.ToLower().Contains(searchText))
                .Select(p=>p.Title)
                .ToListAsync();


            var response = new ServiceResponse<List<string>>
            {
                Data = result
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> SearchProduct(string searchText)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p=>p.Title.ToLower().Contains(searchText) 
                || p.Description.ToLower().Contains(searchText))
                .Include(p=>p.Variants)
                .ToListAsync()
            };

            return response;
        }
    }
}
