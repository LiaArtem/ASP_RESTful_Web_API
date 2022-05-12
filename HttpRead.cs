using System.Text;

namespace ASP_RESTful_Web_API
{
    public class HttpRead
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task<byte[]> DownloadFileAsyncByte(string uri)
        {
            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            return fileBytes;
        }

        public static async Task<string> DownloadFileAsyncString(string uri)
        {
            string file = await _httpClient.GetStringAsync(uri);
            return file;
        }

        public static async Task<string> DownloadFileAsyncString(string uri, string enc)
        {
            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);            
            Encoding encoding = Encoding.GetEncoding(enc);
            return encoding.GetString(fileBytes, 0, fileBytes.Length);
        }

        public static async Task<string> SetPOSTAsync(string uri, string request, Encoding enc, string content = "application/json")
        {
            // пример enc = Encoding.UTF8
            var response = await _httpClient.PostAsync(uri, new StringContent(request, enc, content));
            return response.ToString();
        }
    }
}
