using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using BookCat.Properties;

namespace BookCat
{
    public class BookHelper
    {
        public static void GetSubtitlefromTitle(Book book)
        {
            string title = book.Title;
            string[] strArray = title.Split(new char[] { ':' });
            if (strArray.Length >= 2)
            {
                book.Title = strArray[0].TrimEnd(new char[0]).TrimStart(new char[0]);
                book.Subtitle = title.Remove(0, book.Title.Length + 1).TrimEnd(new char[0]).TrimStart(new char[0]);
            }
            else
            {
                book.Title = book.Title.TrimEnd(new char[0]).TrimStart(new char[0]);
                book.Subtitle = string.Empty;
            }
        }
    }


    public class ProcessXml
    {
        // Methods
        /*
        public static void ConvertResultToGPB(Result re, Book gpb, bool IsDownloadFrontCover, string strCoversFolder)
        {
            if (re != null)
            {
                string str = re.Title.Trim();
                if (string.IsNullOrEmpty(str))
                {
                    str = "<Not Found>";
                }
                if (str.Length > 0xfe)
                {
                    str = str.Substring(0, 0xfe);
                }
                if (Settings.Default.fsplit)
                {
                    gpb.Title = str;
                    BookHelper.GetSubtitlefromTitle(gpb);
                }
                else
                {
                    gpb.Title = str;
                }

                gpb.Alauthors.Clear();
                foreach (string str2 in re.GetAuthors)
                {
                    string str3 = str2.Trim();
                    if (!string.IsNullOrEmpty(str3) && (str3.Length > 0xfe))
                    {
                        gpb.Alauthors.Add(str3.Substring(0, 0xfe));
                    }
                    else
                    {
                        gpb.Alauthors.Add(str3);
                    }
                }
                gpb.Date = re.PublicationsDate;
                string str4 = re.NumberOfPages.Trim();
                if (!string.IsNullOrEmpty(str4) && (str4.Length > 6))
                {
                    str4 = str4.Substring(0, 6);
                }
                if (!string.IsNullOrEmpty(str4))
                {
                    gpb.Pages = str4;
                }
                string str5 = re.Publisher.Trim();
                if (!string.IsNullOrEmpty(str5) && (str5.Length > 0xfe))
                {
                    str5 = str5.Substring(0, 0xfe);
                }
                DataRow[] rowArray = Program.ds.Publishers.Select(string.Format("Name = '{0}'", str5.Replace("'", "''")));
                if ((rowArray != null) && (rowArray.Length == 1))
                {
                    gpb.Publishers = str5;
                }
                else
                {
                    DataRow row = Program.ds.Publishers.NewRow();
                    row["Name"] = str5;
                    if (!string.IsNullOrEmpty(re.PublisherImage))
                    {
                        row["Image"] = re.DownloadImageByte(re.PublisherImage);
                    }
                    Program.ds.Publishers.Rows.Add(row);
                    BaseAccess.AccessPublishersRow(row);
                    row["Pub_Id"] = BaseAccess.Publisher_IDENTITY;
                    row.AcceptChanges();
                    gpb.Publishers = str5;
                }
                string str6 = re.Serie.Trim();
                if (!string.IsNullOrEmpty(str6) && (str6.Length > 0xfe))
                {
                    str6 = str6.Substring(0, 0xfe);
                }
                DataRow[] rowArray2 = Program.ds.Series.Select(string.Format("Name = '{0}'", str6.Replace("'", "''")));
                if (((rowArray2 != null) && (rowArray2.Length == 1)) && (str6.Length != 0))
                {
                    gpb.Series = str6;
                }
                else if (str6.Length != 0)
                {
                    DataRow row2 = Program.ds.Series.NewRow();
                    row2["Name"] = str6;
                    Program.ds.Series.Rows.Add(row2);
                    BaseAccess.AccessSeriesRow(row2);
                    row2["Ser_Id"] = BaseAccess.Series_IDENTITY;
                    row2.AcceptChanges();
                    gpb.Series = str6;
                }
                string str7 = re.Edition.Trim();
                if (!string.IsNullOrEmpty(str7) && (str7.Length > 2))
                {
                    str7 = str7.Substring(0, 2);
                }
                if (!string.IsNullOrEmpty(str7))
                {
                    gpb.Edition = str7;
                }
                string str8 = re.ISBN.Trim();
                if (!string.IsNullOrEmpty(str8) && (str8.Length > 20))
                {
                    str8 = str8.Substring(0, 20);
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    gpb.Isbn_10 = str8;
                }
                string str9 = re.ISBN13.Trim();
                if (!string.IsNullOrEmpty(str9) && (str9.Length > 30))
                {
                    str9 = str9.Substring(0, 30);
                }
                if (!string.IsNullOrEmpty(str9))
                {
                    gpb.Isbn_13 = str9;
                }
                string str10 = re.Binding.Trim();
                if (!string.IsNullOrEmpty(str10) && (str10.Length > 20))
                {
                    str10 = str10.Substring(0, 20);
                }
                if (!string.IsNullOrEmpty(str10))
                {
                    gpb.Paperback = str10;
                }
                string str11 = re.Weight.Trim();
                if (!string.IsNullOrEmpty(str11) && (str11.Length > 10))
                {
                    str11 = str11.Substring(0, 10);
                }
                if (!string.IsNullOrEmpty(str11))
                {
                    gpb.Weight = str11;
                }
                string str12 = re.Price.Trim();
                if (!string.IsNullOrEmpty(str12) && (str12.Length > 10))
                {
                    str12 = str12.Substring(0, 10);
                }
                if (!string.IsNullOrEmpty(str12))
                {
                    gpb.Price = str12;
                }
                if (!string.IsNullOrEmpty(re.WebPage))
                {
                    gpb.Web_links = re.WebPage.Trim();
                }
                if (!string.IsNullOrEmpty(re.Dimension))
                {
                    gpb.SetDimension(re.Dimension.Trim());
                }
                if (!string.IsNullOrEmpty(re.About))
                {
                    gpb.About = re.About.Trim();
                }
                if (!string.IsNullOrEmpty(re.TOC))
                {
                    gpb.Toc = re.TOC.Trim();
                }
                Image image = re.GetImage().GetThumbnailImage(50, 0x4b, null, IntPtr.Zero);
                if (image != null)
                {
                    gpb.Thumbs = image;
                }
                if (!IsDownloadFrontCover)
                {
                    gpb.Front_cover = re.LargeImageUrl.Trim();
                }
                else if (!string.IsNullOrEmpty(re.LargeImageUrl.Trim()))
                {
                    DownloadImage image2 = new DownloadImage(re.LargeImageUrl.Trim());
                    if (image2.Download())
                    {
                        string title = "frontcover";
                        if (!string.IsNullOrEmpty(gpb.Isbn_10))
                        {
                            title = gpb.Isbn_10;
                        }
                        else if (!string.IsNullOrEmpty(gpb.Isbn_13))
                        {
                            title = gpb.Isbn_13;
                        }
                        else if (!string.IsNullOrEmpty(gpb.Title))
                        {
                            title = gpb.Title;
                        }
                        foreach (char ch in Path.GetInvalidFileNameChars())
                        {
                            title = title.Replace(ch.ToString(), string.Empty);
                        }
                        if (string.IsNullOrEmpty(strCoversFolder))
                        {
                            strCoversFolder = Application.StartupPath;
                        }
                        string path = Path.Combine(strCoversFolder, title + ".jpg");
                        if (File.Exists(path))
                        {
                            try
                            {
                                File.Delete(path);
                            }
                            catch
                            {
                            }
                        }
                        if (image2.SaveImage(path, ImageFormat.Jpeg))
                        {
                            gpb.Front_cover = path;
                        }
                        else
                        {
                            gpb.Front_cover = re.LargeImageUrl.Trim();
                        }
                    }
                    else
                    {
                        gpb.Front_cover = re.LargeImageUrl.Trim();
                    }
                }
            }
        }
*/
        public static List<Result> XMLtoListResults(XmlDocument xml, string descr)
        {
            List<Result> list = new List<Result>();
            try
            {
                foreach (XmlNode node2 in xml.SelectSingleNode("Books").ChildNodes)
                {
                    try
                    {
                        Result item = new Result();
                        item.SourceSite = descr;
                        string str = string.Empty;
                        foreach (XmlNode node3 in node2.SelectNodes("Author"))
                        {
                            item.AddAuthor(node3.InnerText);
                        }
                        foreach (XmlNode node4 in node2.SelectNodes("Genre"))
                        {
                            item.Genres.Add(node4.InnerText);
                        }
                        XmlNode node5 = node2.SelectSingleNode("Binding");
                        if (node5 != null)
                        {
                            item.Binding = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("DetailPageURL");
                        if (node5 != null)
                        {
                            item.WebPage = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Edition");
                        if (node5 != null)
                        {
                            item.Edition = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("ISBN");
                        if (node5 != null)
                        {
                            item.ISBN = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Serie");
                        if (node5 != null)
                        {
                            item.Serie = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("FormattedPrice");
                        if (node5 != null)
                        {
                            item.Price = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("NumberOfPages");
                        if (node5 != null)
                        {
                            item.NumberOfPages = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Height");
                        if (node5 != null)
                        {
                            str = str + node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Length");
                        if (node5 != null)
                        {
                            str = str + "x" + node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Width");
                        if (node5 != null)
                        {
                            str = str + "x" + node5.InnerText;
                        }
                        item.Dimension = str;
                        node5 = node2.SelectSingleNode("Weight");
                        if (node5 != null)
                        {
                            item.Weight = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("PublicationDate");
                        if (node5 != null)
                        {
                            string[] strArray = node5.InnerText.Split(new char[] { '-' });
                            if (strArray.Length == 3)
                            {
                                try
                                {
                                    item.PublicationsDate = new DateTime(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));
                                }
                                catch
                                {
                                    item.PublicationsDate = DateTime.Now;
                                }
                            }
                        }
                        node5 = node2.SelectSingleNode("Publisher");
                        if (node5 != null)
                        {
                            item.Publisher = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("Title");
                        if (node5 != null)
                        {
                            item.Title = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("LargeImage");
                        if (node5 != null)
                        {
                            item.LargeImageUrl = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("About");
                        if (node5 != null)
                        {
                            /*
                            be be = new be();
                            be.a(d.b);
                            be.f(1);
                            be.g("Footer");
                            be.i("Header");
                            be.a(ai.a);
                            be.a(au.a);
                            string str3 = be.h(HttpUtility.HtmlDecode(node5.InnerText));
                            item.About = str3;
                            */
                            item.Description = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("ISBN13");
                        if (node5 != null)
                        {
                            item.ISBN13 = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("TOC");
                        if (node5 != null)
                        {
                            /*
                            be be2 = new be();
                            be2.a(d.b);
                            be2.f(1);
                            be2.g("Footer");
                            be2.i("Header");
                            be2.a(ai.a);
                            be2.a(au.a);
                            string str4 = be2.h(HttpUtility.HtmlDecode(node5.InnerText));
                            item.TOC = str4;
                            */
                            item.TOC = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("LargeBackImage");
                        if (node5 != null)
                        {
                            item.LargeBackImageUrl = node5.InnerText;
                        }
                        node5 = node2.SelectSingleNode("SmallImage");
                        if (node5 != null)
                        {
                            item.Thumbs = item.DownloadImage(node5.InnerText, 50, 0x4b);
                        }
                        else
                        {
                            item.Thumbs = item.DownloadImage(string.Empty, 50, 0x4b);
                        }
                        node5 = node2.SelectSingleNode("PublisherImage");
                        if (node5 != null)
                        {
                            item.PublisherImage = node5.InnerText;
                        }
                        list.Add(item);
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
    }

}
