using System.Text.Json;
using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Serialization
{
    internal static class MeSizesJson
    {
        internal static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        internal static List<SubClienteGarmentSizeUpdateDto>? ParseSubClienteGarmentUpdates(JsonElement body)
        {
            return body.ValueKind switch
            {
                JsonValueKind.Array => JsonSerializer.Deserialize<List<SubClienteGarmentSizeUpdateDto>>(
                    body.GetRawText(), Options),
                JsonValueKind.Object => JsonSerializer.Deserialize<MeSizesPutRequest>(
                    body.GetRawText(), Options)?.Garments,
                _ => null
            };
        }
    }
}
