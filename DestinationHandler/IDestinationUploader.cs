using System.Threading.Tasks;

namespace DestinationHandler
{
    public interface IDestinationUploader
    {
        Task Upload(string sourceUrl);
    }
}
