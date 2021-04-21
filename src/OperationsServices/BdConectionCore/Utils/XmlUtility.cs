using ModelsCore.Common;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BdConectionCore.Utils
{
    public static class XmlUtility
    {
        public static List<User> ToUserList(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);

                var items = (from r in doc.Root.Elements("user")
                             select new User()
                             {
                                 FullName = (string)r.Attribute("FullName"),
                                 Userid = (int)r.Attribute("id"),
                                 UserLogin = (string)r.Attribute("UserName"),
                             }).ToList();

                return items;
            }
            catch (System.Exception ex)
            {
                return new List<User>();
            }
        }

    }

}
