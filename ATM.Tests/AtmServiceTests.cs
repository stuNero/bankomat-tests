namespace ATM.Test;

public class AtmServiceTests
{
  Account account;
  Card card;
  AtmService AtmService;
  public AtmServiceTests()
  {
    account = new Account(9000);
    card = new Card("1234-123456", account, CardType.Debit);
    card.SetPin("0123");
    AtmService = new AtmService(11000);
  }

  [Theory]
  // Too long
  [InlineData("12345")]
  // Too short
  [InlineData("123")]
  // letters
  [InlineData("abcd")]
  public void ActivateCard_ShouldFail(string pincode)
  {
    bool success = AtmService.ActivateCard(pincode);
    Assert.False(success);
  }

  [Fact]
  public void InsertCard_Test()
  {
    AtmService.InsertCard(card);
    Assert.True(AtmService.HasCardInserted);
  }
  [Fact]
  public void EjectCard_Test()
  {
    AtmService.InsertCard(card);
    AtmService.EjectCard();
    Assert.False(AtmService.HasCardInserted);
  }
  [Fact]
  public void EnterPin_Wrong()
  {
    AtmService.InsertCard(card);
    Assert.False(AtmService.EnterPin("1234"));
  }
  [Fact]
  public void EnterPin_Right()
  {
    AtmService.InsertCard(card);
    Assert.True(AtmService.EnterPin("0123"));
  }
  [Fact]
  public void Withdraw5000_Right()
  {
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.True(AtmService.Withdraw(5000));
  }
  [Fact]
  public void Withdraw7000_Wrong()
  {
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.True(AtmService.Withdraw(5000));
    AtmService.EjectCard();
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.False(AtmService.Withdraw(7000));
  }
  [Fact]
  public void Withdraw6000_Wrong()
  {
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.True(AtmService.Withdraw(5000));
    AtmService.EjectCard();
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.False(AtmService.Withdraw(6000));
  }
}