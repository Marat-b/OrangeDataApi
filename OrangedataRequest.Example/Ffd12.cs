using System;
using System.Threading.Tasks;
using OrangedataRequest;
using OrangedataRequest.DataService;
using OrangedataRequest.Enums;
using OrangedataRequest.Models;

namespace TestLauncher;

public class Ffd12
{
    public async Task Run()
    {
        var prKeyPath = "<RSAKeyValue><Modulus>t8nC/Eth8UabQbXu8pdro3v7NqUanV8Y+g92YgT7z1xqkBLRHXZ1guml3PxrqjNX9AvOmu8R+qaKOyHfJW0PcRDLzCoIUcHNAwpDO/E5j6WAaLIv7gAjTtyr9kJB9rfJaparViJNZu3RSUYGTvVznOmXMf7LTOTMR6HP/5H1TP5n1g4+BbLmC9EhjUf2eNFqwZBqPtzybBb6jaHBRaJ0XdE3lh2OeE9/OF0BtLwiYPDKsVTxIekbNf7l/DREy+YbUOxQLceeHXrvbYLiGWecP0a7CqHGj9ZNY1oJThK3AwrSd4yHa9Wnx/GaZUNtWud1BaP9g3sVX+sRV9xtnI96dw==</Modulus><Exponent>AQAB</Exponent><P>3WSb72a1erb6jcLkyZA2Y21VNIipGz+ta1RP+iacs3xnktFsxgTYgqWyt6SWZ2rStp0u4vb/IAHyKhgJPNTUSi2u0G44MOsRxMC/FWTF8zdyrDF4BjPBM4j84nAmE/FQYv5F8ldDkakc96zEPiTk5Fka3MPeN8mMk6/OA59JdF0=</P><Q>1IRVid5SsDrOwJQAEKkdT436XEb0sVWe9AcU8JyaCEEMj0NPzownNbIrebPofMYdDHikopQpr2XqxZYDbb7AneoHkhEV26TfpPVbN4wBJFXih3lAP2n5hqhgqHGp5Wq2Lu7jUS376Ruw3bhwW+MiWpXv1xhMTZ8AtDfnZFFNvOM=</Q><DP>Fo5KiNCJCtCbpFfH4XVM5UJdXPXTbNBHBdlYMJ9AddTl5IJrt50ExgLFu4oMPMsYXryS61LI2WT5XCqIvmbcnhYbambgWLOKYuZUUYSr2kS67So5FUCunWaGhTdx2bRLQVqwm6kiXDPDnMRAViiCHXWqk/VsrXheVymhLqNK440=</DP><DQ>mowSWMzhfV+G8+2tjnAt7KjnpSvEzyHhEr4DsGdybQZBR/4/j4nFCfukOkFnlTXN8j/aGpF9Lx0C+uX5YFoUYcLL9qGOL8lbCu+TgnXCbtY2gybeXj+HQzI3+MeQMlLEYqU/ks3KIOAOY2+55ljrpszbOqVk+B3luSnekMm/qtk=</DQ><InverseQ>aP5e5F1j6s82Pm7dCpH3mRZWnfZIKqoNQIq2BO8vA9/WrdFI2C27uNhxCp2ZDMulRdBZcoeHcwJjnyDzg4I4gBZ2nSKkVdlN1REoTjLBBdlHi8XKiXzxvpItc2wjNC2AKHaJqj/dnh3bbTAQD1iUAxPmmLJYYkhfZ2i1IrTVxZE=</InverseQ><D>PUfM+Aq6kZSVWAetsL3EajKAxOuwQCDhVx+ovW4j+DQ8Y+WiTEyfShNV9qVD0PBltz3omch1GjpFhQn6OaRvraeIDH9HXttb3FOjr2zzYG4yrrYbPSRWoYj63ZWiIP2O7zdl0caGQHezfNcYa2N0NTG99DGc3/q6EnhlvjWQsSbiEjmxcPx8fmV1i4DoflMQ383nsixAFapgrROUAtCgMvhWn1kSeoojKd+e4eKZxa/SNYulsBJWNFkmo1CZH4YTqlPM+IwYeDUOnOUGNxGurRZ3qQdWs2N2ZQhnrvlh+zpzurD2hwAz6gQXP7mxxMR1xHtAD8XQ+w4OiJK6VWjoIQ==</D></RSAKeyValue>";
        var certPath = "orange_test_client.pfx";
        var certPass = "1234";

        Random random = new Random();
        int randomNumber = random.Next(1000, 999999999);
        string documentId = randomNumber.ToString();
            
        Console.WriteLine("Starting");

        var dummyOrangeRequest = new OrangeRequest(prKeyPath, certPath, certPass, "https://apip.orangedata.ru:12001/api/v2");

        var dummyCreateCheckRequest = new ReqCreateCheck
        {
            Id = "11223344",
            INN = "7727401209",
            Content = new Content
            {
                Type = DocTypeEnum.In,
                AgentType = AgentTypeEnum.PayingAgent,
                TotalSum = 132.35m,
                CheckClose = new CheckClose
                {
                    Payments = new[]
                    {
                        new Payment
                        {
                            Amount = 132.35m,
                            Type = PaymentTypeEnum.Cash
                        }
                    },
                    TaxationSystem = TaxationSystemEnum.Simplified
                },
                Positions = new[]
                {
                    new Position
                    {
                        Price = 123.45m,
                        Quantity = 1m,
                        Tax = VATRateEnum.NONE,
                        Text = "Булка",
                        PaymentMethodType = PaymentMethodTypeEnum.Full,
                        PaymentSubjectType = PaymentSubjectTypeEnum.Product
                    },
                    new Position
                    {
                        Price = 4.45m,
                        Quantity = 2m,
                        Tax = VATRateEnum.VAT110,
                        Text = "Спички",
                        PaymentMethodType = PaymentMethodTypeEnum.Full,
                        PaymentSubjectType = PaymentSubjectTypeEnum.Product
                    }
                },
                CustomerContact = "foo@example.com"
            }
        };

        var dummyCreateCorrectionCheckRequest = new ReqCreateCorrectionCheck
        {
            Id = "1122334414",
            INN = "7727401209",
            Content = new CorrectionContent
            {
                Type = DocTypeEnum.In,
                CashSum = 2000,
                TaxationSystem = TaxationSystemEnum.Common,
                Tax4Sum = 2000,
                CauseDocumentDate = DateTime.UtcNow.Date,
                CauseDocumentNumber = "11223344"
            }
        };

        var dummyCreateCheckRequest12 = new ReqCreateCheck
        {
            Id = documentId,
            INN = "7727401209",
            Group = "vend_2",
            Key = "0278951841",
            Content = new Content
            {
                Type = DocTypeEnum.In,
                Positions = new Position[]
                {
                    new Position
                    {
                        Quantity = 1.000M,
                        Price = 123.45M,
                        Tax = VATRateEnum.NONE,
                        Text = "Булка",
                        PaymentMethodType = PaymentMethodTypeEnum.Full,
                        PaymentSubjectType = PaymentSubjectTypeEnum.ATM,
                        // AgentType = AgentTypeEnum.Other,
                        // AgentInfo = new AgentInfo
                        // {
                        //     PaymentTransferOperatorPhoneNumbers = new string[] { "+79200000001", "+74997870001" },
                        //     PaymentAgentOperation = "Какая-то операция 1",
                        //     PaymentAgentPhoneNumbers = new string[] { "+79200000003" },
                        //     PaymentOperatorPhoneNumbers = new string[] { "+79200000002", "+74997870002" },
                        //     PaymentOperatorName = "ООО \"Атлант\"",
                        //     PaymentOperatorAddress = "Воронеж, ул. Недогонная, д. 84",
                        //     PaymentOperatorINN = "7727257386"
                        // },
                        // AdditionalAttribute = "Доп. атрибут и все тут",
                        // ManufacturerCountryCode = "643",
                        // CustomsDeclarationNumber = "АД 11/77 от 01.08.2018",
                        // Excise = (decimal)23.45,
                        // UnitTaxSum = (decimal)0.23,
                        ItemCode = "010460406000600021N4N57RSCBUZTQ\u001d2403004002910161218\u001d1724010191ffd0\u001d92tIAF/YVoU4roQS3M/m4z78yFq0fc/WsSmLeX5QkF/YVWwy8IMYAeiQ91Xa2z/fFSJcOkb2N+uUUmfr4n0mOX0Q==",
                        // PlannedStatus = 2,
                        // FractionalQuantity = new System.Collections.Generic.Dictionary<string, ulong>
                        // {
                        //     { "numerator", 1 },
                        //     { "denominator", 2 }
                        // },
                        // IndustryAttribute = new System.Collections.Generic.Dictionary<string, string>
                        // {
                        //     { "foivId", "012" },
                        //     { "causeDocumentDate", "12.08.2021" },
                        //     { "causeDocumentNumber", "666" },
                        //     { "value", "position industry" }
                        // },
                        // Barcodes = new System.Collections.Generic.Dictionary<string, string>
                        // {
                        //     { "ean8", "46198532" },
                        //     { "ean13", "4006670128002" },
                        //     { "itf14", "14601234567890" },
                        //     { "gs1", "010460043993125621JgXJ5.T" },
                        //     { "mi", "RU-401301-AAA0277031" },
                        //     { "egais20", "NU5DBKYDOT17ID980726019" },
                        //     { "egais30", "13622200005881" },
                        //     { "f1", null },
                        //     { "f2", null },
                        //     { "f3", null },
                        //     { "f4", null },
                        //     { "f5", null },
                        //     { "f6", null }
                        // }
                    },
                    new Position
                    {
                        Quantity = 2.000M,
                        Price = 4.45M,
                        Tax = VATRateEnum.VAT110,
                        Text = "Спички",
                        PaymentMethodType = PaymentMethodTypeEnum.Full,
                        PaymentSubjectType = PaymentSubjectTypeEnum.Other,
                        // SupplierINN = "9715225506",
                        // SupplierInfo = new SupplierInfo
                        // {
                        //     PhoneNumbers = new string[] { "+79266660011", "+79266660022" },
                        //     Name = "ПАО \"Адамас\""
                        // },
                        QuantityMeasurementUnit = 10
                    }
                },
                CheckClose = new CheckClose
                {
                    Payments = new Payment[]
                    {
                        new Payment { Type = PaymentTypeEnum.Cash, Amount = 123.45M },
                        new Payment { Type = PaymentTypeEnum.Emoney, Amount = 8.90000M }
                    },
                    TaxationSystem = TaxationSystemEnum.Simplified
                },
                CustomerContact = "foo@example.com",
                // AdditionalUserAttribute = new System.Collections.Generic.Dictionary<string, string>
                // {
                //     { "name", "Любимая цитата" },
                //     { "value", "В здоровом теле здоровый дух, этот лозунг еще не потух!" }
                // },
                AutomatNumber = "123456789",
                SettlementAddress = "г.Москва, Красная площадь, д.1",
                SettlementPlace = "Палата №6",
                // AdditionalAttribute = "Доп атрибут чека",
                Cashier = "Кассир",
                SenderEmail = "ru@example.mail",
                // CustomerInfo = new System.Collections.Generic.Dictionary<string, string>
                // {
                //     { "name", "Кузнецов Иван Петрович" },
                //     { "inn", "7727401209" },
                //     { "birthDate", "15.09.1988" },
                //     { "citizenship", "643" },
                //     { "identityDocumentCode", "01" },
                //     { "identityDocumentData", "multipassport" },
                //     { "address", "Басеенная 36" }
                // },
                // OperationalAttribute = new OperationalAttribute
                // {
                //     Date = "2021-08-12T18:36:16",
                //     Id = 0,
                //     Value = "operational"
                // },
                // IndustryAttribute = new System.Collections.Generic.Dictionary<string, string>
                // {
                //     { "foivId", "010" },
                //     { "causeDocumentDate", "11.08.2021" },
                //     { "causeDocumentNumber", "999" },
                //     { "value", "industry" }
                // },
                FfdVersion = FfdVersionEnum.Ffd12,
                TotalSum = 132.35m
            },
            // Meta = "some meta",
            // CallbackUrl = "http://call.back/?doc=2",
            IgnoreItemCodeCheck = false
        };

        // var dummyCreateCorrectionCheckRequest12 = new ReqCreateCorrectionCheck12
        // {
        //     Id = "11223344121",
        //     INN = "7727401209",
        //     Group = "Main_2",
        //     Key = "7727401209",
        //     Content = new CorrectionContent12
        //     {
        //         FfdVersion = FfdVersionEnum.Ffd12,
        //         Type = DocTypeEnum.In,
        //         Positions = new Position[]
        //         {
        //             new Position
        //             {
        //                 Quantity = 1.000M,
        //                 Price = 123.45M,
        //                 Tax = VATRateEnum.NONE,
        //                 Text = "Булка",
        //                 PaymentMethodType = PaymentMethodTypeEnum.Full,
        //                 PaymentSubjectType = PaymentSubjectTypeEnum.Product,
        //                 AgentType = AgentTypeEnum.Other,
        //                 AgentInfo = new AgentInfo
        //                 {
        //                     PaymentTransferOperatorPhoneNumbers = new string[] { "+79200000001", "+74997870001" },
        //                     PaymentAgentOperation = "Какая-то операция 1",
        //                     PaymentAgentPhoneNumbers = new string[] { "+79200000003" },
        //                     PaymentOperatorPhoneNumbers = new string[] { "+79200000002", "+74997870002" },
        //                     PaymentOperatorName = "ООО \"Атлант\"",
        //                     PaymentOperatorAddress = "Воронеж, ул. Недогонная, д. 84",
        //                     PaymentOperatorINN = "7727257386"
        //                 },
        //                 AdditionalAttribute = "Доп. атрибут и все тут",
        //                 ManufacturerCountryCode = "643",
        //                 CustomsDeclarationNumber = "АД 11/77 от 01.08.2018",
        //                 Excise = (decimal)23.45,
        //                 UnitTaxSum = (decimal)0.23,
        //                 ItemCode = "010460406000600021N4N57RSCBUZTQ\u001d2403004002910161218\u001d1724010191ffd0\u001d92tIAF/YVoU4roQS3M/m4z78yFq0fc/WsSmLeX5QkF/YVWwy8IMYAeiQ91Xa2z/fFSJcOkb2N+uUUmfr4n0mOX0Q==",
        //                 PlannedStatus = 2,
        //                 FractionalQuantity = new System.Collections.Generic.Dictionary<string, ulong>
        //                 {
        //                     { "numerator", 1 },
        //                     { "denominator", 2 }
        //                 },
        //                 IndustryAttribute = new System.Collections.Generic.Dictionary<string, string>
        //                 {
        //                     { "foivId", "012" },
        //                     { "causeDocumentDate", "12.08.2021" },
        //                     { "causeDocumentNumber", "666" },
        //                     { "value", "position industry" }
        //                 },
        //                 Barcodes = new System.Collections.Generic.Dictionary<string, string>
        //                 {
        //                     { "ean8", "46198532" },
        //                     { "ean13", "4006670128002" },
        //                     { "itf14", "14601234567890" },
        //                     { "gs1", "010460043993125621JgXJ5.T" },
        //                     { "mi", "RU-401301-AAA0277031" },
        //                     { "egais20", "NU5DBKYDOT17ID980726019" },
        //                     { "egais30", "13622200005881" },
        //                 }
        //             }
        //         },
        //         CheckClose = new CheckClose
        //         {
        //             Payments = new Payment[] { new Payment { Type = PaymentTypeEnum.Cash, Amount = 123.45M } },
        //             TaxationSystem = TaxationSystemEnum.Simplified
        //         },
        //         CustomerContact = "foo@example.com",
        //         CorrectionType = CorrectionTypeEnum.OnPrescription,
        //         CauseDocumentDate = "2021-08-13T00:00:00",
        //         CauseDocumentNumber = "1122334412",
        //         AdditionalUserAttribute = new System.Collections.Generic.Dictionary<string, string>
        //         {
        //             { "name", "Любимая цитата" },
        //             { "value", "В здоровом теле здоровый дух, этот лозунг еще не потух!" }
        //         },
        //         //AutomatNumber = "123456789",
        //         //SettlementAddress = "г.Москва, Красная площадь, д.1",
        //         //SettlementPlace = "Палата №6",
        //         AdditionalAttribute = "Доп атрибут чека",
        //         Cashier = "Кассир",
        //         SenderEmail = "ru@example.mail",
        //         CustomerInfo = new System.Collections.Generic.Dictionary<string, string>
        //         {
        //             { "name", "Кузнецов Иван Петрович" },
        //             { "inn", "7727401209" },
        //             { "birthDate", "15.09.1988" },
        //             { "citizenship", "643" },
        //             { "identityDocumentCode", "01" },
        //             { "identityDocumentData", "multipassport" },
        //             { "address", "Басеенная 36" }
        //         },
        //         OperationalAttribute = new OperationalAttribute
        //         {
        //             Date = "2021-08-12T18:36:16",
        //             Id = 0,
        //             Value = "operational"
        //         },
        //         IndustryAttribute = new System.Collections.Generic.Dictionary<string, string>
        //         {
        //             { "foivId", "010" },
        //             { "causeDocumentDate", "11.08.2021" },
        //             { "causeDocumentNumber", "999" },
        //             { "value", "industry" }
        //         },
        //         TotalSum = 123.45m
        //     }
        // };

        #region  AccessSate
        // Console.WriteLine("GetAccessStateAsync...");
        // var checkAccess = await dummyOrangeRequest.GetAccessStateAsync(new ReqAccessStatus { INN = "7727401209", Group = "Main_2", Key = "0278951841" });
        // Console.WriteLine("Result statusCode: " + checkAccess.StatusCode + "; response: " + checkAccess.Response);
        #endregion
        // Console.WriteLine("CreateCheckAsync...");
        // var res1 = await dummyOrangeRequest.CreateCheckAsync(dummyCreateCheckRequest);
        // Console.WriteLine("Result statusCode: " + res1.StatusCode + "; response: " + res1.Response);
        //
        // Console.WriteLine("GetCheckStateAsync...");
        // var res2 = await dummyOrangeRequest.GetCheckStateAsync("7727401209", "11223344");
        // Console.WriteLine("Result statusCode: " + res2.StatusCode + "; response: " + res2.Response);

        // Console.WriteLine("CreateCorrectionCheckAsync...");
        // var res3 = await dummyOrangeRequest.CreateCorrectionCheckAsync(dummyCreateCorrectionCheckRequest);
        // Console.WriteLine("Result statusCode: " + res3.StatusCode + "; response: " + res3.Response);
        //
        // Console.WriteLine("GetCorrectionCheckStateAsync...");
        // var res4 = await dummyOrangeRequest.GetCorrectionCheckStateAsync("7727401209", "1122334414");
        // Console.WriteLine("Result statusCode: " + res4.StatusCode + "; response: " + res4.Response);

        Console.WriteLine("CreateCheckAsync for 1.2...");
        var res5 = await dummyOrangeRequest.CreateCheckAsync(dummyCreateCheckRequest12);
        Console.WriteLine("Result statusCode: " + res5.StatusCode + "; response: " + res5.Response);

        Console.WriteLine("GetCheckStateAsync...");
        var res6 = await dummyOrangeRequest.GetCheckStateAsync("7727401209", documentId);
        Console.WriteLine("Result statusCode: " + res6.StatusCode + "; response: " + res6.Response);

        // Console.WriteLine("GetKKTDevicesStateAsync...");
        // var res61 = await dummyOrangeRequest.GetKKTDevicesStateAsync("7727401209", "Main_2");
        // Console.WriteLine("Result statusCode: " + res61.StatusCode + "; response: " + res61.Response);

        // Console.WriteLine("CreateCorrectionCheckAsync12...");
        // var res7 = await dummyOrangeRequest.CreateCorrectionCheckAsync12(dummyCreateCorrectionCheckRequest12);
        // Console.WriteLine("Result statusCode: " + res7.StatusCode + "; response: " + res7.Response);
        //
        // Console.WriteLine("GetCorrectionCheckStateAsync12...");
        // var res8 = await dummyOrangeRequest.GetCorrectionCheckStateAsync12("7727401209", "11223344121");
        // Console.WriteLine("Result statusCode: " + res8.StatusCode + "; response: " + res8.Response);

        #region ItemCodeCheck
        // randomNumber = random.Next(1000, 999999999);
        // documentId = randomNumber.ToString();
        //
        // Console.WriteLine("CreateItemCodeCheckAsync12...");
        // var res9 = await dummyOrangeRequest.CreateItemCodeCheckAsync12(new ReqItemCodeCheck
        // {
        //     Id = documentId,
        //     INN = "7727401209",
        //     Group = "Main_2",
        //     Key = "0278951841",
        //     Content = new ItemCodeContent
        //     {
        //         // Quantity = 1,
        //         // QuantityMeasurementUnit = 0,
        //         PlannedStatus = 1 ,
        //         ItemCode = "00000046210654eK7fYtcAAModGVz"
        //     }
        // });
        // Console.WriteLine("Result statusCode: " + res9.StatusCode + "; response: " + res9.Response);

        
        // Console.WriteLine("GetItemCodeStateAsync...");
        // var res10 = await dummyOrangeRequest.GetItemCodeStateAsync("7727401209", documentId);
        // Console.WriteLine("Result statusCode: " + res10.StatusCode + "; response: " + res10.Response);
        //
        // Console.WriteLine("Press any key to exit...");
        // Console.ReadKey();
        // Console.WriteLine("END");
        
        #endregion
    }

}