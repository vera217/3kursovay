using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Client
    {
        [Key]
        [Column("client_id")]
        public string ClientId { get; set; } 
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        public string email { get; set; }
        [ForeignKey("ClientId")] 
        public User User { get; set; }
    }

}
