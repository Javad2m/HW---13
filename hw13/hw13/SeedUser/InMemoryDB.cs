using hw13.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.SeedUser;

public static class InMemoryDB
{
    public static User? CurrentUser { get; set; }
}
