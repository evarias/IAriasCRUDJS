using DL;

namespace BL
{
    public class Product
    {
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
    }
}
