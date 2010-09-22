using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BookCat;

namespace BookCat_was
{
    public class Book
    {
        public DataRow book;

        public string bName
        {
            get { return (string)book["Name"]; }
            set { book["Name"] = value; }
        }
        public int bYear
        {
            get { return (int)book["Year"]; }
            set { book["Year"] = value; }
        }
        public long bFile_CRC
        {
            get { return (long)book["File_crc"]; }
            private set { book["File_crc"] = value; }
        }
        public string bFile_fullname
        {
            get { return (string)book["File_fullname"]; }
            set {
                book["File_fullname"] = value;
                bFile_ext = Path.GetExtension(value);
            }
        }
        public string bFile_ext
        {
            get { return (string)book["File_ext"]; }
            private set { book["File_ext"] = value; }
        }
        public byte[] bFile_Contents
        {
            get { return (byte[])book["File_contents"]; }
            set { 
                book["File_contents"] = value;
                bFile_CRC = Crc32.Compute(value);
            }
        }
        public long bBook_id
        {
            get; set;
        }

        public Book (int _book_id)
        {
            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Book_id=@id");
            sc.Parameters.AddWithValue("@id", 10);

            DataTable dt1 = Db.Fill(sc);

            if (dt1.Rows.Count != 1) throw new ApplicationException("Неверный @id");

            bBook_id = _book_id;
            book = dt1.Rows[0];
        }

        public Book()
        {
            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Book_id = -10000");
            DataTable dt1 = Db.Fill(sc);

            book = dt1.NewRow();

            bBook_id = 0;
            //bName = "";
            //bYear = 0;

            book["Name"] = "";
            book["Year"] = 0;

            book["Announce"] = "";
            book["Comments"] = "";
            
        }

        public void Update()
        {
            if (bBook_id == 0)
            {
                SQLiteCommand sc = new SQLiteCommand("INSERT INTO Book(Name, Year, Author_id, File_contents, File_fullname, File_crc, File_ext) VALUES (@Name,@Year,@Author_id,@File_contents,@File_fullname,@File_crc,@File_ext)");
                sc.Parameters.AddWithValue("@Name", bName);
                sc.Parameters.AddWithValue("@Year", bYear);
                sc.Parameters.AddWithValue("@Author_id", 6);
                sc.Parameters.AddWithValue("@File_contents", bFile_Contents);
                sc.Parameters.AddWithValue("@File_fullname", bFile_fullname);
                sc.Parameters.AddWithValue("@File_crc", bFile_CRC);
                sc.Parameters.AddWithValue("@File_ext", bFile_ext);

                bBook_id = Db.ExecuteNonQueryInsert(sc);
            }
            else
            {
                SQLiteCommand sc = new SQLiteCommand(@"UPDATE Book SET 
                                                    Name=@Name, 
                                                    Year=@Year, 
                                                    Author_id=@Author_id, 
                                                    File_contents=@File_contents, 
                                                    File_fullname=@File_fullname, 
                                                    File_crc=@File_crc, 
                                                    File_ext=@File_ext");
                sc.Parameters.AddWithValue("@Name", bName);
                sc.Parameters.AddWithValue("@Year", bYear);
                sc.Parameters.AddWithValue("@Author_id", 6);
                sc.Parameters.AddWithValue("@File_contents", bFile_Contents);
                sc.Parameters.AddWithValue("@File_fullname", bFile_fullname);
                sc.Parameters.AddWithValue("@File_crc", bFile_CRC);
                sc.Parameters.AddWithValue("@File_ext", bFile_ext);

                Db.ExecuteNonQuery(sc);
            }
        }
    }
}
