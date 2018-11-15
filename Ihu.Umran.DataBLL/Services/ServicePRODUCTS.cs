using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ihu.Umran.DataDAL.ModelsView;
using Ihu.Umran.DataDAL.Models;
using System.Web;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Ihu.Umran.DataBLL.Services
{
    public class ServicePRODUCTS
    {

        //public NorthwindEntities Entities = new NorthwindEntities();
        //public List<VievPRODUCTS> GetAll()
        //{
        //    List<VievPRODUCTS> result = new List<VievPRODUCTS>();



        //     result = Entities.Products.Select(product => new VievPRODUCTS
        //     {
        //        ProductID = product.ProductID,
        //            ProductName = product.ProductName,
        //            SupplierID = product.SupplierID,
        //            CategoryID = product.CategoryID,
        //            QuantityPerUnit = product.QuantityPerUnit,
        //            UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
        //            UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
        //            UnitsOnOrder = product.UnitsOnOrder,
        //            ReorderLevel = product.ReorderLevel,
        //            Discontinued = product.Discontinued,

        //        }).ToList();

        //    return result;

        //}

        //public IEnumerable<VievPRODUCTS> Read()
        //{
        //    return GetAll();
        //}

        private static bool UpdateDatabase = false;
        public NorthwindEntities Entities = new NorthwindEntities();

        //public ServicePRODUCTS(NorthwindEntities entities)
        //{
        //    this.Entities =entities ;
        //}

        public IList<VievPRODUCTS> GetAll()
        {

            //var result = HttpContext.Session["Products"] as IList<VievPRODUCTS>;
            List<VievPRODUCTS> result = new List<VievPRODUCTS>();

            //if (result == null || UpdateDatabase)
            //{
            result = Entities.Products.Select(product => new VievPRODUCTS
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
                    UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
                    UnitsOnOrder = product.UnitsOnOrder.HasValue ? product.UnitsOnOrder.Value : default(short),
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                    Category = new ViewCATEGORIES()
                    {
                        CategoryID = product.Categories.CategoryID,
                        CategoryName = product.Categories.CategoryName
                    },



                }).ToList();

            //    HttpContext.Current.Session["Products"] = result;
            //}

            return result;
        }

        public IEnumerable<VievPRODUCTS> Read()
        {
            return GetAll();
        }

        public void Create(VievPRODUCTS product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.ProductID).FirstOrDefault();
                var id = (first != null) ? first.ProductID : 0;

                product.ProductID = id + 1;

                if (product.CategoryID == null)
                {
                    product.CategoryID = 1;
                }

                if (product.Category == null)
                {
                    product.Category = new ViewCATEGORIES() { CategoryID = 1, CategoryName = "Beverages" };
                }

                GetAll().Insert(0, product);
            }
            else
            {
                var entity = new Products();

                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.UnitsInStock = (short)product.UnitsInStock;
                entity.Discontinued = product.Discontinued;
                entity.CategoryID = product.CategoryID;

                if (entity.CategoryID == null)
                {
                    entity.CategoryID = 1;
                }

                if (product.Category != null)
                {
                    entity.CategoryID = product.Category.CategoryID;
                }

                Entities.Products.Add(entity);
                Entities.SaveChanges();

                product.ProductID = entity.ProductID;
            }
        }

        public void Update(VievPRODUCTS product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.ProductID == product.ProductID);

                if (target != null)
                {
                    target.ProductName = product.ProductName;
                    target.UnitPrice = product.UnitPrice;
                    target.UnitsInStock = product.UnitsInStock;
                    target.Discontinued = product.Discontinued;

                    if (product.CategoryID == null)
                    {
                        product.CategoryID = 1;
                    }

                    if (product.Category != null)
                    {
                        product.CategoryID = product.Category.CategoryID;
                    }
                    else
                    {
                        product.Category = new ViewCATEGORIES()
                        {
                            CategoryID = (int)product.CategoryID,
                            CategoryName = Entities.Categories.Where(s => s.CategoryID == product.CategoryID).Select(s => s.CategoryName).First()
                        };
                    }

                    target.CategoryID = product.CategoryID;
                    target.Category = product.Category;
                }
            }
            else
            {
                var entity = new Products();

                entity.ProductID = product.ProductID;
                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.UnitsInStock = (short)product.UnitsInStock;
                entity.Discontinued = product.Discontinued;
                entity.CategoryID = product.CategoryID;

                if (product.Category != null)
                {
                    entity.CategoryID = product.Category.CategoryID;
                }

                Entities.Products.Attach(entity);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.SaveChanges();
            }
        }

        public void Destroy(VievPRODUCTS product)
        {
            if (!UpdateDatabase)
            {
                var target = GetAll().FirstOrDefault(p => p.ProductID == product.ProductID);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = new Products();

                entity.ProductID = product.ProductID;

                Entities.Products.Attach(entity);

                Entities.Products.Remove(entity);

                var orderDetails = Entities.Order_Details.Where(pd => pd.ProductID == entity.ProductID);

                foreach (var orderDetail in orderDetails)
                {
                    Entities.Order_Details.Remove(orderDetail);
                }

                Entities.SaveChanges();
            }
        }

        public VievPRODUCTS One(Func<VievPRODUCTS, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            Entities.Dispose();
        }

    }



}

