using CommonLibs;
using CommonLibs.Interfaces;
using DestinationHandler;
using SourceHandler;
using System.Text;
using System.Threading.Tasks;

namespace ADMApplication
{
    class App
    {
        static async Task Main(string[] args)
        {
            Init();

            ISourceRetriever source = SourceManager.GetSourceRetriever();
            string sourceUrl = await source.RetrieveItem();

            IDestinationUploader destination = DestinationManager.GetDestinationUploader();
            await destination.Upload(sourceUrl);
        }

        private static void Init()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
        }
    }
}
