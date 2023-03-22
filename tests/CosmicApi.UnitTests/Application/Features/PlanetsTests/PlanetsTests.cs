using AutoMapper;

using MockQueryable.Moq;
using Moq;
using Xunit;
using Xunit.Categories;

using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Application.Common.Responses;
using CosmicApi.UnitTests.Application.Helpers;
using CosmicApi.Application.Features.Planets;
using CosmicApi.Application.Features.Planets.CreatePlanet;

namespace CosmicApi.UnitTests.Application.Features.GalaxyTests
{

    [UnitTest]
    public class PlanetsTests : FeatureTests
    {
        private IMapper _mapper ;
        private List<Planet> source_planets = new();
        private Mock<IContext> context = new Mock<IContext>();

        public PlanetsTests()
        {
            _mapper = CreateMapper();

            List<Planet> planets = new() {
                new Planet() { Name = "PlanetA", NumberOfMoons = 5 },
                new Planet() { Name = "PlanetB", NumberOfMoons = 15 },
                new Planet() { Name = "PlanetC", NumberOfMoons = 5 },
                new Planet() { Name = "PlanetD", NumberOfMoons = 55 },
                new Planet() { Name = "PlanetE", NumberOfMoons = 1 },
                new Planet() { Name = "PlanetF", NumberOfMoons = 35 },
                new Planet() { Name = "PlanetG", NumberOfMoons = 0 },
                new Planet() { Name = "PlanetH", NumberOfMoons = 5 },
                new Planet() { Name = "PlanetJ", NumberOfMoons = 15 },
                new Planet() { Name = "PlanetK", NumberOfMoons = 5 },
                new Planet() { Name = "PlanetL", NumberOfMoons = 55 },
                new Planet() { Name = "PlanetM", NumberOfMoons = 1 },
                new Planet() { Name = "PlanetN", NumberOfMoons = 35 },
                new Planet() { Name = "PlanetO", NumberOfMoons = 0 },
            };
            source_planets.AddRange(planets);
            
            var mockDbsetGalaxies = source_planets.AsQueryable().BuildMockDbSet();
            context.Setup(x => x.Planets).Returns(mockDbsetGalaxies.Object);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(1, 10)]
        [InlineData(1, 5, 0, 100)]
        [InlineData(1, 5, 0, 5)]
        [InlineData(1, 5, 10, 50)]
        [InlineData(1, 5, 1, 1)]
        public async void ShouldReturnALlFilteredPlanets(int currentPage, int pageSize, int? minNumberOfMoons = null, int? maxNumberOfMoons = null)
        {
            // Arrange
            GetPlanetRequest request = new GetPlanetRequest()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                MaxNumberOfMoons = maxNumberOfMoons,
                MinNumberOfMoons = minNumberOfMoons 
            };

            GetPlanetHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<PaginatedList<PlanetResponse>>(response);
            Assert.Equivalent(response.CurrentPage, request.CurrentPage);

            if (minNumberOfMoons != null)
            {
                foreach (var item in response.Result)
                {
                    Assert.True( item.NumberOfMoons >= minNumberOfMoons);
                }
            }

            if (maxNumberOfMoons != null)
            {
                foreach (var item in response.Result)
                {
                    Assert.True(item.NumberOfMoons <= maxNumberOfMoons);
                }
            }
        }

        [Fact]
        public async void ShouldReturn_PlanetById_WhenPlanetExists()
        {
            // Arrange
            var id = source_planets[0].Id;
            GetPlanetByIdRequest request = new GetPlanetByIdRequest(id);

            GetPlanetByIdHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<PlanetResponse>(response);
            Assert.Equal(response.Id, id);
        }

        [Fact]
        public async void ShouldReturn_Null_WhenGalaxyDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            GetPlanetByIdRequest request = new GetPlanetByIdRequest(id);

            GetPlanetByIdHandler handler = new(context.Object, _mapper);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.Null(response);
        }

    }
}
