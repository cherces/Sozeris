using Bogus;
using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Data.DbContext.Seeder;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!await context.Database.CanConnectAsync())
            return;

        await context.Database.MigrateAsync();

        var faker = new Faker("ru");

        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Белый хлеб", Price = 25, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Черный хлеб", Price = 30, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Батон нарезной", Price = 28, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Хлеб с отрубями", Price = 35, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Булочка ржаная", Price = 15, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Хлеб синий", Price = 35, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Хлеб красный", Price = 35, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Хлеб зеленый", Price = 35, Image = Array.Empty<byte>(), IsActive = true },
                new Product { Name = "Хлеб желтый", Price = 35, Image = Array.Empty<byte>(), IsActive = true }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var users = new List<User>
            {
                new User { Login = "admin", Password = "adminpassword", Salt = "testsalt", Role = UserRole.Admin, Phone = "89161234567", Address = "г. Москва, ул. Пушкина", IsActive = true},
                new User { Login = "courier1", Password = "password1", Salt = "testsalt", Role = UserRole.Courier, Phone = "89169876543", Address = "г. Москва, ул. Ленина", IsActive = true },
                new User { Login = "user1", Password = "password2", Salt = "testsalt", Role = UserRole.User, Phone = "89162345678", Address = "г. Москва, ул. Горького", IsActive = true },
                new User { Login = "user2", Password = "password3", Salt = "testsalt", Role = UserRole.User, Phone = "89163456789", Address = "г. Москва, ул. Чехова", IsActive = true },
                new User { Login = "user3", Password = "password4", Salt = "testsalt", Role = UserRole.User, Phone = "89164567890", Address = "г. Москва, ул. Толстого", IsActive = true }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        var usersList = context.Users.ToList();
        var productsList = context.Products.ToList();

        if (!context.Subscriptions.Any())
        {
            var subscriptions = new List<Subscription>();
            for (int i = 0; i < 50; i++)
            {
                var user = faker.PickRandom(usersList.Where(u => u.Role == UserRole.User));
                subscriptions.Add(new Subscription
                {
                    UserId = user.Id,
                    StartDate = DateTime.UtcNow.AddDays(-faker.Random.Int(0, 90)),
                    EndDate = DateTime.UtcNow.AddDays(faker.Random.Int(30, 60)),
                    IsActive = faker.Random.Bool()
                });
            }

            context.Subscriptions.AddRange(subscriptions);
            await context.SaveChangesAsync();
        }

        var subsList = context.Subscriptions.ToList();

        if (!context.Orders.Any())
        {
            var orders = new List<Order>();
            int ordersCount = faker.Random.Int(120, 200);

            for (int i = 0; i < ordersCount; i++)
            {
                var sub = faker.PickRandom(subsList);
                var product = faker.PickRandom(productsList);
                int quantity = faker.Random.Int(1, 5);

                orders.Add(new Order
                {
                    SubscriptionId = sub.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    Price = product.Price * quantity
                });
            }

            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
        }

        if (!context.DeliveryHistory.Any())
        {
            var deliveries = new List<DeliveryHistory>();
            int deliveryCount = 50;

            for (int i = 0; i < deliveryCount; i++)
            {
                var sub = faker.PickRandom(subsList);
                var user = usersList.FirstOrDefault(u => u.Id == sub.UserId);

                deliveries.Add(new DeliveryHistory
                {
                    UserId = user!.Id,
                    SubscriptionId = sub.Id,
                    DeliveryDate = DateTime.UtcNow.AddDays(-faker.Random.Int(0, 180)),
                    Status = faker.PickRandom<DeliveryStatus>(),
                    Reason = faker.Random.Bool(0.2f) ? "Клиент не был дома" : null
                });
            }

            context.DeliveryHistory.AddRange(deliveries);
            await context.SaveChangesAsync();
        }
    }
}