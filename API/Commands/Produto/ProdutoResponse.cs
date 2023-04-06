namespace API.Commands
{
    public class ProdutoResponse
    {   
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public float Desconto { get; set; } = 0;
        public string PrecoDesconto { get; set; }
        public string Marca { get; set; }
        public string Sessao { get; set; }
        public string Descricao { get; set; }
        public string UnidadeVenda { get; set; }
        public string FotoUrl { get; set; }
        public string Usuario { get; set; }
        public List<ProdutoFotoResponse> Fotos { get; set; } = new List<ProdutoFotoResponse>();
    }
}