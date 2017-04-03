using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books
{
    public static Book[] BooksArray =
        {
            new Book("高等数学", "TP001", 1, 1, 3, 0),
            new Book("大学物理", "TM001", 2, 0, 5, 1)
        };

    public static Book FindBookById(string id)
    {
        foreach (Book b in BooksArray)
        {
            if (b.Id == id)
            {
                return b;
            }
        }

        return new Book();
    }
}

public struct Book
{
    public string Name;
    public string Id;
    public int Floor;
    public int BookshelfNum;
    public int DistanceCount;
    public int Direction;

    public Book(string Name, string Id, int Floor, int BookshelfNum, int DistanceCount, int Direction)
    {
        this.Name = Name;
        this.Id = Id;
        this.Floor = Floor;
        this.BookshelfNum = BookshelfNum;
        this.DistanceCount = DistanceCount;
        this.Direction = Direction;
    }
}
