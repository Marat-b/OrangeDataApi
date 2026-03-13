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
            var ffd12 = new Ffd12();
            await ffd12.Run();
        }
    }
}