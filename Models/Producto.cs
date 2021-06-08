using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int CategoriaForeignKey { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Pedido> Pedido { get; set; }
        public List<Detalle> Detalle { get; set; }
    }
}