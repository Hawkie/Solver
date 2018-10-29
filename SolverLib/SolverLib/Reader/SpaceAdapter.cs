using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Windows.Media.TextFormatting;
using System.Xml;
using System.Xml.Serialization;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.Reader
{
    public class SpaceAdapter<TKey>
    {

        public static void SaveBinary(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void SaveSoap(object obj)
        {
            IFormatter formatter = new SoapFormatter();
            Stream stream = new FileStream("MyFileSoap.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void SaveXml(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            XmlTextWriter xmlWriter = new XmlTextWriter("MyFileXml.xml", null);
            xmlSerializer.Serialize(xmlWriter, obj);

        }

        public static void ToFile(string s, int index)
        {
            Stream stream = new FileStream("MyFile" + index + ".txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Close();
        }

        public static void SaveText(ISpace<TKey> space)
        {
            
            Stream stream = new FileStream("MyFile.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter writer = new StreamWriter(stream);
            
            writer.Close();
            stream.Close();
        }

        public static void SerializeText(ISpace<TKey> space)
        {
            Stream stream = new FileStream("MyFile.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter writer = new StreamWriter(stream);
            
            SerializeText(space, writer);
        
            writer.Close();
            stream.Close();
        }

        public static void SerializeText(ISerializable so, StreamWriter writer)
        {
            SerializationInfo info = new SerializationInfo(so.GetType(), new FormatterConverter());
            StreamingContext context = new StreamingContext();
            so.GetObjectData(info, context);
            SerializationInfoEnumerator e = info.GetEnumerator();

            while (e.MoveNext())
            {
                string name = e.Current.Name;
                string value = e.Current.Value.ToString();
                if (e.Current.ObjectType.IsArray)
                {
                    Array array = e.Current.Value as Array;
                    foreach (ISerializable item in array)
                    {
                        SerializeText(item, writer);
                    }
                }
                writer.WriteLine(name + "," + value);
            }
        }


        


        public static void WriteConsole(ISpace<int> space)
        {
            foreach (KeyValuePair<int, IPossible> value in space)
            {
                if (value.Value.Values.Count == 1)
                {
                    System.Console.Write(value.Value.First());
                }
                else if (value.Value.Values.Count == 0)
                {
                    System.Console.Write("-");
                }
                else
                {
                    System.Console.Write("0");
                }
                int rem = 0;
                Math.DivRem(value.Key, 9, out rem);
                if (rem == 0)
                    System.Console.WriteLine();
            }
        }
    }
}
