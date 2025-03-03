using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace fetchazuresqlApiManuelTest001.Models
{
    public class OutputType
    {
        [SqlOutput("dbo.Customers", connectionStringSetting: "AzureSqlConnectionString")]
        public Customers Customer { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
}
