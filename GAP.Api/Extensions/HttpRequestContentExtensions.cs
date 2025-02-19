using System.Net;
using System.Security.Claims;
using System.Text;
using GAP.Api.Authentication;
using GAP.Api.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Serilog;

namespace GAP.Api.Extensions
{
    public static class HttpRequestContentExtensions
    {
        public static async Task<HttpResponseData> MapResponse<T>(this HttpRequestData req, T response, ILogger log, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (statusCode != HttpStatusCode.NotFound && response is BaseResponse)
            {
                statusCode = response is BaseResponse { IsSuccessful: true }
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest;
            }

            var responseData = req.CreateResponse(statusCode);

            try
            {
                await responseData.WriteAsJsonAsync(response);
            }
            catch (Exception)
            {
                var body = JsonConvert.SerializeObject(response);
                var byteArray = Encoding.UTF8.GetBytes(body);
                responseData.Body = new MemoryStream(byteArray);
            }

            if (responseData.StatusCode != statusCode)
                responseData.StatusCode = statusCode;

            if (response is not BaseResponse r) return responseData;

            LogMessages(r, log);

            return responseData;
        }

        public static async Task<HttpResponseData> MapHttpResponse<T>(this HttpRequestData req, T response, ILogger log, HttpStatusCode statusCode)
        {
            var responseData = req.CreateResponse(statusCode);
            await responseData.WriteAsJsonAsync(response);
            if (response is not BaseResponse r) return responseData;

            LogMessages(r, log);

            return responseData;
        }

        private static void LogMessages(BaseResponse response, ILogger log)
        {
            if (response.Warnings is { Count: > 0 })
            {
                foreach (var warning in response.Warnings)
                {
                    log.Warning(warning);
                }

            }

            if (response.Errors is { Count: > 0 })
            {
                foreach (var error in response.Errors)
                {
                    log.Error(error);
                }
            }
        }

        public static TokenResponse ValidateAuthorizationRole(this HttpRequestData req, IJwtAuthentication authentication, string roles)
        {
            var authorizationHeader = req.Headers.FirstOrDefault(x => x.Key == "Authorization");
            var parts = authorizationHeader.Value.FirstOrDefault()?.Split(' ') ?? Array.Empty<string>();
            var token = parts[0].Equals("Bearer") ? parts[1] : string.Empty;

            var tokenResponse = authentication.ValidateToken(token);
            var identity = new ClaimsIdentity(tokenResponse.JwtSecurityToken?.Claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);

            var claimRoles = principal.Claims.Where(c => c.Type == "role");

            if (claimRoles.Any(x => x.Value == roles)) return tokenResponse;
            tokenResponse.JwtSecurityToken = null;
            tokenResponse.Exception = $"Unauthorized for Role: {roles}";

            return tokenResponse;
        }

        public static TokenResponse ValidateAuthorization(this HttpRequestData req, IJwtAuthentication authentication)
        {
            var authorizationHeader = req.Headers.FirstOrDefault(x => x.Key == "Authorization");
            var parts = authorizationHeader.Value.FirstOrDefault()?.Split(' ') ?? Array.Empty<string>();
            var token = parts[0].Equals("Bearer") ? parts[1] : string.Empty;

            return authentication.ValidateToken(token);
        }
    }
}