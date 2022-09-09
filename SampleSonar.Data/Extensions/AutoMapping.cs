using AutoMapper;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Requests;

namespace SampleSonar.Data.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
