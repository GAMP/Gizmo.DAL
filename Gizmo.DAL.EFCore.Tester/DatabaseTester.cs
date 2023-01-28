using Gizmo.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gizmo.DAL.EFCore.Tester
{
    public static class DatabaseTester
    {
        /// <summary>
        /// Method to examplify of database reading
        /// </summary>
        public static void ExemplifyDatabaseReading()
        {
            // Create Host DI and resolve dbcontext DI
            using IHost host = DependencyContainer.CreateHostDependencies();
            var dbContext = host.Services.GetRequiredService<DefaultDbContext>();

            // Get all taxes
            var taxes = dbContext.Taxes.ToList();
            Console.WriteLine("Get Taxes: ");

            foreach (var tax in taxes)
                Console.WriteLine($" Id: {tax.Id} | Name: '{tax.Name}' | Value: {tax.Value}.");
        }

        /// <summary>
        /// Method to examplify of database insertion
        /// </summary>
        public static void ExemplifyDatabaseInsertion()
        {
            // Create Host DI and resolve dbcontext DI
            using IHost host = DependencyContainer.CreateHostDependencies();
            var dbContext = host.Services.GetRequiredService<DefaultDbContext>();

            Console.WriteLine("Add Tax record: ");
            // Add new tax record
            var taxes = new Tax()
            {
                Name = "Test Tax",
                Value = 10,
            };
            dbContext.Taxes.Add(taxes);
            dbContext.SaveChanges();

            Console.WriteLine($" Tax record added successfully: '{taxes.Name}'");
        }

        /// <summary>
        /// Method to examplify of database delete
        /// </summary>
        public static void ExemplifyDatabaseDelete()
        {
            // Create Host DI and resolve dbcontext DI
            using IHost host = DependencyContainer.CreateHostDependencies();
            var dbContext = host.Services.GetRequiredService<DefaultDbContext>();

            Console.WriteLine("Delete new added tax record: ");

            // Get lastest tax record
            var tax = dbContext.Taxes.OrderBy(x => x.Id).LastOrDefault();
            if (tax == null)
                return;

            dbContext.Taxes.Remove(tax);
            dbContext.SaveChanges();

            Console.WriteLine($" Tax record deleted successfully: '{tax.Name}'");
        }
    }
}
