using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAspAzure.Application.DTOs {
    public class ShipmentDto {
        public int Id { get; set; }
        public DateTime StartData { get; set; }
        public DateTime CompletionData { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
        public int CargoId { get; set; }
    }
}
