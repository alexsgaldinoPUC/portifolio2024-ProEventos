namespace ProEventos.API.Util.Services.Contratos.Uploads
{
    public interface IUploadServices
    {
        void DeleteImagem(string _imageUrl, string _destino);

        Task<string> SaveImagem(int eventoId, IFormFile _imageUrl, string _destino);
    }
}
