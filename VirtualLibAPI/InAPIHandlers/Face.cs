using VirtualLibrarity.Interfaces;

namespace VirtualLibAPI
{
    public struct Face:IFace
    {
        public string Image64String { get; set; } 
        public bool IsForSave { get; set; }
    }
}