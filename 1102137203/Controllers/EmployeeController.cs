using _1102137203.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1102137203.Controllers
{
    public class EmployeeController : Controller
    {
        DB db = new DB();
        // GET: Employee
        public ActionResult Index()
        {
            List<String> etitle = db.Employees.Select(x => x.Title).ToList();
            ViewBag.etitle = etitle;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection emp)
        {
            String empId = emp["empId"];
            String emplo = emp["emplo"];
            String title = emp["title"];
            String hireDate = emp["hireDate"];
            return RedirectToAction("search",
                new { empId, emplo, title,  hireDate });
        }
        public ActionResult search(String empId, String emplo, String title, String hireDate) {
            DateTime hrdt = Convert.ToDateTime(hireDate);
            List<Employees> bingo=db.Employees.Where(x=>
             (String.IsNullOrEmpty(empId) ? true : x.EmployeeID.ToString() == empId) &&
             (String.IsNullOrEmpty(emplo) ? true : x.FirstName == emplo) &&
             (String.IsNullOrEmpty(title) ? true : x.Title == title) &&
             (String.IsNullOrEmpty(hireDate) ? true : x.HireDate == hrdt)
             ).ToList();
            ViewBag.bingo = bingo;
            return View();
        }
        public ActionResult add()
        {

            int empId = db.Employees.Select(x => x.EmployeeID).Count() + 1;
            ViewBag.empId = empId;

            List<Employees> employee = db.Employees.ToList();
            ViewBag.employee = employee;
            

            return View();
        }
        [HttpPost]
        public ActionResult add(FormCollection inputs)
        {
            String empId = inputs["empId"];
            String firstname = inputs["firstname"];
            String lastname = inputs["lastname"];
            String title = inputs["title"];
            String titleofcourtesy = inputs["titleofcourtesy"];
            String hiredate = inputs["hiredate"];
            String birthdate = inputs["birthdate"];
            String country = inputs["country"];
            String city = inputs["city"];
            String add = inputs["add"];
            String phone = inputs["phone"];
            String gender = inputs["gender"];
            String manager = inputs["manager"];
            String msalary = inputs["msalary"];
            String ysalary = inputs["ysalary"];

            Employees data = new Employees();

            //data.EmployeeID =int.Parse(empId);
            data.LastName = lastname;
            data.FirstName = firstname;
            data.Title = title;
            data.TitleOfCourtesy = titleofcourtesy;
            data.HireDate = Convert.ToDateTime(hiredate);
            data.BirthDate = Convert.ToDateTime(birthdate);
            data.Country = country;
            data.City = city;
            data.Address = add;
            data.Phone = phone;
            data.Gender = gender;
            data.ManagerID = Convert.ToInt32( manager);
            data.MonthlyPayment = Convert.ToInt32(msalary);
            data.YearlyPayment = Convert.ToInt32(ysalary);

            db.Employees.Add(data);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public void delete(int emploId)
        {
            Employees emp = db.Employees.Find(emploId);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}