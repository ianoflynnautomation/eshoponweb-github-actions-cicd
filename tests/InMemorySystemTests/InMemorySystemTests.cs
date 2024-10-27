
namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BaseTest
{
    [Test]
    public void Customer_Order_UserJourney()
    {
        _eShopOnWebApp.HeaderSection.OpenLogin();
        _eShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        _eShopOnWebApp.HomePage.AddItemToBasket(".NET Black & White Mug");
        _eShopOnWebApp.BasketPage.Checkout();
        _eShopOnWebApp.CheckoutPage.PayNow();
        _eShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}
