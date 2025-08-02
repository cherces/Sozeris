using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services;

public class SubscriptionService : ISubscriptionService
{
    public async Task<List<Subscription>> GetSubscriptionsByUserIdAsync(int userId)
    {
        await Task.Delay(100); // Эмуляция задержки запроса

        var product1 = new Product
        {
            Id = 1,
            Name = "Товар A",
            Price = 150
        };

        var product2 = new Product
        {
            Id = 2,
            Name = "Товар B",
            Price = 200
        };
        
        var product4 = new Product
        {
            Id = 2,
            Name = "Товар B",
            Price = 200
        };
        
        var product3 = new Product
        {
            Id = 2,
            Name = "Товар B",
            Price = 200
        };

        return new List<Subscription>
        {
            new Subscription
            {
                Id = 345634,
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(3),
                PurchaseDate = DateTime.UtcNow.AddMonths(-3),
                IsActive = true,
                Price = 234,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 1,
                        Quantity = 2,
                        Price = product1.Price * 2,
                        Product = product1
                    },
                    new Order
                    {
                        Id = 2,
                        Quantity = 1,
                        Price = product2.Price,
                        Product = product2
                    }
                }
            },
            new Subscription
            {
                Id = 227543,
                StartDate = DateTime.UtcNow.AddYears(-1),
                EndDate = DateTime.UtcNow.AddMonths(-6),
                PurchaseDate = DateTime.UtcNow.AddMonths(-3),
                IsActive = false,
                Price = 534,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product2
                    }
                }
            },
            new Subscription
            {
                Id = 646325,
                StartDate = DateTime.UtcNow.AddYears(-1),
                EndDate = DateTime.UtcNow.AddMonths(-6),
                PurchaseDate = DateTime.UtcNow.AddMonths(-3),
                IsActive = false,
                Price = 534,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product1
                    },
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product2
                    },
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product3
                    },                    
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product4
                    }
                }
            },
            new Subscription
            {
                Id = 375544,
                StartDate = DateTime.UtcNow.AddYears(-1),
                EndDate = DateTime.UtcNow.AddMonths(-6),
                PurchaseDate = DateTime.UtcNow.AddMonths(-3),
                IsActive = false,
                Price = 534,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 3,
                        Quantity = 3,
                        Price = product2.Price * 3,
                        Product = product2
                    }
                }
            }
        };
    }
}