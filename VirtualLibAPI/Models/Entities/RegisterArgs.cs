using VirtualLibrarity.EFModel;

namespace VirtualLibrarity.Models
{
    public struct RegisterArgs
    {
        public users User { get; set; }
        public string Image { get; set; }
    }
}