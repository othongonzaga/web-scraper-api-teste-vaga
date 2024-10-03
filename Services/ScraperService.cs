using HtmlAgilityPack;
using WebScraperTesteAPI.Models;

namespace WebScraperTesteAPI.Services
{
    public class ScraperService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://www.tbca.net.br/base-dados/composicao_estatistica.php?pagina=";

        public ScraperService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Alimento>> ExtrairAlimentosAsync(int pagina = 1)
        {
            var alimentos = new List<Alimento>();

            try
            {
                var html = await _client.GetStringAsync($"{_baseUrl}{pagina}&atuald=1");
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var tabelaAlimentos = doc.DocumentNode.SelectNodes("//table[@class='table table-striped']/tbody/tr");

                if (tabelaAlimentos == null) return alimentos;

                foreach (var linha in tabelaAlimentos)
                {
                    var colunaCodigo = linha.SelectSingleNode("td[1]/a");
                    var codigo = colunaCodigo?.InnerText.Trim();
                    var urlComponente = colunaCodigo?.Attributes["href"].Value;
                    var nomeAlimento = linha.SelectSingleNode("td[2]")?.InnerText.Trim();
                    var nomeCientifico = linha.SelectSingleNode("td[3]")?.InnerText.Trim();
                    var grupo = linha.SelectSingleNode("td[4]")?.InnerText.Trim();

                    var alimento = new Alimento
                    {
                        Codigo = codigo,
                        Nome = nomeAlimento,
                        NomeCientifico = nomeCientifico,
                        Grupo = grupo,
                        Componentes = await ExtrairComponentesAsync(urlComponente)
                    };

                    alimentos.Add(alimento);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro ao acessar a URL: {e.Message}");
            }

            return alimentos;
        }

        private async Task<List<Componente>> ExtrairComponentesAsync(string url)
        {
            var componentes = new List<Componente>();

            var urlCompleta = $"https://www.tbca.net.br/base-dados/{url}";

            Console.WriteLine($"Acessando a URL: {urlCompleta}");

            try
            {
                var html = await _client.GetStringAsync(urlCompleta);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var tabelaComponentes = doc.DocumentNode.SelectNodes("//table[@id='tabela1']/tbody/tr");

                if (tabelaComponentes != null)
                {
                    foreach (var linha in tabelaComponentes)
                    {
                        var nomeComponente = linha.SelectSingleNode("td[1]")?.InnerText.Trim();

                        var componente = new Componente
                        {
                            NomeComponente = nomeComponente
                        };

                        componentes.Add(componente);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro ao acessar a URL de componentes: {e.Message}");
            }

            return componentes;
        }
    }
}
