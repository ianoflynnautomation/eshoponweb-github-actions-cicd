using BlazorAdmin;
using BlazorShared.Models;

namespace EShopOnWeb.DockerSystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BaseTest
{

    [Test]
    public async Task TC_01_Customer_Order_UserJourney()
    {
        var request = new CreateCatalogItemRequest()
        {
            CatalogBrandId = 2,
            CatalogTypeId = 1,
            Description = ".NET Green Mug",
            Name = ".NET Green Mug",
            Price = 20.0m
        };

        await _fixture.SystemTestHost.CreateCatelogItem(request);

        await _eShopOnWebApp.HeaderSection.OpenLogin();
        await _eShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await _eShopOnWebApp.HomePage.FilterForProduct(".NET", "Mug");
        await _eShopOnWebApp.HomePage.AddItemToBasket(".NET Green Mug");
        await _eShopOnWebApp.BasketPage.Checkout();
        await _eShopOnWebApp.CheckoutPage.PayNow();
        await _eShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");

    }

}
