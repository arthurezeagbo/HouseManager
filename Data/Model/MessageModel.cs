using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class MessageModel : BaseModel
    {
        public MessageModel() : base()
        {

        }

        public string Message { get; set; }
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public bool IsDelivered { get; set; }

    }
}
