using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProWeb.Models
{
    public class Pedido
    {
        public int Id { get; set; }
         public DateTime Fecha { get; set; }
        public decimal Total { get; set; } 
        public ICollection<Producto> producto { get; set; }
        public List<Detalle> Detalle { get; set; }
      
    }
}