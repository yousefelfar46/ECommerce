using System.Text.Json.Serialization;

namespace ECommerce.Application.Common
{
    public sealed record Error(string Code, string Description, ErrorType ErrorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure", string description = "General Failure Has Occured")
        => new Error(code, description);
        public static Error NotFound(string code = "General.NotFound", string description = "Resource Not Found")
       => new Error(code, description, ErrorType.NotFound);

        public static Error Validation(string code = "General.Validation", string description = "Validation Error")
       => new Error(code, description, ErrorType.Validation);
        public static Error Conflict(string code = "General.Conflict", string description = "Conflict Has Occured")
       => new Error(code, description, ErrorType.Conflict);
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Unauthorized User")
       => new Error(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "Forbidden")
       => new Error(code, description, ErrorType.Forbidden);
        public static Error InvalidCredintails(string code = "General.InvalidCredintails", string description = "InvalidCredintails")
       => new Error(code, description, ErrorType.InvalidCredintails);

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
        Forbidden = 5,
        InvalidCredintails = 6
    }
}