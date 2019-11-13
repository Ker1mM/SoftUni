using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("Users")]
    public class UserAndProductDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public List<UserExportDTO> Users { get; set; }
    }

    [XmlType("User")]
    public class UserExportDTO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public UserAndProductSoldProductsDTO SoldProducts { get; set; }
    }

    [XmlType("SoldProducts")]
    public class UserAndProductSoldProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<SoldProductsDTO> Products { get; set; }
    }
}
