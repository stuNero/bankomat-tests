namespace ATM;

public class Card
{
    public string CardNumber { get; }
    public string PinCode { get; }
    public Account Account { get; }
    public bool isAdmin;

    public Card(string cardNumber, Account account)
    {
        CardNumber = cardNumber;
        Account = account;
    }

    public bool MatchesPin(string pinCode)
    {
        return PinCode == pinCode;
    }
}