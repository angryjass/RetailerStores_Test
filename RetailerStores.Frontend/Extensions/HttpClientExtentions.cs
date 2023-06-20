using Microsoft.AspNetCore.Mvc;
using RetailerStores.Frontend.Extensions;

namespace RetailerStores.Frontend.Extensions
{
    public static class ResponseUtils
    {
        public static async Task<T> GetResponse<T>(this HttpClient client, HttpRequestMessage request)
        {
            using var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                throw new Exception(problemDetails.Detail);
            }

            var responseData = await response.Content.ReadAsByteArrayAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent || responseData.Length == 0)
            {
                return default!;
            }

            var content = await responseData.Deserealize<T>();

            return content ?? throw new Exception($"Content cannot convert to type {typeof(T).Name}");
        }
    }
}
