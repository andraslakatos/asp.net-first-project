using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebAPI.Tests
{
    internal class TestDbInitializer
    {
        public static void Initialize(FoodOrderContext context)
        {
            

            IList<Category> defaultCategories = new List<Category>
            {
                new Category()
                {
                    Name = "TesztÉtelek",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "TesztÉtelSima",
                            Description = "Teszt Leírás Sima, Teszt Leírás Sima, Teszt Leírás Sima.",
                            Price = 1000,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "TesztÉtelCsípős",
                            Description = "Teszt Leírás Csípős, Teszt Leírás Csípős, Teszt Leírás Csípős.",
                            Price = 2000,
                            Spicy = true,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "TesztÉtelVegán",
                            Description = "Teszt Leírás Vegán, Teszt Leírás Vegán, Teszt Leírás Vegán.",
                            Price = 3000,
                            Spicy = false,
                            Vegan = true,
                        }
                    }
                },
                new Category()
                {
                    Name = "Italok",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "TesztItalSima",
                            Description = "",
                            Price = 250,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "TesztItalVegan",
                            Description = "",
                            Price = 500,
                            Spicy = false,
                            Vegan = true,
                        }
                    }
                }
            };

            IList<Order> defaultOrders = new List<Order>()
            {
                new Order()
                {
                    CartItems = "0 2,4 5",
                    Name = "Teszt Tamás",
                    Address = "1000 Tesztváros, Teszt utca 10.",
                    PhoneNumber = "06301234567",
                    TimeOfOrder = DateTime.Now - TimeSpan.FromDays(7),
                    TimeOfCompletion = null,
                    TotalPrice = 4500
                },
                new Order()
                {
                    CartItems = "1 2,2 1,4 4",
                    Name = "Teszt Tamás",
                    Address = "1010 Tesztfalu, Teszt út 10.",
                    PhoneNumber = "06301234567",
                    TimeOfOrder = DateTime.Now - TimeSpan.FromDays(7),
                    TimeOfCompletion = DateTime.Now,
                    TotalPrice = 8000
                }
            };

            context.AddRange(defaultCategories);
            context.AddRange(defaultOrders);
            context.SaveChanges();
        }
    }
}
