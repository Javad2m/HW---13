using hw13.Contract;
using hw13.Entity;
using hw13.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Services;

public class LibraryServices : ILibraryServices
{
    ILibraryRepository _libraryRepository = new LibraryRepository();
    public List<Book>? ShowBooks()
    {
        var books = _libraryRepository.GetAll()
        .Where(x => x.UserID == null).ToList();

        return books;


    }
    public void Barrow(int userid, int bookid)
    {
        var book = _libraryRepository.GetById(bookid);
        _libraryRepository.UpdateB(userid, book);

    }

    public List<Book>? BarrowList(int userid)
    {
        var books = _libraryRepository.GetAll()
        .Where(x => x.UserID == userid).ToList();

        return books;

    }
    public void Rbarrow(int bookid)
    {
        var book = _libraryRepository.GetById(bookid);

        _libraryRepository.UpdateR(book);

    }

    public List<Book> ShowAllBooks()
    {
        var books = _libraryRepository.GetAll()
       .ToList();

        return books;
    }
}
