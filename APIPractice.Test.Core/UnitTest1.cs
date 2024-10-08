using Newtonsoft.Json;

namespace APIPractice.Test.Core;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        RestClient client = new RestClient("https://gorest.co.in/");
        RestRequest request = new RestRequest("public-api/users");
        request.AddHeader("accept", "application/json")
            .AddHeader("Content-Type", "application/json");


        var response = await client.GetAsync(request);
        var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
        Console.WriteLine(result["data"][1]["name"]);
    }
}