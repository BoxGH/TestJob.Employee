using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Data.Entity;
using TestJob.Employee.Models;
using TestJob.Employee.Controllers;
using TestJob.Employee.ViewModel;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Mvc;
using System.IO;

namespace xUnitTests
{
    public class TestIoDb
    {
        [Fact]
        public void TestDbEnsureCreate()
        {
            TestDbContext context = new TestDbContext();
            Assert.True(context.Database.EnsureCreated());
            context.Dispose();
        }
        [Fact]
        public async void VerifyingSetUser()
        {
            TestDbContext context = new TestDbContext();
            EmployeesController employeecontroller = new EmployeesController(context, null);
  
            Worker worker = new Worker
            {
                FirstName = "Fworkername",
                LastName = "Lworkername",
                Position = "SomeJobs",
                Status = true,
                Tasso = 10000
            };
            await employeecontroller.SetWorker(worker);
            Workman workman = context.Workmen.FirstOrDefault();
            
            Assert.Equal("Fworkername", workman.FirstName);
            Assert.True(workman.Tax == 1000);
            Assert.True(workman.TaxRate == 10);
            Assert.True(workman.Wages == 9000);

            context.Dispose();
        }
        [Fact]
        public async void VerifyingActionStart()
        {
            TestDbContext context = new TestDbContext();
            EmployeesController employeecontroller = new EmployeesController(context, null);
            Workman workman = new Workman
            {
                FirstName = "Fworkername",
                LastName = "Lworkername",
                Position = "SomeJobs",
                Status = true,
                Tasso = 100000,
                TaxRate = 25,
                Tax = 25000,
                Wages = 75000
            };
            context.Workmen.Add(workman);
            context.Workmen.Add(workman);
            context.Workmen.Add(workman);
            await context.SaveChangesAsync();

            var result = (ViewResult)employeecontroller.Start().Result;
            ComplexModel compModel = (ComplexModel)result.ViewData.Model;

            Assert.NotEmpty(compModel.Workmen);
            Assert.Equal<int>(compModel.CurrentPage, 0);
            Assert.True(compModel.Status == null);
            Assert.Equal<decimal>(compModel.Workmen.FirstOrDefault().Wages, 75000);

            context.Dispose();
        }
        //[Fact]
        //public async void VerifyingActionReport()
        //{
        //    TestDbContext context = new TestDbContext();
        //    EmployeesController controller = new EmployeesController(context, null);

        //    Worker worker = new Worker
        //    {
        //        FirstName = "Fworkername",
        //        LastName = "Lworkername",
        //        Position = "SomeJobs",
        //        Status = true,
        //        Tasso = 10000
        //    };
        //    await controller.SetWorker(worker);
        //    var result = controller.Report().Result;
        //    StreamReader sr = new StreamReader(controller.Report().Result.FileStream);
        //    MemoryStream ms = new MemoryStream();
        //    StreamWriter sw = new StreamWriter(ms);
        //    sw.Write(result.FileStream.ReadByte());
        //    //var result = new FileStreamResult(controller.Report().Result.FileStream, "text/pain");
        //    //Assert.NotNull(result.FileDownloadName);
        //    Assert.NotNull(ms.Length);
        //    sr.Dispose();
        //    ms.Dispose();
        //    context.Dispose();
        //}
    }



    public class TestDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase();
            base.OnConfiguring(options);
        }
    }
}
