
namespace GAP.Api.Models
{
    public class BaseResponse
    {
        public bool IsSuccessful => !Errors.Any();

        public List<string> Errors { get; set; } = [];

        public List<string> Warnings { get; set; } = [];
    }
}
