using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Bot_C_Sharp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            var botClient = new TelegramBotClient("6021502100:AAHOP9cqKcZvxDqAckbdLDsUUGUgXBnjUlk");

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        }
    }
}
