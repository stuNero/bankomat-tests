namespace ATM;

public class Card
{
    public string CardNumber { get; }
    public string PinCode { get; }
    public Account Account { get; }
    public bool isAdmin;

    public Card(string cardNumber, string pinCode, Account account)
    {
        CardNumber = cardNumber;
        PinCode = pinCode;
        Account = account;
        if (pinCode == "admin")
        {
            isAdmin = true;
        }
        else
        {
            isAdmin = false;
        }
    }

    public bool MatchesPin(string pinCode)
    {
        return PinCode == pinCode;
    }
}