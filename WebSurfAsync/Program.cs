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
            var web = new WebClient();
            string content;

            try
            {
                content = web.DownloadString(url);
            }
            catch (WebException)
            {
                return;
            }

            Console.WriteLine($"{url.Substring(0, Math.Min(url.Length, 100)),100} - " +
                              $"{content.Length,10}");
        }
    }
}