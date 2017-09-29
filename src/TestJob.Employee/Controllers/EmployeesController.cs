using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using TestJob.Employee.Models;
using TestJob.Employee.ViewModel;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
//using System.Web.UI;
//using System.Web.UI.WebControls;
namespace TestJob.Employee.Controllers
{
    public class EmployeesController : Controller
    {
        AppDbContext context;
        readonly IApplicationEnvironment appEnvironment;
        int CountPerPage = 7;

        public EmployeesController(AppDbContext _context, IApplicationEnvironment _appEnvironment)
        {
            context = _context;
            appEnvironment = _appEnvironment;
        }
        
        public async Task<IActionResult> Start(int page = 0, bool? status = null)
        {
            ComplexModel compModel = new ComplexModel();
            compModel.CurrentPage = page;
            compModel.Status = status;
            if(status != null)
             {
                compModel.TotallPage = await context.Workmen.Where(x => x.Status == status).CountAsync() / CountPerPage + 1;
                compModel.Workmen = await context.Workmen.Where(x=> x.Status == status).Skip(page * CountPerPage).Take(CountPerPage).ToListAsync();
                return View(compModel);
            }
            else
            {
                compModel.TotallPage = await context.Workmen.CountAsync() / CountPerPage + 1;
                compModel.Workmen = await context.Workmen.Skip(page * CountPerPage).Take(CountPerPage).ToListAsync();
                return View(compModel);
            }
            //return HttpNotFound();
        }

        [HttpPost]
        public async Task<JsonResult> SetWorker(Worker worker)
        {
            int persent = 10;
            if (worker.Tasso > 10000 && worker.Tasso <= 25000)
                persent = 15;
            if (worker.Tasso > 25000)
                persent = 25;

            context.Workmen.Add(new Workman{
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Position = worker.Position,
                Status = worker.Status,
                Tasso = worker.Tasso,
                TaxRate = persent,
                Tax = worker.Tasso/100*persent,
                Wages = worker.Tasso - worker.Tasso / 100 * persent
            });
            await context.SaveChangesAsync();
            return Json("Данные сохранены");
        }

        public async Task<FileStreamResult> Report()
        {
            //string path = Path.Combine(appEnvironment.ApplicationBasePath, fname);
            string fname = "Report_" + DateTime.Today.ToString() + "_.txt";
            List<Workman> workmen = await context.Workmen.ToListAsync();
 
            MemoryStream memStream = new MemoryStream();
            StreamWriter outputFile = new StreamWriter(memStream);

                await outputFile.WriteLineAsync(
                        "Имя" + " " +
                        "Фамилия" + " " +
                        "Должность" + " " +
                        "Статус" + " " +
                        "Оклад" + " " +
                        "Налог%" + " " +
                        "Налог сумма" + " " +
                        "Зарплата"
                        );
                await outputFile.WriteLineAsync(" ");
                foreach (var x in workmen)
                {
                    await outputFile.WriteLineAsync(
                        x.FirstName + " " +
                        x.LastName + " " +
                        x.Position + " " +
                        x.Status.ToString() + " " +
                        x.Tasso.ToString() + " " +
                        x.TaxRate.ToString() + " " +
                        x.Tax.ToString() + " " +
                        x.Wages.ToString()
                        );
                }
                memStream.Position = 0;
                return File(memStream, "text/plain", fname);   
            }

    }
}
