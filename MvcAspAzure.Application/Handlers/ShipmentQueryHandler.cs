using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcAspAzure.Application.DTOs;
using MvcAspAzure.Application.Queries;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers {
    public class GetShipmentQueryHandler : IRequestHandler<GetShipmentQuery, ShipmentDto> {
        private readonly IShipmentRepository _repository;
        public Task<ShipmentDto> Handle() { }
    }
}
