using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
    class enr
    {
        public static byte[] StrToByteArray(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public static string ByteArrayToStr(byte[] barr)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(barr, 0, barr.Length);
        }

        public static byte[] getBytesFromFile(string fname)
        {
            List<byte> b = new List<byte>();

            FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            for (int i = 0; i < fs.Length; i++)
            {
                b.Add(r.ReadByte());
            }
            r.Close();
            fs.Close();

            return b.ToArray();            
        }

        public static DialogResult dialogRealyDelete
        {
            get
            {
                return MessageBox.Show("Действительно удалить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        public static DialogResult dialogError(string msg)
        {
            return MessageBox.Show(msg, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
