using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_2_Warehouse.Data;
using Project_2_Warehouse.Models;

namespace Project_2_Warehouse.Services
{
    public static class WarehouseService
    {
        public static Task<List<ProductView>> GetProductsAsync(string filter)
        {
            // Run database work in the background so the form stays responsive.
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext())
                {
                    var query = db.produse.Include("categorie").AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        filter = filter.Trim();
                        query = query.Where(p => p.denumire.Contains(filter) || p.descriere.Contains(filter) || p.categorie.denumire.Contains(filter));
                    }
                    return query.OrderBy(p => p.denumire).Select(p => new ProductView
                    {
                        id = p.id, denumire = p.denumire, descriere = p.descriere,
                        dataIntrare = p.dataIntrare, dataExpirare = p.dataExpirare,
                        cantitate = p.cantitate, idCategorie = p.idCategorie,
                        categorie = p.categorie.denumire
                    }).ToList();
                }
            });
        }

        public static Task<List<CategorieProdus>> GetCategoriesAsync()
        {
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext()) return db.categorii.OrderBy(c => c.denumire).ToList();
            });
        }

        public static Task AddCategoryAsync(string name)
        {
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext())
                {
                    db.categorii.Add(new CategorieProdus { denumire = name.Trim() });
                    db.SaveChanges();
                }
            });
        }

        public static Task AddProductAsync(Produs product)
        {
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext()) { db.produse.Add(product); db.SaveChanges(); }
            });
        }

        public static Task AddExistingProductQuantityAsync(int idProdus, int quantity)
        {
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext())
                {
                    var product = db.produse.Find(idProdus);
                    if (product == null) throw new InvalidOperationException("The product does not exist.");
                    product.cantitate += quantity;
                    db.SaveChanges();
                }
            });
        }

        public static Task SellProductAsync(int idProdus, int quantity)
        {
            // Save the sale and update the stock together.
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext())
                using (var transaction = db.Database.BeginTransaction())
                {
                    var product = db.produse.Find(idProdus);
                    if (product == null) throw new InvalidOperationException("The product no longer exists.");
                    if (quantity <= 0 || quantity > product.cantitate) throw new InvalidOperationException("The quantity to sell is invalid.");
                    db.istoricVanzari.Add(new IstoricVanzari { idProdus = product.id, cantitate = quantity, dataVanzare = DateTime.Now });
                    product.cantitate -= quantity;
                    if (product.cantitate == 0) db.produse.Remove(product);
                    db.SaveChanges();
                    transaction.Commit();
                }
            });
        }

        public static Task<List<SaleView>> GetSalesAsync()
        {
            return AsyncTask.Run(() =>
            {
                using (var db = new WarehouseContext())
                {
                    return (from sale in db.istoricVanzari
                            join product in db.produse on sale.idProdus equals product.id into productGroup
                            from product in productGroup.DefaultIfEmpty()
                            join category in db.categorii on product.idCategorie equals category.id into categoryGroup
                            from category in categoryGroup.DefaultIfEmpty()
                            orderby sale.dataVanzare descending
                             select new SaleView { id = sale.id, idProdus = sale.idProdus, cantitate = sale.cantitate, dataVanzare = sale.dataVanzare, categorie = category == null ? "Deleted Product" : category.denumire, denumireProdus = product == null ? "Deleted Product" : product.denumire }).ToList();
                }
            });
        }
    }

    public class ProductView
    {
        public int id { get; set; }
        public string denumire { get; set; }
        public string descriere { get; set; }
        public DateTime dataIntrare { get; set; }
        public DateTime dataExpirare { get; set; }
        public int cantitate { get; set; }
        public int idCategorie { get; set; }
        public string categorie { get; set; }
    }

    public class SaleView
    {
        public int id { get; set; }
        public int idProdus { get; set; }
        public string denumireProdus { get; set; }
        public string categorie { get; set; }
        public int cantitate { get; set; }
        public DateTime dataVanzare { get; set; }
    }
}
