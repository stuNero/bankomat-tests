namespace ATM.Test;

public class CardTests
{
  private Card card;
  private Account account;
  public CardTests()
  {
    account = new Account(5000);
    card = new Card("1234-5678", account);
  }
}