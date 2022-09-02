namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "TitleOne",
                    Description = "DescOne",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg/330px-The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg",
                    Price = 9.99m
                },
                new Product
                {
                    Id = 2,
                    Title = "TitleTwo",
                    Description = "TwoDesc",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg/330px-The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg",
                    Price = 9.99m
                },
                new Product
                {
                    Id = 3,
                    Title = "TitleThree",
                    Description = "DescOne",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg/330px-The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg",
                    Price = 9.99m
                }
            );
        }

        public DbSet<Product> Products { get; set; }
    }
}
