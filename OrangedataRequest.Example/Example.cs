using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrangedataRequest;
using OrangedataRequest.DataService;
using OrangedataRequest.Models;
using OrangedataRequest.Enums;

namespace TestLauncher
{
    public class Example
    {
        static async Task Main(string[] args)
        {
            // var prKeyPath = "F:\\tmp\\API\\private_key.xml";
            // var certPath = "F:\\tmp\\API\\client.pfx";
            var prKeyPath = "C:\\softz\\Projects\\OrangeData\\private_key_test.xml";
            var certPath = "C:\\softz\\Projects\\OrangeData\\client.pfx";
            var certPass = "1234";
            string documentId = "66549876219";

            var dummyOrangeRequest = new OrangeRequest(prKeyPath, certPath, certPass);

            var dummyCreateCheckRequest = new ReqCreateCheck
            {
                Id = documentId,
                // INN = "5001104058",
                // Id = "66549876217",
                INN = "7727401209",
                Key = "7727401209",
                // Group = "vend",
                Content = new Content
                {
                    Type = DocTypeEnum.In,
                    // AgentType = AgentTypeEnum.PayingAgent,
                    AgentType = null,
                    TotalSum = 123.45m,
                    CheckClose = new CheckClose
                    {
                        Payments = new[]
                        {
                            new Payment
                            {
                                // Amount = 132.35m,
                                Amount = 123.45m,
                                Type = PaymentTypeEnum.Cash
                            }
                        },
                        TaxationSystem = TaxationSystemEnum.Simplified
                    },
                    Positions = new []
                    {
                        new Position
                        {
                            Price = 123.45m,
                            Quantity = 1m,
                            Tax = VATRateEnum.NONE,
                            Text = "Булка",
                            PaymentMethodType = PaymentMethodTypeEnum.Full,
                            PaymentSubjectType = PaymentSubjectTypeEnum.Product,
                            TaxSum = 0m
                        }
                        // new Position
                        // {
                        //     Price = 4.45m,
                        //     Quantity = 2m,
                        //     Tax = VATRateEnum.VAT110,
                        //     Text = "Спички",
                        //     PaymentMethodType = PaymentMethodTypeEnum.Full,
                        //     PaymentSubjectType = PaymentSubjectTypeEnum.Product
                        // }
                    },
                    CustomerContact = "foo@example.com"
                }
            };

            var dummyCreateCorrectionCheckRequest = new ReqCreateCorrectionCheck
            {
                Id = "66549876216",
                INN = "5001104058",
                Content = new CorrectionContent
                {
                    Type = DocTypeEnum.In,
                    CashSum = 2000,
                    TaxationSystem = TaxationSystemEnum.Common,
                    Tax4Sum = 2000,
                    CauseDocumentDate = DateTime.UtcNow.Date,
                    CauseDocumentNumber = "21"
                }
            };
            Console.WriteLine(JsonConvert.SerializeObject(dummyCreateCheckRequest));
            var res1 = await dummyOrangeRequest.CreateCheckAsync(dummyCreateCheckRequest);
            // var res2 = await dummyOrangeRequest.GetCheckStateAsync("5001104058", "12345678990");
            var res2 = await dummyOrangeRequest.GetCheckStateAsync("7727401209", documentId);
            // var res3 = await dummyOrangeRequest.CreateCorrectionCheckAsync(dummyCreateCorrectionCheckRequest);
            // var res4 = await dummyOrangeRequest.GetCorrectionCheckStateAsync("5001104058", "12345678990");
            Console.WriteLine("Well done!");
            Console.WriteLine($"res1.StatusCode={res1.StatusCode}, res1.Response={res1.Response}");
            Console.WriteLine($"res2.StatusCode={res2.StatusCode},res2={res2.Response}");
            // Console.WriteLine($"res3={res3.Response}");
            // Console.WriteLine($"res4={res4.Response}");
            // Console.ReadKey();
        }
    }
}