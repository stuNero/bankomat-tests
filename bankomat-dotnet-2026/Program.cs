using ATM;
while (true)
{
  var account = new Account(5000);
  Card card = new Card("1234-5678", "1234", account);
  var atm = new AtmService(11000);
  ConsoleRunner.Run(atm, card);
}