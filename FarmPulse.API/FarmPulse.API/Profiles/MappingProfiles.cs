using AutoMapper;
using FarmPulse.API.DTOs;
using FarmPulse.Core.Models;

namespace FarmPulse.API.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Chicken, ChickenToReturnDto>();

			CreateMap<ChickenInputDto, Chicken>()
				.ForMember(dest => dest.CurrentWeight, opt => opt.MapFrom(src => src.Weight))
				.ForMember(dest => dest.ActivityStatus, opt => opt.MapFrom(src => ActivityStatus.Alive.ToString()))
				.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Now));
			

			CreateMap<Chicken, Weight>()
				.ForMember(dest => dest.EntryWeight, opt => opt.MapFrom(src => src.CurrentWeight))
				.ForMember(dest => dest.ChickenId, opt => opt.MapFrom(src => src.ChickenId)) 
				.ForMember(dest => dest.EntryTime, opt => opt.MapFrom(src => DateTime.Now));

			CreateMap<Weight, WeighToReturnDto>();

			CreateMap<Notification, NotificationDto>();

			CreateMap<Statistics, StatisticsDto>();

		}
	}
}
