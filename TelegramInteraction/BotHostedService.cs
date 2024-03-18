using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramInteraction;

public class BotHostedService : IHostedService
{
    private readonly TelegramBotClient _botClient;
    private readonly ConcurrentDictionary<long, int> QuerryCounts = new ();
    private readonly CatApi _catApi;

    public BotHostedService(IConfiguration configuration, CatApi catApi)
    {
        _botClient = new TelegramBotClient(configuration["TelegramToken"]!);
        _catApi = catApi;
    }

    /// <summary>
    /// Выполняется при запуске приложения
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken)
    {



        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        // Начинаем принимать сообщения из бота
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

        var me = await _botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
    }

    /// <summary>
    /// Выполняется  при получении ошибки из бота
    /// </summary>
    /// <param name="client">Клиент бота</param>
    /// <param name="exception">Ошибка</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        throw exception;
    }

    /// <summary>
    /// Принимает сообщения из бота
    /// </summary>
    /// <param name="client">Клиент бота</param>
    /// <param name="update">Сообщение</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId} from {message.From}.");

        switch (messageText)
        {
            case Buttons.Button1:
                await CustomerChoose(client, chatId, token);
                
                break;
            case Buttons.Button2:
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Вот каталог: нихуя нет",
                    cancellationToken: token);
                break;
            case Buttons.Button3:
                await client.SendPhotoAsync(chatId: chatId,
                     photo: InputFile.FromString(await _catApi.GetCatUri
                    ()),
                    cancellationToken: token);
                break;
            default:
                // Echo received message text
                var replyKeyboardMarkup = new ReplyKeyboardMarkup( new[] { 
                    new KeyboardButton[] { Buttons.Button1, Buttons.Button2, Buttons.Button3 } 
                }) { ResizeKeyboard = true };

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose a response",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: token);
                break;
        }
    }

    private async Task CustomerChoose(ITelegramBotClient client, long chatId, CancellationToken token)
    {
        var result = QuerryCounts.AddOrUpdate(chatId, 1, (id, count) => count + 1);

        if (result < 3)
        {
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Ваше обращение принято!",
                cancellationToken: token);
        } else
        {
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"Вы подали обращение уже {result} раза. Мы пришли к выводу что вы пидор.",
                cancellationToken: token);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
