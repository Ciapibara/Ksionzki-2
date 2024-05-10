using Biblioteka;

Console.CursorVisible = false;

StartMenu();

void StartMenu(string? msg = null, ConsoleColor color = ConsoleColor.White)
{
  int Choice = 0;
  bool ChoiceMade = false;
  string[] Options = ["Pokaz wszystkie książki", "Dodaj nową książke"];

  while (!ChoiceMade)
  {
    Console.Clear();

    if (msg != null)
    {
   
      Console.ForegroundColor = color;
      Console.WriteLine(msg);
      Console.ResetColor();
    }
    ShowOptions(Options, Choice);

    ConsoleKeyInfo key = Console.ReadKey(false);
    switch (key.Key)
    {
      case ConsoleKey.UpArrow:

               
        Choice = Choice == 0 ? Options.Length - 1 : Choice - 1;
        break;

      case ConsoleKey.DownArrow:
        Choice = Choice == Options.Length - 1 ? 0 : Choice + 1;
        break;

      case ConsoleKey.Enter:
        ChoiceMade = true;
        break;

      case ConsoleKey.Escape:
        Environment.Exit(0);
        break;
    }
  }

  if (ChoiceMade)
  {
    switch (Choice)
    {
      case 0:
        ShowAllBooks();
        break;

      case 1:
        AddBook();
        break;
    }
  }

  void ShowAllBooks()
  {
    Books.ShowAll();

    ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Escape)
            StartMenu();
        

    if (int.TryParse(Console.ReadLine(), out int bookId))
      BookMenu(bookId);
    else
      StartMenu("Podaj poprawne id", ConsoleColor.Red);
  }

  void AddBook()
  {
    Console.WriteLine("Podaj tytul");
    string name = Console.ReadLine();

    Console.WriteLine("Podaj ilosc stron");
    string? pagesString = Console.ReadLine();

    if (int.TryParse(pagesString, out int pagesCount))
    {
      Books.Add(name, true, pagesCount);
      StartMenu($"Dodano {name}", ConsoleColor.Green);
    }
    else
      StartMenu("Dodawanie ksiazki nie powiodło się!", ConsoleColor.Red);
  }
}

void BookMenu(int bookId)
{
  Book? b = Books.FindById(bookId);

  if (b == null)
    StartMenu("Podana ksiazka nie istnieje!", ConsoleColor.Red);

  int Choice = 0;
  bool ChoiceMade = false;
  string action = b.Available ? "Wypozycz" : "Zwroc";
  string[] Options = ["Usun książke", action];

  while (!ChoiceMade)
  {
    Console.Clear();

    b.Show();
    ShowOptions(Options, Choice);

    ConsoleKeyInfo key = Console.ReadKey(false);
    switch (key.Key)
    {
      case ConsoleKey.UpArrow:
        Choice = Choice == 0 ? Options.Length - 1 : Choice - 1;
        break;

      case ConsoleKey.DownArrow:
        Choice = Choice == Options.Length - 1 ? 0 : Choice + 1;
        break;

      case ConsoleKey.Enter:
        ChoiceMade = true;
        break;

      case ConsoleKey.Escape:
        StartMenu();
        break;
    }
  }

  if (ChoiceMade)
  {
    switch (Choice)
    {
      case 0:
        RemoveBook();
        break;

      case 1:
        BorrowOrReturn();
        break;
    }
  }

  void RemoveBook()
  {
    Books.Remove(b);
    StartMenu($"Usunieto {b.Name}", ConsoleColor.Red);
  }

  void BorrowOrReturn()
  {
    b.BorrowOrReturn();
    BookMenu(bookId);
  }
}

void ShowOptions(string[] options, int choice)
{

  for (int i = 0; i < options.Length; i++)
  {
    if (choice == i)
      Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"{(choice == i ? ">" : " ")} {options[i]}");
    Console.ResetColor();
  }
}
