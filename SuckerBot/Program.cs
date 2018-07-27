using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace SuckerBot
{
    class Program
    {

        DiscordSocketClient _client;
        CommandHandler _handler;

        /*Main Function has been redirected to start the process asynchronously*/

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null)
            {
                Console.WriteLine("Error 1: The Token was Found Empty");
                return;
            }
            else
            {
                _client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = Discord.LogSeverity.Verbose });
                _client.Log += Log;

                await _client.LoginAsync(TokenType.Bot, Config.bot.token);
                await _client.StartAsync();

                _handler = new CommandHandler();

                await _handler.InitializeAsync(_client);
                await Task.Delay(-1);
            }

        }

        private async Task Log(Discord.LogMessage arg)
        {
            Console.WriteLine(arg.Message);
        }


    }
}
