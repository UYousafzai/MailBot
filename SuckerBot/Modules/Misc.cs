using Discord;
using Discord.Audio;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SuckerBot.Modules

{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        /*Max_Limit is the amount of maximum mails you're willing to accept at any given moment*/
        static int Max_Limit = 10;
        private static Dictionary<string, string> MailList = new Dictionary<string, string>();

        /*help command (subjected to edit at later points)*/
        [Command("help")]
        public async Task Help()
        {
            string message = "1. If you're looking for a List of Bot Commands Type: !list\n" +
                "2. If you're Interested in the Bot and it's source code just type !Sendmail follwed by your message.\n";
            var embed = new EmbedBuilder();
            embed.WithTitle("Please Read The Following Carefully");
            embed.WithDescription(message);
            embed.WithColor(new Color(83, 20, 119));
            embed.WithFooter("Signed | Brute");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        /*About Command Basically lists all the creator and friends messages*/
        [Command("about")]
        public async Task about()
        {
            string message = "\n`If you want to know a little about us just type these commands:` \n\n" +
                "1.brute\n2.jj\n3.kewb\n4.nigeell\n5.vahiid\n\nNote: Remember to use \"!\" before the Command.\n\n";
            var embed = new EmbedBuilder();
            embed.WithTitle("A Little about the creator and friends");
            embed.WithDescription(message);
            embed.WithColor(new Color(247, 195, 51));
            embed.WithFooter("Signed | Valhalla");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        /*Creator and Friends Messages*/
        [Command("brute")]
        public async Task Creator()
        {
            string message = "This bot is Coded with love by Brute himself, I'll be posting the full source code on Github as an Open source bot\nGithub: https://uyousafzai.github.io/";
            var embed = new EmbedBuilder();
            embed.WithTitle("Brute");
            embed.WithDescription(message);
            embed.WithColor(new Color(83, 20, 119));
            embed.WithFooter("Signed | Brute");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("jj")]
        public async Task Owner()
        {
            string message = "JJ is a pretty chill dude, he'll mostly play what he wants to, He goes by the name JJFisch in the server.";
            var embed = new EmbedBuilder();
            embed.WithTitle("JJ");
            embed.WithDescription(message);
            embed.WithColor(new Color(45, 237, 89));
            embed.WithFooter("Signed | JJ");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Nigeell")]
        public async Task Myman()
        {
            string message = "Nigeell has been the homie of the server, great laugh, a bit of a rager from time to time.";
            var embed = new EmbedBuilder();
            embed.WithTitle("Nigeell");
            embed.WithDescription(message);
            embed.WithColor(new Color(45, 237, 89));
            embed.WithFooter("Signed | Nigeell");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Kewb")]
        public async Task Cube()
        {
            string message = "Kewb has the same width and heigh on all six sides, he's pretty evenly distributed guy.";
            var embed = new EmbedBuilder();
            embed.WithTitle("Kewb");
            embed.WithDescription(message);
            embed.WithColor(new Color(45, 237, 89));
            embed.WithFooter("Signed | Kewb");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Vahiid")]
        public async Task Vahiid()
        {
            string message = "Vahiid is all fun and games until you start playing siege, after that it's a job it ain't fun and games anymore.";
            var embed = new EmbedBuilder();
            embed.WithTitle("Vahiid");
            embed.WithDescription(message);
            embed.WithColor(new Color(45, 237, 89));
            embed.WithFooter("Signed | Vahiid");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        /*Commands Listing Code*/
        [Command("list")]
        public async Task listCommands()
        {
            string message = "All of the Following Commands are available: \n" +
                "1.Sendmail\n2.Checkmail (Admin Only)\n3.Delete (Admin Only)\n4.Deleteall (Admin Only)\n5.About\n\nNote: Remember to use \"!\" before the Command.\n\n`Send mail Command will replace your older mail if you chose to mail again.`";
            var embed = new EmbedBuilder();
            embed.WithTitle("Valhallas Humble Servant");
            embed.WithDescription(message);
            embed.WithColor(new Color(247, 195, 51));
            embed.WithFooter("Signed | Valhalla");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        /*Mailing System*/
        /*CheckMail*/
        [Command("Checkmail")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Inbox()
        {
            MailList = PersistentData.GetAllData();

            var TemplateMessage = await Context.User.GetOrCreateDMChannelAsync();
            await TemplateMessage.SendMessageAsync("Inbox Mail List:\n\n`Username`\t\t\t\t\t\t\t`Message`\n\n");

            string message = "";

            foreach (var dataitem in MailList)
            {

                message = message + dataitem.Key + "\t\t\t\t" + dataitem.Value + "\n\n";
            }

            var DirectMessage = await Context.User.GetOrCreateDMChannelAsync();
            await DirectMessage.SendMessageAsync(message);

            Context.Channel.SendMessageAsync("Please Check Your Inbox For Details.");
        }


        /*Send Mail*/
        [Command("Sendmail")]
        public async Task Send([Remainder] string message)
        {
            string username = Context.User.Username;

            if (PersistentData.GetCount() < Max_Limit)
            {
                if (PersistentData.DoesExist(username))
                {
                    PersistentData.DeleteMail(username);
                    PersistentData.DataStore(username, message);
                    Context.Channel.SendMessageAsync(":thumbsup: Your Message was successfully replaced");
                }
                else
                {
                    PersistentData.DataStore(username, message);
                    Context.Channel.SendMessageAsync(":thumbsup: Your Message has been successfully sent");
                }
            }
            else
            {
                Context.Channel.SendMessageAsync(":thumbsdown: Sorry Mailing List Limit Has Been Reached,\nSpace will be created once the admin has cleared messages");
            }
        }

        /*Clearing Mailing List*/
        /*Selective Delete*/
        [Command("Delete")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Del([Remainder] string id)
        {
            if (PersistentData.DoesExist(id))
            {
                PersistentData.DeleteMail(id);
                Context.Channel.SendMessageAsync(":thumbsup: The Message Has Been Deleted.");
            }
            else
            {
                string message = "'Did you Make sure to copy the user id correctly?'";
                var DirectMessage = await Context.User.GetOrCreateDMChannelAsync();
                await DirectMessage.SendMessageAsync(message);
            }

        }

        /*Delete All*/
        [Command("deleteall")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Delall()
        {
            PersistentData.DeleteAll();
            Context.Channel.SendMessageAsync(":thumbsup: All Messages Have Been Deleted!");
        }


        /*Music Player Fully functional but this was just to mess arond with ffmpeg and to play with streaming music*/
        /*was not required by our channel*/
        /*Bot Music Properties Start*/
        /*Creating Music Stream*/
        //private Process CreateStream(string path)
        //{
        //    var ffmpeg = new ProcessStartInfo
        //    {
        //        FileName = "ffmpeg",
        //        Arguments = $"-i {path} -ac 2 -f s16le -ar 48000 pipe:1",
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //    };
        //    return Process.Start(ffmpeg);
        //}

        ///*Catching the Music Stream created with ffmpeg and feeding it to Discord Bot Output stream*/
        //private async Task SendAsync(IAudioClient client)
        //{
        //    string path = "Theme/theme.mp3";
        //    var ffmpeg = CreateStream(path);
        //    var output = ffmpeg.StandardOutput.BaseStream;
        //    var discord = client.CreatePCMStream(AudioApplication.Mixed);
        //    await output.CopyToAsync(discord);
        //}
        ///*Command Function, this Primarily creates a Client that can be fed data, Primary method for the command*/
        //[Command("Anthem", RunMode = RunMode.Async)]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //public async Task JoinChannel()
        //{
        //    IVoiceChannel channel = null;

        //    channel = (Context.User as IGuildUser)?.VoiceChannel;
        //    if (channel == null) { await Context.Channel.SendMessageAsync(":grimacing: User must be in a voice channel to play the theme"); return; }

        //    await Context.Channel.SendMessageAsync("Hold Your Horses Lads for our Anthem!");

        //    var audioClient = await channel.ConnectAsync();
        //    SendAsync(audioClient);
        //}
        ///*Bot Music Properties End*/

    }

}
