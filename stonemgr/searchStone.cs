using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //mysql 
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;

namespace stonemgr
{
    public partial class searchStone : Form
    {
        public searchStone()
        {
            InitializeComponent();
        }

        private void searchStone_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            //调试用
            //label2.Text = Common.strPrefix(richTextBox1.Text) + Common.goodsNum(richTextBox1.Text.Substring(1));  
        }

        //s输入货号触发事件
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //richTextBox3.Text = Common.picPath;
                richTextBox1.Text = Common.RemoveNotNumber(richTextBox1.Text);
                if (richTextBox1.Text.Length >= 1)
                {
                    richTextBox1.SelectionStart = richTextBox1.Text.Length ; //过滤字符后修改光标位置
                    label2.Text = Common.strPrefix(richTextBox1.Text) + Common.goodsNum(richTextBox1.Text.Substring(1));
                    showImages(label2.Text);
                }
            }
            catch (Exception richTextBox1ERR)
            {
                MessageBox.Show("查询输入检测到异常,异常信息" + richTextBox1ERR.Message + "\r\n 点击确定继续使用");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //查询载入数据到dgv1  dgv2
        private void button1_Click(object sender, EventArgs e)
        {
            string gName = label2.Text;
            string sql = "SELECT `goodsid`,`goods_name`,`goods_stone`,`comment`,`add_time`,`add_user` FROM `s_goods` WHERE   1=1 AND goods_name ='" + gName + "';";
            try
            {
                MySqlConnection link2 = new MySqlConnection(Common.connStr);
                link2.Open();
                DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, link2);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);
                link2.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int index = dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = ds.Tables[0].Rows[i]["goods_name"]; //货号
                    this.dataGridView1.Rows[index].Cells[1].Value = ds.Tables[0].Rows[i]["goods_stone"];//石位信息
                    this.dataGridView1.Rows[index].Cells[2].Value = @ds.Tables[0].Rows[i]["comment"];//备注
                    this.dataGridView1.Rows[index].Cells[3].Value = ds.Tables[0].Rows[i]["add_time"];//添加时间
                    
                    this.dataGridView1.Rows[index].Cells[4].Value = ds.Tables[0].Rows[i]["add_user"];//添加者

                    dataGridView1.Rows[index].Selected = true; //选中
                    dataGridView1.FirstDisplayedScrollingRowIndex = index;
                    label8.Text = "添加者: " + ds.Tables[0].Rows[i]["add_user"];
                    try
                    {
                        richTextBox4.Rtf = "" + @ds.Tables[0].Rows[i]["comment"];
                    }
                    catch (Exception)
                    {
                        richTextBox4.Text = "" + @ds.Tables[0].Rows[i]["comment"];
                        
                    }
                    
                    richTextBox3.Text = " " + ds.Tables[0].Rows[i]["goods_stone"];
                    richTextBox2.Text = "" + ds.Tables[0].Rows[i]["goods_name"];
                }
                
                //dataGridView3.DataSource = ds.Tables[0];
                

                string sqllog = "SELECT `goodsid`,`goods_name`,`goods_stone`,`comment`,`add_time`,`add_user` FROM `s_goodslog` WHERE   1=1 AND goods_name ='" + gName + "';";

                DataTable result2 = Common.getStoneInfo(sqllog);
                //richTextBox5.Text = result2.Rows.Count.ToString();
                dataGridView2.Rows.Clear();
                for (int k = 0; k <result2.Rows.Count ; k++)
                {
                     int idx = dataGridView2.Rows.Add();
                     this.dataGridView2.Rows[idx].Cells[0].Value = result2.Rows[k]["goods_name"]; //货号
                     this.dataGridView2.Rows[idx].Cells[1].Value = result2.Rows[k]["goods_stone"];//石位信息

                     this.dataGridView2.Rows[idx].Cells[2].Value = @result2.Rows[k]["comment"];//备注

                     this.dataGridView2.Rows[idx].Cells[3].Value = result2.Rows[k]["add_time"];//添加时间
                     this.dataGridView2.Rows[idx].Cells[4].Value = result2.Rows[k]["add_user"];//添加者
                }
                //dataGridView2.DataSource = result2;
            }
            catch (Exception searchERR)
            {
                MessageBox.Show("查询数据异常 ,异常消息 :" + searchERR.Message + "\r\n点击确定继续");
            }
        }

        //显示图片,复制图片到剪贴板
        private void showImages(string gName)
        {
            try
            {
                //Ping ping = new Ping();//去除图片ping 测试
                //PingReply pingReply = ping.Send(Common.picIp,800);
                //if (pingReply.Status == IPStatus.Success)
                //{
                    string pic = Common.picPath + "\\" + gName + ".jpg";
                    if (File.Exists(pic))
                    {
                        label10.Visible = false;
                        Image img = Image.FromFile(pic);
                        Clipboard.SetImage(img);//复制图片到剪贴板
                        //Clipboard.SetDataObject(Image.FromFile(pic) + ".jpg", true);
                        pictureBox1.Load(Common.picPath + "\\" + gName + ".jpg"); //载入图片
                        label11.Text = gName + "已复制到剪贴板"; //文字提示

                    }
                    else
                    {
                        label10.Visible = true;
                        label10.Text = "目标图片不存在,请检查";
                        pictureBox1.Image = null;
                    }
                //}
                //else
                //{
                //    label10.Visible = true;
                //    label10.Text = "图片服务超时,请检查";
                //    pictureBox1.Image = null;
                //}
            }
            catch (Exception showImagesERR)
            {
                label10.Text = "错误提示" + showImagesERR.Message;
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label10.Visible = false;
            ToolTip tips = new ToolTip();
            tips.InitialDelay = 800;
            tips.ReshowDelay = 300;
            tips.ShowAlways = true;
            tips.IsBalloon = true;
            tips.SetToolTip(this.pictureBox1, "图片会自动复制到剪贴板");
        }

        //复制图片到剪贴板
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                string pic = Common.picPath + "\\" + label2.Text + ".jpg";
                if (File.Exists(pic))
                {
                    Image img = Image.FromFile(pic);
                    Clipboard.SetImage(img);
                    label11.Text = label2.Text + "已复制到剪贴板";
                }
                else 
                {
                    label11.Text = label2.Text + "不存在!!";
                }
                
            }
            catch (Exception cpPicERR)
            {
                MessageBox.Show("复制图片异常 :异常信息" + cpPicERR + "\r\n 点击确定继续使用");
            }
        }




        //点击单元格载入数据到富文本框
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                richTextBox5.Text = e.RowIndex.ToString();
                if (e.RowIndex >= 0  )//判断是否是行头选中
                {
                    
                    richTextBox5.Text = " " + e.ColumnIndex;
                    //MessageBox.Show(this.dataGridView1.CurrentCell.Value.GetType().ToString());
                    //MessageBox.Show(this.dataGridView1.CurrentCell.Value);
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)//单元格空白行不载入数据
                    {
                        richTextBox5.Text = "111";
                    }
                    else
                    {
                        string gName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        showImages(gName);//点击表单的内容载入相应的图片
                        string sql = "SELECT `goods_name`,`goods_stone`,`comment`,`add_time`,`add_user` FROM `s_goodslog` WHERE   1=1 AND goods_name ='" + gName + "';";

                        richTextBox2.Text = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                        try
                        {
                            richTextBox4.Rtf = @dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        }
                        catch (Exception)
                        {

                            richTextBox4.Text = @dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        }
                        

                        richTextBox3.Text = "" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                        label8.Text = "添加者: " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                           
                        DataTable ds = Common.getStoneInfo(sql); //点击单元格后重查数据并载入dgv2
                        dataGridView2.Rows.Clear(); //先清空后载入
                        for (int i = 0; i < ds.Rows.Count; i++)
                        {
                            int index = dataGridView2.Rows.Add();
                            this.dataGridView2.Rows[index].Cells[0].Value = ds.Rows[i]["goods_name"]; //货号
                            this.dataGridView2.Rows[index].Cells[1].Value = ds.Rows[i]["goods_stone"];//石位信息

                            this.dataGridView2.Rows[index].Cells[2].Value = ds.Rows[i]["comment"].ToString();//备注

                            this.dataGridView2.Rows[index].Cells[3].Value = ds.Rows[i]["add_time"];//添加时间
                            this.dataGridView2.Rows[index].Cells[4].Value = ds.Rows[i]["add_user"];//添加者
                        }
                            
                    }
                        
                    //richTextBox5.Text = (string.Format("第{0}行第{1}列被选中", e.RowIndex + 1, e.ColumnIndex + 1));
                }
            }
            catch (Exception puchERR)
            {
                MessageBox.Show("dataGridView1_CellClick 载入数据错误,错误信息:" + puchERR);
                //throw;
            }
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                richTextBox5.Text = e.RowIndex.ToString();
                if (e.RowIndex >= 0)
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells[2].Value == null)//单元格空白行不载入数据
                    {
                        //&& dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() !=""
                        richTextBox5.Text = "111";
                    }
                    else
                    {
                        richTextBox2.Text = "" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                        try
                        {
                            richTextBox4.Rtf = @dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

                        }
                        catch (Exception rtfERR)
                        {
                            if (rtfERR.Message == "File format is not alid")
                            {
                                MessageBox.Show("1");
                                richTextBox4.Text = @dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                            }
                            else if(rtfERR.Message == "File format is not alid.")
                            {
                                MessageBox.Show("11");
                                richTextBox4.Text = @dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                            }

                        }
                        
                        richTextBox3.Text = "" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                        label8.Text = "添加者: " + dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                    }
                }
            }
            catch (Exception puchERR)
            {
                MessageBox.Show("dataGridView2选中单元格载入数据错误,错误信息:" + puchERR);
                //throw;
            }
        }

        //dataGridView2_RowStateChanged查询刷新表格行号
        //行号没有完全显示出来的解决办法是将DataGridView的RowHeadersWidthSizeMode属性设置为AutoSizeToAllHeaders、AutoSizeToDisplayedHeaders或者AutoSizeToFirstHeader。
        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);   //方法 1 
            }
            catch (Exception flashERR)
            {
                MessageBox.Show("dgv1刷新行号异常 ,提示:" + flashERR.Message);
                
            }
        }


        //dataGridView1_RowStateChanged查询刷新表格行号
        private void dataGridView1_RowStateChanged_1(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                //RowHeadersWidthSizeMode = AutoSizeToAllHeaders
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);   //1
            }
            catch (Exception flashERR)
            {
                MessageBox.Show("dgv2刷新行号异常 ,提示:" + flashERR.Message);
                
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
