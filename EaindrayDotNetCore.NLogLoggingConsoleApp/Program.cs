// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

var logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
logger.LogInformation("Program has started.");
Console.ReadKey();

Console.WriteLine("Hello, World!");