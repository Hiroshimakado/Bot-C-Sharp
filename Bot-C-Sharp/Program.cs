﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot_C_Sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().Wait();
        }

        public async Task Run()
        {
            var botClient = new TelegramBotClient("6021502100:AAHOP9cqKcZvxDqAckbdLDsUUGUgXBnjUlk");

            using CancellationTokenSource cts = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery != null)
            {
                if (update.CallbackQuery.Data == "pidor")
                {
                   await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message.Chat.Id,
                        update.CallbackQuery.Message.MessageId,
                        "Факты"

                        );
                }
                else
                {
                    await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message.Chat.Id,
                        update.CallbackQuery.Message.MessageId,
                        "Пиздабол"

                        );
                }
            }
            if (update.Message == null)
                return;
            var message = update.Message;

            
            if (message.Text == null)
                return;

            var messageText = message.Text;

            var chatId = message.From.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ты пидор?",

                replyMarkup: new InlineKeyboardMarkup(new List<InlineKeyboardButton>() {
                    InlineKeyboardButton.WithCallbackData("Да","pidor"),
                    InlineKeyboardButton.WithCallbackData("Нет","not_pidor")
                    }
                    ),
                cancellationToken: cancellationToken) ; 
        }

        

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
