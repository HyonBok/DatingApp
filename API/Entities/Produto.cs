namespace API.Entities
{
    public class Produto
    {
        // public Produto(string nome, string marca, float preco, 
        //     string unidadeVenda, AppUser appUser)
        // {
        //     Nome = nome;
        //     Marca = marca;
        //     Preco = preco;
        //     UnidadeVenda = unidadeVenda;
        //     AppUser = appUser;
        // }
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public float Desconto { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public string UnidadeVenda { get; set; }
        public string FotoUrl { get; set; }
        public string Sessao { get; set; }
        public AppUser AppUser { get; set; }
        public List<Foto> Fotos { get; set; } = new();
    }
}