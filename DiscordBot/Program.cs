using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiscordBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;

        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult(); //Start StartAsync() asynchroniusly to main

        public async Task StartAsync()
        {
            Console.WriteLine(Utils.GetFormattedAlert("WELCOME_&NAME", "Bartosz"));

            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose, //get as much inforamtion as possible
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1); //Wait forever
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg);
        }
    }
}
