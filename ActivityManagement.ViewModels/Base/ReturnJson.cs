using System.Collections.Generic;
namespace ActivityManagement.ViewModels.Base
{
    public class ReturnJson
    {
        public ReturnJson()
        {
            Message= new List<string>();
        }
        public MessageType MessageType { get; set; }
        public List<string> Message { get; set; }
        public string Html { get; set; }
        public string Script { get; set; }
    }

    public enum MessageType
    {
        Success=1,
        Error=2
    }

}