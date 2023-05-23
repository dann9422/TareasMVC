using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TareasMVC.Models;

namespace TareasMVC.Servicios
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        private string connectionString;
        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task<AlmacenarArchivoResultado[]> Almacenar(string contenedor, IEnumerable<IFormFile> archivos)
        {
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var tareas = archivos.Select(async archivo =>
            {
                var nombreArchivoOriginal = Path.GetFileName(archivo.FileName);
                var extension = Path.GetExtension(archivo.FileName);
                var nombreArchivo = $"{Guid.NewGuid()}{extension}";
                var blod = cliente.GetBlobClient(nombreArchivo);
                var blodHttpHeaders = new BlobHttpHeaders();
                blodHttpHeaders.ContentType = archivo.ContentType;
                await blod.UploadAsync(archivo.OpenReadStream(), blodHttpHeaders);
                return new AlmacenarArchivoResultado
                {
                    Url = blod.Uri.ToString(),
                    Titulo = nombreArchivo
                };


            });
            var resultados = await Task.WhenAll(tareas);
            return resultados;
        }

        public async Task Borrar(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var nombreArchivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(nombreArchivo);
            await blob.DeleteIfExistsAsync();
        }
    }
}
