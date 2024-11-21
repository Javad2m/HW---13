using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Entity;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Author { get; set; }

    public User? User { get; set; }
    public int? UserID { get; set; }



}
