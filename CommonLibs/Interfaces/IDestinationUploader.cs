using System.Threading.Tasks;

namespace CommonLibs.Interfaces
{
    public interface IDestinationUploader
    {
        Task Upload(string sourceUrl);
    }
}
