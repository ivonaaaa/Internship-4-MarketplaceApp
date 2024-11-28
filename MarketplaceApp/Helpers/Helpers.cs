using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Helpers
{
    public static class Helper
    {
        public static string GetValidInputFromUser(string prompt, IEnumerable<string> validOptions)
        {
            string choice;
            do
            {
                Console.WriteLine(prompt);
                choice = Console.ReadLine();
            } while (!validOptions.Contains(choice));

            return choice;
        }

        public static decimal GetValidDecimalInput(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Neispravan unos. Pokušajte ponovo.");
            }
        }

        public static string GetValidEmailInput(string prompt)
        {
            string email;
            do
            {
                Console.WriteLine(prompt);
                email = Console.ReadLine();
            } while (string.IsNullOrEmpty(email) || !email.Contains("@"));
            return email;
        }

        public static string SelectCategory()
        {
            Console.WriteLine("Odaberite kategoriju:");
            Console.WriteLine("1 - Electronics");
            Console.WriteLine("2 - Clothing");
            Console.WriteLine("3 - Books");
            Console.WriteLine("4 - Furniture");
            Console.WriteLine("5 - Food");

            var choice = GetValidInputFromUser("Unesite broj kategorije", new[] { "1", "2", "3", "4", "5" });
            var categoryMap = new Dictionary<string, string>
            {
                { "1", "electronics" },
                { "2", "clothing" },
                { "3", "books" },
                { "4", "furniture" },
                { "5", "food" }
            };
            return categoryMap[choice];
        }
    }
}
