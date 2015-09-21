using System;

namespace ActiveRecordPattern.Attributes
{
    public class ActiveRecord : Attribute
    {
        public string Name { get; set; }
    }
}
