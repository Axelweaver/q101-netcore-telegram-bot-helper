# Q101.NetCoreTelegramBotHelper
 Telegram Bot Helper .net core v3.1 class library
 
[![NuGet Package](https://img.shields.io/nuget/v/Q101.NetCoreTelegramBotHelper.svg?style=for-the-badge&logo=appveyor)](https://www.nuget.org/packages/Q101.NetCoreTelegramBotHelper)
[![NuGet Package](https://img.shields.io/nuget/dt/Q101.NetCoreTelegramBotHelper.svg?style=for-the-badge&logo=appveyor)](https://www.nuget.org/packages/Q101.NetCoreTelegramBotHelper)

#### This library allows you to send messages on behalf of the bot to Telegram chats and channels, as well as photos


 To install this assembly (class library) on the package manager console tab, run the following command:
```bash

Install-Package Q101.NetCoreTelegramBotHelper -Version 1.0.2

```

### How create telegram bot: https://core.telegram.org/bots

## How to use it

You must create a class that inherits from ITelegramBotHelperConfig

```cs

/// <summary>
/// Configure bot helper
/// </summary>
public class TelegramBotHelperConfig : ITelegramBotHelperConfig
{
    /// <summary>
    /// The token looks something like 123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Optional base url (Default https://api.telegram.org/bot )
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Optional proxy host
    /// </summary>
    public string HttpProxyHost { get; set; }

    /// <summary>
    /// User SSL for proxy
    /// </summary>
    public bool HttpProxyUseSsl { get; set; }	

    /// <summary>
    /// Optional proxy
    /// </summary>
    public string HttpProxyPort { get; set; }

    /// <summary>
    /// Optional login
    /// </summary>
    public string HttpProxyUserName { get; set; }

    /// <summary>
    /// Optional password
    /// </summary>
    public string HttpProxyUserPassword { get; set; }

    public TelegramBotHelperConfig()
    {
        Token = "Here you bot token";
    }
}

```

Then you need to register the dependencies

```cs
		
		...
		
    public void RegisterServices(IServiceCollection services)
    {

        ...
			
        // Here you specify the type of class that you created earlier for configuration
        services.AddQ101TelegramBotHelper<TelegramBotHelperConfig>();

        ...
    }

```

or

```cs

...

using Microsoft.Extensions.DependencyInjection;

...
/// <summary>
/// Main program class
/// </summary>
class Program
{
    /// <summary>
    /// Entry point to application
    /// </summary>
    /// <param name="args"></param>
    static async Task Main(string[] args)
    {

        var services = new ServiceCollection()
            .AddQ101TelegramBotHelper<TelegramBotHelperConfig>()
            .BuildServiceProvider(true);

        ...

    }
}
 ...

```

Then you can do dependency injection into any of your class such as controller class


```cs

/// <summary>
/// Your awesome class
/// </summary>
public class YourAwesomeClass
{
    private readonly ITelegramBotHelper _telegramBotHelper;

    /// <summary>
    /// ctor
    /// </summary>		
    public YourAwesomeClass(ITelegramBotHelper telegramBotHelper)
    {
        _telegramBotHelper = telegramBotHelper;
    }
		
		...

```

And use helper methods


```cs

...
/// <summary>
/// Your awesome class
/// </summary>
public class YourAwesomeClass
{
    private readonly ITelegramBotHelper _telegramBotHelper;

    /// <summary>
    /// ctor
    /// </summary>		
    public YourAwesomeClass(ITelegramBotHelper telegramBotHelper)
    {
        _telegramBotHelper = telegramBotHelper;
    }

    /// <summary>
    /// Send message to telegram chat by your bot
    /// </summary>			
    public async Task<ApiResponseDtoModel<MessageDtoModel>> SendMessage(string chatId, string message)
    {
        var response = await _telegramBotHelper.SendSimpleMessage(chatId, message);

        return response;
    }

    /// <summary>
    /// Send photo to chat by your bot
    /// </summary>			
    public async Task<ApiResponseDtoModel<MessageDtoModel>> SendPhoto(string chatId, string photoFilePath)
    {
        var response = await _telegramBotHelper.SendPhoto(chatId, photoFilePath);

        return response;
    }
		
		...

```

#### Simple example without using dependency injection

```cs

/// <summary>
/// Configure bot helper
/// </summary>
public class TelegramBotHelperConfig : ITelegramBotHelperConfig
{
    /// <summary>
    /// The token looks something like 123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Optional base url (Default https://api.telegram.org/bot )
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Optional proxy host
    /// </summary>
    public string HttpProxyHost { get; set; }

    /// <summary>
    /// User SSL for proxy
    /// </summary>
    public bool HttpProxyUseSsl { get; set; }	

    /// <summary>
    /// Optional proxy
    /// </summary>
    public string HttpProxyPort { get; set; }

    /// <summary>
    /// Optional login
    /// </summary>
    public string HttpProxyUserName { get; set; }

    /// <summary>
    /// Optional password
    /// </summary>
    public string HttpProxyUserPassword { get; set; }

    public TelegramBotHelperConfig()
    {
        Token = "Here you bot token";
    }
}


/// <summary>
/// Main program class
/// </summary>
class Program
{
    /// <summary>
    /// Entry point to application
    /// </summary>
    /// <param name="args"></param>
    static async Task Main(string[] args)
    {

        var telegramConfig = new TelegramBotHelperConfig();

        var telegramHelper = new TelegramBotHelper(telegramConfig);

        var chatId = "yourChatId";

        await _telegramBotHelper.SendSimpleMessage(chatId, "Test text message");

        await _telegramBotHelper.SendPhoto(chatId, @"C:\photo.jpg");
    }
}

```
