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

public class DeleteTests(ITestOutputHelper outputHelper) : TestContextBase(outputHelper)
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
            PictureUri = "https://example.com/image.jpg"
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton<ICatalogItemService>(catalogItemServiceMock);

        var cut = RenderComponent<Delete>();

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        // Assert
        var modalTitle = cut.Find("[data-testid='modal-title']");
        Assert.Equal("Delete .NET Black & White Mug", modalTitle.TextContent);

        var itemName = cut.Find("dd:nth-of-type(1)");
        Assert.Equal(".NET Black & White Mug", itemName.TextContent);

        var itemDescription = cut.Find("dd:nth-of-type(2)");
        Assert.Equal(".NET Black & White Mug", itemDescription.TextContent);

    }

    [Fact]
    public async Task Should_Call_Delete_On_DeleteClick()
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
            PictureUri = "https://example.com/image.jpg"
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Delete>();

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        cut.Find("[data-testid='delete-button']").Click();

        // Assert
        cut.WaitForAssertion(() => catalogItemServiceMock.Received(1)
        .Delete(1), TimeSpan.FromSeconds(2));

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
            PictureUri = "https://example.com/image.jpg"
        };

        catalogItemServiceMock.GetById(1).Returns(Task.FromResult(mockItem));

        Services.AddSingleton(catalogItemServiceMock);

        var cut = RenderComponent<Delete>();

        // Act
        await cut.InvokeAsync(() => cut.Instance.Open(1));

        cut.Find("[data-testid='cancel-button']").Click();

        // Assert
        Assert.DoesNotContain("Show", cut.Markup);

    }

}