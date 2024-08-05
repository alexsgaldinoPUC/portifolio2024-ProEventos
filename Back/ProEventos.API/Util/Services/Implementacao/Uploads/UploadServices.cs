using ProEventos.API.Util.Services.Contratos.Uploads;

namespace ProEventos.API.Util.Services.Implementacao.Uploads
{
    public class UploadServices : IUploadServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public UploadServices(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        public void DeleteImagem(string _imageUrl, string _destino)
        {
            if (!string.IsNullOrEmpty(_imageUrl))
            {
                var pathImage = Path.Combine(webHostEnvironment.ContentRootPath, @$"Recursos/{_destino}", _imageUrl);

                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
            };
        }

        public async Task<string> SaveImagem(int eventoId, IFormFile _imageUrl, string _destino)
        {
            string imageNameNew = new String(Path
                .GetFileNameWithoutExtension(_imageUrl.FileName)
                .Take(15)
                .ToArray()
            )
            .Replace(' ', '-');

            imageNameNew = $"{imageNameNew}{DateTime.UtcNow:yymmssfff}{Path.GetExtension(_imageUrl.FileName)}";

            var pathImage = Path.Combine(webHostEnvironment.ContentRootPath, @$"Recursos/{_destino}", imageNameNew);

            using (var fileStream = new FileStream(pathImage, FileMode.Create))
            {
                await _imageUrl.CopyToAsync(fileStream);
            };

            return imageNameNew;
        }
    }
}
