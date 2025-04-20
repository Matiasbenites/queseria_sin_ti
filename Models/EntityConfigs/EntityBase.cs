using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models.EntityConfigs
{
    /// <summary>
    /// Clase abstracta que brinda ID para la entidad.
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

    }
}