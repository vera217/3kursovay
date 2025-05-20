using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logic.Services;
using model;


namespace logic.Facade
{
    public interface IAdminFacade
    {
        List<Items> GetAllItems();
        bool AddItem(Items item);
        bool DeleteItem(int productId);
        List<AppraisalItems> GetItemsForAppraisal();

        bool AppraiseItem(int itemId, decimal price);                
        bool AppraiseItem(int itemId, decimal price, string employeeId); 
    }

}

