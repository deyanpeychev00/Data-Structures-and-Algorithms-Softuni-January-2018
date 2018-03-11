using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    public int Count => productsByLabel.Keys.Count;
    private Dictionary<string, Product> productsByLabel;
    private List<Product> productsInOrder;
    private LinkedList<Product> productsByQuantity;
    private LinkedList<Product> alphabeticalProducts;

    public Instock()
    {
        this.productsByLabel = new Dictionary<string, Product>();
        this.productsInOrder = new List<Product>();
        this.productsByQuantity = new LinkedList<Product>();
        this.alphabeticalProducts = new LinkedList<Product>();
    }

    public void Add(Product product)
    {
        if (!this.productsByLabel.ContainsKey(product.Label))
        {
            this.productsByLabel.Add(product.Label, product);
            this.productsInOrder.Add(product);
            this.productsByQuantity.AddLast(product);
            this.alphabeticalProducts.AddLast(product);
        }
        
            
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.productsByLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }
        var dictProduct = this.productsByLabel[product];
        this.productsByLabel.Remove(dictProduct.Label);
        dictProduct.Quantity = quantity;
        // this.productsInOrder.Remove(dictProduct);
        this.productsByLabel.Add(dictProduct.Label, dictProduct);
        this.productsByQuantity.Remove(dictProduct);
        this.productsByQuantity.AddLast(dictProduct);
    }

    public bool Contains(Product product)
    {
        return this.productsByLabel.Keys.Contains(product.Label);
    }

    public Product Find(int index)
    {
        if(index < 0 || index >= this.Count)
        {
            throw new IndexOutOfRangeException();
        }
       // return this.productsByLabel.Values.ToList()[index];
       return this.productsInOrder[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        var products = this.productsByLabel.Values.Where(x => x.Price == price).ToList();
        if (products.Count == 0)
        {
            return new List<Product>();
        }
        return products;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        
        var products = this.productsByQuantity.Where(x => x.Quantity == quantity).ToList();
        if(products.Count == 0)
        {
            return new List<Product>();
        }
        return products;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return this.productsByLabel.Values.Where(x => x.Price > lo && x.Price <= hi).OrderByDescending(x => x.Price);
    }

    public Product FindByLabel(string label)
    {
        if (!this.productsByLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }
        return this.productsByLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count < 0 || count > this.productsByLabel.Values.Count)
        {
            throw new ArgumentException();
        }

        return this.alphabeticalProducts.OrderBy(x => x.Label).Take(count);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if(count < 0 || count > this.productsByLabel.Values.Count)
        {
            throw new ArgumentException();
        }

        return this.productsByLabel.Values.OrderByDescending(x => x.Price).Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in this.productsByLabel.Values)
        {
            yield return product;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
