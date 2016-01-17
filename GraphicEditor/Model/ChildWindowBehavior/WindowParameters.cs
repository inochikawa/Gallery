using System;
using System.IO;
using System.Xml.Serialization;

namespace GraphicEditor.Model.ChildWindowBehavior
{
    [Serializable]
    public class WindowParameters
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsVisible { get; set; }

        public static WindowParameters Load(string filePath)
        {
            XmlSerializer writer = new XmlSerializer(typeof(WindowParameters));

            using (FileStream file = File.Open(filePath, FileMode.OpenOrCreate))
            {
                try
                {
                    return (WindowParameters) writer.Deserialize(file);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void Save(string filePath)
        {
            // Possible FileNotFoundException ?
            XmlSerializer writer = new XmlSerializer(GetType());

            using (FileStream file = File.Create(filePath))
            {
                writer.Serialize(file, this);
            }
        }
    }
}