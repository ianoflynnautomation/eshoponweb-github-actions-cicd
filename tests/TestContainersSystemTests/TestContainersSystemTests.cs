namespace EShopOnWeb.TestContainersSystemTests;

[TestFixture]
public class TestContainersSystemTests : BaseTest
{
    [Test]
    public void Customer_Order_UserJourney()
    {
        EShopOnWebApp.HeaderSection.OpenLogin();
        EShopOnWebApp.LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        EShopOnWebApp.HomePage.AddItemToBasket(".NET Black & White Mug");
        EShopOnWebApp.BasketPage.Checkout();
        EShopOnWebApp.CheckoutPage.PayNow();
        EShopOnWebApp.SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}

