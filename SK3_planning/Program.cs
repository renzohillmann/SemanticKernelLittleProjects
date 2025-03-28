using SK3_planning;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder(); // allow us to add services and plugins

// services # the kernel 



builder.AddAzureOpenAIChatCompletion(
    deploymentName: "",
    modelId: "",
    endpoint: "",
    apiKey: "");

// Plugins: SK will know what to do with those depending on our prompts

builder.Plugins.AddFromType<NewsPlugin>(); // add the news plugin to the kernel
builder.Plugins.AddFromType<ArchivePlugin>(); // add the archive plugin to the kernel

// build the kernel
Kernel kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>(); // get the chat completion service to work with

string instructions = "Your name is Patricio. Start the conversation by greeting the user and ask for a news or several news categories" +
    "if the user ask for news, ask if they want to save them in a file";
ChatHistory chatMessages = new ChatHistory(instructions); // create a chat history object

// loop to chat with the AI

while (true)
{

    Console.Write("Prompt:");
    chatMessages.AddUserMessage(Console.ReadLine()); // add user message to chat history

    // get the AI response
    var completion = chatService.GetStreamingChatMessageContentsAsync(
        chatMessages,
        executionSettings: new OpenAIPromptExecutionSettings() //we tell our app that it's ok to execute functions automatically
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions //let us execute the plugin
        }, //besides we 
        kernel: kernel);

    string fullMessage = "";
    await foreach (var content in completion)
    {
        Console.Write(content.Content); // print the AI response
        fullMessage += content.Content; // add the AI response to the full message
    }

    chatMessages.AddAssistantMessage(fullMessage); // add the AI response to the chat history
    Console.WriteLine();

}