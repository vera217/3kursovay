using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using logic.Services;
using logic.DataBase;


namespace logic.Facade
{
    public class DataBaseFacade : IClientFacade, IAdminFacade, IAuthFacade
    {
        private readonly AppDbContext _db;
        private readonly ItemsService _itemsService;
        private readonly AppraisalService _appraisalService;
        private readonly AuthService _authService;

        public DataBaseFacade(AppDbContext db)
        {
            _db = db;
            _itemsService = new ItemsService(db);
            _appraisalService = new AppraisalService(db);
            _authService = new AuthService(db);
        }

        // IClientFacade
        public List<Items> GetAllItems() => _itemsService.GetAllItems();
        public bool SendForAppraisal(string itemName, string photoPath, string clientId)
            => _appraisalService.SendForAppraisal(itemName, photoPath, clientId);
        public List<AppraisalItems> GetMyAppraisalItems(string clientId)
            => _appraisalService.GetClientAppraisalItems(clientId);

        // IAdminFacade
        public bool AddItem(Items item) => _itemsService.AddItem(item);
        public bool DeleteItem(int productId) => _itemsService.DeleteItem(productId);
        public List<AppraisalItems> GetItemsForAppraisal()
        {
            return _db.appraisal_items.Where(i => i.EstimatedPrice == null).ToList();
        }
        public bool AppraiseItem(int itemId, decimal price)
        {
            return _itemsService.AppraiseItem(itemId, price, null);
        }

        public bool AppraiseItem(int itemId, decimal price, string employeeId)
        {
            return _itemsService.AppraiseItem(itemId, price, employeeId);
        }

        // IAuthFacade
        public User Login(string username, string password) => _authService.Login(username, password);
        public bool RegisterClient(string clientId, string phone, string email, string password)
            => _authService.RegisterClient(clientId, phone, email, password);
    }
}
