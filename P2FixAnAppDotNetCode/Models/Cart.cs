using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Razor.Language;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        private List<CartLine> CartLineList = new List<CartLine>();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return CartLineList;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {

            var results = GetCartLineList().Where(l => l.Product.Id == product.Id).FirstOrDefault();

            if (!(results is null))
            {
                GetCartLineList().First(l => l.Product.Id == product.Id).Quantity += quantity;
            }
            else
            {
                CartLine cl = new CartLine
                {
                    OrderLineId = GetCartLineList().Count + 1
                };
                cl.Quantity = quantity;
                cl.Product = product;

                GetCartLineList().Add(cl);
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double count = 0;

            if (GetCartLineList().Any())
            {
                count = GetCartLineList().Sum(l => l.Product.Price * l.Quantity);
            }

            return count;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            double average = 0;

            if (GetCartLineList().Any())
            {
                double sum = GetTotalValue();
                double count = GetCartLineList().Sum(l => l.Quantity);
                average = sum / count;
            }

            return average;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return GetCartLineList().Where(l => l.Product.Id == productId).FirstOrDefault().Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
