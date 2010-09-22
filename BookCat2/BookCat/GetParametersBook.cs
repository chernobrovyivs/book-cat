using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace BookCat
{
    public class GetParametersBook //: INotifyPropertyChanged
    {
        /*


        // Fields
        public byte[] _bytethumbs;
        private string _description = string.Empty;
        private DataSet1.GenreRow _drgenre;
        private DataSet1.SeriesRow _drseries;
        private string _height = string.Empty;
        private bool _isadobe;
        private bool _isgs;
        private string _lenght = string.Empty;
        private string _LocationDrive;
        private string _LocationDriveName;
        private string _LocationType;
        private Image _thumbs;
        private string _title;
        private string _toc;
        private string _width = string.Empty;
        public List<string> al = new List<string>();
        public int Book_Id;
        public DataSet1.BooksRow dr;
        private DataSet1.PublishersRow drpublisher;
        private string genrefullpath = string.Empty;
        private bool IsNew;
        private bool IsThumbsNull;
        public List<string> lstTOC;

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Methods
        public GetParametersBook(int book_Id)
        {
            if (book_Id != 0)
            {
                this.Book_Id = book_Id;
                this.dr = Program.ds.Books.FindByBook_Id(this.Book_Id);
                if ((this.dr == null) || (book_Id < 0))
                {
                    return;
                }
                this.FillAuthors();
            }
            else
            {
                this.IsNew = true;
                this.dr = Program.ds.Books.NewBooksRow();
                this.dr.Date_Add = DateTime.Now;
                this.dr.Last_Access = DateTime.Now;
                this.dr.View_Count = 0;
                this.dr.Date = DateTime.Now;
            }
            this._title = this.Title;
        }

        public static int ArTheSameDrive(string serial)
        {
            DataSet1.DrivesRow[] rowArray = Program.ds.Drives.Select(string.Format("Serial = '{0}'", serial)) as DataSet1.DrivesRow[];
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                return rowArray[0].Drive_Id;
            }
            return 0;
        }

        public string AuthorsToString()
        {
            string str = null;
            for (int i = 0; i < this.Alauthors.Count; i++)
            {
                string[] strArray = this.Alauthors[i].ToString().Split(new char[] { ',' });
                if (str == null)
                {
                    if (strArray.Length > 1)
                    {
                        str = strArray[1].TrimEnd(new char[0]).TrimStart(new char[0]) + " " + strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                    }
                    else
                    {
                        str = this.Alauthors[i].ToString().TrimEnd(new char[0]).TrimStart(new char[0]);
                    }
                }
                else if (strArray.Length > 1)
                {
                    str = str + ", " + strArray[1].TrimEnd(new char[0]).TrimStart(new char[0]) + " " + strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                }
                else
                {
                    str = str + ", " + this.Alauthors[i].ToString().TrimEnd(new char[0]).TrimStart(new char[0]);
                }
            }
            return str;
        }

        public static int CreateAndSaveDrive(LogicalDrives drive)
        {
            BaseAccess.Drives_IDENTITY = -1;
            DataSet1.DrivesRow[] rowArray = Program.ds.Drives.Select(string.Format("Serial = '{0}'", drive.Serial)) as DataSet1.DrivesRow[];
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                return rowArray[0].Drive_Id;
            }
            DataSet1.DrivesRow row = Program.ds.Drives.NewDrivesRow();
            row.Serial = drive.Serial;
            row.Letter = drive.Letter;
            row.Name = drive.Label;
            row.Type = drive.DriveType;
            Program.ds.Drives.Rows.Add(row);
            BaseAccess.AccessDrivesTable(row);
            row.Drive_Id = BaseAccess.Drives_IDENTITY;
            row.AcceptChanges();
            return BaseAccess.Drives_IDENTITY;
        }

        public BitmapSource CreateBitmap(IntPtr handle)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public void DeleteBooks()
        {
            try
            {
                DataRow[] rowArray = Program.ds.Book_Author.Select(string.Format("Book_Id = {0}", this.Book_Id));
                if ((rowArray != null) && (rowArray.Length > 0))
                {
                    foreach (DataRow row in rowArray)
                    {
                        row.Delete();
                    }
                }
                this.dr.Delete();
                using (DataSet1 set = Program.ds.GetChanges() as DataSet1)
                {
                    if (set != null)
                    {
                        using (DataSet1.Book_AuthorDataTable table = set.Tables["Book_Author"] as DataSet1.Book_AuthorDataTable)
                        {
                            if (table != null)
                            {
                                BaseAccess.UpdateBook_AuthorsTable(table);
                                Program.ds.Book_Author.AcceptChanges();
                            }
                        }
                        BaseAccess.AccessBooksRow(this.dr);
                        Program.ds.Books.AcceptChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, StringResources.AEM_exception_title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public string DimensionToString()
        {
            if (!this.dr.IsDimensionNull())
            {
                try
                {
                    return string.Format(StringResources.GPB_Dimension, Math.Round((double) (Convert.ToDouble(this.Width) / 100.0), 2), Math.Round((double) (Convert.ToDouble(this.Lenght) / 100.0), 2), Math.Round((double) (Convert.ToDouble(this.Height) / 100.0), 2));
                }
                catch
                {
                    return string.Format(StringResources.GPB_Dimension_hun, this.Width, this.Lenght, this.Height);
                }
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            GetParametersBook book = (GetParametersBook) obj;
            return (book.Book_Id == this.Book_Id);
        }

        public void FillAuthors()
        {
            foreach (DataRow row in this.dr.GetChildRows("Books_Book_Author"))
            {
                DataSet1.AuthorsRow parentRow = row.GetParentRow("Authors_Book_Author") as DataSet1.AuthorsRow;
                if (parentRow != null)
                {
                    this.al.Add(parentRow.Name);
                }
            }
        }

        public string GetDescriptionTextFormat()
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(this.Description) && this.Description.Contains(@"{\rtf1\"))
            {
                l l = new l();
                l.Rtf = this.Description;
                l.SelectionBackColor = Color.White;
                str = l.a(true, true);
                l = null;
                return str;
            }
            return this.Description;
        }

        public string GetDriveLetter(int nDriveId)
        {
            DataSet1.DrivesRow row = Program.ds.Drives.FindByDrive_Id(nDriveId);
            if (row != null)
            {
                return row.Letter;
            }
            return string.Empty;
        }

        private void GetGenreParent(int parentgenid)
        {
            DataSet1.GenreRow row = Program.ds.Genre.FindByGen_Id(parentgenid);
            if ((((row != null) && (parentgenid != 0)) && (row.Name != "Genres")) && (row.Name.ToString() != "Genres"))
            {
                if (string.IsNullOrEmpty(this.genrefullpath))
                {
                    this.genrefullpath = row.Name;
                }
                else
                {
                    this.genrefullpath = string.Format("{1}>>{0}", this.genrefullpath, row.Name);
                }
                this.GetGenreParent(row.Parent_Id);
            }
        }

        public int GetHashCode(GetParametersBook obj)
        {
            return obj.GetHashCode();
        }

        public void GetSubtitlefromTitle()
        {
            string title = this.Title;
            string[] strArray = title.Split(new char[] { ':' });
            if (strArray.Length >= 2)
            {
                this.Title = strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                this.Subtitle = title.Remove(0, this.Title.Length + 1).TrimEnd(new char[0]).TrimStart(new char[0]);
            }
            else
            {
                this.Title = this.Title.TrimEnd(new char[0]).TrimStart(new char[0]);
                this.Subtitle = string.Empty;
            }
        }

        public string GetTOCTextFormat()
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(this.Toc) && this.Toc.Contains(@"{\rtf1\"))
            {
                l l = new l();
                l.Rtf = this.Toc;
                l.SelectionBackColor = Color.White;
                str = l.a(true, true);
                l = null;
                return str;
            }
            return this.Toc;
        }

        public void ModifyBook()
        {
            if ((this.dr != null) && ((this.dr.RowState == DataRowState.Modified) || (this.dr.RowState == DataRowState.Added)))
            {
                BaseAccess.AccessBooksRow(this.dr);
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private string RTFtoText(string _text)
        {
            string rtf = string.Empty;
            if (!string.IsNullOrEmpty(_text) && !_text.StartsWith(@"{\rtf1\"))
            {
                using (RichTextBox box = new RichTextBox())
                {
                    box.Text = _text;
                    rtf = box.Rtf;
                    box.Dispose();
                    return rtf;
                }
            }
            return _text;
        }

        public void SaveBook()
        {
            try
            {
                if (this.IsNew)
                {
                    if (Program.ds.Books.Count >= 50)
                    {
                        MessageBox.Show(StringResources.Main_demo_mess, StringResources.AEM_title_mess, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        Program.ds.Books.RejectChanges();
                        Program.ds.Authors.RejectChanges();
                        Program.ds.Book_Author.RejectChanges();
                    }
                    else
                    {
                        Program.ds.Books.Rows.Add(this.dr);
                        BaseAccess.AddNewBooksRow(this.dr);
                        this.dr.AcceptChanges();
                        this.SetAuthors(BaseAccess.Book_IDENTITY);
                        this.Book_Id = BaseAccess.Book_IDENTITY;
                    }
                }
                else if ((this.dr.RowState == DataRowState.Modified) || (this.dr.RowState == DataRowState.Added))
                {
                    this.dr["Thumbs"] = this.ByteThumbs;
                    this.dr["Description"] = this.Description;
                    this.dr["TOC"] = this.Toc;
                    if ((!string.IsNullOrEmpty(this._title) && (this._title != this.Title)) && (Program.OnNodeTitleChanged != null))
                    {
                        Program.OnNodeTitleChanged();
                    }
                    BaseAccess.AccessBooksRow(this.dr);
                    this.dr.AcceptChanges();
                    this.SetAuthors(this.Book_Id);
                }
            }
            catch (Exception exception)
            {
                Program.ds.Books.RejectChanges();
                Program.ds.Authors.RejectChanges();
                Program.ds.Book_Author.RejectChanges();
                MessageBox.Show(StringResources.AEM_errorsavebook.Replace(@"\r\n", "\r\n") + exception.ToString(), StringResources.AEM_exception_title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void SetAuthors(int newBookId)
        {
            List<DataSet1.AuthorsRow> first = new List<DataSet1.AuthorsRow>();
            foreach (DataRow row in this.dr.GetChildRows("Books_Book_Author"))
            {
                DataSet1.AuthorsRow parentRow = row.GetParentRow("Authors_Book_Author") as DataSet1.AuthorsRow;
                first.Add(parentRow);
            }
            List<DataSet1.AuthorsRow> second = new List<DataSet1.AuthorsRow>();
            foreach (string str in this.al)
            {
                DataSet1.AuthorsRow[] source = Program.ds.Authors.Select(string.Format("Name = '{0}'", str.Replace("'", "''").TrimEnd(new char[0]).TrimStart(new char[0]))) as DataSet1.AuthorsRow[];
                if ((source != null) && (source.Length > 0))
                {
                    second.Add(source.First<DataSet1.AuthorsRow>());
                }
                else
                {
                    BaseAccess.Author_IDENTITY = -1;
                    DataSet1.AuthorsRow row3 = Program.ds.Authors.NewAuthorsRow();
                    row3.BeginEdit();
                    row3.Name = str;
                    Program.ds.Authors.Rows.Add(row3);
                    BaseAccess.AccessAuthorsRow(row3);
                    row3.Author_Id = BaseAccess.Author_IDENTITY;
                    row3.AcceptChanges();
                    row3.EndEdit();
                    second.Add(row3);
                }
            }
            foreach (DataSet1.AuthorsRow row4 in first.Except<DataSet1.AuthorsRow>(second))
            {
                DataSet1.Book_AuthorRow[] rowArray2 = Program.ds.Book_Author.Select(string.Format("Book_Id = {0} AND Author_Id = {1}", newBookId, row4.Author_Id)) as DataSet1.Book_AuthorRow[];
                rowArray2.First<DataSet1.Book_AuthorRow>().Delete();
            }
            foreach (DataSet1.AuthorsRow row5 in second.Except<DataSet1.AuthorsRow>(first))
            {
                DataSet1.Book_AuthorRow row6 = Program.ds.Book_Author.NewBook_AuthorRow();
                row6.Book_Id = newBookId;
                row6.Author_Id = row5.Author_Id;
                Program.ds.Book_Author.AddBook_AuthorRow(row6);
            }
            using (DataSet1 set = Program.ds)
            {
                if (set != null)
                {
                    using (DataSet1.Book_AuthorDataTable table = set.Tables["Book_Author"] as DataSet1.Book_AuthorDataTable)
                    {
                        if (table != null)
                        {
                            try
                            {
                                Book_AuthorTableAdapter adapter = new Book_AuthorTableAdapter();
                                adapter.Connection = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\"", Program.strFile));
                                adapter.Update(table);
                                Program.ds.Book_Author.AcceptChanges();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("Error when try Save Book_Authors \n\r" + exception.Message, StringResources.AEM_exception_title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                Program.ds.Book_Author.RejectChanges();
                            }
                        }
                    }
                }
            }
        }

        public void SetDimension(string dim)
        {
            string[] strArray = dim.Split(new char[] { 'x' });
            switch (strArray.Length)
            {
                case 1:
                    this._height = strArray[0];
                    break;

                case 2:
                    this._height = strArray[0];
                    this._width = strArray[1];
                    break;

                case 3:
                    this._height = strArray[0];
                    this._width = strArray[1];
                    this._lenght = strArray[2];
                    break;
            }
            this.dr.Dimension = this._height + "x" + this._width + "x" + this._lenght;
            this.NotifyPropertyChanged("Dimension");
        }

        public override string ToString()
        {
            return this.Title;
        }

        // Properties
        public List<string> Alauthors
        {
            get
            {
                return this.al;
            }
        }

        public string AuthorsLine
        {
            get
            {
                string str = null;
                for (int i = 0; i < this.Alauthors.Count; i++)
                {
                    string[] strArray = this.Alauthors[i].ToString().Split(new char[] { ',' });
                    if (str == null)
                    {
                        if (strArray.Length > 1)
                        {
                            str = strArray[1].TrimEnd(new char[0]).TrimStart(new char[0]) + " " + strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                        }
                        else
                        {
                            str = this.Alauthors[i].ToString().TrimEnd(new char[0]).TrimStart(new char[0]);
                        }
                    }
                    else if (strArray.Length > 1)
                    {
                        str = str + ", " + strArray[1].TrimEnd(new char[0]).TrimStart(new char[0]) + " " + strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                    }
                    else
                    {
                        str = str + ", " + this.Alauthors[i].ToString().TrimEnd(new char[0]).TrimStart(new char[0]);
                    }
                }
                return str;
            }
        }

        public string Back_cover
        {
            get
            {
                if (!this.dr.IsBackNull() && !this.dr.IsDrive_BackNull())
                {
                    return (this.GetDriveLetter(this.dr.Drive_Back) + this.dr.Back);
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] strArray = value.Split(new string[] { @":\" }, StringSplitOptions.None);
                    if (strArray.Length == 2)
                    {
                        strArray[0] = strArray[0] + @":\";
                        strArray[0] = strArray[0].ToUpper();
                        LogicalDrives drive = LogicalDrives.LocalSerial(strArray[0]);
                        DataRow[] rowArray = Program.ds.Drives.Select("Serial = '" + drive.Serial + "'");
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.dr.Back = strArray[1];
                            this.dr.Drive_Back = (int) rowArray[0]["Drive_Id"];
                            if (drive.Letter != rowArray[0]["Letter"].ToString())
                            {
                                rowArray[0]["Letter"] = drive.Letter;
                                BaseAccess.AccessDrivesTable(rowArray[0]);
                            }
                        }
                        else
                        {
                            int num = CreateAndSaveDrive(drive);
                            this.dr.Back = strArray[1];
                            this.dr.Drive_Back = num;
                        }
                        this.NotifyPropertyChanged("Back_cover");
                    }
                    else if (value.StartsWith("http://"))
                    {
                        this.dr.Back = value;
                        this.dr.Drive_Back = -1;
                        this.NotifyPropertyChanged("Back_cover");
                    }
                }
                else
                {
                    this.dr.SetBackNull();
                    this.dr.SetDrive_BackNull();
                    this.NotifyPropertyChanged("Back_cover");
                }
            }
        }

        public string Back_coverHTMLFormat
        {
            get
            {
                if (!this.dr.IsBackNull() && !this.dr.IsDrive_BackNull())
                {
                    return string.Format("file:///{0}|/{1}", this.GetDriveLetter(this.dr.Drive_Back).Remove(1), this.dr.Back);
                }
                return string.Empty;
            }
        }

        public bool BackExists
        {
            get
            {
                return File.Exists(this.Back_cover);
            }
        }

        public BitmapSource BitmapFileType
        {
            get
            {
                BitmapImage image = null;
                try
                {
                    Func<FileImageExtension, bool> predicate = null;
                    string ext = Path.GetExtension(this.Location);
                    if (Program.lstFileIcons == null)
                    {
                        Program.lstFileIcons = new List<FileImageExtension>();
                    }
                    if (!Program.lstFileIcons.Contains(new FileImageExtension(ext, null)))
                    {
                        image = new BitmapImage();
                        using (MemoryStream stream = new MemoryStream())
                        {
                            string location = this.Location;
                            if (!string.IsNullOrEmpty(location) && !File.Exists(location))
                            {
                                a9.a(location).Save(stream, ImageFormat.Png);
                            }
                            else
                            {
                                Icon.ExtractAssociatedIcon(location).ToBitmap().Save(stream, ImageFormat.Png);
                            }
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = stream;
                            image.EndInit();
                        }
                        Program.lstFileIcons.Add(new FileImageExtension(ext, image));
                        return image;
                    }
                    if (predicate == null)
                    {
                        predicate = delegate (FileImageExtension fi) {
                            return fi.Extension == ext;
                        };
                    }
                    image = Program.lstFileIcons.Where<FileImageExtension>(predicate).Select<FileImageExtension, BitmapImage>(delegate (FileImageExtension fi) {
                        return fi.FileImage;
                    }).First<BitmapImage>();
                }
                catch
                {
                }
                return image;
            }
        }

        public BitmapSource BitmapRating
        {
            get
            {
                Bitmap bitmap = null;
                switch (this.dr.Rating)
                {
                    case 0:
                        bitmap = Resources.stars0;
                        break;

                    case 1:
                        bitmap = Resources.stars1;
                        break;

                    case 2:
                        bitmap = Resources.stars2;
                        break;

                    case 3:
                        bitmap = Resources.stars3;
                        break;

                    case 4:
                        bitmap = Resources.stars4;
                        break;

                    case 5:
                        bitmap = Resources.stars5;
                        break;
                }
                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public BitmapImage BitmapThumb
        {
            get
            {
                if ((this.ByteThumbs != null) && (this.ByteThumbs != null))
                {
                    BitmapImage image = new BitmapImage();
                    MemoryStream output = new MemoryStream(this.ByteThumbs);
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = output;
                    image.EndInit();
                    image.Freeze();
                    try
                    {
                        using (BinaryWriter writer = new BinaryWriter(output))
                        {
                            writer.Write(1);
                        }
                    }
                    catch (Exception)
                    {
                        output.Flush();
                        output.Close();
                    }
                    return image;
                }
                BitmapImage image2 = new BitmapImage();
                using (MemoryStream stream2 = new MemoryStream())
                {
                    Resources.noimage1.Save(stream2, ImageFormat.Png);
                    image2.BeginInit();
                    image2.CacheOption = BitmapCacheOption.OnLoad;
                    image2.StreamSource = stream2;
                    image2.EndInit();
                }
                return image2;
            }
        }

        public ImageSource BitmapThumbGenre
        {
            get
            {
                if ((this.drgenre != null) && !this.drgenre.IsImageNull())
                {
                    MemoryStream stream = new MemoryStream(this.drgenre.Image);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
                return null;
            }
        }

        public ImageSource BitmapThumbPub
        {
            get
            {
                if ((this.drpub == null) || this.drpub.IsImageNull())
                {
                    return null;
                }
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(this.drpub.Image, false))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                return image;
            }
        }

        public ImageSource BitmapThumbSeries
        {
            get
            {
                if ((this.drseries == null) || this.drseries.IsImageNull())
                {
                    return null;
                }
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(this.drseries.Image, false))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                return image;
            }
        }

        public byte[] ByteThumbs
        {
            get
            {
                try
                {
                    if (Convert.IsDBNull(this.dr["Thumbs"]) && !this.IsThumbsNull)
                    {
                        using (OleDbCommand command = new OleDbCommand(string.Format("SELECT Book_Id, Thumbs FROM Books WHERE Book_Id = {0}", this.Book_Id.ToString()), Program.adapterBooks.Connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                            {
                                while (reader.Read())
                                {
                                    this._bytethumbs = reader.GetValue(1) as byte[];
                                }
                            }
                            goto Label_00A0;
                        }
                    }
                    this._bytethumbs = this.dr["Thumbs"] as byte[];
                }
                catch (Exception)
                {
                }
            Label_00A0:
                return this._bytethumbs;
            }
            set
            {
                this.dr["Thumbs"] = value;
            }
        }

        public DateTime Date
        {
            get
            {
                if (!this.dr.IsDateNull())
                {
                    return this.dr.Date;
                }
                return DateTime.Now;
            }
            set
            {
                this.dr.Date = value;
                this.NotifyPropertyChanged("Date");
            }
        }

        public DateTime Date_Add
        {
            get
            {
                if (!this.dr.IsDate_AddNull())
                {
                    return this.dr.Date_Add;
                }
                return DateTime.Now;
            }
            set
            {
                this.dr.Date_Add = value;
                this.NotifyPropertyChanged("Date");
            }
        }

        public string Date_AddtoString
        {
            get
            {
                if (!this.dr.IsDate_AddNull())
                {
                    return string.Format("{0}, {1}", this.dr.Date_Add.ToString("T", Settings.Default.language), this.dr.Date_Add.ToString("D", Settings.Default.language));
                }
                return string.Format("{0}, {1}", DateTime.Now.ToString("T", Settings.Default.language), DateTime.Now.ToString("D", Settings.Default.language));
            }
        }

        public DateTime Date_Last_Access
        {
            get
            {
                if (!this.dr.IsLast_AccessNull())
                {
                    return this.dr.Last_Access;
                }
                return DateTime.Now;
            }
            set
            {
                this.dr.Last_Access = value;
                this.NotifyPropertyChanged("Date");
            }
        }

        public string DateToString
        {
            get
            {
                if (!this.dr.IsDateNull())
                {
                    return this.dr.Date.ToString("D", Settings.Default.language);
                }
                return DateTime.Now.ToString("D", Settings.Default.language);
            }
        }

        public string Description
        {
            get
            {
                string str;
                try
                {
                    if ((Program.adapterBooks.Connection.State == ConnectionState.Open) && Convert.IsDBNull(this.dr["Description"]))
                    {
                        using (OleDbCommand command = new OleDbCommand(string.Format("SELECT Book_Id, Description FROM Books WHERE Book_Id = {0}", this.Book_Id.ToString()), Program.adapterBooks.Connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                            {
                                while (reader.Read())
                                {
                                    this._description = reader.GetString(1);
                                }
                            }
                            goto Label_00A0;
                        }
                    }
                    this._description = this.dr["Description"] as string;
                Label_00A0:
                    str = this._description = this.RTFtoText(this._description);
                }
                catch
                {
                    str = string.Empty;
                }
                return str;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr["Description"] = value;
                }
                else
                {
                    this.dr["Description"] = string.Empty;
                }
                this.NotifyPropertyChanged("Description");
            }
        }

        private string DescriptionLine
        {
            get
            {
                return this.GetDescriptionTextFormat();
            }
        }

        public string[] Dimension
        {
            get
            {
                if (!this.dr.IsDimensionNull())
                {
                    return this.dr.Dimension.Split(new char[] { 'x' });
                }
                return null;
            }
            set
            {
                this.dr.Dimension = this._height + "x" + this._width + "x" + this._lenght;
                this.NotifyPropertyChanged("Dimension");
            }
        }

        public DataSet1.GenreRow drgenre
        {
            get
            {
                if (!this.dr.IsGen_IdNull() && (this.dr.Gen_Id > 0))
                {
                    this._drgenre = Program.ds.Genre.FindByGen_Id(this.dr.Gen_Id);
                }
                return this._drgenre;
            }
            set
            {
                this._drgenre = value;
                if (value != null)
                {
                    this.dr.Gen_Id = value.Gen_Id;
                }
            }
        }

        public DataSet1.PublishersRow drpub
        {
            get
            {
                if (!this.dr.IsPub_IdNull() && (this.dr.Pub_Id > 0))
                {
                    this.drpublisher = Program.ds.Publishers.FindByPub_Id(this.dr.Pub_Id);
                }
                return this.drpublisher;
            }
            set
            {
                this.drpublisher = value;
                if (value != null)
                {
                    this.dr.Pub_Id = value.Pub_Id;
                }
            }
        }

        public DataSet1.SeriesRow drseries
        {
            get
            {
                if (!this.dr.IsSer_IdNull() && (this.dr.Ser_Id > 0))
                {
                    this._drseries = Program.ds.Series.FindBySer_Id(this.dr.Ser_Id);
                }
                return this._drseries;
            }
            set
            {
                this._drseries = value;
                if (value != null)
                {
                    this.dr.Ser_Id = value.Ser_Id;
                }
            }
        }

        public string Edition
        {
            get
            {
                return this.dr.Edition.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        this.dr.Edition = int.Parse(value);
                    }
                    catch
                    {
                        this.dr.SetEditionNull();
                    }
                }
                else
                {
                    this.dr.SetEditionNull();
                }
                this.NotifyPropertyChanged("Edition");
            }
        }

        public string Front_cover
        {
            get
            {
                if (!this.dr.IsFrontNull() && !this.dr.IsDrive_FrontNull())
                {
                    return (this.GetDriveLetter(this.dr.Drive_Front) + this.dr.Front);
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] strArray = value.Split(new string[] { @":\" }, StringSplitOptions.None);
                    if (strArray.Length == 2)
                    {
                        strArray[0] = strArray[0] + @":\";
                        strArray[0] = strArray[0].ToUpper();
                        LogicalDrives drive = LogicalDrives.LocalSerial(strArray[0]);
                        DataRow[] rowArray = Program.ds.Drives.Select("Serial = '" + drive.Serial + "'");
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.dr.Front = strArray[1];
                            this.dr.Drive_Front = (int) rowArray[0]["Drive_Id"];
                            if (drive.Letter != rowArray[0]["Letter"].ToString())
                            {
                                rowArray[0]["Letter"] = drive.Letter;
                                BaseAccess.AccessDrivesTable(rowArray[0]);
                            }
                        }
                        else
                        {
                            int num = CreateAndSaveDrive(drive);
                            this.dr.Front = strArray[1];
                            this.dr.Drive_Front = num;
                        }
                        this.NotifyPropertyChanged("Front_cover");
                    }
                    else if (value.StartsWith("http://"))
                    {
                        this.dr.Front = value;
                        this.dr.Drive_Front = -1;
                        this.NotifyPropertyChanged("Front_cover");
                    }
                }
                else
                {
                    this.dr.SetFrontNull();
                    this.dr.SetDrive_FrontNull();
                    this.NotifyPropertyChanged("Front_cover");
                }
            }
        }

        public string Front_coverHTMLFormat
        {
            get
            {
                if (!this.dr.IsFrontNull() && !this.dr.IsDrive_FrontNull())
                {
                    return string.Format("file:///{0}|/{1}", this.GetDriveLetter(this.dr.Drive_Front).Remove(1), this.dr.Front);
                }
                return string.Empty;
            }
        }

        public bool FrontExists
        {
            get
            {
                return File.Exists(this.Front_cover);
            }
        }

        public string FullGenrePath
        {
            get
            {
                if (this.drgenre == null)
                {
                    return string.Empty;
                }
                this.genrefullpath = string.Empty;
                this.GetGenreParent(this.drgenre.Parent_Id);
                if (string.IsNullOrEmpty(this.genrefullpath))
                {
                    return this.drgenre.Name;
                }
                if (this.drgenre.Name != "Genres")
                {
                    return (this.genrefullpath + ">>" + this.drgenre.Name);
                }
                return this.genrefullpath;
            }
        }

        public string Genre
        {
            get
            {
                if ((this.drgenre != null) && !this.drgenre.IsNameNull())
                {
                    return this.drgenre.Name;
                }
                return "Add To...";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DataRow[] rowArray = Program.ds.Genre.Select(string.Format("Name = '{0}'", value.TrimEnd(new char[0]).TrimStart(new char[0]).Replace("'", "''")));
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        DataSet1.GenreRow row = rowArray[0] as DataSet1.GenreRow;
                        this.dr.Gen_Id = row.Gen_Id;
                        this.drgenre = row;
                    }
                    else
                    {
                        this.dr.SetGen_IdNull();
                        this.drgenre = null;
                    }
                }
                else
                {
                    this.dr.SetGen_IdNull();
                    this.drgenre = null;
                }
                this.NotifyPropertyChanged("Genre");
            }
        }

        public string GenreDB
        {
            get
            {
                if ((this.drgenre != null) && !this.drgenre.IsNameNull())
                {
                    return this.drgenre.Name;
                }
                return string.Empty;
            }
        }

        public string Height
        {
            get
            {
                if (this.Dimension.Length >= 0)
                {
                    this._height = this.Dimension[0];
                }
                return this._height;
            }
            set
            {
                this._height = value;
                this.dr.Dimension = this._height + "x" + this._width + "x" + this._lenght;
                this.NotifyPropertyChanged("Height");
            }
        }

        public Image IconDrive
        {
            get
            {
                if (this.LocationType != null)
                {
                    ExplorerTree.IconSet set = (ExplorerTree.IconSet) Enum.Parse(typeof(ExplorerTree.IconSet), this.LocationType, true);
                    return ExplorerTree.GetDesktopLargeIcon(set).ToBitmap();
                }
                return null;
            }
        }

        public bool IsAdobe
        {
            get
            {
                return this._isadobe;
            }
            set
            {
                this._isadobe = value;
            }
        }

        public string Isbn_10
        {
            get
            {
                return this.dr.ISBN_10;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.ISBN_10 = value;
                }
                else
                {
                    this.dr.SetISBN_10Null();
                }
                this.NotifyPropertyChanged("Isbn_10");
            }
        }

        public string Isbn_13
        {
            get
            {
                return this.dr.ISBN_13;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.ISBN_13 = value;
                }
                else
                {
                    this.dr.SetISBN_13Null();
                }
                this.NotifyPropertyChanged("Isbn_13");
            }
        }

        public int ISBNPrefix
        {
            get
            {
                if (!this.dr.IsPrefix_IdNull())
                {
                    return this.dr.Prefix_Id;
                }
                return -1;
            }
            set
            {
                if (value != -1)
                {
                    this.dr.Prefix_Id = value;
                    this.NotifyPropertyChanged("Prefix_Id");
                }
            }
        }

        public bool IsGS
        {
            get
            {
                return this._isgs;
            }
            set
            {
                this._isgs = value;
            }
        }

        public bool? IsMarked
        {
            get
            {
                return new bool?(Program.MarkedBooks.Contains(this.dr.Book_Id));
            }
            set
            {
                if (value.Value)
                {
                    if (!Program.MarkedBooks.Contains(this.dr.Book_Id))
                    {
                        Program.MarkedBooks.Add(this.dr.Book_Id);
                    }
                }
                else
                {
                    Program.MarkedBooks.Remove(this.dr.Book_Id);
                }
            }
        }

        public string Language
        {
            get
            {
                return this.dr.Lang;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.Lang = value;
                }
                else
                {
                    this.dr.SetLangNull();
                }
                this.NotifyPropertyChanged("Language");
            }
        }

        public string Lenght
        {
            get
            {
                if (this.Dimension.Length >= 2)
                {
                    this._lenght = this.Dimension[2];
                }
                return this._lenght;
            }
            set
            {
                this._lenght = value;
                this.dr.Dimension = this._height + "x" + this._width + "x" + this._lenght;
                this.NotifyPropertyChanged("Lenght");
            }
        }

        public string Location
        {
            get
            {
                if (!this.dr.IsLocationNull() && !this.dr.IsDrive_IdNull())
                {
                    return (this.GetDriveLetter(this.dr.Drive_Id) + this.dr.Location);
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] strArray = value.Split(new string[] { @":\" }, StringSplitOptions.None);
                    if (strArray.Length == 2)
                    {
                        strArray[0] = strArray[0] + @":\";
                        strArray[0] = strArray[0].ToUpper();
                        LogicalDrives drive = LogicalDrives.LocalSerial(strArray[0]);
                        DataRow[] rowArray = Program.ds.Drives.Select("Serial = '" + drive.Serial + "'");
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.dr.Location = strArray[1];
                            this.dr.Drive_Id = (int) rowArray[0]["Drive_Id"];
                            if (drive.Letter != rowArray[0]["Letter"].ToString())
                            {
                                rowArray[0]["Letter"] = drive.Letter;
                                BaseAccess.AccessDrivesTable(rowArray[0]);
                            }
                        }
                        else
                        {
                            int num = CreateAndSaveDrive(drive);
                            this.dr.Location = strArray[1];
                            this.dr.Drive_Id = num;
                        }
                        this.NotifyPropertyChanged("Location");
                        if (File.Exists(value))
                        {
                            FileInfo info = new FileInfo(value);
                            this.Size_nature = info.Length.ToString();
                            info = null;
                        }
                    }
                }
                else
                {
                    this.dr.SetLocationNull();
                    this.dr.SetDrive_IdNull();
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        public string LocationDrive
        {
            get
            {
                if (!this.dr.IsLocationNull() && !this.dr.IsDrive_IdNull())
                {
                    DataSet1.DrivesRow row = Program.ds.Drives.FindByDrive_Id(this.dr.Drive_Id);
                    if ((row != null) && (row.Type == "CDRom"))
                    {
                        this._LocationDrive = string.Format("[{0}], Serial: {1}", row.Name, row.Serial);
                    }
                }
                return this._LocationDrive;
            }
        }

        public string LocationDriveName
        {
            get
            {
                if (!this.dr.IsLocationNull() && !this.dr.IsDrive_IdNull())
                {
                    DataSet1.DrivesRow row = Program.ds.Drives.FindByDrive_Id(this.dr.Drive_Id);
                    if (row != null)
                    {
                        this._LocationDriveName = string.Format("{1} ({0}), Serial:{2}", row.Letter.Trim(new char[] { Path.DirectorySeparatorChar }), row.Name, row.Serial);
                    }
                }
                return this._LocationDriveName;
            }
        }

        public string LocationType
        {
            get
            {
                if (!this.dr.IsLocationNull() && !this.dr.IsDrive_IdNull())
                {
                    DataSet1.DrivesRow row = Program.ds.Drives.FindByDrive_Id(this.dr.Drive_Id);
                    if (row != null)
                    {
                        this._LocationType = row.Type;
                    }
                }
                return this._LocationType;
            }
        }

        public bool Marked
        {
            get
            {
                return Program.MarkedBooks.Contains(this.dr.Book_Id);
            }
            set
            {
                if (!Program.MarkedBooks.Contains(this.dr.Book_Id))
                {
                    Program.MarkedBooks.Add(this.dr.Book_Id);
                }
            }
        }

        public string Pages
        {
            get
            {
                return this.dr.Pages.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        this.dr.Pages = int.Parse(value);
                    }
                    catch
                    {
                        this.dr.SetPagesNull();
                    }
                }
                else
                {
                    this.dr.SetPagesNull();
                }
                this.NotifyPropertyChanged("Pages");
            }
        }

        public string Paperback
        {
            get
            {
                return this.dr.Paperback;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.Paperback = value;
                }
                else
                {
                    this.dr.SetPaperbackNull();
                }
                this.NotifyPropertyChanged("Paperback");
            }
        }

        public string Price
        {
            get
            {
                return this.dr.Price;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.Price = value;
                }
                else
                {
                    this.dr.SetPriceNull();
                }
                this.NotifyPropertyChanged("Price");
            }
        }

        public string PublisherDB
        {
            get
            {
                if ((this.drpub != null) && !this.drpub.IsNameNull())
                {
                    return this.drpub.Name;
                }
                return string.Empty;
            }
        }

        public string Publishers
        {
            get
            {
                if ((this.drpub != null) && !this.drpub.IsNameNull())
                {
                    return this.drpub.Name;
                }
                return "Add To...";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.dr.SetPub_IdNull();
                    this.drpub = null;
                    this.NotifyPropertyChanged("Publishers");
                }
                else
                {
                    DataRow[] rowArray = Program.ds.Publishers.Select(string.Format("Name = '{0}'", value.TrimEnd(new char[0]).TrimStart(new char[0]).Replace("'", "''")));
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        DataSet1.PublishersRow row = rowArray[0] as DataSet1.PublishersRow;
                        this.dr.Pub_Id = row.Pub_Id;
                        this.drpub = row;
                        this.NotifyPropertyChanged("Publishers");
                    }
                    else
                    {
                        this.dr.SetPub_IdNull();
                        this.drpub = null;
                    }
                }
            }
        }

        public int Rating
        {
            get
            {
                return this.dr.Rating;
            }
            set
            {
                this.dr.Rating = value;
                this.NotifyPropertyChanged("Rating");
            }
        }

        public Image RatingImages
        {
            get
            {
                switch (this.dr.Rating)
                {
                    case 0:
                        return Resources.stars0;

                    case 1:
                        return Resources.stars1;

                    case 2:
                        return Resources.stars2;

                    case 3:
                        return Resources.stars3;

                    case 4:
                        return Resources.stars4;

                    case 5:
                        return Resources.stars5;
                }
                return null;
            }
        }

        public byte[] RatingReport
        {
            get
            {
                Image image = Resources.stars0;
                if ((this.dr != null) && !this.dr.IsNull("Rating"))
                {
                    switch (Convert.ToInt32(this.dr["Rating"].ToString()))
                    {
                        case 0:
                            image = Resources.stars0;
                            break;

                        case 1:
                            image = Resources.stars1;
                            break;

                        case 2:
                            image = Resources.stars2;
                            break;

                        case 3:
                            image = Resources.stars3;
                            break;

                        case 4:
                            image = Resources.stars4;
                            break;

                        case 5:
                            image = Resources.stars5;
                            break;
                    }
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    if (image == null)
                    {
                        image = Resources.stars0;
                    }
                    image.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        public string Series
        {
            get
            {
                if ((this.drseries != null) && !this.drseries.IsNameNull())
                {
                    return this.drseries.Name;
                }
                return "Add To...";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.dr.SetSer_IdNull();
                    this.drseries = null;
                    this.NotifyPropertyChanged("Series");
                }
                else
                {
                    DataRow[] rowArray = Program.ds.Series.Select(string.Format("Name = '{0}'", value.TrimEnd(new char[0]).TrimStart(new char[0]).Replace("'", "''")));
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        DataSet1.SeriesRow row = rowArray[0] as DataSet1.SeriesRow;
                        this.dr.Ser_Id = row.Ser_Id;
                        this.drseries = row;
                        this.NotifyPropertyChanged("Series");
                    }
                    else
                    {
                        this.dr.SetSer_IdNull();
                        this.drseries = null;
                    }
                }
            }
        }

        public string SeriesDB
        {
            get
            {
                if ((this.drseries != null) && !this.drseries.IsNameNull())
                {
                    return this.drseries.Name;
                }
                return string.Empty;
            }
        }

        public string Size_nature
        {
            get
            {
                if (!this.dr.IsSize_NatureNull())
                {
                    return this.dr.Size_Nature.ToString();
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        this.dr.Size_Nature = int.Parse(value);
                    }
                    catch
                    {
                        this.dr.SetSize_NatureNull();
                    }
                }
                else
                {
                    this.dr.SetSize_NatureNull();
                }
                this.NotifyPropertyChanged("Size_nature");
            }
        }

        public string Subtitle
        {
            get
            {
                return this.dr.Subtitle;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length <= 0xff)
                    {
                        this.dr.Subtitle = value;
                    }
                    else
                    {
                        this.dr.Subtitle = value.Remove(0xff);
                    }
                    this.NotifyPropertyChanged("Subtitle");
                }
                else
                {
                    this.dr.SetSubtitleNull();
                }
                this.NotifyPropertyChanged("Subtitle");
            }
        }

        public Image Thumbs
        {
            get
            {
                try
                {
                    byte[] byteThumbs = this.ByteThumbs;
                    if (byteThumbs != null)
                    {
                        using (MemoryStream stream = new MemoryStream(byteThumbs, false))
                        {
                            try
                            {
                                this._thumbs = Image.FromStream(stream);
                            }
                            finally
                            {
                                stream.Flush();
                                stream.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                return this._thumbs;
            }
            set
            {
                if (value == null)
                {
                    this.dr["Thumbs"] = DBNull.Value;
                    this._thumbs = null;
                    this.IsThumbsNull = true;
                    this.NotifyPropertyChanged("Thumbs");
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        try
                        {
                            value.Save(stream, ImageFormat.Png);
                            this.dr["Thumbs"] = stream.ToArray();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, StringResources.AEM_exception_title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        finally
                        {
                            stream.Flush();
                            stream.Close();
                            this.NotifyPropertyChanged("Thumbs");
                        }
                    }
                    this.ByteThumbs = this.dr["Thumbs"] as byte[];
                    this.NotifyPropertyChanged(" ByteThumbs");
                }
            }
        }

        public byte[] ThumbsDBBack
        {
            get
            {
                Image noimage = null;
                if (File.Exists(this.Back_cover))
                {
                    noimage = Image.FromFile(this.Back_cover);
                }
                else
                {
                    noimage = Resources.noimage;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    noimage.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
        }

        public byte[] ThumbsDBFront
        {
            get
            {
                if (File.Exists(this.Front_cover))
                {
                    Image image = Image.FromFile(this.Front_cover);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.Save(stream, ImageFormat.Jpeg);
                        return stream.ToArray();
                    }
                }
                if (!this.dr.IsNull("Thumbs"))
                {
                    return (byte[]) this.dr["Thumbs"];
                }
                Image noimage = Resources.noimage;
                using (MemoryStream stream2 = new MemoryStream())
                {
                    noimage.Save(stream2, ImageFormat.Jpeg);
                    return stream2.ToArray();
                }
            }
        }

        public Image ThumbsDBGenre
        {
            get
            {
                Image image = null;
                if ((this.drgenre != null) && !this.drgenre.IsImageNull())
                {
                    using (MemoryStream stream = new MemoryStream(this.drgenre.Image))
                    {
                        try
                        {
                            image = Image.FromStream(stream);
                        }
                        finally
                        {
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
                return image;
            }
        }

        public Image ThumbsDBPublisher
        {
            get
            {
                Image image = null;
                if ((this.drpub != null) && !this.drpub.IsImageNull())
                {
                    using (MemoryStream stream = new MemoryStream(this.drpub.Image, false))
                    {
                        try
                        {
                            image = Image.FromStream(stream);
                        }
                        finally
                        {
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
                return image;
            }
        }

        public Image ThumbsDBSeries
        {
            get
            {
                Image image = null;
                if ((this.drseries != null) && !this.drseries.IsImageNull())
                {
                    using (MemoryStream stream = new MemoryStream(this.drseries.Image))
                    {
                        try
                        {
                            image = Image.FromStream(stream);
                        }
                        finally
                        {
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
                return image;
            }
        }

        public Image ThumbsShadow
        {
            get
            {
                return Customize.DropSHadowImage(this.Thumbs);
            }
        }

        public string Title
        {
            get
            {
                if (this.dr.IsTitleNull())
                {
                    return string.Empty;
                }
                return this.dr.Title;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length <= 0xff)
                    {
                        this.dr.Title = value;
                    }
                    else
                    {
                        this.dr.Title = value.Remove(0xff);
                    }
                }
                else
                {
                    this.dr.SetTitleNull();
                }
                this.NotifyPropertyChanged("Title");
            }
        }

        public string Toc
        {
            get
            {
                string str;
                try
                {
                    if ((Program.adapterBooks.Connection.State == ConnectionState.Open) && Convert.IsDBNull(this.dr["TOC"]))
                    {
                        using (OleDbCommand command = new OleDbCommand(string.Format("SELECT Book_Id, TOC FROM Books WHERE Book_Id = {0}", this.Book_Id.ToString()), Program.adapterBooks.Connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                            {
                                while (reader.Read())
                                {
                                    this._toc = reader.GetString(1);
                                }
                            }
                            goto Label_00A0;
                        }
                    }
                    this._toc = this.dr["TOC"] as string;
                Label_00A0:
                    str = this._toc = this.RTFtoText(this._toc);
                }
                catch
                {
                    str = string.Empty;
                }
                return str;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr["TOC"] = value;
                }
                else
                {
                    this.dr["TOC"] = string.Empty;
                }
                this.NotifyPropertyChanged("Toc");
            }
        }

        public int View_count
        {
            get
            {
                return this.dr.View_Count;
            }
            set
            {
                this.dr.View_Count = value;
                this.NotifyPropertyChanged("View_count");
            }
        }

        public string Web_links
        {
            get
            {
                return this.dr.Web_Links;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.Web_Links = value;
                }
                else
                {
                    this.dr.SetWeb_LinksNull();
                }
                this.NotifyPropertyChanged("Web_links");
            }
        }

        public string Weight
        {
            get
            {
                return this.dr.Weight;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.dr.Weight = value;
                }
                else
                {
                    this.dr.SetWeightNull();
                }
                this.NotifyPropertyChanged("Weight");
            }
        }

        public string Width
        {
            get
            {
                if (this.Dimension.Length >= 1)
                {
                    this._width = this.Dimension[1];
                }
                return this._width;
            }
            set
            {
                this._width = value;
                this.dr.Dimension = this._height + "x" + this._width + "x" + this._lenght;
                this.NotifyPropertyChanged("Width");
            }
        }
    */
    }
}
