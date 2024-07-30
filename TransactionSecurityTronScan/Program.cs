using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string transaction_hashes = "853793d552635f533aa982b92b35b00e63a1c1add062c099da2450a15119bcb2";

    static async Task Main()
    {
        var url = $"https://apilist.tronscanapi.com/api/security/transaction/data?hashes={transaction_hashes}";
        var response = await client.GetStringAsync(url);
        using (JsonDocument jsonDoc = JsonDocument.Parse(response))
        {
            var root = jsonDoc.RootElement;
            foreach (var property in root.EnumerateObject())
            {
                string transactionHash = property.Name;
                var riskInfo = property.Value;
                Console.WriteLine($"Transaction Hash: {transactionHash}");
                Console.WriteLine($"RiskToken: {riskInfo.GetProperty("riskToken").GetBoolean()}");
                Console.WriteLine($"ZeroTransfer: {riskInfo.GetProperty("zeroTransfer").GetBoolean()}");
                Console.WriteLine($"RiskAddress: {riskInfo.GetProperty("riskAddress").GetBoolean()}");
                Console.WriteLine($"SameTailAttach: {riskInfo.GetProperty("sameTailAttach").GetBoolean()}");
                Console.WriteLine($"RiskTransaction: {riskInfo.GetProperty("riskTransaction").GetBoolean()}");
            }
        }
    }
}