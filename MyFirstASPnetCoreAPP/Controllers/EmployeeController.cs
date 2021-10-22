using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstASPnetCoreAPP.Models;

namespace MyFirstASPnetCoreAPP.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();

        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> allList = new List<Employee>();
            allList = objemployee.GetAllEmployees().ToList();
            return View(allList);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Employee createemployee = new Employee();
            try
            {
                createemployee.FirstName = collection["FirstName"];
                createemployee.LastName = collection["LastName"];
                createemployee.City = collection["City"];
                createemployee.Department = collection["Department"];
                createemployee.Gender = collection["Gender"];
                if(objemployee.AddEmployee(createemployee))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                { return View(); }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee emp = new Employee();

            emp = objemployee.GetEmployeeData(id);

            if(emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Employee updateemployee = new Employee();
            try
            {
                updateemployee.EmployeeID =Convert.ToInt32(collection["EmployeeID"]);
                updateemployee.FirstName = collection["FirstName"];
                updateemployee.LastName = collection["LastName"];
                updateemployee.City = collection["City"];
                updateemployee.Department = collection["Department"];
                updateemployee.Gender = collection["Gender"];
                if (objemployee.UpdateEmployee(updateemployee))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                { return View(); }
                
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
