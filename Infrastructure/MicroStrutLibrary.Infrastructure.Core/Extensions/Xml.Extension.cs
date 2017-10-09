using System;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace MicroStrutLibrary.Infrastructure.Core
{
    /// <summary>
    /// XML扩展
    /// </summary>
    public static class XmlExtension
    {
        /// <summary>
        /// 安全地从父XElement获取子XElement的值
        /// </summary>
        /// <param name="parentXElement">父XElement</param>
        /// <param name="xElementName">子XElement的名称</param>
        /// <returns></returns>
        public static string GetXElementValueSafely(this XElement parentXElement, string xElementName)
        {
            string xElementValue = string.Empty;
            if (parentXElement != null)
            {
                XElement xElement = parentXElement.Element(xElementName);
                if (xElement != null)
                {
                    xElementValue = xElement.Value;
                }
            }

            return xElementValue;
        }

        /// <summary>
        /// 安全地从父XElement获取子XElement的值
        /// </summary>
        /// <param name="parentXElement">父XElement</param>
        /// <param name="xElementName">子XElement的名称</param>
        /// <returns></returns>
        public static T GetXElementValueSafely<T>(this XElement parentXElement, string xElementName)
        {
            T xElementValue = default(T);
            string xElementString = GetXElementValueSafely(parentXElement, xElementName);
            if (string.IsNullOrWhiteSpace(xElementString) == false)
            {
                if (typeof(T).GetTypeInfo().IsEnum)
                {
                    xElementValue = (T)Enum.Parse(typeof(T), xElementString); ;
                }
                else
                {
                    xElementValue = (T)Convert.ChangeType(xElementString, typeof(T));
                }
            }

            return xElementValue;
        }

        /// <summary>
        /// 安全地从XElement获取属性的值
        /// </summary>
        /// <param name="element">XElement</param>
        /// <param name="xAttrName">属性的名称</param>
        /// <returns></returns>
        public static string GetXElementAttrValueSafely(this XElement element, string xAttrName)
        {
            string xElementValue = string.Empty;
            if (element != null)
            {
                XAttribute attribute = element.Attribute(xAttrName);
                if (attribute != null)
                {
                    xElementValue = attribute.Value;
                }
            }

            return xElementValue;
        }

        /// <summary>
        /// 安全地从父XElement获取子XElement的值
        /// </summary>
        /// <param name="element">父XElement</param>
        /// <param name="xAttrName">Attribute的名称</param>
        /// <returns></returns>
        public static T GetXElementAttrValueSafely<T>(this XElement element, string xAttrName)
        {
            T xElementValue = default(T);
            string xElementString = GetXElementAttrValueSafely(element, xAttrName);
            if (string.IsNullOrWhiteSpace(xElementString) == false)
            {
                xElementValue = (T)Convert.ChangeType(xElementString, typeof(T));
            }

            return xElementValue;
        }

        /// <summary>
        /// 获取给定的节点（或者属性）的值
        /// </summary>
        /// <param name="xmlNode">xml节点信息（即在其内通过xPath查找信息）</param>
        /// <param name="nodePath">xml节点xPath</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static string GetNodeValue(this XmlNode xmlNode, string nodePath, string attributeName = "")
        {
            string xpath = nodePath;

            XmlNode targetNode = xmlNode.SelectSingleNode(xpath);

            string nodeValue = string.Empty;
            if (targetNode != null)
            {
                if (string.IsNullOrWhiteSpace(attributeName))
                {
                    nodeValue = xmlNode.SelectSingleNode(xpath).InnerXml;
                }
                else
                {
                    XmlAttribute targetAttr = xmlNode.SelectSingleNode(xpath).Attributes[attributeName];
                    if (targetAttr != null)
                    {
                        nodeValue = xmlNode.SelectSingleNode(xpath).Attributes[attributeName].Value;
                    }
                }
            }

            return nodeValue;
        }
    }
}
