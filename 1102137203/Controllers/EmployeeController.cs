using _1102137203.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ActionResult update(String empId) {
            List<Employees> bingo = db.Employees.Where(x => x.EmployeeID.ToString() == empId).ToList();

            Employees data = new Employees();

            String birthDate = "";
            String birthMon = "";
            String birthDay = "";

            String hireDate = "";
            String hireMon = "";
            String hireDay = "";

            foreach (var item in bingo)
            {
                data.EmployeeID = item.EmployeeID;
                data.FirstName = item.FirstName;
                data.LastName = item.LastName;
                data.Title = item.Title;
                data.TitleOfCourtesy = item.TitleOfCourtesy;
                data.Country = item.Country;
                data.City = item.City;
                data.Address = item.Address;
                data.Phone = item.Phone;
                data.Gender = item.Gender;
                data.ManagerID = item.ManagerID;
                data.MonthlyPayment = item.MonthlyPayment;
                data.YearlyPayment = item.YearlyPayment;
                

                birthMon = alterDate(item.BirthDate.Month.ToString());
                birthDay = alterDate(item.BirthDate.Day.ToString());
                birthDate = String.Format("{0}-{1}-{2}", item.BirthDate.Year, birthMon, birthDay);

                hireMon = alterDate(item.HireDate.Month.ToString());
                hireDay = alterDate(item.HireDate.Day.ToString());
                hireDate = String.Format("{0}-{1}-{2}", item.HireDate.Year, hireMon, hireDay);
            }
            ViewBag.data = data;
            ViewBag.birthDate = birthDate;
            ViewBag.hireDate = hireDate;

            List<Employees> bingolastname = db.Employees.Where(x => x.LastName != data.LastName).ToList();
            ViewBag.bingolastnamelist = bingolastname;

            List<Employees> bingofirstname = db.Employees.Where(x => x.FirstName != data.FirstName).ToList();
            ViewBag.bingofirstnamelist = bingofirstname;

            List<Employees> bingotitle = db.Employees.Where(x => x.Title != data.Title).ToList();
            ViewBag.bingotitlelist = bingotitle;

            List<Employees> bingotitleofcourtesy = db.Employees.Where(x => x.TitleOfCourtesy != data.TitleOfCourtesy).ToList();
            ViewBag.bingotitleofcourtesylist = bingotitleofcourtesy;
            return View();
        }
        public String alterDate(String date)
        {
            StringBuilder change = new StringBuilder();
            if (date.Length < 2)
            {
                change.Append("0");//append字串相加
            }
            change.Append(date);
            return change.ToString();
        }
        public void delete(int emploId)
        {
            Employees emp = db.Employees.Find(emploId);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}