using Loja.Model;
using LojaTopMoveis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public bool FeaturedProduct { get; set; } = false;
        //Estudar a melhor forma de armazenar imagens
        public Category? Category { get; set; }

        public Guid? CategoryID { get; set; }
        public int Amount { get; set; }
        public bool Inactive { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
        public List<Photo>? Photos { get; set; }
        public List<Color>? Colors { get; set; }
        public List<SubcategoriesProduct>? SubcategoriesProducts { get; set; }

    }
}
