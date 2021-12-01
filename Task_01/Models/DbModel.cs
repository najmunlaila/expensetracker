using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Task_01.Validation;

namespace Task_01.Models
{
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            this.DailyExpenses = new List<DailyExpense>();
        }
        public int ExpenseCategoryId { get; set; }
        [Required, StringLength(30)]
        public string CategoryName { get; set; }
        //
        public virtual ICollection<DailyExpense> DailyExpenses { get; set; }
    }
    public class DailyExpense
    {
        public int DailyExpenseId { get; set; }
        [Required, Column(TypeName ="date"), FutureDateValidation(ErrorMessage ="Value cannot be future date")]
        public DateTime ExpenseDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Required, ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }
        //
        public virtual ExpenseCategory ExpenseCategory { get; set; }

    }
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<DailyExpense> DailyExpenses { get; set; }
    }
}
