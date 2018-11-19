using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace stonemgr
{
    public partial class stoneList : Form
    {

        private int totalRow, page,perPage =3000,currentPage=1,offet =0; //总记录数 . 页数. 每页数量 ,当前页,起始位置
        string listSql = "";
        public stoneList()
        {
            InitializeComponent();
        }

        //窗体加载载入第一页
        private void stoneList_Load(object sender, EventArgs e)
        {
            try
            {
                button3.Enabled = false;//初始化上下页按钮状态
                button4.Enabled = false;
                string recordCount = "SELECT COUNT(goodsid) as total FROM `s_goods`;";//主记录
                string historyCount = "SELECT COUNT(goodsid) as logTotal FROM `s_goodslog`;";//历史存档记录
                DataTable total = Common.getData(recordCount);
                DataTable logTotal = Common.getData(historyCount);
                label3.Text = total.Rows[0]["total"].ToString();
                label4.Text = "历史记录数:" + logTotal.Rows[0]["logTotal"].ToString();
                
                showInfo();
                loadData();
                chageBtn34();
            }
            catch (Exception loadRecordERR)
            {
                MessageBox.Show("加载记录数异常提示: " + loadRecordERR.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                loadData(); ;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }



        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);  //速度更好
            }
            catch (Exception flashERR)
            {
                MessageBox.Show("石位列表刷新行号异常 ,提示:" + flashERR.Message);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //showData();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //int i = 0;
            
            //string listSql = "INSERT INTO `s_goods_copy`(`goodsno`, `goods_name`, `goods_stone`, `comment`, `add_time`, `add_user`) VALUES ('8018271', 'E-8018271', 'RS1.75(24)L	\nRS2.0(24)L	\nRS2.5(12)RS4.0(2)	\n细O字链长3.5CM	', '8018271', '2017-10-31 11:46:44', 'test'); ";
            //MySqlConnection link2 = new MySqlConnection(Common.connStr);
            //link2.Open();
            //while (i < 1000000)
            //{
                
            //    MySqlCommand cmd = new MySqlCommand(listSql, link2);
            //    int result = cmd.ExecuteNonQuery(); //执行插入数据
            //    textBox1.Text = "";
            //    textBox1.Text = i.ToString();
            //    i++;
            //}
            //link2.Close();
            //sw.Stop();
            //TimeSpan ts = sw.Elapsed;
            //textBox1.Text = ts.ToString();
        }

        //根据页码修改上下页按钮状态
        private void chageBtn34() 
        {
            totalRow = Convert.ToInt32(label3.Text);
            page = totalRow / perPage + 1 ;
            //textBox1.Text = page.ToString();
            label7.Text = "当前页  " + currentPage + "/" + page;

            if ( page == 1 )
            {
                button3.Enabled = false;
                button4.Enabled = false;

            }
            else if (page ==2 )
            {
                if (currentPage==1)
                {
                    button3.Enabled = false;
                    button4.Enabled = true;
                }
                else
                {
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
            }
            else if (page>= 3 )
            {
                if (currentPage == 1)
                {
                    button3.Enabled = false;
                    button4.Enabled = true;
                }
                else if (currentPage == page)
                {
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
                else 
                {
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
                
            }
        }

        private void loadData()
        {
            try
            {
                // private int totalRow, page,perPage =5000,currentPage=1,offet =0; //总记录数 . 页数. 每页数量 ,当前页,起始位置
                Stopwatch sw = new Stopwatch();
                sw.Start();
                listSql = "SELECT `goods_name` ,`goods_stone` ,`comment`,`add_time` ,`add_user` FROM `s_goods`   limit "+offet+","+perPage +"; ";
                //调试表
                //数据库db2
                // string listSql = "SELECT `goods_name` ,`goods_stone` ,`comment`,`add_time` ,`add_user` FROM `s_goods_copy`   limit 30000 ; ";
                try
                {
                    MySqlConnection link2 = new MySqlConnection(Common.connStr);
                    link2.Open();
                    DataTable dt = new DataTable();
                    MySqlCommand cmd = new MySqlCommand(listSql, link2);
                    //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    MySqlDataReader reader = cmd.ExecuteReader();//读取查询记录
                    DataColumn newDC;
                    newDC = new DataColumn("货号", System.Type.GetType("System.String"));
                    dt.Columns.Add(newDC);
                    newDC = new DataColumn("石位信息", System.Type.GetType("System.String"));
                    dt.Columns.Add(newDC);
                    newDC = new DataColumn("备注", System.Type.GetType("System.String"));
                    dt.Columns.Add(newDC);
                    newDC = new DataColumn("添加时间", System.Type.GetType("System.String"));
                    dt.Columns.Add(newDC);
                    newDC = new DataColumn("添加用户", System.Type.GetType("System.String"));
                    dt.Columns.Add(newDC);

                    while (reader.Read())
                    {
                        string goods_name = reader["goods_name"].ToString();
                        string goods_stone = reader["goods_stone"].ToString();
                        string comment = reader["comment"].ToString();
                        string add_time = reader["add_time"].ToString();
                        string add_user = reader["add_user"].ToString();
                        DataRow newDR;
                        newDR = dt.NewRow();
                        newDR["货号"] = goods_name;
                        newDR["石位信息"] = goods_stone;
                        newDR["备注"] = comment;
                        newDR["添加时间"] = add_time;
                        newDR["添加用户"] = add_user;
                        dt.Rows.Add(newDR);
                    }
                    link2.Close();
                    label1.Text = "当前页记录数 :" + dt.Rows.Count.ToString();

                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);//刷新行号
                }
                catch (MySqlException stongeERR)//
                {
                    MessageBox.Show("获取石位信息列表错误,错误信息" + stongeERR.Message + "\r\n 点击确定继续使用");
                }
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                textBox1.Text = ts.ToString();//统计耗时
            }
            catch (Exception flashERR2)
            {
                MessageBox.Show("刷新失败 ,提示:" + flashERR2.Message);
            }
        }

        

        //上页按钮操作数据
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

                currentPage = currentPage - 1;
                if (currentPage <= 1)//当前页不能小于页码总       
                {
                    offet =0; 
                }
                else
                {
                    offet = currentPage * perPage - perPage;
                }

                loadData();
                showInfo(); //修改 limit offset , length
                chageBtn34();//修改上页下页状态
            }

            catch (Exception btnERR)
            {

                MessageBox.Show("翻页错误提示信息" + btnERR.Message);
            }
        }

        //下页按钮操作数据
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (currentPage > page) //当前页不能大于总页码
                {
                    offet = perPage * page;//修改起始位置
                }
                else
                {
                    offet = currentPage * perPage;
                }

                
                loadData(); //载入数据到dgv1
                currentPage = currentPage + 1; //修改页码
                showInfo();//修改 page  offset 数值
                chageBtn34();//修改上页下页按钮状态
            }
            catch (Exception btnERR)
            {

                MessageBox.Show("翻页错误提示信息" + btnERR.Message);
            }
        }

        private void showInfo()
        {
            label5.Text = "当前页 currentPage : " + currentPage + "  offset : " + offet;

        }
        
    }
}
