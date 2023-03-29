using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Fotos")]
    public class Foto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}