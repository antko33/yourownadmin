using System.Threading.Tasks;

namespace CommonLibs.Interfaces
{
    public interface ISourceRetriever
    {
        Task<string> RetrieveItem();
    }
}
