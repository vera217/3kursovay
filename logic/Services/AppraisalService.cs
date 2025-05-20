using logic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace logic.Services
{
    public class AppraisalService
    {
        private readonly AppDbContext _db;

        public AppraisalService(AppDbContext db)
        {
            _db = db;
        }

        public bool SendForAppraisal(string itemName, string photoPath, string clientId)
        {
            try
            {
                _db.appraisal_items.Add(new AppraisalItems
                {
                    ItemName = itemName,
                    PhotoPath = photoPath,
                    ClientId = clientId,
                    AppraisalDate = DateTime.UtcNow.Date 
                });
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

        public List<AppraisalItems> GetClientAppraisalItems(string clientId)
        {
            return _db.appraisal_items
                .Where(a => a.ClientId == clientId)
                .ToList();
        }
    }
}
