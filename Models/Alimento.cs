namespace WebScraperTesteAPI.Models
{
    public class Alimento
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string? NomeCientifico { get; set; }
        public string Grupo { get; set; }
        public List<Componente> Componentes { get; set; }
    }
}
