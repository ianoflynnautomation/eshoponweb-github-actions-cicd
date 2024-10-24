using BlazorAdmin.Pages.CatalogItemPage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using NSubstitute;
using Xunit.Abstractions;

namespace EShopOnWeb.Blazor.UI.UnitTests.CatalogItemPage;

public class EditTests(ITestOutputHelper outputHelper) : TestContextBase(outputHelper)
{

    [Fact]
    public async Task Should_Open_Modal_And_Load_Item()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        var catalogItemServiceMock = Substitute.For<ICatalogItemService>();

        var mockItem = new CatalogItem
        {
            Id = 1,
            Name = ".NET Black & White Mug",
            Description = ".NET Black & White Mug",
            Price = 8,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Edit>(parameters => parameters
           .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
           .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
       );

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        var modalTitle = cut.Find("[data-testid='modal-title']");

        // Assert
        Assert.Contains("Edit .NET Black & White Mug", modalTitle.TextContent);
    }

    [Fact]
    public async Task Should_Update_Item_On_SaveClick()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        var catalogItemServiceMock = Substitute.For<ICatalogItemService>();

        var mockItem = new CatalogItem
        {
            Id = 1,
            Name = ".NET Black & White Mug",
            Description = ".NET Black & White Mug",
            Price = 8,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Edit>(parameters => parameters
           .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
           .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
       );

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        var nameInput = cut.Find("[data-testid='catalog-item-name-input']");
        var descriptionInput = cut.Find("[data-testid='catalog-item-description-input']");
        var priceInput = cut.Find("[data-testid='catalog-item-price-input']");

        nameInput.Change("Updated Item Name");
        descriptionInput.Change("Updated Item Description");
        priceInput.Change("10");

        cut.Find("[data-testid='submit-button']").Click();

        // Assert
        cut.WaitForAssertion(() => catalogItemServiceMock.Received(1)
        .Edit(Arg.Is<CatalogItem>(item => item.Name
        .Equals("Updated Item Name"))), TimeSpan.FromSeconds(2));

    }

    [Fact]
    public async Task Should_Close_Modal_On_Cancel()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        var catalogItemServiceMock = Substitute.For<ICatalogItemService>();

        var mockItem = new CatalogItem
        {
            Id = 1,
            Name = ".NET Black & White Mug",
            Description = ".NET Black & White Mug",
            Price = 8,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Edit>(parameters => parameters
           .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
           .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
       );

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        cut.Find("[data-testid='cancel-button']").Click();

        // Assert
        Assert.DoesNotContain("Show", cut.Markup);

    }

}