using Microsoft.AspNetCore.Mvc.ModelBinding; // <-- 1. ADD THIS AT THE TOP
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tenanto.Models
{
    [Table("RentRecords")]
    public class RentRecord
    {
        [Key]
        public int RentRecordId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseRent { get; set; }
        public decimal? ElectricityBill { get; set; }
        public decimal? WaterBill { get; set; }
        public decimal? GasBill { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal TotalRent { get; set; }
        public string Status { get; set; } // "Paid" or "Due"

        // Foreign Key to link to the Tenant
        public int TenantId { get; set; }

        [BindNever] // <-- 2. ADD THIS ATTRIBUTE
        public Tenant Tenant { get; set; }
    }
}