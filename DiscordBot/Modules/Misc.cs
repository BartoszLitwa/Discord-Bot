using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        public List<string> CommandsList = new List<string>() { "Help", "Echo", "Secret", "Data", "MyStats" };

        [Command("MyStats")]
        public async Task MyStats()
        {
            var account = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendMessageAsync($"You have {account.XP} XP and {account.Points} points");
        }

        [Command("data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data Has " + DataStorage.GetPairsCount() + " pairs.");
            DataStorage.AddPairToStorage("Name", Context.User.Username);
        }

        [Command("echo")]
        public async Task Echo([Remainder]string message) //Remainder - whole text  string - gives only 1st word
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message by: " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(Color.Green);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("pick")]
        public async Task Pick([Remainder]string messsage)
        {
            string[] options = messsage.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries); //Removes emtpy strings like test||test

            Random r = new Random();
            string selected = options[r.Next(0, options.Length)]; //Max value is excluded - max = 10 it means max will be 9

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice");
            embed.WithDescription(selected);
            embed.WithColor(Color.Blue);
            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("secret")]
        public async Task Secret([Remainder]string arg = "")
        {
            if (!IfUserHasRole((SocketGuildUser)Context.User, "SecretOwner"))
            {
                await Context.Channel.SendMessageAsync(":x: You need the SecretOwner role to use that command! " + Context.User.Mention);
                return;
            }

            var dmChannel = await Context.User.GetOrCreateDMChannelAsync(); //Get the user channel dm
            await dmChannel.SendMessageAsync(Utils.GetAlert("SECRET"));
        }

        [Command("help")]
        public async Task Help() //Remainder - whole text  string - gives only 1st word
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Commands - help");
            string helpString = "";
            foreach(string s in CommandsList)
            {
                helpString += $"-{s}\n";
            }
            embed.WithDescription(helpString);
            embed.WithColor(Color.Red);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        private bool IfUserHasRole(SocketGuildUser user, string roleName) //To Optimize it - Bot should get the roles id at the start
        {
            var result = from r in user.Guild.Roles where r.Name == roleName select r.Id; //SQL Syntax
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) //Didnt find anything - that role doesnt exists
                return false;

            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}
