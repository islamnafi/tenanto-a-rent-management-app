using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace tenanto.Models
{
    [Table("Tenants")]
    public class Tenant
    {
        [Key] 
        public int TenantId { get; set; }

        public string Name { get; set; }
        public string RoomNumber { get; set; }

        public ICollection<RentRecord> RentRecords { get; set; }
    }
}