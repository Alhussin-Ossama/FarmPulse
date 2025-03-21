using FarmPulse.API.Errors;
using FarmPulse.API.Profiles;
using FarmPulse.Core;
using FarmPulse.Core.Interfaces;
using FarmPulse.Repository;
using FarmPulse.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace FarmPulse.API.Extentions
{
    public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddApplictionServices(this IServiceCollection Services)
		{
			Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			Services.AddScoped<IChickenRepository, ChickenRepository>();
			Services.AddScoped<IWeightRepository, WeightRepository>();
			Services.AddScoped<INotificationRepository, NotificationRepository>();
			Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
			Services.AddScoped<IUnitOfWork,UnitOfWork>();
			Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder => builder.AllowAnyOrigin()
									  .AllowAnyMethod()
									  .AllowAnyHeader());
			});
			Services.AddAutoMapper(typeof(MappingProfiles));
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														 .SelectMany(P => P.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToArray();
					var ValidationErrorRespons = new ApiErrorValidationRespons()
					{
						Message = "Validation failed. See errors for details.",
						Errors = errors
					};
					return new BadRequestObjectResult(ValidationErrorRespons);
				};
			});
			return Services;
		}
	}
}
