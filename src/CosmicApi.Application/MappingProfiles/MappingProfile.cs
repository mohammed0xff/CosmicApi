using AutoMapper;
using CosmicApi.Application.Features.Auth.Signup;
using CosmicApi.Application.Features.Galaxies;
using CosmicApi.Application.Features.Galaxies.CreateGalaxy;
using CosmicApi.Application.Features.Moons;
using CosmicApi.Application.Features.Moons.CreateMoon;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Planets;
using CosmicApi.Application.Features.Planets.CreatePlanet;
using CosmicApi.Application.Features.Stars;
using CosmicApi.Application.Features.Stars.CreateStar;
using CosmicApi.Application.Features.Users;
using CosmicApi.Application.Features.Users.UpdatePassword;
using CosmicApi.Domain.Entities;

namespace CosmicApi.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // User Map
            CreateMap<User, UserResponse>()
                .ReverseMap();
            CreateMap<SignupRequest, User>();

            CreateMap<UpdatePasswordRequest, User>();

            // Galaxy
            CreateMap<Galaxy, GalaxyResponse>()
                .ForMember(x => x.Type,
                opt => opt.MapFrom(
                    source => Enum.GetName(source.Type)
                    ));

            CreateMap<CreateGalaxyRequest, Galaxy>()
                .ForMember(x => x.Type,
                    opt => opt.MapFrom(source => Galaxy.TryParseType(source.Type))
                );

            CreateMap<UpdateGalaxyRequest, Galaxy>()
                .ForMember(x => x.Type,
                    opt => opt.MapFrom(source => Galaxy.TryParseType(source.Type))
                );

            // Star
            CreateMap<CreateStarRequest, Star>();
            CreateMap<Star, StarResponse>();

            // Planet 
            CreateMap<CreatePlanetRequest, Planet>();
            CreateMap<Planet, PlanetResponse>();

            // Moon
            CreateMap<CreateMoonRequest, Moon>();
            CreateMap<Moon, MoonResponse>();
            
            // picture
            var address = AppDomain.CurrentDomain.GetData("BaseUrl")?.ToString();
            if (address == null)
                throw new ArgumentNullException(nameof(AppDomain.CurrentDomain));

            CreateMap<Picture, PictureResponse>()
                .ForMember(dest => dest.URL,
                    opt => opt.MapFrom(
                        (source) => address + "/pictures/" + source.Name
                        )
                );

        }
    }
}