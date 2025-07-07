using Moq;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoMoq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Controllers.API;
using MvcAspAzure.Application.Common;

public class WarehouseControllerTests {
    public class Result {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }

    private IFixture CreateFixture() {
        var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        return fixture;
    }

    [Theory, AutoMoqData]
    public async Task Create_ValidCommand_ReturnsCreated(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        [Frozen] Mock<IWarehouseService> serviceMock,
        WarehouseController controller,
        CreateWarehouseCommand command) {

        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());

        serviceMock.Setup(s => s.CreateWarehouseAsync(It.IsAny<CreateWarehouseCommand>()))
                   .ReturnsAsync(new ServiceResult { Success = true });

        var result = await controller.Create(command);

        Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result;
        Assert.That(created.ActionName, Is.EqualTo(nameof(WarehouseController.GetById)));
    }

    [Theory, AutoMoqData]
    public async Task Create_InvalidCommand_ReturnsValidationProblem(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        WarehouseController controller,
        CreateWarehouseCommand command) {

        var failures = new[]
        {
            new ValidationFailure("Name", "Name is required")
        };
        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult(failures));

        var result = await controller.Create(command);

        var objectResult = result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.Value, Is.TypeOf<ValidationProblemDetails>());

        var details = (ValidationProblemDetails)objectResult.Value!;
        Assert.That(details.Errors.ContainsKey("Name"));
    }
}

public class Result {
    public bool Success { get; set; }
    public required string[] Errors { get; set; }
}

