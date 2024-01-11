// See https://aka.ms/new-console-template for more information

using EaindrayDotNetCore.ConsoleApp.AdoDotNetExamples;
using EaindrayDotNetCore.ConsoleApp.DapperExamples;
using EaindrayDotNetCore.ConsoleApp.EFCore_Examples;
using EaindrayDotNetCore.ConsoleApp.HttpClientExamples;
using EaindrayDotNetCore.ConsoleApp.RefitExamples;


Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

adoDotNetExample.Run();




//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Run();
//DapperExample dapperExample = new DapperExample();
//dapperExample.Run(); 
//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

Console.WriteLine("Please wait for api...");
Console.ReadKey();

//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Run();

RefitExample refitExample = new RefitExample();
await refitExample.Run();

Console.ReadKey();