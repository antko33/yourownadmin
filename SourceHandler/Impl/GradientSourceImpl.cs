using CommonLibs.HTTP;
using CommonLibs.Interfaces;
using Newtonsoft.Json;
using SourceHandler.Models;
using System;
using System.Threading.Tasks;

namespace SourceHandler.Impl
{
    internal class GradientSourceImpl : ISourceRetriever
    {
        private readonly KenotHTTPClient httpClient = new();

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
            return sourceUrl;
        }

        private string GetSourceItemUrl()
        {
            int index = new Random().Next(SecuredConstants.SourceList.Count);
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
