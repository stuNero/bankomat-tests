namespace ATM;

public class Card
{
    public string CardNumber { get; }
    public string PinCode { get; private set; }
    public Account Account { get; }
    public bool isActivated = false;
    public bool isAdmin;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Card(string cardNumber, Account account)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        if (!String.IsNullOrWhiteSpace(PinCode))
        {
            isActivated = true;
        }
        CardNumber = cardNumber;
        Account = account;
    }

    public bool MatchesPin(string pinCode)
    {
        return PinCode == pinCode;
    }
    public void SetPin(string newPin)
    {
        PinCode = newPin;
    }
}