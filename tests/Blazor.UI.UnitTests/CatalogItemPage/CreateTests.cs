using BlazorAdmin.Pages.CatalogItemPage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using NSubstitute;
using Xunit.Abstractions;

namespace EShopOnWeb.Blazor.UI.UnitTests.CatalogItemPage;

public class CreateTests(ITestOutputHelper outputHelper) : TestContextBase(outputHelper)
{
    [Fact]
    public void Modal_Should_Not_Be_Visible_On_InitialLoad()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        Services.AddSingleton(Substitute.For<ICatalogItemService>());

        var cut = RenderComponent<Create>(parameters => parameters
            .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
            .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
        );

        // Assert
        Assert.DoesNotContain("show", cut.Markup);
        Assert.Contains("none", cut.Markup);

    }

    [Fact]
    public async Task Modal_Should_Be_Visible_After_Open_Method_Call()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        Services.AddSingleton(Substitute.For<ICatalogItemService>());

        var cut = RenderComponent<Create>(parameters => parameters
            .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
            .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
        );

        // Act
        await cut.InvokeAsync(cut.Instance.Open);

        // Assert
        Assert.Contains("block", cut.Markup);
        Assert.Contains("show", cut.Markup);
    }

    [Fact]
    public void Should_Bind_Form_Fields_Correctly()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        Services.AddSingleton(Substitute.For<ICatalogItemService>());

        var cut = RenderComponent<Create>(parameters => parameters
            .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
            .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
        );

        // Act
        cut.InvokeAsync(cut.Instance.Open);

        var itemNameInput = cut.Find("[data-testid='catalog-item-name-input']");
        var itemDescriptionInput = cut.Find("[data-testid='catalog-item-description-input']");
        var itemPriceInput = cut.Find("[data-testid='catalog-item-price-input']");

        itemNameInput.Change("Test Product");
        itemDescriptionInput.Change("Test Description");
        itemPriceInput.Change("100");

        // Assert
        Assert.Equal("Test Product", itemNameInput.GetAttribute("value"));
        Assert.Equal("Test Description", itemDescriptionInput.GetAttribute("value"));
        Assert.Equal("100", itemPriceInput.GetAttribute("value"));
    }

    [Fact]
    public async Task Should_Create_Item_And_Invoke_On_SaveClick()
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

        catalogItemServiceMock.Create(Arg.Any<CreateCatalogItemRequest>()).Returns(Task.FromResult(mockItem));

        var saveCallbackTriggered = false;

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Create>(parameters => parameters
            .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
            .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
            .Add(p => p.OnSaveClick, EventCallback.Factory.Create<string>(this, () => saveCallbackTriggered = true))
        );

        // Act
        await cut.InvokeAsync(cut.Instance.Open);

        cut.Find("[data-testid='catalog-item-name-input']").Change(mockItem.Name);
        cut.Find("[data-testid='catalog-item-description-input']").Change(mockItem.Description);
        cut.Find("[data-testid='catalog-item-price-input']").Change(mockItem.Price.ToString());

        cut.Find("[data-testid='submit-button']").Click();

        // Assert
        cut.WaitForAssertion(() => catalogItemServiceMock.Received(1)
           .Create(Arg.Is<CreateCatalogItemRequest>(i => i.Name
           .Equals(".NET Black & White Mug"))), TimeSpan.FromSeconds(2));

        Assert.True(saveCallbackTriggered);
    }

    [Fact]
    public async Task Should_Close_Modal_On_Cancel_Click()
    {
        // Arrange
        Services.AddSingleton(Substitute.For<ILogger<Create>>);
        Services.AddSingleton(Substitute.For<IJSRuntime>());
        Services.AddSingleton(Substitute.For<ICatalogItemService>());

        var cut = RenderComponent<Create>(parameters => parameters
            .Add(p => p.Brands, [new CatalogBrand { Id = 1, Name = ".NET", }])
            .Add(p => p.Types, [new CatalogType { Id = 1, Name = "Mug" }])
        );

        // Act
        await cut.InvokeAsync(cut.Instance.Open);

        Assert.Contains("block", cut.Markup);

        cut.Find("[data-testid='cancel-button']").Click();

        // Assert
        Assert.Contains("none", cut.Markup);
    }

}