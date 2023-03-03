using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class GetGalaxyValidator : AbstractValidator<GetGalaxyRequest>
    {
        public GetGalaxyValidator()
        {
        }
    }
}
