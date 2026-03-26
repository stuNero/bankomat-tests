namespace ATM;

static class Utils
{
  public static void TryClear()
  {
    try { Console.Clear(); } catch { }
  }
  public static void PrintColor(string msg, ConsoleColor color = ConsoleColor.White, bool paus = true)
  {
    Console.ForegroundColor = color;
    Console.WriteLine(msg);
    if (paus)
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("Klicka 'ENTER' för att gå vidare...");
      Console.ReadKey(true);
    }
    Console.ResetColor();
  }
}