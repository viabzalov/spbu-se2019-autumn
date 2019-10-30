using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebSurfAsync
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var inputUrl = Console.ReadLine();

            await GetMentionedUrls(inputUrl);

            Console.ReadKey();
        }

        private static async Task GetMentionedUrls(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            await Task.WhenAll(doc.DocumentNode.SelectNodes("//a[@href]")
                .Select(node => Task.Run(() => ReadUrlAsync(node.Attributes["href"].Value))).ToList());
        }

        private static void ReadUrlAsync(string url)
        {
            if (!url.StartsWith("http")) return;

            var web = new WebClient();
            var content = "";

            Info.Print(url, content.Length);

            try
            {
                content = web.DownloadString(url);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                Info.Print(url, content.Length);
            }
        }
    }
}