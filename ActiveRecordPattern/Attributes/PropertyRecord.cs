using System;

namespace ActiveRecordPattern.Attributes
{
    public class PropertyRecord : Attribute
    {
        public string Name { get; set; }
    }
}
