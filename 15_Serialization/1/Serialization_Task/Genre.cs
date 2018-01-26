using System.Xml.Serialization;

namespace Serialization_Task
{
    [XmlType(TypeName = "genre")]
    public enum Genre
    {
        [XmlEnum]
        Computer,
        [XmlEnum]
        Fantasy,
        [XmlEnum]
        Romance,
        [XmlEnum]
        Horror,
        [XmlEnum(Name = "Science Fiction")]
        ScienceFiction
    }
}
