using System.Text.Json;

namespace Biblioteka
{
  internal static class Books
  {
    public static List<Book>? books;
    private static string jsonPath = "../../../books.json";
    private static int nextId;

    static Books()
    {
      if (File.Exists(jsonPath))
      {
        books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(jsonPath));
        nextId = books.Count + 1;
      }
      else
      {
        File.Create(jsonPath).Close();

        books = new List<Book>();
        nextId = 0;
      }
    }

    public static void Add(string name, bool available, int pagesCount)
    {
      Book bookToAdd = new Book(nextId++, name, available, pagesCount);
      books.Add(bookToAdd);
      SaveData();
    }

    public static void Remove(int id)
    {
      Book bookToRemove = FindById(id);
      books.Remove(bookToRemove);
      books.ForEach(b => b.Id = books.IndexOf(b) + 1); // Normalize IDs
      SaveData();
    }

    public static void Borrow(int id)
    {
      Book bookToBorrow = FindById(id);
      bookToBorrow.Return();
    }

    public static void Return(int id)
    {
      Book bookToReturn = FindById(id);
      bookToReturn.Return();
    }

    public static void Show(int id)
    {
      Book bookToShow = FindById(id);
      bookToShow.Show();
    }

    public static void ShowAll()
    {
      books.ForEach(b =>
      {
        Console.WriteLine($"{b.Id} | {b.Name}");
        Console.WriteLine();
      });
    }

    public static Book? FindById(int id)
    {
      Book b = books.FirstOrDefault(b => b.Id == id);
      return b != null ? b : null;
    }

    private static void SaveData()
    {
      File.WriteAllText(jsonPath, JsonSerializer.Serialize(books));
    }
  }
}