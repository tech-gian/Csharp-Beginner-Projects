using System;

namespace ACM.BL
{
    public class ProductRepository
    {
        // Retrieve one product
        public Product Retrieve(int productId)
        {
            Product product = new Product(productId);

            // Code that retrieves the defined product

            if (productId == 2)
            {
                product.ProductName = "SunFlowers";
                product.ProductDescription = "Assorted Size Set of 4 Bright Yellow Mini SunFlowers";
                product.CurrentPrice = 15.96M;
            }

            return product;
        }

        // Saves the current product
        public bool Save(Product product)
        {
            var success = true;

            if (product.HasChanges)
            {
                if (product.IsValid)
                {
                    if (product.IsNew)
                    {
                        // Call an Insert Stored Procedure
                    }
                    else
                    {
                        // Call an Update Stored Procedure
                    }
                }
                else
                {
                    success = false;
                }
            }
            else
            {
                success = false;
            }

            return success;
        }
    }
}
