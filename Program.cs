using Biblioteka;

Console.CursorVisible = false;

StartMenu();

void StartMenu(string? msg = null)
{
  int Choice = 0;
  bool ChoiceMade = false;
  string[] Options = new[] { "Pokaz wszystkie książki", "Dodaj nową książke" };

  while (!ChoiceMade)
  {
    Console.Clear();

    if (msg != null)
    {
      Console.WriteLine(msg);
      Console.ResetColor();
    }

    for (int i = 0; i < Options.Length; i++)
    {
      Console.WriteLine($"{(Choice == i ? ">" : " ")} {Options[i]}");
    }

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

    if (int.TryParse(Console.ReadLine(), out int bookId))
    {
      BookMenu(bookId);
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      StartMenu("Podaj poprawne id");
    }
  }

  void AddBook()
  {
    Console.WriteLine("Podaj tytul");
    string name = Console.ReadLine();

    Console.WriteLine("Podaj ilosc stron");
    string pagesString = Console.ReadLine();

    if (int.TryParse(pagesString, out int pagesCount))
    {
      Books.Add(name, true, pagesCount);
      Console.ForegroundColor = ConsoleColor.Green;
      StartMenu($"Dodano {name}");
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      StartMenu("Dodawanie ksiazki nie powiodło się!");
    }
  }
}

void BookMenu(int bookId)
{
  Book b = Books.FindById(bookId);

  if (b == null)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    StartMenu("Podana ksiazka nie istnieje!");
  }

  int Choice = 0;
  bool ChoiceMade = false;
  string action = b.Available ? "Wypozycz" : "Zwroc";
  string[] Options = new[] { "Usun książke", action };

  while (!ChoiceMade)
  {
    Console.Clear();

    Books.Show(bookId);

    for (int i = 0; i < Options.Length; i++)
    {
      Console.WriteLine($"{(Choice == i ? ">" : " ")} {Options[i]}");
    }

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
    Book bookToRemove = Books.FindById(bookId);

    Books.Remove(bookToRemove.Id);
    Console.ForegroundColor = ConsoleColor.Red;
    StartMenu($"Usunieto {bookToRemove.Name}");
  }

  void BorrowOrReturn()
  {
    if (b.Available)
      b.Borrow();
    else
      b.Return();

    BookMenu(bookId);
  }
}