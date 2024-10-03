using System.Text.Json.Serialization;

namespace WebScraperTesteAPI.Models
{
    public class Componente
    {
        public int Id { get; set; }
        public string NomeComponente { get; set; }
        public int AlimentoId { get; set; }

        [JsonIgnore]
        public Alimento Alimento { get; set; }
    }
}
