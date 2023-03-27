namespace API.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public bool EmPromocao { get; set; }
        public float PrecoPromocao { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public string UnidadeVenda { get; set; }
        public string FotoUrl { get; set; }
        public string Sessao { get; set; }
        public AppUser AppUser { get; set; }
        public List<Photo> Fotos { get; set; } = new();
    }
}