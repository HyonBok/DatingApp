using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class RegistrarProdutoDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public float Preco { get; set; }
        public string Foto { get; set; }
        public string Marca { get; set; }
        [Required]
        public string UnidadeVenda { get; set; }
        public string UserName { get; set; }
    }
}