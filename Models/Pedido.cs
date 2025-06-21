using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using QueseriaSoftware.Models.EstadosPedido;

namespace QueseriaSoftware.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime ModificadoEn { get; set; }

        // Este es el estado que se guarda en la base de datos
        public string Estado { get; set; } = "Nuevo";

        // Este no se mapea en la BD, sirve para manejar la lógica de estados
        [NotMapped]
        private IEstadoPedido? _estadoPedido;

        [NotMapped]
        public IEstadoPedido EstadoPedido
        {
            get
            {
                if (_estadoPedido == null)
                {
                    _estadoPedido = Estado switch
                    {
                        "Nuevo" => new NuevoState(),
                        "En preparación" => new EnPreparacionState(),
                        "Enviado" => new EnviadoState(),
                        "Entregado" => new EntregadoState(),
                        _ => new NuevoState()
                    };
                }
                return _estadoPedido;
            }
            set
            {
                _estadoPedido = value;
                Estado = value.ObtenerEstado(); // Actualiza la cadena Estado
            }
        }

        public decimal Total { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public int? IdDireccion { get; set; }

        [ForeignKey("IdDireccion")]
        public Direccion? Direccion { get; set; }
        public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
    }
}
