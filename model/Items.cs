using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("item_id")]
        public int ItemId { get; set; }
        [Column("item_name")]
        public string ItemName { get; set; }
        [Column("photo_path")]
        public string PhotoPath { get; set; }
        public decimal price { get; set; }
        [Column("storage_location")]
        public string StorageLocation { get; set; }
        [Column("client_id")]
        public string ClientId { get; set; }
        [Column("employee_id")]
        public string EmployeeId { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
    }
}
