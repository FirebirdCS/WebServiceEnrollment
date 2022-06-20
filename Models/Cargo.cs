namespace Web_Final.Models
{
    public class Cargo
    {
        public string CargoId { get; set; }

        public string Descripcion { get; set; }

        public string Prefijo { get; set; }

        public double Monto { get; set; }

        public bool IsGeneraMora { get; set; }

        public int PorcentajeMora { get; set; }
    }
}