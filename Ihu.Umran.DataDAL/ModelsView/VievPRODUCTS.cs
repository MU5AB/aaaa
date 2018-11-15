
namespace Ihu.Umran.DataDAL.ModelsView
{using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ihu.Umran.DataDAL.Models;
using System.ComponentModel.DataAnnotations;
  



    public class VievPRODUCTS
    {
        public ViewCATEGORIES Category;

        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }


        public string QuantityPerUnit { get; set; }


        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }


        public bool Discontinued { get; set; }
        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        public virtual Suppliers Suppliers { get; set; }
        //public ViewCATEGORIES Category { get; set; }
    }
}
