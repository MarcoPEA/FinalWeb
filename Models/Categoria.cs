
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProWeb.Models
{
     public class Categoria
    {
       public int Id { get; set; }
        public string Nombrecategoria { get; set; }
         public List<Producto> Producto { get; set; }

    }
}