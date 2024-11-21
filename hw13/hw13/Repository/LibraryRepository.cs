using hw13.Contract;
using hw13.DBContex;
using hw13.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Repository;

public class LibraryRepository : ILibraryRepository
{
    private readonly AppDBContex _appContext;
    public LibraryRepository()
    {
        _appContext = new AppDBContex();
    }
    public List<Book> GetAll()
    {
        var books = _appContext.Books.AsNoTracking().ToList();
        return books;
    }

    public Book GetById(int id)
    {
        var book = _appContext.Books.AsNoTracking().Where(t => t.Id == id).FirstOrDefault();
        return book;
    }

    public void UpdateB(int userID, Book book)
    {
        var bok = _appContext.Books.FirstOrDefault(p => p.Id == book.Id);
        bok.UserID = userID;
        _appContext.SaveChanges();
        
        
    }
    public void UpdateR(Book book)
    {
        var bok = _appContext.Books.FirstOrDefault(p => p.Id == book.Id);
        bok.UserID = null;
        _appContext.SaveChanges();
    }

}
