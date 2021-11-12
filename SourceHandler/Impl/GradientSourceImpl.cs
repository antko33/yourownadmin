using CommonLibs.HTTP;
using CommonLibs.Interfaces;
using Newtonsoft.Json;
using SourceHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceHandler.Impl
{
    internal class GradientSourceImpl : ISourceRetriever
    {
        private readonly KenotHTTPClient httpClient = new();
        private readonly Random random = new();

        public async Task<string> RetrieveItem()
        {
            string sourceUrl = GetSourceItemUrl();

            var rawData = await httpClient.GetRawAsync(sourceUrl);
            int startIndex = rawData.IndexOf(SecuredConstants.EdgesListPropertyName);
            rawData = rawData[startIndex..];
            startIndex = rawData.IndexOf("{");
            rawData = rawData[startIndex..];
            int endIndex = GetEndIndex(rawData);
            rawData = rawData[..endIndex];

            var sourceData = JsonConvert.DeserializeObject<SourceData>(rawData);
            List<string> availableUrls = new();
            foreach (var item in sourceData.Edges)
            {
                var node = item.Node;
                if (node.IsVideo)
                {
                    continue;
                }

                if (node.Children == null)
                {
                    availableUrls.Add(node.DisplayUrl);
                }
                else
                {
                    availableUrls.AddRange(node.Children.Edges.Where(i => !i.Node.IsVideo).Select(i => i.Node.DisplayUrl));
                }
            }

            int index = random.Next(availableUrls.Count);
            //return availableUrls[index];
            return @"https://klkfavorit.ru/wp-content/uploads/3/3/7/337ba1247298643b77ac8e18869a72be.jpeg";
        }

        private string GetSourceItemUrl()
        {
            int index = random.Next(SecuredConstants.SourceList.Count);
            string sourceItemName = SecuredConstants.SourceList[index];
            return string.Format(SecuredConstants.SourceUrlPattern, sourceItemName);
        }

        private int GetEndIndex(string data)
        {
            const char LeftBrace = '{';
            const char RightBrace = '}';
            int count = 1;
            int i;
            for (i = 1; i < data.Length && count > 0; i++)
            {
                if (data[i] == LeftBrace)
                {
                    count++;
                }
                else if (data[i] == RightBrace)
                {
                    count--;
                }
            }
            return i;
        }
    }
}
