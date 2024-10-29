namespace EShopOnWeb.TestContainersSystemTests;

[TestFixture]
public class TestContainersSystemTests : BaseTest
{
    [Test]
    public void TC_01_Customer_Order_UserJourney()
    {
        _eShopOnWebApp.HeaderSection.OpenLogin();
        _eShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        _eShopOnWebApp.HomePage.FilterForProduct(".NET", "Mug")
                               .AddItemToBasket(".NET Black & White Mug");
        _eShopOnWebApp.BasketPage.Checkout();
        _eShopOnWebApp.CheckoutPage.PayNow();
        _eShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}

