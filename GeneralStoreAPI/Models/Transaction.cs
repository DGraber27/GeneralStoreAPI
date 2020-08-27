using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }
        [Required]
        public int ItemCount { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }
    }
}