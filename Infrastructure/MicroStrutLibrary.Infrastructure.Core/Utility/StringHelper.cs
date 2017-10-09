using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace MicroStrutLibrary.Infrastructure.Core
{
    public static class StringHelper
    {
        public static DataSet XmlToDataSet(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString)) return null;

            DataSet ds = new DataSet();

            using (StringReader reader = new StringReader(xmlString))
            {
                ds.ReadXml(reader);
            }

            return ds;
        }
    }
}
