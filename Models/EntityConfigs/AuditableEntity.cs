namespace QueseriaSoftware.Models.EntityConfigs
{
    /// <summary>
    /// Clase abstracta que brinda propiedades basicas para el control del estado de las entidades.
    /// </summary>
    /// <remarks>
    /// Esta clase incluye las propiedades: fecha de creación, fecha de modificación y activo.
    /// </remarks>
    public abstract class AuditableEntity : EntityBase
    {
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public DateTime? ModificadoEn { get; set; }

        public bool Activo { get; set; } = true;
    }

}
