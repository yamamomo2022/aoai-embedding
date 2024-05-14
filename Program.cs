using Azure;
using Azure.AI.OpenAI;
using System.Text.Json;
using Microsoft.Extensions.Configuration;


namespace aoai_embedding;
class Program
{
    static void Main(string[] args)
    {
        var cbr = new ConfigurationBuilder()
        .AddUserSecrets<Program>().Build();

        string nonAzureOpenAIApiKey = cbr["OAI_KEY"];
        
        // Uri oaiEndpoint = new ("https://YOUR_RESOURCE_NAME.openai.azure.com");
        // AzureKeyCredential credentials = new (oaiKey);
        // OpenAIClient openAIClient = new (oaiEndpoint, credentials);
        
        var openAIClient = new OpenAIClient(nonAzureOpenAIApiKey, new OpenAIClientOptions());

        EmbeddingsOptions embeddingOptions = new()
        {
            DeploymentName = "text-embedding-ada-002",
            Input = { "熱輻射" },
        };

        var returnValue = openAIClient.GetEmbeddings(embeddingOptions);

        var options = new JsonSerializerOptions { WriteIndented = true };

        string fileName = "./test.json"; 
        string jsonString = JsonSerializer.Serialize(returnValue, options);
        Console.WriteLine(jsonString);
        File.WriteAllText(fileName, jsonString);

        // foreach (float item in returnValue.Value.Data[0].Embedding.ToArray())
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("Hello, World!");
    }
}


