using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProductApp.Data
{
    public static class DbHelper
    {
        public static string ConnectionString =
            "Server=localhost\\SQLEXPRESS01;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
