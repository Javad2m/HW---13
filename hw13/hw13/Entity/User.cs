using hw13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Entity;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public RoleUserEnum Role { get; set; }
    public DateTime BorrowLimitEndDate { get; set; } = DateTime.Now.AddDays(30);
    public List<Book>? Books { get; set; }
}
