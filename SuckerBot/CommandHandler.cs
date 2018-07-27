using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;



namespace SuckerBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _services;

        /*This Helps us in Error Handling and Structuring the Command Handling*/
        //if you want to make a custom bot this section of the code is not for you

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _services = new CommandService();

            await _services.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;

        }

        private async Task HandleCommandAsync(SocketMessage sarg)
        {
            var arg = sarg as SocketUserMessage;
            if (arg == null) return;

            var context = new SocketCommandContext(_client, arg);
            int argPosition = 0;

            if (arg.HasStringPrefix(Config.bot.cmdPrefix, ref argPosition) || arg.HasMentionPrefix(_client.CurrentUser, ref argPosition))
            {
                var result = await _services.ExecuteAsync(context, argPosition);
                if ((!result.IsSuccess) && (result.Error != CommandError.UnknownCommand))
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }

        }
    }
}
