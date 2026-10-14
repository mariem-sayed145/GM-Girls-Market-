using GM.Application.Features.Authentication.Commands.Register;
using GM.Application.Features.CustomerRequests.Commands.CreateCustomerRequest;
using GM.Application.Features.Products.Commands.CreateProduct;
using GM.Application.Features.Services.Commands.CreateService;
using GM.Domain.Entities;
using GM.Domain.Enums;
using GM.Infrastructure.Services;
using System.IO;
using System.Text;
using Xunit;

namespace GM.Tests;

public class DomainEntityTests
{
    [Fact]
    public void User_ShouldInitializeWithDefaultCustomerRole()
    {
        var user = new User("Jane Doe", "jane@example.com", "hashed_pwd");

        Assert.Equal("Jane Doe", user.FullName);
        Assert.Equal("jane@example.com", user.Email);
        Assert.Equal(UserRole.Customer, user.Role);
    }

    [Fact]
    public void CustomerRequest_ShouldInitializeWithPendingStatus()
    {
        var request = new CustomerRequest(1, 10, "Custom styling required");

        Assert.Equal(1, request.UserId);
        Assert.Equal(10, request.ServiceId);
        Assert.Equal("Custom styling required", request.Description);
        Assert.Equal(RequestStatus.Pending, request.Status);
    }

    [Fact]
    public void CustomerRequest_UpdateStatus_ShouldUpdateStatusAndTimestamp()
    {
        var request = new CustomerRequest(1, 10, "Custom styling required");
        request.UpdateStatus(RequestStatus.InProgress);

        Assert.Equal(RequestStatus.InProgress, request.Status);
        Assert.NotNull(request.UpdatedAt);
    }
}

public class ValidationTests
{
    [Theory]
    [InlineData("", "test@example.com", "P@ssword123", false)]
    [InlineData("Valid Name", "invalid-email", "P@ssword123", false)]
    [InlineData("Valid Name", "test@example.com", "short", false)]
    [InlineData("Valid Name", "test@example.com", "StrongPassword123!", true)]
    public void RegisterCommandValidator_ShouldValidateCorrectly(
        string fullName, string email, string password, bool expectedValid)
    {
        var validator = new RegisterCommandValidator();
        var command = new RegisterCommand(fullName, email, password);

        var result = validator.Validate(command);

        Assert.Equal(expectedValid, result.IsValid);
    }

    [Fact]
    public void CreateCustomerRequestValidator_ShouldRejectEmptyDescription()
    {
        var validator = new CreateCustomerRequestCommandValidator();
        var command = new CreateCustomerRequestCommand(1, 2, "", null);

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }

    [Fact]
    public void CreateCustomerRequestValidator_ShouldAcceptValidCommand()
    {
        var validator = new CreateCustomerRequestCommandValidator();
        var command = new CreateCustomerRequestCommand(1, 2, "Custom gift box wrapping", DateTime.UtcNow.AddDays(3));

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateProductValidator_ShouldRejectZeroPrice()
    {
        var validator = new CreateProductCommandValidator();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("fake_image_bytes"));
        var command = new CreateProductCommand("Product A", "Description", 0, "Dresses", stream, "product.jpg");

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Price");
    }

    [Fact]
    public void CreateServiceValidator_ShouldRejectEmptyCategory()
    {
        var validator = new CreateServiceCommandValidator();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("fake_image_bytes"));
        var command = new CreateServiceCommand("Service A", "Description", 150, "", stream, "service.jpg");

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Category");
    }
}

public class SecurityTests
{
    [Fact]
    public void PasswordHasher_ShouldHashAndVerifySuccessfully()
    {
        var hasher = new PasswordHasher();
        const string rawPassword = "SecurePassword@2026!";

        var hash = hasher.Hash(rawPassword);

        Assert.NotEmpty(hash);
        Assert.NotEqual(rawPassword, hash);
        Assert.True(hasher.Verify(rawPassword, hash));
        Assert.False(hasher.Verify("WrongPassword123!", hash));
    }
}
