using AutoMapper;
using CosmicApi.Application.MappingProfiles;


namespace CosmicApi.UnitTests.Application.Helpers
{
    public class FeatureTests
    {
        public IMapper CreateMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            // prevent throwing ArgumentNullException when configuring <Picture , PictureResponse> mapping
            AppDomain.CurrentDomain.SetData("BaseUrl", "");
            return mappingConfig.CreateMapper();
        }
    }
}
