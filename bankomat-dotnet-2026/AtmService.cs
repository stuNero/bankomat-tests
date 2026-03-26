namespace ATM;

public class AtmService
{
    public Card? _currentCard;
    private bool _isAuthenticated;

    public bool HasCardInserted => _currentCard != null;
    public bool IsAuthenticated => _isAuthenticated;

    public int AtmBalance { get; private set; }

    public AtmService(int initialBalance)
    {
        AtmBalance = initialBalance;
    }

    public void InsertCard(Card card)
    {
        _currentCard = card;
        _isAuthenticated = false;
    }

    public bool ActivateCard(string pin)
    {
        // Om pin är lika med giltig pinkod struktur
        // Kortets pin blir inmatad pin kod
        if (int.TryParse(pin, out int correctPin) && pin.Length == 4 && _currentCard != null)
        {
            _currentCard.SetPin(pin);
            _currentCard.isActivated = true;
            return true;
        }
        else
            return false;
    }

    public void EjectCard()
    {
        _currentCard = null;
        _isAuthenticated = false;
    }

    public bool EnterPin(string pinCode)
    {
        if (_currentCard == null)
        {
            return false;
        }

        _isAuthenticated = _currentCard.MatchesPin(pinCode);
        return _isAuthenticated;
    }

    public int GetBalance()
    {
        EnsureAuthenticated();
        return _currentCard!.Account.GetBalance();
    }

    public bool Withdraw(int amount)
    {
        EnsureAuthenticated();
        if (AtmBalance - amount < 0)
        {
            return false;
        }
        AtmBalance -= amount;
        return _currentCard!.Account.Withdraw(amount);
    }

    public bool Deposit(int amount)
    {
        EnsureAuthenticated();
        AtmBalance += amount;
        return _currentCard!.Account.Deposit(amount);
    }

    public void EnsureAuthenticated()
    {
        if (_currentCard == null || !_isAuthenticated)
        {
            throw new InvalidOperationException("Ingen autentiserad session.");
        }
    }
}