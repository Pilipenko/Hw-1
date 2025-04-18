using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcAspAzure.Application.DTOs;

namespace MvcAspAzure.Application.Queries {
    public class GetShipmentQuery {

        public record GetShipmentByIdQuery(int Id) : IRequest<ShipmentDto>;
    }
}
