using API.Entities;

namespace API.DTOs
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public float PrecoPromocao { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public string UnidadeVenda { get; set; }
        public string Foto { get; set; }
    }
}