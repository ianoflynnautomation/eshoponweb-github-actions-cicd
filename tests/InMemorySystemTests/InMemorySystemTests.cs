

using BlazorAdmin;
using BlazorShared.Models;

namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BaseTest
{

    [Test]
    public void TC_01_Customer_Order_UserJourney()
    {
        //  var request = new CreateCatalogItemRequest()
        // {
        //     CatalogBrandId = 1,
        //     CatalogTypeId = 2,
        //     Description = "test description 001",
        //     Name = "001 Name",
        //     Price = 1.23m
        // };

        //await _fixture.SystemTestHost.CreateCatelogItem(request);

        _eShopOnWebApp.HeaderSection.OpenLogin();
        _eShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        _eShopOnWebApp.HomePage.FilterForProduct(".NET", "Mug");
        _eShopOnWebApp.HomePage.AddItemToBasket(".NET Black & White Mug");
        _eShopOnWebApp.BasketPage.Checkout();
        _eShopOnWebApp.CheckoutPage.PayNow();
        _eShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");

        //await _fixture.SystemTestHost.DeleteCatalogItem(request);
    }

}
