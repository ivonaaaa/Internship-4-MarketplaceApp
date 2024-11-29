using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Domain;
using MarketplaceApp.Domain.Dtos;

namespace MarketplaceApp.Helpers
{
    public static class Printers
    {
        //public static void PrintProduct(ProductDto productDto)
        //{
        //    if (productDto == null)
        //    {
        //        Console.WriteLine("Proizvod ne postoji.");
        //        return;
        //    }

        //    Console.WriteLine($"ID: {productDto.Id}");
        //    Console.WriteLine($"Naziv: {productDto.Name}");
        //    Console.WriteLine($"Opis: {productDto.Description}");
        //    Console.WriteLine($"Cijena: {productDto.Price:C}");
        //    //Console.WriteLine($"Status: {productDto.Status}");
        //    Console.WriteLine(new string('-', 30));
        //}

        //public static void PrintProductList(IEnumerable<ProductDto> productDtos)
        //{
        //    if (productDtos == null || !productDtos.Any())
        //    {
        //        Console.WriteLine("Nema dostupnih proizvoda za prikaz.");
        //        return;
        //    }

        //    Console.WriteLine("\nLista proizvoda:");
        //    foreach (var productDto in productDtos)
        //    {
        //        PrintProduct(productDto);
        //    }
        //}
    }
}
