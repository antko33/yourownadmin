using CommonLibs.Interfaces;
using System.Threading.Tasks;

namespace SourceHandler.Impl
{
    internal class GradientSourceImpl : ISourceRetriever
    {
        public Task<string> RetrieveItem()
        {
            return Task.FromResult(@"https://images-api.intrepidgroup.travel/Peregrine/123909/8802845098014.jpg");
        }
    }
}
