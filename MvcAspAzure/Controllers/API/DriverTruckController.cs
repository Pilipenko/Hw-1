﻿using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Entity;
using MvcAspAzure.Repository;

namespace MvcAspAzure.Controllers.API {


    [Route("api/[controller]")]
    [ApiController]
    public sealed class DriverTruckController : BaseController<Cargo> {
        public DriverTruckController(IRepositoryAsync<Cargo> repository) : base(repository) { }

    }
    //public sealed class DriverTruckController : ControllerBase {
    //    readonly IRepository<DriverTruck> repository;

    //    public DriverTruckController(IRepository<DriverTruck> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(DriverTruck driverTruck) {
    //        await repository.Insert(driverTruck);
    //        return Ok(driverTruck);
    //    }
    //}
}
