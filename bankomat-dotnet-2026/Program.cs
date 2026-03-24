using ATM;
while (true)
{
  var account = new Account(5000);
  List<Card> cards = new();
  cards.Add(new Card("1234-5678", "1234", account));
  cards.Add(new Card("1234-admin", "admin", account));
  var atm = new AtmService(3000);
  int choose = 0;
  while (true)
  {
    Console.WriteLine("Välj ett kort");
    for (int i = 0; i < cards.Count; i++)
    {
      Console.WriteLine($"[{i}] " + cards[i].CardNumber);
    }
    choose = Convert.ToInt32(Console.ReadLine());
    break;
  }
  ConsoleRunner.Run(atm, cards[choose]);
}