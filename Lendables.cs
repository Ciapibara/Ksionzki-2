namespace Biblioteka
{
  internal interface ILendable
  {
    void BorrowOrReturn();
  }

  /* Ta klasa miala by znacznie wiecej sensu, gdyby tych rzeczy do wypozyczenia bylo wiecej.
  Mialem je dodac w trakcie, ale nie chcialem robic z tego wiekszego spaghetti niz aktualnie jest */

  internal class Lendable(int id, string name, bool available) : ILendable
  {
    public int Id { get; set; } = id;
    public string? Name { get; set; } = name;
    public bool Available { get; set; } = available;

    public void BorrowOrReturn() => Available = Available ? false : true;
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