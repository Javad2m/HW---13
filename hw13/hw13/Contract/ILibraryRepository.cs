using hw13.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Contract;

public interface ILibraryRepository
{
    public List<Book> GetAll();
    public void UpdateB(int userID, Book book);
    public void UpdateR(Book book);
    public Book GetById(int id);

}
