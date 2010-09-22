using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Xml;
using Path = System.IO.Path;

namespace BookCat
{
    public class SitesDll
    {
        public SitesDll(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name
        {
            get;set;
        }

        public string Path
        {
            get; set;
        }

        public static List<XmlDocument> GetSearchResults(string strSearchText, string dllSearch)
        {
            List<XmlDocument> list = new List<XmlDocument>();
            try
            {
                ConstructorInfo constructor;
                object[] objArray;
                Assembly assembly = Assembly.LoadFrom(dllSearch);
                string name = System.IO.Path.GetFileNameWithoutExtension(dllSearch) + "." + System.IO.Path.GetFileNameWithoutExtension(dllSearch);
                Type type = assembly.GetType(name, true, true);

                constructor = type.GetConstructor(new Type[] { typeof(string) });
                objArray = new object[] { strSearchText };

                object obj2 = constructor.Invoke(objArray);
                MethodInfo method = type.GetMethod("ReturnData", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                list.Add((XmlDocument)method.Invoke(obj2, null));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "StringResources.AEM_exception_title", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            return list;
        }
    
    }
}