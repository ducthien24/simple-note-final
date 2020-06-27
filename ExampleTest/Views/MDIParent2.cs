using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleTest
{
    public partial class MDIParent2 : Form
    {
        private int childFormNumber = 0;

        // My code
        int panelMenu;
        bool Hidden;
        int panelAdd;
        bool flag;
        int panelInfo;
        bool infoHidden;
        //int panelTrash;
        //bool trashHidden;

        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();

        // My back-end
        DUANEntities db = new DUANEntities();
        

        public int a;
        public int b;
        public string ma;
        
        public MDIParent2()
        {
            InitializeComponent();
            label10.Text = DateTime.Now.ToString();

        }
        
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();

            // My code
            panelMenu = panelLeft.Width;
            panelAdd = panelMid1.Width;
            panelInfo = panelRight.Width;
            //panelTrash = panelBot.Height;
            Hidden = false;
            flag = false;
            infoHidden = false;
            //trashHidden = false;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            loadTag();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                panelLeft.Width = panelLeft.Width + 50;
                if (panelLeft.Width >= 187)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 50;
                if (panelLeft.Width <= 0)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (flag == true)
            {
                panelLeft.Width = panelLeft.Width + 187;
                panelMid1.Width = panelMid1.Width + 280;

                if ((panelLeft.Width >= panelMenu) && (panelMid1.Width >= panelAdd))
                {
                    timer2.Stop();
                    flag = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 100;
                panelMid1.Width = panelMid1.Width - 100;
                if ((panelLeft.Width <= 0) && (panelMid1.Width <= 0))
                {
                    timer2.Stop();
                    flag = true;
                    this.Refresh();
                }
            }
        }
        int checkword(string str)
        {
            int l = 0;
            int bien_dem = 1;

            /* lap toi phan cuoi cua chuoi */
            while (l <= str.Length - 1)
            {
                /* kiem tra xem ky tu hien tai co phai la khoang trang 
                 * hay la ky tu new line hay ky tu tab */
                if (str[l] == ' ' || str[l] == '\n' || str[l] == '\t')
                {
                    bien_dem++;
                }

                l++;
            }
            return bien_dem;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer3.Start();

            // My code


            var delData = (from s in db.NoiDungs
                           where s.namefile.Contains(txtcontent.Text)
                           select s).FirstOrDefault<NoiDung>();
            label10.Text = delData.thoigian;
            label12.Text = checkword(delData.namefile).ToString() + " words";
            string str = txtcontent.Text;

            label13.Text = str.Length.ToString() + " characters";

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (infoHidden)
            {
                panelRight.Width = panelRight.Width + 190;
                if (panelRight.Width >= panelInfo)
                {
                    timer3.Stop();
                    infoHidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panelRight.Width = panelRight.Width - 100;
                if (panelRight.Width <= 0)
                {
                    timer3.Stop();
                    infoHidden = true;
                    this.Refresh();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hidden = false;
            timer1.Start();
            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
            loadData();
        }

        private void MDIParent2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer3.Start();
            timer4.Start();
            this.ActiveControl = txtcontent;
            txtcontent.Focus();
            loadData();

            
            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
            loadTag();
        }

        // Function Load Data
        void loadData()
        {
            var query = (from s in db.NoiDungs
                         where s.userId == Form1.key
                         select s).ToList();
            dataGridView1.DataSource = query;
        }
        void loadData2()
        {
            var result = (from p in db.XoaNDs
                          select p).ToList();
            dataGridView1.DataSource = result;
        }
        void loadTag()
        {
            var query = (from s in db.NoiDungs
                         where s.userId == Form1.key
                         group s by s.tag
                         ).ToList();
            dataGridView2.DataSource = query;

            //dataGridView2.ColumnCount = 1;
            //dataGridView2.Columns[0].Name = "Tags";
            //foreach (NoiDung data in query)
            //{
            //    dataGridView2.Rows.Add(data);
            //}
        }
        int KTcount()
        {
            var checkfile = (from s in db.NoiDungs
                             where s.userId == Form1.key
                             select s).ToList();
            return checkfile.Count();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            a = KTcount();
            
            if (a == 0)
            {
                var std = new NoiDung()
                {
                    Id = 0,
                    namefile = txtcontent.Text,
                    thoigian = (DateTime.Now).ToString(),
                    userId = Form1.key,
                    tag = txttag.Text
                };
                db.NoiDungs.Add(std);

                db.SaveChanges();
            }
            else
            {
                var item = db.NoiDungs.OrderByDescending(i => i.Id).FirstOrDefault();
                b = item.Id;
                //MessageBox.Show(b.ToString());

                var td = new NoiDung()
                {
                    Id = b + 1,
                    namefile = txtcontent.Text,
                    thoigian = (DateTime.Now).ToString(),
                    userId = Form1.key,
                    tag = txttag.Text
                };
                db.NoiDungs.Add(td);
                db.SaveChanges();
            }
            loadData();
            loadTag();

        }
        // Trash 1 
        // còn một lỗi khi thêm khóa trùng nhau
        private void button3_Click(object sender, EventArgs e)
        {
            int d = int.Parse(ma);
            var delData = (from s in db.NoiDungs
                           where s.Id == d
                           select s).FirstOrDefault<NoiDung>();

            db.NoiDungs.Remove(delData);

            var td = new XoaND()
            {
                Id = delData.Id,
                namefile = delData.namefile,
                thoigian = delData.thoigian,
                userId = Form1.key,
                tag = delData.tag
            };
            db.XoaNDs.Add(td);

            db.SaveChanges();
            loadData();
        }
        // All Trash
        private void button6_Click(object sender, EventArgs e)
        {
            
            Hidden = false;
            timer1.Start();
            button14.Visible = true;
            button15.Visible = true;
            button16.Visible = true;

            loadData2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtcontent.Text = dataGridView1.Rows[e.RowIndex].Cells["namefile"].FormattedValue.ToString();
                txttag.Text = dataGridView1.Rows[e.RowIndex].Cells["tag"].FormattedValue.ToString();
                ma = dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            //if (trashHidden)
            //{
            //    //panelLeft.Height = panelLeft.Width + 10;
            //    panelBot.Height = panelBot.Height + 30;
            //    if (panelLeft.Height >= 72)
            //    {
            //        timer1.Stop();
            //        trashHidden = false;
            //        this.Refresh();
            //    }
            //}
            //else
            //{
            //    //panelLeft.Width = panelLeft.Width - 10;
            //    panelBot.Height = panelBot.Height - 30;
            //    if (panelBot.Height <= 0)
            //    {
            //        timer1.Stop();
            //        trashHidden = true;
            //        this.Refresh();
            //    }
            //}
        }
        // Empty Trash
        private void button16_Click(object sender, EventArgs e)
        {
            var query = (from s in db.XoaNDs
                         select s).ToList();

           

            foreach (XoaND data in query)
            {
                db.XoaNDs.Remove(data);
                db.SaveChanges();
            }

            loadData2();
            
        }
        // Delete Forever
        private void button15_Click(object sender, EventArgs e)
        {
            var delData = (from s in db.XoaNDs
                           where s.namefile.Contains(txtcontent.Text)
                           select s).FirstOrDefault<XoaND>();

            db.XoaNDs.Remove(delData);
            db.SaveChanges();
            loadData2();
        }
        // Restore Note
        private void button14_Click(object sender, EventArgs e)
        {
            var Restore = (from s in db.XoaNDs
                           where s.namefile.Contains(txtcontent.Text)
                           select s).FirstOrDefault<XoaND>();
            var td = new NoiDung()
            {
                Id = (int)Restore.Id,
                namefile = Restore.namefile,
                thoigian = Restore.thoigian,
                userId = Form1.key,
                tag = Restore.tag
            };
            db.NoiDungs.Add(td);
            db.XoaNDs.Remove(Restore);
            //a++;

            

            db.SaveChanges();
            loadData2();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        // Update Data
        private void button10_Click(object sender, EventArgs e)
        {
            int c = int.Parse(ma);
            var upData = (from s in db.NoiDungs
                           where s.Id == c
                           select s).FirstOrDefault<NoiDung>();
            upData.namefile = txtcontent.Text;
            upData.tag = txttag.Text;
            db.SaveChanges();
            loadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var list = (from s in db.NoiDungs
                        where s.namefile.Contains(textBox1.Text) || s.tag.Contains(textBox1.Text)
                        select s).ToList();
            dataGridView1.DataSource = list;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Selected = true;
            dataGridView1.CurrentRow.Selected = false;
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {

            darkToolStripMenuItem.Checked = true;
            lightToolStripMenuItem.Checked = false;
            foreach (Control c in this.Controls)
            {
                UpdateColorControls(c);
            }
        }
        public void UpdateColorControls(Control myControl)
        {
            myControl.BackColor = Color.Black;
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorControls(subC);
            }
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkToolStripMenuItem.Checked = false;
            lightToolStripMenuItem.Checked = true;
            foreach (Control c in this.Controls)
            {
                UpdateColorControlss(c);
            }
        }
        public void UpdateColorControlss(Control myControl)
        {
            myControl.BackColor = Color.White;
            myControl.ForeColor = Color.Black;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorControlss(subC);
            }
        }

        private void aphabeticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aphabeticalToolStripMenuItem.Checked = true;
            dateCreatedToolStripMenuItem.Checked = false;
            var query = (from s in db.NoiDungs
                        orderby s.namefile
                        select s).ToList();
            dataGridView1.DataSource = query;
        }

        private void dateCreatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aphabeticalToolStripMenuItem.Checked = false;
            dateCreatedToolStripMenuItem.Checked = true;
            var query = (from s in db.NoiDungs
                         select s).ToList();
            dataGridView1.DataSource = query;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                string str = dataGridView2.Rows[e.RowIndex].Cells["Key"].FormattedValue.ToString();
                var query = (from s in db.NoiDungs
                             where s.userId == Form1.key && s.tag == str
                             select s).ToList();
                dataGridView1.DataSource = query;

            }
        }

        private void sortAphabeticalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sortAphabeticalyToolStripMenuItem.Checked == false)
            {
                sortAphabeticalyToolStripMenuItem.Checked = true;
                var query = (from s in db.NoiDungs
                             where s.userId == Form1.key
                             orderby s.tag
                             group s by s.tag).ToList();
                dataGridView2.DataSource = query;
            }
            else
            {
                sortAphabeticalyToolStripMenuItem.Checked = false;
                var query = (from s in db.NoiDungs
                             where s.userId == Form1.key
                             orderby s.tag
                             group s by s.tag).ToList();
                dataGridView2.DataSource = query;
            }
        }

        private void toggleFullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            a = KTcount();

            if (a == 0)
            {
                var std = new NoiDung()
                {
                    Id = 0,
                    namefile = txtcontent.Text,
                    thoigian = (DateTime.Now).ToString(),
                    userId = Form1.key,
                    tag = txttag.Text
                };
                db.NoiDungs.Add(std);

                db.SaveChanges();
            }
            else
            {
                var item = db.NoiDungs.OrderByDescending(i => i.Id).FirstOrDefault();
                b = item.Id;
                //MessageBox.Show(b.ToString());

                var td = new NoiDung()
                {
                    Id = b + 1,
                    namefile = txtcontent.Text,
                    thoigian = (DateTime.Now).ToString(),
                    userId = Form1.key,
                    tag = txttag.Text
                };
                db.NoiDungs.Add(td);
                db.SaveChanges();
            }
            loadData();
            loadTag();
        }
    }
}
