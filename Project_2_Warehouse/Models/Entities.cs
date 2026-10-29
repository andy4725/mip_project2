using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_2_Warehouse.Models
{
    public class CategorieProdus
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(50)]
        public string denumire { get; set; }

        public virtual ICollection<Produs> produse { get; set; }

        public CategorieProdus()
        {
            produse = new HashSet<Produs>();
        }

        public override string ToString()
        {
            return denumire;
        }
    }

    public class Produs
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(30)]
        public string denumire { get; set; }

        [StringLength(500)]
        public string descriere { get; set; }

        public DateTime dataIntrare { get; set; }
        public DateTime dataExpirare { get; set; }
        public int cantitate { get; set; }
        public int idCategorie { get; set; }

        public virtual CategorieProdus categorie { get; set; }
    }

    public class IstoricVanzari
    {
        [Key]
        public int id { get; set; }
        public int idProdus { get; set; }
        public int cantitate { get; set; }
        public DateTime dataVanzare { get; set; }
    }
}
