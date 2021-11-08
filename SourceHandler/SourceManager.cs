using CommonLibs.Interfaces;
using SourceHandler.Impl;

namespace SourceHandler
{
    public static class SourceManager
    {
        public static ISourceRetriever GetSourceRetriever()
        {
            return new GradientSourceImpl();
        }
    }
}
