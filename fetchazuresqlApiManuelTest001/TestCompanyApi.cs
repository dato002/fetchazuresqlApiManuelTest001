using Azure;
using fetchazuresqlApiManuelTest001.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Net;

namespace fetchazuresqlApiManuelTest001
{
    public class TestCompanyApi
    {
        private readonly ILogger<TestCompanyApi> _logger;

        public TestCompanyApi(ILogger<TestCompanyApi> logger)
        {
            _logger = logger;
        }

        [Function("GetCustomers")]
        public OutputType GetCustomers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Customers")] HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function GetCustomers processed a request.");

            string? connectionString = Environment.GetEnvironmentVariable("AzureSqlConnectionString");

            _logger.LogInformation($"AzureSqlConnectionString: {connectionString}");


            string SqlConnectionString = connectionString;
            SqlConnection conn = new SqlConnection(SqlConnectionString);
            conn.Open();

            string queryString = "SELECT TOP (10) * FROM [dbo].[Customers]";
            SqlCommand command = new SqlCommand(queryString, conn);
            command.CommandType = System.Data.CommandType.Text;

            var message = "Welcome to Azure Functions!";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    _logger.LogInformation($"Successfully Executed Azure Function at:{ DateTime.Now}");
                    _logger.LogInformation($"CustomerId: " + reader[0].ToString());
                    _logger.LogInformation($"FirstName: " + reader[1].ToString());
                    _logger.LogInformation($"LastName: " + reader[2].ToString());
                    _logger.LogInformation($"Email: " + reader[3].ToString());
                    message += reader[0].ToString(); 
                    message += reader[1].ToString(); 
                    message += reader[2].ToString();
                    message += reader[3].ToString();
                }
            }
            conn.Close();


            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteStringAsync(message);

            // Return a response to both HTTP trigger and Azure SQL output binding.
            return new OutputType()
            {
                /*Customer = new Customers
                {
                    Id = System.Guid.NewGuid(),
                    FirstName = "testFirstName",
                    LastName = "testLastName",
                    Email = "testEmail"
                },*/
                HttpResponse = response
            };
            //return new OkObjectResult("Welcome to Azure Functions! GetCustomers called");
        }

        [Function("GetCustomersById")]
        public IActionResult GetCustomersById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Customers/{id}")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function GetCustomersById processed a request.");
            return new OkObjectResult("Welcome to Azure Functions! GetCustomersById called");
        }
    }
}
