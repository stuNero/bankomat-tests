namespace ATM.Test;

public class AtmServiceTests
{
  Account account;
  Card card;
  AtmService AtmService;
  public AtmServiceTests()
  {
    account = new Account(9000);
    card = new Card("1234-123456", "0123", account);
    AtmService = new AtmService(11000);
  }
  [Fact]
  public void InsertCard_Test()
  {
    AtmService.InsertCard(card);
    Assert.True(AtmService.HasCardInserted);
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
  public void Withdraw_Right()
  {
    AtmService.InsertCard(card);
    AtmService.EnterPin("0123");
    Assert.True(AtmService.Withdraw(5000));
  }
}