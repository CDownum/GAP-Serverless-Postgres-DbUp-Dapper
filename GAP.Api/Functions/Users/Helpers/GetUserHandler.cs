using AutoMapper;
using GAP.Api.Functions.Users.Models;
using GAP.Api.Models;
using GAP.Core.Database.Repository;

namespace GAP.Api.Functions.Users.Helpers
{
    public interface IUserService
    {
        Task<GetUsersResponse> GetUsers(int companyId);
        //Task<User> GetUser(Guid id);
    }

    public class UserService(IUserRepository repository, IMapper mapper) : IUserService
    {
        public async Task<GetUsersResponse> GetUsers(int companyId)
        {
            var response = new GetUsersResponse();

            var users = await repository.GetUsers(companyId);
            response.Users = mapper.Map<IEnumerable<User>>(users);

            return response;
        }

        //public async Task<User> GetUser(Guid id)
        //{
        //    return mapper.Map<User>(await repository.GetUser(facilityId, badgeId));
        //}
    }
}
