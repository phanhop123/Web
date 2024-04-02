using AutoMapper;
using BusinessObject.Data;
using BusinessObject.Viewmodel;

namespace WebProject.Helpers
{
    public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<UserLogin, User>();
		}
	}
}
