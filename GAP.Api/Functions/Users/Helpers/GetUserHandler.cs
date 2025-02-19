using AutoMapper;
using GAP.Core.Database.Repository;

namespace GAP.Api.Functions.Users.Helpers
{
    public interface IGetUserHandler
    {
        //Task<GetUsersResponse> GetUsers();
        //Task<User> GetUser(Guid id);
    }

    public class GetUserHandler(IGapRepository repository, IMapper mapper) : IGetUserHandler
    {
        //public async Task<GetUsersResponse> GetUsers()
        //{
        //    var response = new GetUsersResponse();

        //    var users = await repository.GetUsers();
        //    var mapped = mapper.Map<IEnumerable<User>>(users);
        //    response.Users = mapped?.OrderBy(u => u.LastName);

        //    return response;
        //}

        //public async Task<User> GetUser(Guid id)
        //{
        //    return mapper.Map<User>(await repository.GetUser(facilityId, badgeId));
        //}
    }
}
