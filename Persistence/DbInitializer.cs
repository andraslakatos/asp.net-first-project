using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DbInitializer
    {
        private static FoodOrderContext _context;
        private static UserManager<ApplicationUser> _userManager;

        public static void Initialize(IServiceScope serviceScope)
        {
            _context = serviceScope.ServiceProvider.GetRequiredService<FoodOrderContext>();
            _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _context.Database.Migrate();

            if (!_context.Categories.Any())
            {
                IList<Category> defCategories = new List<Category>()
            {
                new Category
                {
                    Name = "Főételek",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Bolognai Spagetti",
                            Description =
                                "A bolognai spagetti egy olasz tésztaétel, amely spagetti tésztából és egy olasz szószból áll, ami darált húsból, szalonnából és paradicsomból készül. Parmezán sajttal tálalják.",
                            Price = 2500,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Bécsi szelet",
                            Description =
                                "A tradicionális bécsi szelet a bécsi konyha közismert, 3-4 milliméter vastagságúra kivert, borjúhúsból készített, panírozott, húsétele.",
                            Price = 2800,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Szecsuáni csirke",
                            Description =
                                "A szecsuáni csirke Kína Szecsúán tartományából származó, fűszeres étel. Tojásba és kukoricakeményítőbe mártott, pácolt csirkehúsból készül, szárított chilipaprikával és egyszerű szója- és szezámmártással megsütve.",
                            Price = 3200,
                            Spicy = true,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Cordon bleu",
                            Description =
                                "A cordon bleu egy Francia országból származó főétel. Az étel borjó húsból készül, melyet vékonyra törnek és egy szelet sonka és egy szelet sajt köré tekernek, majd serpenyőben kirántják.",
                            Price = 2750,
                            Spicy = false,
                            Vegan = false,
                        },
                    }
                },

                new Category
                {
                    Name = "Előételek",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Velős pirítós",
                            Description =
                                "A velős pirítós pírított, fűszeres velővel megkent, magyar erős paprikával megszórt pirítós.",
                            Price = 800,
                            Spicy = true,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Skót tojás",
                            Description =
                                "A skót tojás kolbászhúsba csomagolt, zsemlemorzsával bevont, sült vagy rántott kemény tojás.",
                            Price = 900,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Vegán görög saláta",
                            Description =
                                "A vegán görögsaláta paradicsomból, uborkából, lilahagymából, fekete olívabogyóból, vegán görög sajtből és paprikából álló, oliva olajjal leöntött saláta.",
                            Price = 1100,
                            Spicy = false,
                            Vegan = true,
                        },
                    }

                },
                new Category
                {
                    Name = "Pizzák",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Songoku Pizza",
                            Description = "Paradicsomszósz, sonka, gomba, kukorica.",
                            Price = 2400,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Sonkás Pizza",
                            Description = "Paradicsomszósz, sonka, mozzarella.",
                            Price = 2000,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Sajtimádó Pizza",
                            Description = "Sajtkrém, bacon, csirkemell, cheddar, mozzarella.",
                            Price = 2700,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Bivaly Pizza",
                            Description = "Paradicsomszósz, chiliszósz, kolbász, zöldpaprika, jalapeno, mozzarella.",
                            Price = 2600,
                            Spicy = true,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Vegán Pepperone Pizza",
                            Description = "Paradicsomszósz, vegán sajt, csirke ízesítésű vegán sonka, édes pepperone.",
                            Price = 3200,
                            Spicy = false,
                            Vegan = true,
                        },
                        new Item()
                        {
                            Name = "Szarvasgombás Pizza",
                            Description = "Paradicsomszósz, mozzarella, cheddar, camambert, csirke mell darabkák, saláta, szarvasgomba",
                            Price = 10000,
                            Spicy = false,
                            Vegan = false,
                        },
                    }
                },
                new Category
                {
                    Name = "Desszertek",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Palacsinta",
                            Description =
                                "A palacsinta a világ egyik legnépszerűbb desszertje, melyet mindenhol másképp csinálnak. A magyar palacisnta egy a serpenyőben mindkét oldalán megsütött tészta, édes vagy sós ízesítéssel.",
                            Price = 800,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Tiramisu",
                            Description = "A tiramisu egy olasz eredetű kávé ízű, krémes desszert.",
                            Price = 1100,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Mákos guba",
                            Description =
                                "A mákos guba egy jellegzetes magyar édesség, szikkadt vizes kifliből, vaníliás cukorból vagy tejből és cukros mákból készül.",
                            Price = 900,
                            Spicy = false,
                            Vegan = false,
                        },
                        new Item()
                        {
                            Name = "Vegán répatorta",
                            Description =
                                "A répatorta egy nyugat európából száramzó desszert., melnyek jellegzetessége a tésztába kevert sárgarépa és a különbféle mogyorók, valamint a fehér vegán krémsajt-máz.",
                            Price = 1050,
                            Spicy = false,
                            Vegan = true,
                        },
                    }
                },
                new Category
                {
                    Name = "Italok",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Coca-Cola 1.75 l",
                            Description = "",
                            Price = 450,
                            Spicy = false,
                            Vegan = true,
                        },
                        new Item()
                        {
                            Name = "Fanta Narancs 1.75 l",
                            Description = "",
                            Price = 450,
                            Spicy = false,
                            Vegan = true,
                        },
                        new Item()
                        {
                            Name = "Szénsavmentes ásványvíz 1.5 l",
                            Description = "",
                            Price = 200,
                            Spicy = false,
                            Vegan = true,
                        },
                        new Item()
                        {
                            Name = "Kinley Tonic 1.75 l",
                            Description = "",
                            Price = 450,
                            Spicy = false,
                            Vegan = true,
                        }
                    }
                },
            };

                _context.AddRange(defCategories);
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    UserName = "user"
                };
                var Password = "user";

                var result = _userManager.CreateAsync(user, Password).Result;
            }
        }
    }
}
