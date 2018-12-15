namespace VirtualLibDatabase.Entities
{
    public class BookQRCode
    {
        public string QRCode { get; set; }
        public bool IsTaking { get; set; }
        public int UserId { get; set;}
    }
}