using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Product
    {
        public static ML.Result GetById(int productId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = (from product in context.Products
                                 join sup in context.Suppliers on product.SupplierId equals sup.SupplierId
                                 join cat in context.Categories on product.CategoryId equals cat.CategoryId
                                 where product.ProductId == productId
                                 select new
                                 {
                                     product,
                                     sup,
                                     cat
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Products p = new ML.Products();
                        p.Supplier = new ML.Suppliers();
                        p.Category = new ML.Categories();

                        p.ProductID = query.product.ProductId;
                        p.ProductName = query.product.ProductName;
                        p.Supplier.SupplierID = query.product.SupplierId.Value;
                        p.Category.CategoryID = query.product.CategoryId.GetValueOrDefault();
                        p.QuantityPerUnit = query.product.QuantityPerUnit;
                        p.UnitPrice = query.product.UnitPrice.GetValueOrDefault();
                        p.UnitsInStock = query.product.UnitsInStock.GetValueOrDefault();
                        p.UnitsOnOrder = query.product.UnitsOnOrder.GetValueOrDefault();
                        p.ReorderLevel = query.product.ReorderLevel.GetValueOrDefault();
                        p.Discontinued = query.product.Discontinued;

                        p.Supplier.SupplierID = query.sup.SupplierId;
                        p.Supplier.CompanyName = query.sup.CompanyName;

                        p.Category.CategoryID = query.cat.CategoryId;
                        p.Category.CategoryName = query.cat.CategoryName;

                        result.Object = p;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;

                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = (from Products in context.Products
                                 join sup in context.Suppliers on Products.SupplierId equals sup.SupplierId
                                 join cat in context.Categories on Products.CategoryId equals cat.CategoryId
                                 select new
                                 {
                                     Products,
                                     sup,
                                     cat
                                 }).ToList();

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obt in query)
                        {
                            ML.Products product = new ML.Products();

                            product.Supplier = new ML.Suppliers();
                            product.Category = new ML.Categories();

                            product.ProductID = obt.Products.ProductId;
                            product.ProductName = obt.Products.ProductName;
                            product.Supplier.CompanyName = obt.Products.Supplier?.CompanyName;
                            product.Category.CategoryName = obt.Products.Category?.CategoryName;
                            product.QuantityPerUnit = obt.Products.QuantityPerUnit;
                            product.UnitPrice = Convert.ToDecimal(obt.Products.UnitPrice);
                            product.UnitsInStock = Convert.ToInt16(obt.Products.UnitsInStock);
                            product.UnitsOnOrder = Convert.ToInt16(obt.Products.UnitsOnOrder);
                            product.ReorderLevel = Convert.ToInt16(obt.Products.ReorderLevel);
                            product.Discontinued = Convert.ToBoolean(obt.Products.Discontinued);


                            result.Objects.Add(product);



                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }


            return result;
        }

        public static ML.Result Add(ML.Products product)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {

                    //var filasafectadas = context.ProductsAdd(product.ProductName, product.Supplier.SupplierID, product.Category.CategoryID, product.QuantityPerUnit, product.UnitPrice, product.UnitsInStock, product.UnitsOnOrder, product.ReorderLevel, product.Discontinued);

                    var filasafectadas = context.Database.ExecuteSqlRaw(@"
            EXEC ProductsAdd
                @ProductName, 
                @SupplierID, 
                @CategoryID, 
                @QuantityPerUnit, 
                @UnitPrice, 
                @UnitsInStock, 
                @UnitsOnOrder, 
                @ReorderLevel, 
                @Discontinued",
                  new SqlParameter("@ProductName", product.ProductName),
                  new SqlParameter("@SupplierID", product.Supplier?.SupplierID),
                  new SqlParameter("@CategoryID", product.Category?.CategoryID),
                  new SqlParameter("@QuantityPerUnit", product.QuantityPerUnit),
                  new SqlParameter("@UnitPrice", product.UnitPrice),
                  new SqlParameter("@UnitsInStock", product.UnitsInStock),
                  new SqlParameter("@UnitsOnOrder", product.UnitsOnOrder),
                  new SqlParameter("@ReorderLevel", product.ReorderLevel),
                  new SqlParameter("@Discontinued", product.Discontinued));

                    if (filasafectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }


            return result;
        }

        public static ML.Result Update(ML.Products product)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {

                    //var filasafectadas = context.ProductsAdd(product.ProductName, product.Supplier.SupplierID, product.Category.CategoryID, product.QuantityPerUnit, product.UnitPrice, product.UnitsInStock, product.UnitsOnOrder, product.ReorderLevel, product.Discontinued);

                    var filasafectadas = context.Database.ExecuteSqlRaw(@"
            EXEC ProductsUpdate
                @ProductID, 
                @ProductName, 
                @SupplierID, 
                @CategoryID, 
                @QuantityPerUnit, 
                @UnitPrice, 
                @UnitsInStock, 
                @UnitsOnOrder, 
                @ReorderLevel, 
                @Discontinued",
                  new SqlParameter("@ProductID", product.ProductID),
                  new SqlParameter("@ProductName", product.ProductName),
                  new SqlParameter("@SupplierID", product.Supplier?.SupplierID),
                  new SqlParameter("@CategoryID", product.Category?.CategoryID),
                  new SqlParameter("@QuantityPerUnit", product.QuantityPerUnit),
                  new SqlParameter("@UnitPrice", product.UnitPrice),
                  new SqlParameter("@UnitsInStock", product.UnitsInStock),
                  new SqlParameter("@UnitsOnOrder", product.UnitsOnOrder),
                  new SqlParameter("@ReorderLevel", product.ReorderLevel),
                  new SqlParameter("@Discontinued", product.Discontinued));

                    if (filasafectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }


            return result;
        }

        public static ML.Result Delete(int productId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {

                    var var = (from product in context.OrderDetails
                               where product.ProductId == productId
                               select product).ToList();


                    foreach (var obj in var)
                    {
                        context.OrderDetails.Remove(obj);
                    }


                    var prod = (from p in context.Products
                                where p.ProductId == productId
                                select p).SingleOrDefault();

                    if (prod != null)
                    {
                        context.Products.Remove(prod);
                    }
                    else
                    {
                        result.Correct = false;

                        return result;
                    }

                    int filas = context.SaveChanges();

                    if (filas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;

                result.Ex = ex;
            }

            return result;
        }

    }
}
