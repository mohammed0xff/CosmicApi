using AutoMapper;
using CosmicApi.Application.MappingProfiles;


namespace CosmicApi.UnitTests.Application.Helpers
{
    public class FeatureTests
    {
        public IMapper CreateMapper()
        {
            // prevent throwing ArgumentNullException when configuring <Picture , PictureResponse> mapping
            AppDomain.CurrentDomain.SetData("BaseUrl", "BaseUrl");
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
