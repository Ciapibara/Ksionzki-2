using System.Text.Json;

namespace Biblioteka
{
  internal static class Books
  {
    public static List<Book>? books = [];
    private static string jsonPath = "../../../books.json";
    private static int nextId = 1;

    static Books()
    {
      if (File.Exists(jsonPath))
      {
        books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(jsonPath));
        nextId = books.Count + 1;
      }
      else
        File.Create(jsonPath).Close();
    }

    public static void Add(string name, bool available, int pagesCount)
    {
      Book bookToAdd = new Book(nextId++, name, available, pagesCount);
      books?.Add(bookToAdd);
      SaveData();
    }

    public static void Remove(Book bookToRemove)
    {
      books?.Remove(bookToRemove);
      books?.ForEach(b => b.Id = books.IndexOf(b) + 1); // Normalize IDs
      SaveData();
    }

    public static void ShowAll()
    {
      books?.ForEach(b =>
      {
        Console.WriteLine($"{b.Id} | {b.Name}");
        Console.WriteLine();
      });
    }

    public static Book? FindById(int id)
    {
      Book b = books.FirstOrDefault(b => b.Id == id);
      return b;
    }

    private static void SaveData() => File.WriteAllText(jsonPath, JsonSerializer.Serialize(books));
  }
}