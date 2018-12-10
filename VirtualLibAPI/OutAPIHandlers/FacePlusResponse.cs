using VirtualLibrarity.Interfaces;

namespace VirtualLibAPI
{
    public struct FacePlusResponse:IResponse
    { 
        public string Request_id { get; set; }
        public float Confidence { get; set; }
        public object ThreshHolds { get; set; }
        public string Image_id1 { get; set; }
        public string Image_id2 { get; set; }
        public Faces[] Faces1 { get; set; }
        public Faces[] Faces2 { get; set; }
        public int Time_used { get; set; }
        public string Error_message { get; set; }



    }
}