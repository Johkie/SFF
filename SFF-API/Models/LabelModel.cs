using System;

using System.Xml.Serialization;

namespace SFF_API.Models
{
    [XmlType("EtikettData")]
    public class LabelModel
    {
        [XmlElement("FilmNamn")]
        public string MovieName { get; set; }
        
        [XmlElement("Ort")]
        public string Location { get; set; }
        
        [XmlElement("Datum")]
        public DateTime Date { get; set; }
    }
}
