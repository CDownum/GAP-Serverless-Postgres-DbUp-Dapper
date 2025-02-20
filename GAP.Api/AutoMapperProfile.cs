using AutoMapper;
using GAP.Api.Models;

namespace GAP.Api;

/// <summary>
/// 
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<Core.Domain.User, User>().ReverseMap();
    }
}