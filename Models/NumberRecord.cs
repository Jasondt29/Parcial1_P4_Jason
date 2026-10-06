namespace Parcial1_P4_Jason.Models
{
    public record NumberRecord
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Numero { get; set; }
        public int Resultado { get; set; }
    }
}