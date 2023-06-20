using System.Text.Json;

namespace RetailerStores.Frontend.Extensions
{
    public static class JsonSerializerExtention
    {
        private static JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            IgnoreReadOnlyProperties = false,
            PropertyNamingPolicy = null,
            PropertyNameCaseInsensitive = true
        };
        public static async Task<byte[]> Serialize<TPayload>(this TPayload payload, JsonSerializerOptions? options = null)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync(ms, payload, options ?? _defaultOptions);
            return ms.ToArray();
        }

        public static async Task<TPayload?> Deserealize<TPayload>(this byte[] buffer, JsonSerializerOptions? options = null)
        {
            using var ms = new MemoryStream(buffer);
            return await JsonSerializer.DeserializeAsync<TPayload>(ms, options ?? _defaultOptions);
        }
    }
}
