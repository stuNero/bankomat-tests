namespace ATM;

using System.Globalization;

public static class ConsoleRunner
{
    private static readonly AtmService atm = new(11000);
    public static void Run(Card demoCard)
    {
        bool running = true;

        while (running)
        {
            if (!atm.HasCardInserted)
            {
                running = ShowWelcomeMenu(demoCard);
            }
            else if (atm._currentCard != null && !atm._currentCard.isActivated)
            {
                ActivateCardFlow();
            }
            else if (!atm.IsAuthenticated)
            {
                ShowPinMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
        Console.WriteLine("Tack och hej!");
    }

    private static bool ShowWelcomeMenu(Card demoCard)
    {
        Console.WriteLine();
        Console.WriteLine("=== BANKOMAT ===");
        Console.WriteLine("1. Mata in kort");
        Console.WriteLine("0. Avsluta");
        Console.Write("Val: ");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                atm.InsertCard(demoCard);
                Console.WriteLine("Kort inmatat.");
                return true;
            case "0":
                return false;

            default:
                Console.WriteLine("Ogiltigt val.");
                return true;
        }
    }

    private static void ShowPinMenu()
    {
        Console.WriteLine();
        Console.Write("Ange PIN: ");
        string? pin = Console.ReadLine();

        bool ok = atm.EnterPin(pin ?? "");

        if (ok)
        {
            Console.WriteLine("PIN korrekt.");
        }
        else
        {
            Console.WriteLine("Fel PIN.");
            Console.WriteLine("Kortet matas ut.");
            atm.EjectCard();
        }
    }

    private static void ShowMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== HUVUDMENY ===");
        Console.WriteLine("1. Visa saldo");
        Console.WriteLine("2. Ta ut pengar");
        Console.WriteLine("3. Sätt in pengar");
        Console.WriteLine("4. Mata ut kort");
        Console.WriteLine("5. Ändra Pin");
        if (atm._currentCard != null && atm._currentCard.CardType == CardType.Admin)
            Console.WriteLine("0. Ändra bankomat saldo");
        Console.Write("Val: ");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ShowBalance();
                break;
            case "2":
                WithdrawFlow();
                break;
            case "3":
                DepositFlow();
                break;
            case "4":
                atm.EjectCard();
                Console.WriteLine("Kortet är utmatat.");
                break;
            case "5":
                ChangePinFlow();
                break;
            case "0":
                ChangeAtmBalanceFlow();
                break;
            default:
                Console.WriteLine("Ogiltigt val.");
                break;
        }
    }
    private static void ChangeAtmBalanceFlow()
    {
        if (atm._currentCard!.CardType == CardType.Admin)
            return;
        while (true)
        {
            Console.WriteLine("1. Sätt in pengar");
            Console.WriteLine("2. Ta ut pengar");
            Console.WriteLine("3. Gå tillbaka");
            string input = Console.ReadLine()!;
            switch (input)
            {
                case "1":
                    break;
            }
        }
    }
    private static void ChangePinFlow()
    {
        bool success = false;
        while (!success)
        {
            Utils.TryClear();
            Console.WriteLine("Vänligen mata in ny pinkod: (Fyra Siffor)");
            Console.Write("> ");
            string firstInput = Console.ReadLine()!;
            Console.Write("Bekräfta pin: (Fyra Siffor)");
            string secondInput = Console.ReadLine()!;
            if (firstInput != secondInput)
            {
                Utils.PrintColor("Ogiltig pinkod", ConsoleColor.DarkRed, paus: false);
                Console.WriteLine("Vill du mata ut ditt kort?\n ('ja'/'nej'):");
                string input = Console.ReadLine()!;
                switch (input.ToLower().Trim())
                {
                    case "ja":
                        atm.EjectCard();
                        return;
                    default:
                        continue;
                }
            }
            success = atm.ActivateCard(firstInput);
        }
        Utils.PrintColor("Kort aktiverat.", color: ConsoleColor.DarkGreen);
        return;
    }
    private static void ActivateCardFlow()
    {
        bool success = false;
        while (!success)
        {
            Utils.TryClear();
            Console.WriteLine("Ditt kort är ej aktiverat.");
            Console.WriteLine("Vänligen mata in ny pinkod: (Fyra Siffor)");
            Console.Write("> ");
            string input = Console.ReadLine()!;
            success = atm.ActivateCard(input);
            if (!success)
            {
                Utils.PrintColor("Ogiltig pinkod", ConsoleColor.DarkRed, paus: false);
                Console.WriteLine("Vill du mata ut ditt kort?\n ('ja'/'nej'):");
                input = Console.ReadLine()!;
                switch (input.ToLower().Trim())
                {
                    case "ja":
                        atm.EjectCard();
                        return;
                }
            }
            else
            {
                Utils.PrintColor("Kort aktiverat.", color: ConsoleColor.DarkGreen);
                return;
            }
        }
    }

    private static void ShowBalance()
    {
        int balance = atm.GetBalance();
        Console.WriteLine($"Ditt saldo är: {balance} kr");
    }

    private static void WithdrawFlow()
    {
        Console.Write("Ange belopp att ta ut: ");
        string? input = Console.ReadLine();
        if (input == null) // For fixing null reference
        {
            Utils.PrintColor("Du måste ange ett belopp.", color: ConsoleColor.DarkRed);
            return;
        }
        int amount = int.Parse(input);

        bool success = atm.Withdraw(amount);

        if (success)
        {
            Console.WriteLine("Varsågod, ta dina pengar.");
        }
        else
        {
            Console.WriteLine("Uttaget kunde inte genomföras.");
        }
    }

    private static void DepositFlow()
    {
        Console.Write("Ange belopp att sätta in: ");
        string? input = Console.ReadLine();
        if (input == null) // For fixing null reference
        {
            Utils.PrintColor("Du måste ange ett belopp.", ConsoleColor.DarkRed);
            return;
        }
        int amount = int.Parse(input);

        bool success = atm.Deposit(amount);

        if (success)
        {
            Console.WriteLine("Insättning genomförd.");
        }
        else
        {
            Console.WriteLine("Insättningen kunde inte genomföras.");
        }
    }
}