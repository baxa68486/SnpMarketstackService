using Grpc.Core;
using Grpc.Net.Client;
using SnpMarketstackService;
using SnpMarketstackService.Protos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //If we want to use both clients, we do need to recreate the channel
            AppContext.SetSwitch(
           "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            //var input = new HelloRequest { Name = "Tim" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5002");
            
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

           
            var newChannel = GrpcChannel.ForAddress("https://localhost:5002");
            var customerClient = new Customer.CustomerClient(newChannel);

            var clientRequested = new CustomerLookupModel() { UserId = 2 };
            
            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}");
                }
            }

                Console.ReadLine();
        }
    }
}
