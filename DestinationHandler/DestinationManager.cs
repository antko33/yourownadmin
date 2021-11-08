using CommonLibs.Interfaces;

namespace DestinationHandler
{
    public static class DestinationManager
    {
        public static IDestinationUploader GetDestinationUploader()
        {
            return new BlueDestinationImpl();
        }
    }
}
