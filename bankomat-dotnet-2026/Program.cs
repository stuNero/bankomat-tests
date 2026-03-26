using ATM;
var account = new Account(5000);
Card card = new Card("1234-5678", account);
ConsoleRunner.Run(card);