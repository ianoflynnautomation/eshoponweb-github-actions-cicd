using BlazorAdmin.Pages.CatalogItemPage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Bunit;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using NSubstitute;
using Xunit.Abstractions;

namespace EShopOnWeb.Blazor.UI.UnitTests.CatalogItemPage;

public class ListTests(ITestOutputHelper outputHelper) : TestContextBase(outputHelper)
{
    // [Fact]
    // public async Task Should_Display_Spinner_When_CatalogItems_Are_Null()
    // {
    //     // Arrange
    //     var catalogItemService = Substitute.For<ICatalogItemService>();
    //     //var authenticationStateProvider = Substitute.For<AuthenticationStateProvider>();
    //     var catalogBrandService = Substitute.For<ICatalogLookupDataService<CatalogBrand>>();
    //     var catalogTypeService = Substitute.For<ICatalogLookupDataService<CatalogType>>();

    //      var mockItems = new List<CatalogItem>
    //     {
    //         new CatalogItem { Id = 1, Name = "Item 1", CatalogType = "Type 1", CatalogBrand = "Brand 1", Price = 10.00m, Description = "Description 1", PictureUri = "https://example.com/item1.jpg" },
    //         new CatalogItem { Id = 2, Name = "Item 2", CatalogType = "Type 2", CatalogBrand = "Brand 2", Price = 20.00m, Description = "Description 2", PictureUri = "https://example.com/item2.jpg" }
    //     };

    //     var mockBrands = new List<CatalogBrand>
    //     {
    //         new CatalogBrand { Id = 1, Name = "Brand 1" },
    //         new CatalogBrand { Id = 2, Name = "Brand 2" }
    //     };

    //     var mockTypes = new List<CatalogType>
    //     {
    //         new CatalogType { Id = 1, Name = "Type 1" },
    //         new CatalogType { Id = 2, Name = "Type 2" }
    //     };

    //     catalogItemService.List().Returns(Task.FromResult(mockItems));
    //     catalogBrandService.List().Returns(Task.FromResult(mockBrands));
    //     catalogTypeService.List().Returns(Task.FromResult(mockTypes));

    //     Services.AddSingleton(catalogItemService);
    //     //Services.AddSingleton(authenticationStateProvider);
    //     Services.AddSingleton(catalogBrandService);
    //     Services.AddSingleton(catalogTypeService);

    //     // Act
    //     var cut = RenderComponent<List>();

    //     await cut.InvokeAsync(() => cut.Instance.CallRequestRefresh());



    //     // Assert
    //    // cut.WaitForState(() => cut.FindAll("Spinner").Count == 1);
 
   // }
}