using CosmicApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicApi.Application.Features.Planets
{
    public class PlanetResponse
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? NumberOfMoons { get; set; } = null;

        public Guid GalaxyId { get; set; }
        public Guid StarId { get; set; }
    }
}
