using System.Text.Encodings.Web;
using System.Text.Json;

namespace RetailerStores.Frontend.Services
{
    public abstract class BaseService
    {
        protected HttpClient _httpClient;
        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected readonly JsonSerializerOptions DefaultOptions = new()
        {
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
    }
}
