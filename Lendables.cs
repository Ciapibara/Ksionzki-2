namespace Biblioteka
{
  internal interface ILendable
  {
    void BorrowOrReturn();
  }

  internal class Lendable(int id, string name, bool available) : ILendable
  {
    public int Id { get; set;  } = id;
    public string? Name { get; } = name;
    public bool Available { get; set; } = available;

    public void BorrowOrReturn() => Available = !Available;
  }

  internal class Book(int id, string name, bool available, int pageCount) : Lendable(id, name, available)
  {
    public int PageCount { get; } = pageCount;

    public void Show()
    {
      Console.WriteLine($"Title: {Name}");
      Console.WriteLine($"Available: {Available}");
      Console.WriteLine($"Pages: {PageCount}");
      Console.WriteLine();
    }
  }

  /*  internal class Movie(int id, string name, bool available, double Length) : Lendable(id, name, available)
    {
      public double Length { get; } = Length;
    }*/
}