
namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BaseTest
{
    [Test]
    public void Customer_Order_UserJourney()
    {
        EShopOnWebApp.HeaderSection.OpenLogin();
        EShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        //HomePage.FilterForProduct(".NET", "Mug");
        EShopOnWebApp.HomePage.AddItemToBasket(".NET Black & White Mug");
        EShopOnWebApp.BasketPage.Checkout();
        EShopOnWebApp.CheckoutPage.PayNow();
        EShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}
