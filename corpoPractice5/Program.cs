using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        // Пока что не используется
        public class A {
            public string code;
            public string yearOfBirth;
            public string street;
            
            public A(string _code, string _yearOfBirth, string _street)  {
                code = _code;
                yearOfBirth = _yearOfBirth;
                street = _street;
            }
        }
        public class C {
            public string code;
            public string shop;
            public int discount;

            public C(string _code, string _shop, int _discount) {
                code = _code;
                shop = _shop;
                discount = _discount;
            }
        }
        public class B {
            public string id;
            public string category;
            public string country;

            public B(string _id, string _category, string _country) {
                id = _id;
                category = _category;
                country = _country;
            }
        }
        public class D {
            public string id;
            public string shop;
            public int price;

            public D (string _id, string _shop, int _price) {
                id = _id;
                shop = _shop;
                price = _price;
            }
        }
        public class E {
            public string code;
            public string id;
            public string shop;

            public E (string _code, string _id, string _shop) {
                code = _code;
                id = _id;
                shop = _shop;
            }
        }

        static void number1(IEnumerable<A> list_A, IEnumerable<C> list_C) {
            var discounts = from c in list_C
                join a in list_A on c.code equals a.code
                group new {a.code, a.yearOfBirth, c.discount, a.street} by c.shop into g
                orderby g.Key
                select new {
                    shopname = g.Key,
                    MaxDiscountCustomers = g.Where(x => x.discount == g.Max(y => y.discount)).OrderBy(x => x.code)
                };

            foreach (var shop in discounts) {
                Console.WriteLine("Магазин: " + shop.shopname);
                foreach (var consumer in shop.MaxDiscountCustomers) {
                    Console.WriteLine("Максимальная скидка: " + consumer.discount);
                    Console.WriteLine("Код потребителя: " + consumer.code);
                    Console.WriteLine("Год рождения: " + consumer.yearOfBirth);
                    Console.WriteLine("Улица проживания: " + consumer.street);
                }
                
                Console.WriteLine();
            }
        }
        
        static void number2(IEnumerable<A> list_A, IEnumerable<C> list_C) {
            var shopsWithStreets = from a in list_A
                join c in list_C on a.code equals c.code
                group new {a.street, c.shop} by new {c.shop, a.street} into g
                orderby g.Key.shop, g.Key.street
                select new {g.Key.shop, g.Key.street, Count = g.Count()};
            
            foreach (var item in shopsWithStreets)
            {
                Console.WriteLine($"{item.shop}, {item.street}, {item.Count}");
            }
        }

        static void number3(IEnumerable<B> list_B, IEnumerable<D> list_D) {
            var items = from b in list_B
                join d in list_D on b.id equals d.id
                group new {d.shop, b.country, b.category} by b.category into g
                orderby g.Count(), g.Key
                select new {
                    category = g.Key,
                    numberOfShops = g.Select(x => x.shop).Distinct().Count(),
                    numberOfCountries = g.Select(x => x.country).Distinct().Count()
                };

                foreach (var item in items)
                {
                    Console.WriteLine($"Кол-во магазинов: {item.numberOfShops}, Категория: {item.category}, Кол-во стран: {item.numberOfCountries}");
                }
        }

        static void number4(IEnumerable<D> list_D, IEnumerable<E> list_E) {
            var purchases = from e in list_E
                join d in list_D on e.id equals d.id
                group new {d.price, d.id} by d.id into g
                orderby g.Count()
                select new {
                    purchaseCount = g.Count(),
                    id = g.Key,
                    maxPrice = g.Max(x => x.price)
                };

            foreach (var item in purchases) {
                Console.WriteLine($"Кол-во покупок: {item.purchaseCount}, Артикул: {item.id}, Максимальная цена: {item.maxPrice}");
            }
        }

        static void number5(IEnumerable<A> list_A, IEnumerable<B> list_B, IEnumerable<E> list_E) {
            var countriesByYear = from a in list_A
                join e in list_E on a.code equals e.code
                join b in list_B on e.id equals b.id
                // Группируем по годам рождения, потом ищем страну с самым большим кол-вом продаж
                group new {b.country, a.yearOfBirth, e.id} by a.yearOfBirth into g
                orderby g.Count()
                select new {
                    yearOfBirth = g.Key,
                    // Поиск самой частой страны
                    country = g.GroupBy(x => x.country).OrderByDescending(x => x.Count()).FirstOrDefault().Key,
                    // Подсчет того, сколько покупок из самой частой страны - та же формула, что сверху
                    count = g.Count(x => x.country == g.GroupBy(x => x.country).OrderByDescending(x => x.Count()).FirstOrDefault().Key)
                };

            foreach (var year in countriesByYear) {
                Console.WriteLine($"Год рождения клиента: {year.yearOfBirth}, Страна с самым большим кол-вом покупок: {year.country}, Количество покупок: {year.count}");
            }
        }

        static void number6(IEnumerable<A> list_A, IEnumerable<D> list_D, IEnumerable<E> list_E) {
            var salesByStreet = from a in list_A
                join e in list_E on a.code equals e.code
                join d in list_D on new {e.shop, e.id} equals new {d.shop, d.id}
                group new {a.street, e.shop, d.price} by new {a.street, e.shop} into g  // Группируем по улице, магазину и коду покупателя
                orderby g.Count()
                select new {
                    g.Key.street,
                    g.Key.shop,
                    sum = g.Sum(x => x.price)
                };

            foreach (var item in salesByStreet) {
                Console.WriteLine($"Улица проживания: {item.street}, Название магазина: {item.shop}, Сумма покупок: {item.sum}");
            }
        }
        
        static void number7(IEnumerable<A> list_A, IEnumerable<C> list_C, IEnumerable<D> list_D, IEnumerable<E> list_E) {
            var discountsByStreet = from e in list_E
                join a in list_A on e.code equals a.code
                join d in list_D on new {e.id, e.shop} equals new {d.id, d.shop}
                join c in list_C on new {e.code, e.shop} equals new {c.code, c.shop}

                // В группу записываем стоимость товаров по артикулу, улице и скидке
                group d.price by new {e.id, a.street, c.discount} into pricesByStreet

                select new {
                    pricesByStreet.Key.id,
                    pricesByStreet.Key.street,
                    totalDiscount = pricesByStreet.Sum() * pricesByStreet.Key.discount * 0.01
                };

            var mergedDiscount = from item in discountsByStreet
                // В группу записываем цены товара с учетом скидки по артикулу и улице
                group item.totalDiscount by new {item.id, item.street} into g
                
                select new {
                    g.Key.id,
                    g.Key.street,
                    totalDiscount = g.Sum()
                };

            foreach (var item in mergedDiscount.OrderBy(x => x.id).ThenBy(x => x.street)) {
                Console.WriteLine($"Артикул товара: {item.id}, улица: {item.street}, суммарная скидка: {item.totalDiscount}");
            }
        }

        // В сумме группировать по магазинам
        static void number8(IEnumerable<B> list_B, IEnumerable<C> list_C, IEnumerable<D> list_D, IEnumerable<E> list_E) {
            var result = from b in list_B
                join e in list_E on b.id equals e.id
                join d in list_D on b.id equals d.id
                join c in list_C on e.code equals c.code

                group new {d.price, e.shop} by new {b.country, e.code} into g
                orderby g.Key.country, g.Key.code
                select new {
                    g.Key.country,
                    g.Key.code,
                    // totalCost = g.GroupBy(x => x.shop)
                };

            foreach (var item in result) {
                // Console.WriteLine($"Страна-производитель: {item.country}, код потребителя: {item.code}, суммарная стоимость: {item.totalCost}");
            }
        }


        // Читает файл и возвращает по строкам
        static string[] getFromFile(string path) {
           string[] strings = File.ReadAllText(path).Split('\n'); 
           Array.Sort(strings);

           return strings;
        }
        
        static void Main(string[] args)
        {
            string[] consumers = getFromFile("consumers.txt");
            string[] discounts = getFromFile("discounts.txt");
            string[] items = getFromFile("items.txt");
            string[] prices = getFromFile("prices.txt");
            string[] purchases = getFromFile("purchases.txt");

            // Создаем список всех клиентов
            IEnumerable<A> list_A = from i in consumers
                                select new A(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    i.Split('-')[2]
                                );

            // Список скидок
            IEnumerable<C> list_C = from i in discounts
                                select new C(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    int.Parse(i.Split('-')[2])
                                );

            // Список товаров
            IEnumerable<B> list_B = from i in items
                                select new B(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    i.Split('-')[2]
                                );

            // Список цен
            IEnumerable<D> list_D = from i in prices
                                select new D(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    int.Parse(i.Split('-')[2])
                                );

            // Список покупок
            IEnumerable<E> list_E = from i in purchases
                                select new E(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    i.Split('-')[2]
                                );


            // Вызов методов задач
            // number1(list_A, list_C);
            // number2(list_A, list_C);
            // number3(list_B, list_D);
            // number4(list_D, list_E);
            // number5(list_A, list_B, list_E);
            // number6(list_A, list_D, list_E);
            // number7(list_A, list_C, list_D, list_E);
            // number8(list_B, list_C, list_D, list_E);
        }
    }
}