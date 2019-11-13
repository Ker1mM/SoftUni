using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class ExportPrisonerInboxDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public DateTime IncarcerationDate { get; set; }

        [XmlElement("IncarcerationDate")]
        public string DateFormatted
        {
            get { return this.IncarcerationDate.ToString("yyyy-MM-dd"); }
            set { this.IncarcerationDate = DateTime.Parse(value); }
        }

        public List<MessageDTO> EncryptedMessages { get; set; }
    }

    [XmlType("Message")]
    public class MessageDTO
    {
        public string Description { get; set; }
    }
}
