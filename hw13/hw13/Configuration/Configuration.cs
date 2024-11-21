using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Configuration;

public static class Configuration
{
    public static string ConnectionString { get; set; }

    static Configuration()
    {
        ConnectionString = "Data Source=WKK13\\SQLEXPRESS;Initial Catalog=hw13;User ID=sa;Password=111222333aA;TrustServerCertificate=True;Encrypt=True";
    }
}
