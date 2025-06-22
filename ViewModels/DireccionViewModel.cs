namespace QueseriaSoftware.ViewModels
{
    public class DireccionViewModel
    {
        public int Id { get; set; }

        public string Calle { get; set; } = null!;

        public int Numero { get; set; }

        public string TelefonoContacto { get; set; } = null!;

        public LocalidadViewModel Localidad { get; set; } = null!;
    }

}