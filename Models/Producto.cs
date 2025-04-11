//namespace QueseriaSoftware.Models
//{
//    public class Producto
//    {
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }
    }
}
