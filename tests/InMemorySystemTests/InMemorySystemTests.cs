
namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BaseTest
{
    [Test]
    public void Customer_Order_UserJourney()
    {
        EShopOnWebSite.HeaderSection.Login();
        EShopOnWebSite.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        //HomePage.FilterForProduct(".NET", "Mug");
        EShopOnWebSite.HomePage.AddItemToBasket(".NET Black & White Mug");
        EShopOnWebSite.BasketPage.Checkout();
        EShopOnWebSite.CheckoutPage.PayNow();
        EShopOnWebSite.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}
