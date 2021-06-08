using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProWeb.Models
{
    public class Detalle
    {
      
         public int Id { get; set; }
        public int Cantidad { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
         public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        

    }
}