using hw13.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Contract;

public interface ILibraryServices
{
    public List<Book>? ShowBooks();
    public void Barrow(int userid, int bookid);
    public List<Book>? BarrowList(int userid);

    public void Rbarrow(int bookid);

    public List<Book> ShowAllBooks();
}
