using System.Text.Json.Serialization;

namespace test_ASPNET_api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    USER,
    ADMIN
}