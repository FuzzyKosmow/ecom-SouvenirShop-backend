using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ecommerce_api.DTO.Order;
using ecommerce_api.Models;
using ecommerce_api.Services.OrderService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Services.JWT
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOrderService _orderService;

        public DatabaseSeeder(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOrderService orderService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _orderService = orderService;
        }

        public async Task EnsureRoles()
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
        public async Task SeedCategory()
        {
            // Seed categories
            if (await _context.Categories.CountAsync() == 0)
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Architecture Wire Replica",
                    Image = "https://cdn11.bigcommerce.com/s-91br/product_images/uploaded_images/banner-wire-models.jpg"
                    ,
                    Description = "",
                    IsPopular = true
                    },
                    new Category { Name = "Books", Image = "https://cdn11.bigcommerce.com/s-91br/images/stencil/250x250/v/book-detroit-then-and-now_1537376206__89874.original.jpg"
                        ,
                        Description = "",
                        IsPopular = true

                    },
                    new Category { Name = "Christmas Ornaments", Image = "https://www.citysouvenirs.com/product_images/uploaded_images/christmas-xmasinjuly2013.jpg"
                    ,
                    Description = "",
                    IsPopular = true
                    },


                    new Category
                    {
                        Name = "Statues & Models",
                        Image = "https://cdn11.bigcommerce.com/s-91br/images/stencil/250x250/v/statue-nyc-statue-of-liberty-top-zoom_1537379066__22886.original.jpg"
                        ,
                        Description = "",
                        IsPopular = false
                    },
                    new Category
                    {
                        Name = "Games & Fun",
                        Image = "https://cdn11.bigcommerce.com/s-91br/images/stencil/250x250/o/puzzle-pisa-3d_1537377032__87525.original.jpg"
                        ,
                        Description = "",
                        IsPopular = false

                    },
                    new Category
                    {
                        Name = "Snow Globes",
                        Image = "https://cdn11.bigcommerce.com/s-91br/images/stencil/250x250/g/snowglobe-philadelphia-45mm-liberty-bell_1537378994__27063.original.jpg"
                        ,
                        Description = "",
                        IsPopular = true
                    },
                    new Category
                    {
                        Name = "Clothing",
                        Image = "https://cdn11.bigcommerce.com/s-91br/images/stencil/250x250/p/shirt-london-union-jack_1537376493__75222.original.jpg"
                        ,
                        Description = "",
                        IsPopular = false
                    },

                };
                await _context.Categories.AddRangeAsync(categories);
            }
            await _context.SaveChangesAsync();
        }
        public async Task SeedProduct()
        {
            // Check if there are any existing products in the database
            if (await _context.Products.CountAsync() == 0)
            {
                // Snow globes
                var snow1 = new Product
                {
                    Name = "New York City Snow Globe",
                    Price = 250000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2541/21727/snowglobe-nyc-skyline-silver-65mm-WG199-1__38961.1588005982.jpg?c=2" },
                    Description = "This New York City snow globe features the Statue of Liberty, Empire State Building, Chrysler Building, Brooklyn Bridge, and more. Measures 65mm. Made of glass and polyresin. These globes contain snow and do not play music.",
                };


                var snow2 = new Product
                {
                    Name = "Vietnam Big Size Snow Globe",
                    Price = 300000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "Vietnam",
                    RelatedCity = "Ho Chi Minh",
                    Images = new List<string> { "https://thumbs.dreamstime.com/z/flag-vietnam-snow-globe-background-128854962.jpg" },
                    Description = "A simple depiction of cultural icons in Vietnam, this snow globe is a great gift for those who love the country. Measures 65mm. Made of glass and polyres",
                    IsPopular = true,
                    IsNewArrival = true
                };
                // Christmas ornaments
                var christmas1 = new Product
                {
                    Name = "Chicago Christmas Wreath Ornaments",
                    Price = 150000,
                    DiscountPrice = 120000,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "Chicago",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/660x660/products/2762/21484/ornament-chicago-wreath-451IL-0205-1__94435.1576034194.jpg?c=2", "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2762/21482/ornament-chicago-wreath-451IL-0205-2__94174.1576034195.jpg?c=2" },
                    Description = "These Chicago Christmas wreath ornaments are a great way to show your love for the Windy City during the holiday season. Made of glass and polyresin. Measures 4.5 inches.",

                    IsBestSeller = true,
                    IsFeatured = true,
                    IsPopular = true,

                };


                var christmas2 = new Product
                {
                    Name = "St. Louis Porcelain Christmas Ornament",

                    Price = 200000,
                    DiscountPrice = 150000,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "St. Louis",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/1416/16040/ornament-saint-louis-arch-porcelain__97847.1475079680.JPG?c=2",
                    "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/1416/17762/ornament-porcelain-side-view__41696__37412.1541115513.JPG?c=2" },
                    Description = "This St. Louis porcelain Christmas ornament features the Gateway Arch and the city skyline. Measures 2.75 inches. Made of porcelain.",

                    ReleaseDate = new DateTime(2023, 8, 5),
                    CreatedAt = DateTime.Now,
                    IsNewArrival = true
                };

                var christmas3 = new Product
                {
                    Name = "Halong Bay Vietnam Christmas Ornament Porcelain",
                    Price = 200000,
                    DiscountPrice = 150000,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "Vietnam",
                    RelatedCity = "Ha Long",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/3198/16192/ornament-vietnam-halong-porcelain__44320.1477943824.jpg?c=2",
                    "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/3198/19586/ornament-porcelain-side-view__34140__06100.1541864792.JPG?c=2" },
                    Description = " This Halong Bay Vietnam Christmas ornament features the Halong Bay and the city skyline. Measures 2.75 inches. Made of porcelain.",

                    ReleaseDate = new DateTime(2023, 8, 5),
                    CreatedAt = DateTime.Now,
                    IsNewArrival = true
                };
                // Wires
                var wire1 = new Product
                {
                    Name = "Golden Gate Bridge Wire Model",
                    Price = 500000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "San Francisco",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/1672/18016/statue-san-francisco-golden-gate-bridge-wire-model__41454__54940.1541921631.jpg?c=2",
                    "https://cdn11.bigcommerce.com/s-91br/images/stencil/original/products/1672/16137/statue-san-francisco-golden-gate-bridge-wire-model-angle-view__20645.1493828658.jpg?c=2" },
                    Description = "This Golden Gate Bridge wire model is a great gift for those who love San Francisco. Measures 10 inches. Made of metal.",
                    IsBestSeller = true,
                    IsFeatured = true,

                };

                var wire2 = new Product
                {
                    Name = "Chrysler Building Metal Photo and Memo Clip",
                    Price = 500000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2942/14919/memoclip-nyc-chrysler-silver__78990.1647274694.jpg?c=2",
                    "https://cdn11.bigcommerce.com/s-91br/images/stencil/original/products/2942/19214/HTB1Iwd5GXXXXXXNXFXXq6xXFXXXm__13288__90841.1541500862.jpg?c=2" },
                    Description = "This Chrysler Building metal photo and memo clip is a great way to display photos and notes. Measures 4 inches. Made of metal.",
                    IsBestSeller = true,
                    IsFeatured = true,

                };


                // Book
                var book1 = new Product
                {
                    Name = "NYC Postcard Booklet - 33 Postcards",
                    Price = 125000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/1753/18074/stationery_nyc_postcards_33_pack__03391__74162.1541340174.JPG?c=2" },
                    Description = "This NYC postcard booklet features 33 postcards of New York City. Made of paper. Measures 4 x 6 inches.",
                    IsBestSeller = true,

                };


                // Statues & Models
                var statue1 = new Product
                {
                    Name = "4 Inch Statue of Liberty Statue Replica",
                    Price = 500000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2519/16424/statue-nyc-liberty-4in-fg071-v2__46294.1496674971.jpg?c=2",
                     },
                    Description = "This 4 inch Statue of Liberty statue replica is a great gift for those who love New York City. Made of polyresin. Measures 4 inches.",
                    IsBestSeller = true,
                    IsFeatured = true,
                    IsNewArrival = true

                };
                var statue2 = new Product
                {
                    Name = "5 Inch Empire State Building Statue",
                    Price = 500000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",

                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2854/24314/statue-empire-silver-fg054-2__55759.1655863591.jpg?c=2",
                    "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2854/24315/statue-empire-silver-fg054__27225.1655863591.jpg?c=2" },
                    Description = "This 5 inch Empire State Building statue is a great gift for those who love New York City. Made of polyresin. Measures 5 inches.",
                    IsBestSeller = true,
                    IsFeatured = true,
                    IsNewArrival = true

                };

                // Games & Fun
                var game1 = new Product
                {
                    Name = "American Flag",
                    Price = 50000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/1593/15949/flag-usa-stick__91469.1475005788.jpg?c=2" },
                    Description = "This American flag is a great way to show your patriotism. Made of polyester. Measures 12 x 18 inches.",
                    IsBestSeller = true,
                    IsFeatured = true,
                };

                var game2 = new Product
                {
                    Name = "Diecast NYC Pullback Taxi",
                    Price = 50000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2771/16393/toy-nyc-taxi-die-cast-pullback-labelled-medallion__12656.1492819711.JPG?c=2" },
                    Description = "This diecast NYC pullback taxi is a great gift for those who love New York City. Made of diecast metal. Measures 5 inches.",
                    IsBestSeller = true,
                    IsNewArrival = true,
                    IsPopular = true,
                };

                // Clothing

                var cloth1 = new Product
                {
                    Name = "FDNY Embroidered Cap Fire Department",
                    Price = 200000,
                    DiscountPrice = 0,
                    Rating = 4.5f,
                    Availability = true,
                    Stock = 50,
                    Country = "USA",
                    RelatedCity = "New York City",
                    Images = new List<string> { "https://cdn11.bigcommerce.com/s-91br/images/stencil/1280x1280/products/2416/24397/hat-nyc-fdny-patch-1__87393.1656241873.jpg?c=2" },
                    Description = "This FDNY embroidered cap is a great way to show your support for the New York City Fire Department. Made of cotton. One size fits most.",
                    IsBestSeller = true,
                    IsFeatured = true,
                    IsPopular = true,
                };

                await _context.Products.AddRangeAsync(new List<Product> { snow1, snow2, christmas1, christmas2, wire1, wire2, book1, statue1, statue2, game1, game2, cloth1 });
                await _context.SaveChangesAsync();

                // Assign categories to products correctly based on var name
                // Snow globes (snow1, snow2) => Christmas Ornaments
                var categorySnowGlobes = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Snow Globes");
                var categoryChristmasOrnaments = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Christmas Ornaments");
                var categoryWires = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Architecture Wire Replica");
                var categoryBooks = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Books");
                var categoryStatuesModels = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Statues & Models");
                var categoryGamesFun = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Games & Fun");
                var categoryClothing = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Clothing");

                if (categorySnowGlobes != null)
                {
                    categorySnowGlobes.Products = new List<Product> { snow1, snow2 };
                }

                if (categoryChristmasOrnaments != null)
                {
                    categoryChristmasOrnaments.Products = new List<Product> { christmas1, christmas2, christmas3 };
                }

                if (categoryWires != null)
                {
                    categoryWires.Products = new List<Product> { wire1, wire2 };
                }

                if (categoryBooks != null)
                {
                    categoryBooks.Products = new List<Product> { book1 };
                }

                if (categoryStatuesModels != null)
                {
                    categoryStatuesModels.Products = new List<Product> { statue1, statue2 };
                }

                if (categoryGamesFun != null)
                {
                    categoryGamesFun.Products = new List<Product> { game1, game2 };
                }

                if (categoryClothing != null)
                {
                    categoryClothing.Products = new List<Product> { cloth1 };
                }

                // Save changes to assign the relationships
                await _context.SaveChangesAsync();
            }

        }
        public async Task SeedUser()
        {
            if (_context.ApplicationUsers.Count() == 0)
            {
                // Add user

                var userSample = new ApplicationUser
                {
                    Email = "user1@gm.com",
                    FullName = "User 1",
                    PhoneNumber = "1234567890",
                    Address = "1234 Main St",
                    UserName = "user1@gm.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "user1@gm.com".ToUpper(),

                };
                var existingUserByEmail = await _userManager.FindByEmailAsync(userSample.Email);
                var existingUserByUsername = await _userManager.FindByNameAsync(userSample.UserName);
                if (existingUserByEmail != null || existingUserByUsername != null)
                {
                    return;
                }
                else
                {
                    var userCreationResult = await _userManager.CreateAsync(userSample, "Password123!");
                    if (!userCreationResult.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", userCreationResult.Errors.Select(e => e.Description)));
                    }

                    await _userManager.AddToRoleAsync(userSample, "User");

                }
                await _context.SaveChangesAsync();


                // Add admin
                var adminSample = new ApplicationUser
                {
                    Email = "admin@admin.com",
                    FullName = "Admin",
                    PhoneNumber = "1234567890",
                    Address = "1234 Main St",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true,
                };
                var existingAdminByEmail = await _userManager.FindByEmailAsync(adminSample.Email);
                var existingAdminByUsername = await _userManager.FindByNameAsync(adminSample.UserName);
                if (existingAdminByEmail != null || existingAdminByUsername != null)
                {
                    return;
                }
                else
                {
                    var adminCreationResult = await _userManager.CreateAsync(adminSample, "Password123!");
                    if (!adminCreationResult.Succeeded)
                    {
                        throw new Exception("Admin creation failed: " + string.Join(", ", adminCreationResult.Errors.Select(e => e.Description)));
                    }

                    await _userManager.AddToRoleAsync(adminSample, "Admin");

                }
                await _context.SaveChangesAsync();
            }

        }
        public async Task SeedPromotion()

        {
            // Promotion
            // Create a promotion that apply to id 1,2
            // Discount is 50%, valid until 1 month from now
            if (await _context.Promotions.CountAsync() == 0)
            {
                var promotion = new Promotion
                {
                    Name = "50% off for all products",
                    DiscountPercentage = 50,
                    ValidUntil = DateTime.Now.AddMonths(1),
                    IsActive = true,
                    ApplicableProductIds = new List<int> { 1, 2 }
                };
                await _context.Promotions.AddAsync(promotion);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SeedDatabase()
        {
            await EnsureRoles();
            if (!await _roleManager.RoleExistsAsync("User") || !await _roleManager.RoleExistsAsync("Admin"))
            {
                throw new Exception("Roles were not created successfully.");
            }
            await SeedCategory();
            await SeedProduct();
            await SeedUser();
            await SeedPromotion();
            await SeedVoucher();
            await CreateOrder(_context);


        }
        public async Task CreateOrder(AppDbContext context)
        {
            if (await context.Users.CountAsync() == 0)
            {
                Console.WriteLine("No user found");
                return;
            }
            if (await context.Products.CountAsync() == 0)
            {
                Console.WriteLine("No product found");
                return;
            }
            if (await context.Orders.CountAsync() > 0)
            {
                Console.WriteLine("Order already created");
                return;
            }

            // Retrieve the product from the database (assuming it has an Id of 3)
            var product = await context.Products.FindAsync(5);

            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            // Determine the price based on DiscountPrice or Price
            decimal effectivePrice = product.DiscountPrice != null && product.DiscountPrice > 0 ? product.DiscountPrice.Value : product.Price;
            // Add to the first user with role User
            var firstUserInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "user1@gm.com");

            if (firstUserInDb == null)
            {
                Console.WriteLine("User not found");
                return;
            }
            // Create a new order
            var order = new CreateOrderDTO
            {
                CustomerId = firstUserInDb.Id,
                PaymentMethod = "Credit Card",
                OrderDetails = new List<CreateOrderDetailDTO>()
                {
                    new CreateOrderDetailDTO
                    {
                    ProductId = product.Id,
                    Quantity = 2,
                    }
                },
                CustomerName = "John Doe",
                Address = "1234 Main St",
                Province = "Ontario",
                District = "Toronto",
                PhoneNumber = "1234567890",
                ShippingMethod = "Standard",
                CardCvv = "123",
                CardExpireDate = "12/25",
                CardHolder = "John Doe",
                CardNumber = "1234567890123456",

            };


            var orderRes = await _orderService.CreateOrderAsync(order);

            // Save changes to the database
            await context.SaveChangesAsync();

            Console.WriteLine($"Order created successfully with ID {orderRes.Id} with total {orderRes.Total}, sub total {orderRes.SubTotal}");
        }
        public async Task SeedVoucher()
        {
            if (await _context.Vouchers.CountAsync() == 0)
            {
                var voucher = new Voucher
                {
                    Code = "V12345",
                    Name = "50% off christmas",
                    DiscountPercentage = 50,
                    ExpiryDate = DateTime.Now.AddMonths(1),
                    IsActive = true,
                    Description = "50% off for all products",
                };
                await _context.Vouchers.AddAsync(voucher);
                await _context.SaveChangesAsync();
            }
        }




    }
}