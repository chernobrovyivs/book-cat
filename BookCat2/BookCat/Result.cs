using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace BookCat
{
    public class Result : INotifyPropertyChanged
    {
        // Fields
        private string _publisherimage;
        private DateTime dtPublicationsDate = DateTime.Now;
        private List<string> genres = new List<string>();
        private string nEdition = "";
        private string nNumberOfPages = "0";
        private List<string> strAuthors = new List<string>();
        private string strBinding = "";
        private string strBookUrl = "";
        private string strDescription = "";
        private string strDimensions = "";
        private string strISBN = "";
        private string strISBN13;
        private string strLargeBackImageUrl;
        private string strLargeImageUrl = "";
        private string strPrice = "";
        private string strPublisher = "";
        private string strSerie = "";
        private string strSourceSite = "";
        private string strTitle = "";
        private string strToc;
        private string strWeight = "";
		[NonSerialized]
		private Image thumb;

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Methods
        public void AddAuthor(string strAuthor)
        {
            if (!string.IsNullOrEmpty(strAuthor))
            {
                if (strAuthor.IndexOf(".") != -1)
                {
                    string[] strArray = strAuthor.Split(new char[] { '.' });
                    strAuthor = strArray[strArray.Length - 1] + ", " + strArray[0];
                }
                else if (strAuthor.IndexOf(" ") != -1)
                {
                    string[] strArray2 = strAuthor.Split(new char[] { ' ' });
                    strAuthor = strArray2[strArray2.Length - 1] + ", " + strArray2[0];
                }
            }
            if (!string.IsNullOrEmpty(strAuthor) && !this.strAuthors.Contains(strAuthor))
            {
                this.strAuthors.Add(strAuthor);
            }
        }

        public void AddAuthors(List<string> authors)
        {
            this.strAuthors.AddRange(authors);
        }

        public void ClearAuthors()
        {
            this.strAuthors.Clear();
        }

        public Image DownloadImage(string strUrl, int nWidth, int nHeight)
        {
            Image image = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(strUrl))
                    {
                        image = new Bitmap(stream);
                        image = image.GetThumbnailImage(nWidth, nHeight, null, IntPtr.Zero);
                        stream.Close();
                    }
                    return image;
                }
            }
            catch
            {
            }
            return image;
        }

        public byte[] DownloadImageByte(string strUrl)
        {
            byte[] buffer = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(strUrl))
                    {
                        buffer = new byte[0x8000];
                        using (MemoryStream stream2 = new MemoryStream())
                        {
                            while (true)
                            {
                                int count = stream.Read(buffer, 0, buffer.Length);
                                if (count <= 0)
                                {
                                    return stream2.ToArray();
                                }
                                stream2.Write(buffer, 0, count);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return buffer;
        }

        public Image GetImage()
        {
            return this.thumb;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public override string ToString()
        {
            if (this.strAuthors.Count > 0)
            {
                return (this.strAuthors[0] + " - " + this.Title);
            }
            return this.strTitle;
        }

        // Properties
        public string Binding
        {
            get
            {
                return this.strBinding;
            }
            set
            {
                this.strBinding = value;
                this.NotifyPropertyChanged("Binding");
            }
        }

        public string Description
        {
            get
            {
                return this.strDescription;
            }
            set
            {
                this.strDescription = Regex.Replace(value, "<.*?>", "");
                this.strDescription = Regex.Replace(this.strDescription, @"\s+", " ");
                this.strDescription = Regex.Replace(this.strDescription, "&.*?;", "");
                this.NotifyPropertyChanged("About");
            }
        }

        public string Dimension
        {
            get
            {
                return this.strDimensions;
            }
            set
            {
                this.strDimensions = value;
                this.NotifyPropertyChanged("Dimension");
            }
        }

        public string Edition
        {
            get
            {
                return this.nEdition;
            }
            set
            {
                Match match = new Regex(@"(\d+)").Match(value);
                if (match != null)
                {
                    this.nEdition = match.Groups[0].Value;
                }
                else
                {
                    this.nEdition = null;
                }
                this.NotifyPropertyChanged("Edition");
            }
        }

        public List<string> Genres
        {
            get
            {
                return this.genres;
            }
            set
            {
                this.genres = value;
            }
        }

        public List<string> GetAuthors
        {
            get
            {
                return this.strAuthors;
            }
        }

        public string ISBN
        {
            get
            {
                return this.strISBN;
            }
            set
            {
                this.strISBN = value.Replace("-", string.Empty);
                this.NotifyPropertyChanged("ISBN");
            }
        }

        public string ISBN13
        {
            get
            {
                return this.strISBN13;
            }
            set
            {
                this.strISBN13 = value.Replace("-", string.Empty);
                this.NotifyPropertyChanged("ISBN13");
            }
        }

        public string LargeBackImageUrl
        {
            get
            {
                return this.strLargeBackImageUrl;
            }
            set
            {
                this.strLargeBackImageUrl = value;
            }
        }

        public string LargeImageUrl
        {
            get
            {
                return this.strLargeImageUrl;
            }
            set
            {
                this.strLargeImageUrl = value;
            }
        }

        public string NumberOfPages
        {
            get
            {
                return this.nNumberOfPages;
            }
            set
            {
                this.nNumberOfPages = value;
                this.NotifyPropertyChanged("NumberOfPages");
            }
        }

        public string Price
        {
            get
            {
                return this.strPrice;
            }
            set
            {
                this.strPrice = value;
                this.NotifyPropertyChanged("Price");
            }
        }

        public DateTime PublicationsDate
        {
            get
            {
                return this.dtPublicationsDate;
            }
            set
            {
                this.dtPublicationsDate = value;
                this.NotifyPropertyChanged("PublicationsDate");
            }
        }

        public string Publisher
        {
            get
            {
                return this.strPublisher;
            }
            set
            {
                this.strPublisher = value;
                this.NotifyPropertyChanged("Publisher");
            }
        }

        public string PublisherImage
        {
            get
            {
                return this._publisherimage;
            }
            set
            {
                this._publisherimage = value;
            }
        }

        public string Serie
        {
            get
            {
                return this.strSerie;
            }
            set
            {
                this.strSerie = value;
                this.NotifyPropertyChanged("Serie");
            }
        }

        public string SourceSite
        {
            get
            {
                return this.strSourceSite;
            }
            set
            {
                this.strSourceSite = value;
                this.NotifyPropertyChanged("SourceSite");
            }
        }

		public Image Thumbs
        {
            get
            {
                return this.thumb;
            }
            set
            {
                this.thumb = value;
            }
        }

        public string Title
        {
            get
            {
                return this.strTitle;
            }
            set
            {
                this.strTitle = value;
                this.NotifyPropertyChanged("Title");
            }
        }

        public string TOC
        {
            get
            {
                return this.strToc;
            }
            set
            {
                this.strToc = value;
                this.NotifyPropertyChanged("TOC");
            }
        }

        public string WebPage
        {
            get
            {
                return this.strBookUrl;
            }
            set
            {
                this.strBookUrl = value;
                this.NotifyPropertyChanged("WebPage");
            }
        }

        public string Weight
        {
            get
            {
                return this.strWeight;
            }
            set
            {
                this.strWeight = value;
                this.NotifyPropertyChanged("Weight");
            }
        }
    }
}
