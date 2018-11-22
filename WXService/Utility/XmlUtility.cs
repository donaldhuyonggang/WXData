using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WXService.Utility
{
    /// <summary>
    /// xml 工具类。
    /// </summary>
    public class XmlUtility
    {
        public static string GetSingleNodeInnerText(string xml, string nodePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode node  = doc.SelectSingleNode(nodePath);
            if (node != null)
            {
                return node.InnerText;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
