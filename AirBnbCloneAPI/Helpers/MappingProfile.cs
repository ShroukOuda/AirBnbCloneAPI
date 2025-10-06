using AutoMapper;

namespace AirBnbCloneAPI.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //RegisterDto ==> Source
        //User ==> Destination
        CreateMap<RegisterDto, User>();
    }
}