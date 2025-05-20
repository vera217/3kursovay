using logic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace logic.Services
{
    public class ItemsService
    {
        private readonly AppDbContext _db;

        public ItemsService(AppDbContext db)
        {
            _db = db;
        }

        public List<Items> GetAllItems()
        {
            return _db.items.ToList();
        }

        public bool AddItem(Items items)
        {
            try
            {
                _db.items.Add(items);
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Внутренняя ошибка: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool DeleteItem(int productId)
        {
            var items = _db.items.Find(productId);
            if (items == null) return false;

            _db.items.Remove(items);
            return _db.SaveChanges() > 0;
        }
        public bool AppraiseItem(int itemId, decimal price, string employeeId)
        {
            var appraisalItem = _db.appraisal_items.FirstOrDefault(i => i.ItemId == itemId);
            if (appraisalItem == null || appraisalItem.EstimatedPrice != null)
                return false;

            appraisalItem.EstimatedPrice = price;
            appraisalItem.AppraisalDate = DateTime.UtcNow;
            appraisalItem.EmployeeId = employeeId;

            _db.SaveChanges();
            return true;
        }

    }
}
