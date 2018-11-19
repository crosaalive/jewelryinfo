using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace stonemgr
{
    public partial class group : Form
    {
        public group()
        {
            InitializeComponent();
        }

        private void group_Load(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                flashGroup();//刷新用户组
            }
            catch (Exception)
            {
                MessageBox.Show("窗体加载数据错误\r\n点击确定继续");
            }
            

        }

        //刷新用户列表
        private void flashGroup()
        {
            try
            {
                string sql = "SELECT group_name,`comment`,sort  FROM `s_group` order by sort desc; ";
                MySqlConnection link = new MySqlConnection(Common.connStr);
                link.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, link);
                //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                MySqlDataReader reader = cmd.ExecuteReader();//读取查询记录
                DataColumn newDC;
                newDC = new DataColumn("用户组", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("备注", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("排序", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                while (reader.Read())
                {
                    string grouName = reader["group_name"].ToString();
                    string comment = reader["comment"].ToString();
                    string sort = reader["sort"].ToString();
                    DataRow newDR;
                    newDR = dt.NewRow();
                    newDR["用户组"] = grouName;
                    newDR["备注"] = comment;
                    newDR["排序"] = sort;
                    dt.Rows.Add(newDR);
                }

                link.Close();
                //label1.Text = "记录数 :" + dt.Rows.Count.ToString();
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);//刷新行号
            }
            catch (Exception groupERR)
            {
                MessageBox.Show("错误提示" + groupERR.Message);
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);  //显示行号
            }
            catch (Exception flashERR)
            {
                MessageBox.Show("错误提示:" + flashERR.Message);

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string gpName = Common.filterSqlStr(this.richTextBox1.Text);//过滤sql注入
                this.richTextBox1.Text = gpName;
                //richTextBox1.SelectionStart =richTextBox1.Text.Length;
                if (this.richTextBox1.Text.Length >= 1)
                {
                    this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
                }
                string sql = " SELECT `group_name`  FROM `s_group` WHERE 1=1 and  `group_name` ='" + gpName + "'; ";
                DataTable gpTable = Common.getData(sql);
                int num = gpTable.Rows.Count;
                if (num > 0)
                {
                    label4.Text = "管理组已存在";
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            catch (Exception gpERR)
            {

                MessageBox.Show("管理组错误提示信息"+gpERR.Message);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text = Common.filterSqlStr(richTextBox2.Text);//过滤sql注入
                if (richTextBox2.Text.Length >= 1)
                {
                    richTextBox2.SelectionStart = richTextBox2.Text.Length;
                }
            }
            catch (Exception gpERR)
            {

                MessageBox.Show("备注错误提示信息" + gpERR.Message);
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox3.Text = Common.RemoveNotNumber(richTextBox3.Text);//过滤非数字
                if (richTextBox3.Text.Length >= 1)
                {
                    richTextBox3.SelectionStart = richTextBox3.Text.Length;
                }
            }
            catch (Exception gpERR)
            {

                MessageBox.Show("排序输入错误提示信息" + gpERR.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string gpName= richTextBox1.Text;
                string comment = richTextBox2.Text;
                string sort = richTextBox3.Text;
                if (gpName != "")
                {
                    string sql = "INSERT INTO `s_group` (`group_name`, `comment`, `sort`) VALUES ('" + gpName + "', '" + comment + "', '" + sort + "');";
                    Common c1 = new Common();
                    int result = c1.doSql(sql);//插入数据

                    if (result > 0)
                    {
                        flashGroup();
                        richTextBox1.Text = "";
                        richTextBox2.Text = "";
                        richTextBox3.Text = "";
                        flashGroup();
                        button1.Enabled = false;
                    }

                }
            }
            catch (Exception addERR)
            {

                MessageBox.Show("添加用户错误提示" + addERR.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flashGroup();
        }
    }
}
