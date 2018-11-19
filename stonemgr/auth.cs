using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//说明基于rbac思路,简略菜单表保存
//0,1,2,3,4,5,6
//对应mainform 主菜单
//0,searchToolStripMenuItem
//1,addToolStripMenuItem
//2,stoneListToolStripMenuItem
//3,adduserToolStripMenuItem
//4,groupmgrToolStripMenuItem
//5,permissionmgrToolStripMenuItem

namespace stonemgr
{
    public partial class auth : Form
    {
        public auth()
        {
            InitializeComponent();
        }

        List<int> menu = new List<int>();
        private void auth_Load(object sender, EventArgs e)
        {
            try
            {
                comboData();//载入用户组
                richTextBox2.Text = menu.Count.ToString();
                 
                 string str = "";
            }
            catch (Exception loadERR)
            {

                MessageBox.Show("窗体初始化异常 " + loadERR.Message);
            }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        

        private void comboData()
        {
            try
            {
                string sql = "SELECT `group_name` FROM `s_group` order by sort desc limit 1000 ;";
                DataTable dt = Common.getData(sql);
                comboBox1.Items.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    comboBox1.Items.Add(item[0].ToString());
                    //comboBox2.Items.Add(item[0].ToString());
                }
            }
            catch (Exception comboERR)
            {
                MessageBox.Show("combox数据载入错误提示" + comboERR.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox1.Checked == true)
                {
                    menu.Add(checkBox1.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox1.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb1ERR)
            {

                Common.showERR(ckb1ERR.Message);
            }
            
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox2.Checked == true)
                {
                    menu.Add(checkBox2.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox2.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb2ERR)
            {

                Common.showERR(ckb2ERR.Message);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox3.Checked == true)
                {
                    menu.Add(checkBox3.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox3.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb3ERR)
            {

                Common.showERR(ckb3ERR.Message);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox4.Checked == true)
                {
                    menu.Add(checkBox4.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox4.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb4ERR)
            {

                Common.showERR(ckb4ERR.Message);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox5.Checked == true)
                {
                    menu.Add(checkBox5.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox5.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb5ERR)
            {

                Common.showERR(ckb5ERR.Message);
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.checkBox6.Checked == true)
                {
                    menu.Add(checkBox6.TabIndex);
                }
                else
                {
                    menu.Remove(checkBox6.TabIndex);
                }
                menuItem();
            }
            catch (Exception ckb6ERR)
            {

                Common.showERR(ckb6ERR.Message);
            }
        }

        private void menuItem() 
        {
            richTextBox2.Text = "";
            for (int i = 0; i < menu.Count; i++)
            {
                richTextBox2.Text += menu[i]+",";
            }
            //richTextBox2.Text = menu.Count.ToString();
        }

        //保存权限编辑
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择用户组保存");
                    //richTextBox2.Text = "空空空空空空空空空空空空空空空空";
                }
                else
                {
                    string str = "";
                    string group = comboBox1.Text;
                    str = string.Join(",", menu);//转换逗号分割的数据保存
                    string sql = " REPLACE  INTO `s_menu` (`permission`, `group_name`) VALUES ('"+ str +"', '"+ group +"'); ";
                    //richTextBox1.Text = sql;
                    Common c1 = new Common();
                    int num = c1.doSql(sql);
                    if (num > 0)
                    {
                        MessageBox.Show("用户组权限修改已保存");
                        
                    }
                }
            }
            catch (Exception saveERR)
            {
                MessageBox.Show("保存失败 " + saveERR.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
