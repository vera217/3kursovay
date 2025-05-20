using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace logic.Facade
{
    public interface IClientFacade
    {
        List<Items> GetAllItems();
        bool SendForAppraisal(string itemName, string photoPath, string clientId);
        List<AppraisalItems> GetMyAppraisalItems(string clientId);
    }
}
