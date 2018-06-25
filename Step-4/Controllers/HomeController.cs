using Step_4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Step_4.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db = new ProductContext();

        public ActionResult Index()
        {
            IEnumerable<Company> companies = db.Companies;
            ViewBag.Companies = companies;
            return View();
        }

        [HttpGet]
        public JsonResult GetPhones(string id)
        {
            int cId = int.Parse(id);
            var phones = db.Products.Where(p => p.CompanyId == cId).ToList();

            return Json(phones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePhonesChanges(Product data)
        {
            Product phone = db.Products.Where(p => p.Id == data.Id).Single();
            phone.Name = data.Name;
            db.SaveChanges();

            return Json(phone);
        }

        [HttpPost]
        public JsonResult DeletePhone(Product data)
        {
            Product deletedPhone = new Product();
            Product phone = db.Products.Where(p => p.Id == data.Id).Single();
            deletedPhone.CompanyId = phone.CompanyId;
            deletedPhone.Id = phone.Id;

            db.Products.Remove(phone);
            db.SaveChanges();

            return Json(deletedPhone);
        }

        [HttpPost]
        public JsonResult AddPhone(Product data)
        {
            Product phone = db.Products.Add(data);
            db.SaveChanges();

            return Json(phone);
        }

        [HttpPost]
        public JsonResult SaveCompanyChanges(Company data)
        {
            Company company = db.Companies.Where(p => p.Id == data.Id).Single();
            company.Name = data.Name;
            db.SaveChanges();

            return Json(company);
        }

        [HttpPost]
        public JsonResult AddCompany(Company data)
        {
            Company company = db.Companies.Add(data);
            db.SaveChanges();

            return Json(company);
        }

        [HttpPost]
        public JsonResult DeleteCompany(Company data)
        {
            Company deletedCompany = new Company();
            Company company = db.Companies.Where(c => c.Id == data.Id).Single();
            deletedCompany.Id = company.Id;

            var phones = db.Products.Where(p => p.CompanyId == company.Id);

            db.Products.RemoveRange(phones);
            db.Companies.Remove(company);
            db.SaveChanges();

            return Json(deletedCompany);
        }
    }
}