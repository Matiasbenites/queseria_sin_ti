using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueseriaSoftware.Models
{
    public class CarritoLinea : EntityBase
    {
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        public int CarritoId { get; set; }

        [ForeignKey("CarritoId")]
        public Carrito? Carrito { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }
    }
}
