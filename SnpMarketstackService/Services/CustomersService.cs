using Grpc.Core;
using Microsoft.Extensions.Logging;
using SnpMarketstackService.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnpMarketstackService.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "A";
                output.LastName = "B";
            }
            else 
            {
                output.FirstName = "A";
                output.LastName = "B";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, 
                                                   IServerStreamWriter<CustomerModel> responseStream, 
                                                   ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>()
            {
                new CustomerModel()
                {
                   FirstName = "BB",
                   LastName = "BB",
                   Age = 34
                },

                new CustomerModel()
                {
                   FirstName = "AA",
                   LastName = "AA",
                   Age = 35
                },

                new CustomerModel()
                {
                   FirstName = "CC",
                   LastName = "CC",
                   Age = 36
                }
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
