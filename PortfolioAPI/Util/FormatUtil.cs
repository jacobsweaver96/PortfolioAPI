using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace PortfolioAPI.Util
{
    internal enum Format
    {
        INVALID,
        JSON,
        XML
    }

    internal static class FormatUtil
    {
        private static StringBuilder _xmlHolder = new StringBuilder();
        private static StringWriter _xmlWriter => new StringWriter(_xmlHolder);
        private static StringReader _xmlReader;
        private static XmlSerializer _xmlSerializer => new XmlSerializer(typeof(object));

        public static bool TryParseFormat(string formatStr, out Format formatEnum)
        {
            switch (formatStr)
            {
                case "JSON":
                    formatEnum = Format.JSON;
                    return true;
                case "XML":
                    formatEnum = Format.XML;
                    return true;
                default:
                    formatEnum = Format.INVALID;
                    return false;
            }
        }

        public static string Serialize(Format format, object o)
        {
            switch (format)
            {
                case Format.JSON:
                    return JsonConvert.SerializeObject(o);
                case Format.XML:
                    _xmlSerializer.Serialize(_xmlWriter, o);
                    var ret = _xmlHolder.ToString();
                    _xmlHolder.Clear();
                    return ret;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static T Deserialize<T>(Format format, string o)
        {
            switch (format)
            {
                case Format.JSON:
                    return JsonConvert.DeserializeObject<T>(o);
                case Format.XML:
                    _xmlReader = new StringReader(o);
                    var ret = _xmlSerializer.Deserialize(_xmlReader);
                    _xmlReader = null;
                    if (ret is T)
                    {
                        return (T)ret;
                    }
                    return default(T);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}