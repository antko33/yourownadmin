using DestinationHandler;
using System.Text;
using System.Threading.Tasks;

namespace ADMApplication
{
    class App
    {
        static async Task Main(string[] args)
        {
            Init();

            string sourceUrl = @"https://images-api.intrepidgroup.travel/Peregrine/123909/8802845098014.jpg";
            var uploader = DestinationManager.GetDestinationUploader();
            await uploader.Upload(sourceUrl);
        }

        private static void Init()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
        }
    }
}
