using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CBD.Model.Utilities
{
    public static class FileUpload
    {
        public static MultipartFormDataStreamProvider GetMultipartProvider(string urlPath)
        {
            var uploadFolder = urlPath;

            if (Directory.Exists(uploadFolder) == false) Directory.CreateDirectory(uploadFolder);

            return new MultipartFormDataStreamProvider(uploadFolder);
        }

        public static string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        private static string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}