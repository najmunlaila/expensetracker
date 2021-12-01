using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_01.Models;

namespace Task_01.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseDbContext db;
        public ExpensesController(ExpenseDbContext db) { this.db = db; }
        public IActionResult Index(DateTime? from, DateTime? to)
        {
            if(from.HasValue && to.HasValue)
            {
                var data = db.DailyExpenses
                    .Include(x => x.ExpenseCategory)
                    .Where(x => x.ExpenseDate.Date >= from.Value.Date && x.ExpenseDate.Date <= to.Value.Date)
                    .ToList();
                return View(data);
            }
            else
            {
                var data = db.DailyExpenses.Include(x => x.ExpenseCategory).ToList();
                return View(data);
            }
            
        }
        public ActionResult Create()
        {
            ViewBag.Categories = db.ExpenseCategories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(DailyExpense expense)
        {
            if (ModelState.IsValid)
            {
                db.DailyExpenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.ExpenseCategories.ToList();
            return View(expense);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = db.ExpenseCategories.ToList();
            return View(db.DailyExpenses.FirstOrDefault(x => x.DailyExpenseId == id));
        }
        [HttpPost]
        public ActionResult Edit(DailyExpense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.ExpenseCategories.ToList();
            return View(expense);
        }
        public ActionResult Delete(int id)
        {
            return View(db.DailyExpenses.Include(x=> x.ExpenseCategory).FirstOrDefault(x => x.DailyExpenseId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var expense = new DailyExpense { DailyExpenseId = id };
            db.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
