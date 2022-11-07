using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItemRazor.Models
{
    public class Item : IComparable<Item>
    {
        [Key] //ikke nødvendig pga conventions 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public Item()
        {
            
        }

        public Item(int id, string name, decimal price)
        {
            ItemId = id;
            Name = name;
            Price = price;
        }

        public int CompareTo(Item other)
        {
            return ItemId - other.ItemId;
        }
    }
}
