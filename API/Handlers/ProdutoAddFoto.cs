using API.Commands;
using API.Entities;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoAddFoto : IRequestHandler<ProdutoAddFotoRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;
        private readonly IPhotoService _photoService;

        public ProdutoAddFoto(IProdutoRepository repository, IPhotoService photoService)
        {
            _photoService = photoService;
            _repository = repository;
        }

        public async Task<ProdutoResponse> Handle(ProdutoAddFotoRequest request, CancellationToken cancellationToken)
        {
            var produto = await _repository.GetProdutoByIdAsync(request.IdProduto);

            if(produto == null) return null;

            var result = await _photoService.AddPhotoAsync(request.file);

            if(result.Error != null) return null;

            var foto = new Foto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if(produto.Fotos.Count == 0) foto.IsMain = true;

            produto.Fotos.Add(foto);
            await _repository.SaveAllAsync();

            return null;
        }
    }
}