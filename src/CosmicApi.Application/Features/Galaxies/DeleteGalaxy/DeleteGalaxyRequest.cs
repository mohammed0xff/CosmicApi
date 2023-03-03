using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public record DeleteGalaxyRequest(Guid Id) : IRequest<bool>;
}
