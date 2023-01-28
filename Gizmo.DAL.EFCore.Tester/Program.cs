using Gizmo.DAL.EFCore;
using Gizmo.DAL.EFCore.Tester;
using Gizmo.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Started .....");
Console.WriteLine();

// Run examples of database interactions

DatabaseTester.ExemplifyDatabaseReading();
Console.WriteLine();

DatabaseTester.ExemplifyDatabaseInsertion();
Console.WriteLine();

DatabaseTester.ExemplifyDatabaseReading();
Console.WriteLine();

DatabaseTester.ExemplifyDatabaseDelete();
Console.WriteLine();