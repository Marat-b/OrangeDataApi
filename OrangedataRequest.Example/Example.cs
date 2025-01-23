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
            // var prKeyPath = "C:\\softz\\Projects\\OrangeData\\private_key_test.xml";
            var prKeyPath = "<RSAKeyValue><Modulus>t8nC/Eth8UabQbXu8pdro3v7NqUanV8Y+g92YgT7z1xqkBLRHXZ1guml3PxrqjNX9AvOmu8R+qaKOyHfJW0PcRDLzCoIUcHNAwpDO/E5j6WAaLIv7gAjTtyr9kJB9rfJaparViJNZu3RSUYGTvVznOmXMf7LTOTMR6HP/5H1TP5n1g4+BbLmC9EhjUf2eNFqwZBqPtzybBb6jaHBRaJ0XdE3lh2OeE9/OF0BtLwiYPDKsVTxIekbNf7l/DREy+YbUOxQLceeHXrvbYLiGWecP0a7CqHGj9ZNY1oJThK3AwrSd4yHa9Wnx/GaZUNtWud1BaP9g3sVX+sRV9xtnI96dw==</Modulus><Exponent>AQAB</Exponent><P>3WSb72a1erb6jcLkyZA2Y21VNIipGz+ta1RP+iacs3xnktFsxgTYgqWyt6SWZ2rStp0u4vb/IAHyKhgJPNTUSi2u0G44MOsRxMC/FWTF8zdyrDF4BjPBM4j84nAmE/FQYv5F8ldDkakc96zEPiTk5Fka3MPeN8mMk6/OA59JdF0=</P><Q>1IRVid5SsDrOwJQAEKkdT436XEb0sVWe9AcU8JyaCEEMj0NPzownNbIrebPofMYdDHikopQpr2XqxZYDbb7AneoHkhEV26TfpPVbN4wBJFXih3lAP2n5hqhgqHGp5Wq2Lu7jUS376Ruw3bhwW+MiWpXv1xhMTZ8AtDfnZFFNvOM=</Q><DP>Fo5KiNCJCtCbpFfH4XVM5UJdXPXTbNBHBdlYMJ9AddTl5IJrt50ExgLFu4oMPMsYXryS61LI2WT5XCqIvmbcnhYbambgWLOKYuZUUYSr2kS67So5FUCunWaGhTdx2bRLQVqwm6kiXDPDnMRAViiCHXWqk/VsrXheVymhLqNK440=</DP><DQ>mowSWMzhfV+G8+2tjnAt7KjnpSvEzyHhEr4DsGdybQZBR/4/j4nFCfukOkFnlTXN8j/aGpF9Lx0C+uX5YFoUYcLL9qGOL8lbCu+TgnXCbtY2gybeXj+HQzI3+MeQMlLEYqU/ks3KIOAOY2+55ljrpszbOqVk+B3luSnekMm/qtk=</DQ><InverseQ>aP5e5F1j6s82Pm7dCpH3mRZWnfZIKqoNQIq2BO8vA9/WrdFI2C27uNhxCp2ZDMulRdBZcoeHcwJjnyDzg4I4gBZ2nSKkVdlN1REoTjLBBdlHi8XKiXzxvpItc2wjNC2AKHaJqj/dnh3bbTAQD1iUAxPmmLJYYkhfZ2i1IrTVxZE=</InverseQ><D>PUfM+Aq6kZSVWAetsL3EajKAxOuwQCDhVx+ovW4j+DQ8Y+WiTEyfShNV9qVD0PBltz3omch1GjpFhQn6OaRvraeIDH9HXttb3FOjr2zzYG4yrrYbPSRWoYj63ZWiIP2O7zdl0caGQHezfNcYa2N0NTG99DGc3/q6EnhlvjWQsSbiEjmxcPx8fmV1i4DoflMQ383nsixAFapgrROUAtCgMvhWn1kSeoojKd+e4eKZxa/SNYulsBJWNFkmo1CZH4YTqlPM+IwYeDUOnOUGNxGurRZ3qQdWs2N2ZQhnrvlh+zpzurD2hwAz6gQXP7mxxMR1xHtAD8XQ+w4OiJK6VWjoIQ==</D></RSAKeyValue>";
            var certPath = "C:\\softz\\Projects\\OrangeData\\client.pfx";
            var certPass = "1234";
            string documentId = "66549876221";

            var dummyOrangeRequest = new OrangeRequest(prKeyPath, certPath, certPass, "https://apip.orangedata.ru:12001/api/v2");

            var dummyCreateCheckRequest = new ReqCreateCheck
            {
                Id = documentId,
                // INN = "5001104058",
                // Id = "66549876217",
                INN = "7727401209",
                Key = "0278951841",
                Group = "Main",
                Content = new Content
                {
                    Type = DocTypeEnum.In,
                    // AgentType = AgentTypeEnum.PayingAgent,
                    AgentType = null,
                    TotalSum = 666.66m,
                    CheckClose = new CheckClose
                    {
                        Payments = new[]
                        {
                            new Payment
                            {
                                // Amount = 132.35m,
                                Amount = 666.66m,
                                Type = PaymentTypeEnum.Cash
                            }
                        },
                        TaxationSystem = TaxationSystemEnum.Simplified
                    },
                    Positions = new []
                    {
                        new Position
                        {
                            Price = 666.66m,
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
            Console.WriteLine($"CheckRequest={JsonConvert.SerializeObject(res1)}");
            Console.WriteLine($"res1.StatusCode={res1.StatusCode}, res1.Response={res1.Response}");
            Console.WriteLine($"res2.StatusCode={res2.StatusCode},res2={res2.Response}");
            // Console.WriteLine($"res3={res3.Response}");
            // Console.WriteLine($"res4={res4.Response}");
            // Console.ReadKey();
        }
    }
}