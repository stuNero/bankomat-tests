using System.Runtime.CompilerServices;
using System.Transactions;
using ATM;
var account = new Account(5000);

List<Card> cards = [
  new Card("1234-5678", account, CardType.Debit),
  new Card("8765-4321", account, CardType.Credit),
  new Card("0000-1111", account, CardType.Admin)];
cards[2].SetPin("1337");
int selectedIdx = 0;
bool running = true;
while (running)
{
  Utils.TryClear();
  for (int i = 0; i < cards.Count; i++)
  {
    if (selectedIdx == i)
    {
      Console.BackgroundColor = ConsoleColor.Gray;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.WriteLine(cards[i].CardType);
    }
    else
    {
      Console.WriteLine(cards[i].CardType);
    }
    Console.ResetColor();
  }
  switch (Console.ReadKey().Key)
  {
    case ConsoleKey.UpArrow:
      selectedIdx--;
      if (selectedIdx < 0)
        selectedIdx = cards.Count - 1;
      break;
    case ConsoleKey.DownArrow:
      selectedIdx++;
      if (selectedIdx > cards.Count - 1)
        selectedIdx = 0;
      break;
    case ConsoleKey.Enter:
      running = false;
      break;
    default:
      break;
  }
}

ConsoleRunner.Run(cards[selectedIdx]);