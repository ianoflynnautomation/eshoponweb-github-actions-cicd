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

        _eShopOnWebApp.HeaderSection.OpenLogin();
        _eShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        _eShopOnWebApp.HomePage.FilterForProduct(".NET", "Mug");
        _eShopOnWebApp.HomePage.AddItemToBasket(".NET Green Mug");
        _eShopOnWebApp.BasketPage.Checkout();
        _eShopOnWebApp.CheckoutPage.PayNow();
        _eShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");

    }

}
