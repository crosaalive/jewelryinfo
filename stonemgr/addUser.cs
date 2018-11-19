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
    public partial class addUser : Form
    {
        public addUser()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //载入数据
        private void addUser_Load(object sender, EventArgs e)
        {
           
            try
            {
                button1.Enabled = false;
                button4.Enabled = false;
                flashUser();
                comboData();
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                dataGridView1.Columns["密码"].Visible = false;

            }
            catch (Exception loadERR)
            {
                MessageBox.Show("formload载入数据错误" + loadERR.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string user = richTextBox1.Text;
                textBox1.Text = user;
                string pw = richTextBox2.Text;
                string commment = richTextBox3.Text;
                string addTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second).ToString(); //添加时间
                string group = comboBox1.Text;
                string sql = "INSERT INTO `s_user` (`user`, `password`, `addtime`, `ucomment`, `usergroup`) VALUES ('"+user+"', '"+pw+ "', '"+addTime+"','"+commment+"','"+ group +"');";

                if (user != "" && pw != "")
                {
                      Common c1 = new Common();
                int result = c1.doSql(sql);
                richTextBox5.Text = result.ToString();
                if (result>0)
                {
                    flashUser();
                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    richTextBox3.Text = "";
                    button1.Enabled = false;
                }

                }

            }
            catch (Exception addERR)
            {

                MessageBox.Show("添加用户错误提示" + addERR.Message);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)//
                {
                    button3.Enabled = true;
                    comboData();
                    richTextBox4.Text =  dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    richTextBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    richTextBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //label10.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    button4.Enabled = true;
                }
            }
            catch (Exception userCKERR)
            {
                MessageBox.Show("错误提示" + userCKERR.Message);
            }
        }

        //刷新用户列表
        private void flashUser()
        {
            try
            {
                string sql = "SELECT user,password, addtime,ucomment,usergroup  FROM `s_user` order by uid asc; ";
                MySqlConnection link = new MySqlConnection(Common.connStr);
                link.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, link);
                //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                MySqlDataReader reader = cmd.ExecuteReader();//读取查询记录
                DataColumn newDC;
                newDC = new DataColumn("用户名", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("密码", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("添加时间", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("备注", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                newDC = new DataColumn("用户组", System.Type.GetType("System.String"));
                dt.Columns.Add(newDC);
                while (reader.Read())
                {
                    string goods_name = reader["user"].ToString();
                    string goods_stone = reader["password"].ToString();
                    string comment = reader["addtime"].ToString();
                    string add_time = reader["ucomment"].ToString();
                    string group = reader["usergroup"].ToString();
                    DataRow newDR;
                    newDR = dt.NewRow();
                    newDR["用户名"] = goods_name;
                    newDR["密码"] = goods_stone;
                    newDR["添加时间"] = comment;
                    newDR["备注"] = add_time;
                    newDR["用户组"] = group;
                    dt.Rows.Add(newDR);
                }

                link.Close();
                //label1.Text = "记录数 :" + dt.Rows.Count.ToString();
                dataGridView1.DataSource = dt;
                //dataGridView1.Columns[2].FillWeight = 45; 
                dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);//刷新行号
            }
            catch (Exception addUserERR)
            {
                MessageBox.Show("错误提示" + addUserERR.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            flashUser();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);  //速度更好
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
                string userName = Common.filterSqlStr(richTextBox1.Text);
                richTextBox1.Text = userName;
                if (richTextBox1.Text.Length > 0)
                {
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                string sql = " SELECT `user`  FROM `s_user` WHERE 1=1 and  `user` ='" + userName + "'; ";
                DataTable userTable = Common.getData(sql);
                int num =userTable.Rows.Count;
                if (num > 0)
                {
                    label9.Text = "用户已存在";
                    button1.Enabled = false;
                }
                else
                {
                    label9.Text = "用户可用";
                    button1.Enabled = true;
                }
            }
            catch (Exception userERR)
            {

                MessageBox.Show("错误信息" + userERR);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string pw = Common.fillterString(richTextBox2.Text);//过滤字符
                richTextBox2.Text = pw;
                if (richTextBox2.Text.Length >= 1)
                {
                    richTextBox2.SelectionStart = richTextBox2.Text.Length;
                }
            }
            catch (Exception passwordERR)
            {

                MessageBox.Show("用户密码错误意思"+passwordERR.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string sql = "SELECT `group_name` FROM `s_group` limit 1000 ;";
                //DataTable dt = Common.getData(sql);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    comboBox1.Items.Add(dt.Rows[i].ToString());
                //}

            }
            catch (Exception comboERR)
            {
                MessageBox.Show("combobox错误提示" + comboERR.Message); 
               
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox3.Text = Common.filterSqlStr(richTextBox3.Text);
                if (richTextBox3.Text.Length >= 1)
                {
                    richTextBox3.SelectionStart = richTextBox3.Text.Length;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //int i = checkBox4();
                //if (i==1)//
                //{
                string condition = Common.filterSqlStr(richTextBox4.Text);
                string comment = Common.filterSqlStr(richTextBox6.Text);
                string group = Common.filterSqlStr(comboBox2.Text);
                string pw = Common.fillterString(richTextBox5.Text);
                //string condition = user;
                string sql = "UPDATE `s_user` SET  `password`='" + pw + "', `ucomment`='" + comment + "', `usergroup`='" + group + "' WHERE (`user`='" + condition + "'); ";
                Common c1 = new Common();
                int result = c1.doSql(sql);

                if (result > 0)
                {
                    flashUser();
                    MessageBox.Show("修改成功, 点击确定继续");
                    
                } 
                //}
                
            }
            catch (Exception saveERR)
            {
                MessageBox.Show("保存错误提示" + saveERR.Message);
            }
        }

        private void comboData()
        {
            try
            {
                string sql = "SELECT `group_name` FROM `s_group` limit 1000 ;";
                DataTable dt = Common.getData(sql);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    comboBox1.Items.Add(item[0].ToString());
                    comboBox2.Items.Add(item[0].ToString());
                }
            }
            catch (Exception comboERR)
            {
                MessageBox.Show("combox数据载入错误提示" + comboERR.Message);
            }
        }

        //
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox2.Text;
        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox5.Text = Common.fillterString(richTextBox5.Text);
                richTextBox5.SelectionStart = richTextBox5.Text.Length;
            }
            catch (Exception)
            {
                
               
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int i = checkBox4();

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //返回1, 执行修改,返回0, 不操作
        private int  checkBox4()
        {
            try
            {
                string userName = Common.filterSqlStr(richTextBox4.Text);
                richTextBox4.Text = userName;
                if (richTextBox1.Text.Length > 0)
                {
                    richTextBox4.SelectionStart = richTextBox4.Text.Length;
                }
                string sql = " SELECT `user`  FROM `s_user` WHERE 1=1 and  `user` ='" + userName + "'; ";
                DataTable userTable = Common.getData(sql);
                int num =userTable.Rows.Count;
                
                if (num > 0)
                {

                    MessageBox.Show("用户已存在,不能修改");
                    button4.Enabled = false;
                    return 0;
                   
                }
                else
                {
                    return 1;
                    button4.Enabled = true;
                }
            }
            catch (Exception box4ERR)
            {
                MessageBox.Show("richtextbox4检测错误提示信息"+box4ERR.Message);
                return 0;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

    }
}
