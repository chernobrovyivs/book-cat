using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
    public partial class BookCatTree : UserControl
    {
        [Category("Appearance"), Description("Режим редактирования")]
        public bool EditMode { get; set; }

        public BookCatTree()
        {
            InitializeComponent();
        }

        public void Save(long _book_id)
        {
            SQLiteCommand sc = new SQLiteCommand("DELETE FROM BookCatLink WHERE book_id=@book_id");
            sc.Parameters.AddWithValue("@book_id", _book_id);
            Db.ExecuteScalar(sc);

            List<long> l = GetChecked();

            foreach (long ci in l)
            {
                sc = new SQLiteCommand("INSERT INTO BookCatLink(BookCat_id, Book_id) VALUES (@BookCat_id, @Book_id)");
                sc.Parameters.AddWithValue("@BookCat_id", ci);
                sc.Parameters.AddWithValue("@Book_id", _book_id);
                Db.ExecuteNonQuery(sc);
            }

        }

        private List<long> GetChecked()
        {
            List<long> tr = new List<long>();

            Getch(tr, treeView2.Nodes);

            return tr;
        }

        void Getch(List<long> ids, TreeNodeCollection nods)
        {
            foreach (TreeNode nn in nods)
            {
                if (nn.Checked) ids.Add(((TNodeData)nn.Tag).BookCat_id);

                Getch(ids, nn.Nodes);
            }
        }


        public void LoadChekByBookId(long bBook_id)
        {
            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM BookCatLink WHERE book_id=@book_id");
            sc.Parameters.AddWithValue("@book_id", bBook_id);
            DataTable dat = Db.Fill(sc);

            foreach (DataRow rr in dat.Rows)
            {
                CheckItemByCatId((long) rr["BookCat_id"], treeView2.Nodes);
            }
        }

        void CheckItemByCatId(long i, TreeNodeCollection nods)
        {
            foreach (TreeNode nn in nods)
            {
                if (((TNodeData)nn.Tag).BookCat_id == i)
                {
                    nn.Checked = true;
                    break;
                }

                CheckItemByCatId(i, nn.Nodes);
            }
        }

        public void nn(DataTable dat, long top_id, TreeNode tr)
        {
            foreach (DataRow rr in dat.Select("Top_id=" + top_id))
            {
                TreeNode t = new TreeNode((string)rr["Name"])
                
                {
                    Tag =
                        new TNodeData
                        {
                            BookCat_id = (long)rr["BookCat_id"],
                            Name = (string)rr["Name"],
                            Top_id = (long)rr["Top_id"]
                        }
                };

                if (tr == null)
                    treeView2.Nodes.Add(t);
                else
                    tr.Nodes.Add(t);

                nn(dat, (long)rr["BookCat_id"], t);
            }
        }

        class TNodeData
        {
            public long Top_id;
            public long BookCat_id;
            public string Name;
        }

        private void BookCat_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadData();
            }
        }

        void LoadData()
        {
            treeView2.Nodes.Clear();

            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM BookCat");
            DataTable dat = Db.Fill(sc);

            nn(dat, 0, null);
            treeView2.ExpandAll();

            if (EditMode) treeView2.ContextMenuStrip = contextMenuStrip1;
        }

        void ResetData()
        {
            treeView2.Nodes.Clear();
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right )
            {
                treeView2.SelectedNode =e.Node;
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView2.SelectedNode != null)
            {
                FNodeAdd f = new FNodeAdd();
                if (f.ShowDialog() == DialogResult.OK && f.getValue != "")
                {
                    SQLiteCommand sc = new SQLiteCommand("INSERT INTO BookCat(Name, Top_id) VALUES (@Name, @Top_id)");
                    sc.Parameters.AddWithValue("@Name", f.getValue);
                    sc.Parameters.AddWithValue("@Top_id", ((TNodeData) treeView2.SelectedNode.Tag).BookCat_id);
                    long id = Db.ExecuteNonQueryInsert(sc);

                    ResetData();
                    LoadData();
                }
            }
        }

        private void добавитьВКореToolStripMenuItem_Click(object sender, EventArgs e)
        {
                FNodeAdd f = new FNodeAdd();
                if (f.ShowDialog() == DialogResult.OK && f.getValue != "")
                {
                    SQLiteCommand sc = new SQLiteCommand("INSERT INTO BookCat(Name, Top_id) VALUES (@Name, @Top_id)");
                    sc.Parameters.AddWithValue("@Name", f.getValue);
                    sc.Parameters.AddWithValue("@Top_id", 0);
                    long id = Db.ExecuteNonQueryInsert(sc);

                    ResetData();
                    LoadData();
                }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView2.SelectedNode;
            if (treeView2.SelectedNode != null)
            {
                if (enr.dialogRealyDelete != DialogResult.Yes) return;

                if (tn.Nodes.Count>0)
                {
                    MessageBox.Show("Существуют дочение категории. Сперва удалите их.","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                TNodeData td = (TNodeData)tn.Tag;

                SQLiteCommand sc = new SQLiteCommand("SELECT COUNT(*) FROM BookCatLink WHERE BookCat_id=@id");
                sc.Parameters.AddWithValue("@id", td.BookCat_id);
                long c = (long)Db.ExecuteScalar(sc);

                if (c>0)
                {
                    if (DialogResult.Yes !=
                        MessageBox.Show("Существуют книги, входящие в данную категорию. Продолжить удаление и вывести книги из категории?","Внимание",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                        )
                    {
                        return;
                    }
                }

                sc = new SQLiteCommand("DELETE FROM BookCatLink WHERE BookCat_id=@id");
                sc.Parameters.AddWithValue("@id", td.BookCat_id);
                Db.ExecuteNonQuery(sc);

                sc = new SQLiteCommand("DELETE FROM BookCat WHERE BookCat_id=@id");
                sc.Parameters.AddWithValue("@id", td.BookCat_id);
                Db.ExecuteNonQuery(sc);

                LoadData();
            }

        }
    }
}
