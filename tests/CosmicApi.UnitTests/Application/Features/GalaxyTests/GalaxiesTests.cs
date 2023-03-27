using AutoMapper;

using MockQueryable.Moq;
using Moq;
using Xunit;
using Xunit.Categories;

using CosmicApi.Application.Features.Galaxies;
using CosmicApi.Application.Features.Galaxies.CreateGalaxy;
using CosmicApi.Domain.Entities;
using CosmicApi.Domain.Entities.Enums;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Application.Common.Responses;
using CosmicApi.UnitTests.Application.Helpers;

namespace CosmicApi.UnitTests.Application.Features.GalaxyTests
{

    [UnitTest]
    public class GalaxiesTests : FeatureTests
    {
        private IMapper _mapper ;
        private List<Galaxy> source_galaxies = new();
        private Mock<IContext> context = new Mock<IContext>();

        public GalaxiesTests()
        {
            _mapper = CreateMapper();

            source_galaxies.Add(new Galaxy() { Name = "SpiralA", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralB", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralC", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralD", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralE", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralF", Type = GalaxyType.spiral });
            source_galaxies.Add(new Galaxy() { Name = "SpiralG", Type = GalaxyType.spiral });

            source_galaxies.Add(new Galaxy() { Name = "IrregularA", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularB", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularC", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularD", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularE", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularF", Type = GalaxyType.irregular });
            source_galaxies.Add(new Galaxy() { Name = "IrregularG", Type = GalaxyType.irregular });

            source_galaxies.Add(new Galaxy() { Name = "OtherA", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherB", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherC", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherD", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherE", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherF", Type = GalaxyType.other });
            source_galaxies.Add(new Galaxy() { Name = "OtherG", Type = GalaxyType.other });

            var mockDbsetGalaxies = source_galaxies.AsQueryable().BuildMockDbSet();

            context.Setup(x => x.Galaxies).Returns(mockDbsetGalaxies.Object);

        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(1, 10)]
        [InlineData(2, 10)]
        [InlineData(1, 10, "Other")]
        [InlineData(1, 10, "Spiral")]
        [InlineData(1, 10, "Elliptical")]
        public async void ShouldReturnALlGalaxies(int currentPage, int pageSize, string galaxyType = null)
        {
            // Arrange
            GetGalaxyRequest request = new GetGalaxyRequest()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Type = galaxyType
            };

            GetGalaxyHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<PaginatedList<GalaxyResponse>>(response);
            Assert.Equivalent(response.CurrentPage, request.CurrentPage);

            if (galaxyType != null)
            {
                foreach (var item in response.Result)
                {
                    Assert.Equal(item.Type, galaxyType);
                }
            }

        }

        [Fact]
        public async void ShouldReturn_GalaxyById_WhenGalaxyExists()
        {
            // Arrange
            var id = source_galaxies[0].Id;
            GetGalaxyrByIdRequest request = new GetGalaxyrByIdRequest(id);

            GetGalaxyByIdHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<GalaxyResponse>(response);
            Assert.Equal(response.Id, id);

        }

        [Fact]
        public async void ShouldReturn_Null_WhenGalaxyDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            GetGalaxyrByIdRequest request = new GetGalaxyrByIdRequest(id);

            GetGalaxyByIdHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async void ShouldReturn_True_WhenGalaxyDeleted()
        {
            // Arrange
            var galaxy = source_galaxies[0];
            DeleteGalaxyRequest request = new DeleteGalaxyRequest(galaxy.Id);

            DeleteGalaxyHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async void ShouldReturn_True_WhenGalaxyNotfound()
        {
            // Arrange
            var galaxyId = Guid.NewGuid();
            DeleteGalaxyRequest request = new DeleteGalaxyRequest(galaxyId);

            DeleteGalaxyHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.False(response);
        }

        [Theory]
        [InlineData("spiral")]
        [InlineData("Spiral")]
        [InlineData("SPIRAL")]
        [InlineData("Elliptical")]
        [InlineData("Irregular")]
        [InlineData("Other")]
        public void ShouldReturn_GalaxyType_WhenValidString(string type)
        {
            // Arrange
            // Act 
            var res = Galaxy.TryParseType(type);
            // Assert
            Assert.NotNull(res);
            Assert.IsType<GalaxyType>(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("65464")]
        [InlineData("something")]
        [InlineData("another")]
        public void ShouldReturn_Null_WhenStringIsntAValidType(string type)
        {
            // Arrange
            // Act 
            var res = Galaxy.TryParseType(type);
            // Assert
            Assert.Null(res);
        }
    }
}
