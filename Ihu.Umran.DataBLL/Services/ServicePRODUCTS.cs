using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ihu.Umran.DataDAL.ModelsView;
using Ihu.Umran.DataDAL.Models;

namespace Ihu.Umran.DataBLL.Services
{
    public class ServicePRODUCTS
    {

        public NorthwindEntities Entities = new NorthwindEntities();
        public List<VievPRODUCTS> GetAll()
        {
            List<VievPRODUCTS> result = new List<VievPRODUCTS>();



             result = Entities.Products.Select(product => new VievPRODUCTS
             {
                ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
                    UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                 
                }).ToList();

            return result;

        }

        public IEnumerable<VievPRODUCTS> Read()
        {
            return GetAll();
        }

    }



}

