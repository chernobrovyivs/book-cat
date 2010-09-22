using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BookCat
{
	public class UserSettings : ISerializable
	{
		public static object Serializer = new object();

		private static UserSettings InitDefaults()
		{
			// создаем по умолчанию
			List<string> exts = new List<string>();
			exts.Add(".zip");
			exts.Add(".rar");
			exts.Add(".pdf");
			exts.Add(".doc");
			exts.Add(".djvu");
			exts.Add(".txt");

			return new UserSettings {
										txtLocalStore = Application.StartupPath + @"\Collection\",
										Exts = new List<string>()
									};
			
		}

		public static UserSettings deserializeFromXml(string xmlFile)
		{
			UserSettings us;
			try
			{
				lock (Serializer)
				{
					XmlSerializer mySerializer = new XmlSerializer(typeof(UserSettings));
					using (FileStream myFileStream = new FileStream(xmlFile, FileMode.Open))
					{
						us = (UserSettings)mySerializer.Deserialize(myFileStream);
					}
					return us;
				}
			}
			catch 
			{
				if (File.Exists(xmlFile))
				{
					File.SetAttributes(xmlFile, FileAttributes.Normal);
					File.Delete(xmlFile);
				}
				us = InitDefaults();
				us.serializeToXml(xmlFile);
				return us;
			}
		}

		public string txtLocalStore = "";
		public List<string> Exts;

		public void serializeToXml(string xmlFile)
		{
			try
			{
				lock (Serializer)
				{
					XmlSerializer mySerializer = new XmlSerializer(typeof (UserSettings));
					using (StreamWriter myWriter = new StreamWriter(xmlFile))
					{
						mySerializer.Serialize(myWriter, this);
						myWriter.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(UserSettings));
		}
	}
}
