using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;

namespace stonemgr
{
    public partial class addStone : Form
    {
        string user = Common.setName;//标记登录用户
        public addStone()
        {
            InitializeComponent();
        }

        private void addStone_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; 
            label4.Text = user;
            button1.Enabled = false;
            richTextBox4.Focus();
            label9.Text = goodsName();
            //2018-10-12 18:22:08
            //richTextBox3.Text = Common.setName;
            //addStone.MdiParent = this;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result,no;
                string stoneInfo = "", comment = "", editUser = "", addTime = "", insertSql = "", checksql = "", updateSql = "", goodsNoLog = "", gName = "", goodsno ="";
                goodsno = richTextBox4.Text;//约7位数的商品编号,输入强制转换
                stoneInfo = Common.filterSqlStr(richTextBox1.Text);//石位信息,过滤注入
                gName = goodsName();//商品名称 ,示例 N-1000001
               
                //comment = richTextBox2.Rtf.Replace("\\", "\\\\");//备注
                //comment = Common.filterSqlStr(comment.Replace("\\'", "\\\'")); //过滤注入
                comment = richTextBox2.Text.Replace("\\", "\\\\");//备注转义
                //comment = richTextBox2.Rtf.Replace("\\", "\\\\");//备注转义
                comment = @Common.filterSqlStr(comment.Replace(@"'", @"\'"));//分号转义过滤
                //comment = comment.ToString();
                //richTextBox5.Text = comment;

                editUser = Common.setName;//用户
                addTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second).ToString(); //添加时间
                //richTextBox3.Text = goodsno + "\r\n" + stoneInfo + "\r\n" + comment + "\r\n" + editUser + "\r\n" + addTime + "\r\n";

                insertSql = "INSERT INTO `s_goods` ( `goodsno`, `goods_stone`,`goods_name`, `add_time`, `comment`,`add_user`) VALUES ( '" + goodsno + "', '" + stoneInfo + "', '" + gName + "', '" + addTime + "','" + @comment + "','"+ user +"');";
                    
                   // "update `stonedb`.`s_goods` ( `goodsno`, `goods_stone`,`goods_name`, `add_time`, `comment`) VALUES ( '" + goodsno + "', '" + stoneInfo + "', '" + goodsno + "', '" + addTime + "','" + comment + "');";
                checksql = "SELECT `goods_name` FROM `s_goods` WHERE 1 =1 AND goods_name ='" + gName + "';";

                richTextBox3.Text = insertSql;
                richTextBox3.Text += checksql+"\r\n";
                Common c2 = new Common();
                no = c2.checkExist(checksql);//检查输入是否存在,存在返插入id,否则返回0
                if (no == 0)//
                {
                    result = c2.doSql(insertSql);//不存在插入记录
                    if (result != 0) //插入成功后输出到datagridview1中显示
                    {
                        //dataGridView1.Rows.Add(1);
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells[0].Value = gName;
                        this.dataGridView1.Rows[index].Cells[1].Value = stoneInfo;
                        this.dataGridView1.Rows[index].Cells[2].Value = comment;
                        this.dataGridView1.Rows[index].Cells[3].Value = addTime;
                        this.dataGridView1.Rows[index].Cells[4].Value = editUser;
                        dataGridView1.Rows[index].Selected = true; //选中
                        dataGridView1.FirstDisplayedScrollingRowIndex = index;
                        label9.Text = gName + "已保存!!"; //保存后清空输入
                        richTextBox1.Text = "";
                        richTextBox2.Text = "";
                        richTextBox4.Text = "";
                    }
                }
                else
                { //存在则先存log再更新
                    if (checkBox1.Checked)
                    {
                        //2018-10-12 修复更新者信息
                        goodsNoLog = "INSERT INTO `s_goodslog` (goodsid,goods_name,goods_stone,comment,add_time,add_user) SELECT goodsid,goods_name,goods_stone,comment,add_time,add_user FROM `s_goods`WHERE goods_name = '" + gName + "' ;";
                        updateSql = " UPDATE `s_goods` SET `goodsno`='" + goodsno + "', `goods_stone`='" + stoneInfo + "',  `goods_name`='" + gName + "', `comment`='" + comment + "', `add_user`='" + editUser + "', `add_time`='" + addTime + "' WHERE (`goods_name`='" + gName + "'); ";
                        int transRersult = Common.sqlTrans(goodsNoLog, updateSql);//执行事务处理
                        if (transRersult == 1)
                        {
                            int index = this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[index].Cells[0].Value = gName;
                            this.dataGridView1.Rows[index].Cells[1].Value = stoneInfo;
                            this.dataGridView1.Rows[index].Cells[2].Value = comment;
                            this.dataGridView1.Rows[index].Cells[3].Value = addTime;
                            this.dataGridView1.Rows[index].Cells[4].Value = editUser;
                            dataGridView1.Rows[index].Selected = true; //选中
                            dataGridView1.FirstDisplayedScrollingRowIndex = index;
                            richTextBox1.Text = "";
                            richTextBox2.Text = "";
                            richTextBox4.Text = "";
                            label9.Text = gName + "已保存!!";
                        }
                        else 
                        {
                            label9.Text = gName+ "数据添加失败,请编辑后重新提交!";  
                        }
                    }
                }
            }
            catch (Exception addEr)
            {
                MessageBox.Show( "保存异常,异常提示:"+addEr.Message.ToString()); //弹出错误提示
            }
        }

        #region 显示行号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush brushOne = new SolidBrush(Color.Red);//定义一个颜色为红色的画刷
            //绘制行号
            e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, brushOne, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        #endregion

        //显示行号
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);   //方法 1 
            }
            catch (Exception dataGridView1_RowStateChangedERR )
            {
                MessageBox.Show("添加刷新行号错误  "+ dataGridView1_RowStateChangedERR);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isEmpty();
        }


        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //自动填充商品编号到备注处
                if (checkBox2.Checked)
                {
                    richTextBox2.Text = richTextBox4.Text;
                }
                commonCheck();
                isEmpty();
                
            }
            catch (Exception richTextBox4ERR)
            {
                MessageBox.Show("异常错误信息" + richTextBox4ERR.Message);
            }

        }

        //公共检测编号录入状态 / 批量录入 的保存按钮状态
        public void commonCheck()
        {
            try
            {
                
                string sql = "", gName = "";
                int no;
                label8.Text = goodsName();
                gName = label8.Text;

                //Thread t = new Thread(new ParameterizedThreadStart(showImages));
                //t.Start(gName);
                
                 //Thread th = new  showImages(gName);
                
                sql = "SELECT `goods_name` FROM `s_goods` WHERE  1= 1 AND  goods_name = '" + gName + "';";
                Common check = new Common();
                no = check.checkExist(sql);
                textBox2.Text = no.ToString();

                if (no > 0)
                {
                    //richTextBox4.Text = richTextBox4.Text + "_";
                    label6.Text = "编号数据已存在";
                    if (checkBox1.Checked) //批量录入/更新模式按钮状态 切换
                    {
                        button1.Enabled = true;
                    }
                    else
                    {
                        
                        button1.Enabled = false;
                    }
                }
                else
                {
                    isEmpty();
                    //button1.Enabled = true;
                    label6.Text = "";
                }
                showImages(gName);
            }
            catch (Exception testERR)
            {
                MessageBox.Show("commonCheck exception :" + testERR.Source + testERR.Message);
            }

        }

        //返回商品名称 例N-4000001 或空值
        public string goodsName()
        {
            try
            {
                richTextBox4.Text = Common.RemoveNotNumber(richTextBox4.Text);
                richTextBox4.SelectionStart = richTextBox4.Text.Length + 1; //过滤字符后修改光标位置状态
                if (richTextBox4.Text != "")
                {
                    return Common.strPrefix(richTextBox4.Text) + Common.goodsNum(richTextBox4.Text.Substring(1));
                }
                else { return " "; }
            }
            catch (Exception)
            {

                //MessageBox.Show("输入检测异常");
                return "";
            }

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            string comment = richTextBox2.Rtf.Replace("\\", "\\\\");//备注
            //comment = Common.filterSqlStr(comment.Replace("\'", "\\\'")); //过滤注入
            richTextBox5.Text = comment.ToString();
            if (checkBox3.Checked)
            {
                richTextBox1.Text = richTextBox2.Text;
                //richTextBox1.Paste(DataFormats.GetFormat(DataFormats.Text));
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            ToolTip removeInput = new ToolTip();
            removeInput.InitialDelay = 200;
            removeInput.ReshowDelay = 300;
            removeInput.ShowAlways = true;
            removeInput.IsBalloon = true;
            removeInput.SetToolTip(this.button2, "清空文本框录入的内容,方便重新录入新的内容,本操作不会删除已保存的数据");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                isEmpty();
                button1.Text = "保存/更新";
            }
            else {
                button1.Text = "保存";
            }
            //commonCheck();
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip removeInput = new ToolTip();
            removeInput.InitialDelay = 200;
            removeInput.ReshowDelay = 300;
            removeInput.ShowAlways = true;
            removeInput.IsBalloon = true;
            removeInput.SetToolTip(this.checkBox1, "未选中状态: 对于已存在的货号,无法保存\r\n选中状态: 保存本次录入的数据,对于已存在的数据会另存在日志记录中并更新当前货号石位信息,方便查看修改记录");
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox4.Text = "";
        }

        //检查石位信息是否填写,修改保存按钮状态
        private void isEmpty()
        {
            if (richTextBox1.Text == "" || richTextBox4.Text.Length<2)
            {
                button1.Enabled = false;
            }
            else {
                button1.Enabled = true;
            }
        }

        //显示图片,复制图片到剪贴板
        private void showImages(string gName)
        {
            try
            {
                //Ping ping = new Ping();
                //PingReply pingReply = ping.Send(Common.picIp, 3000);//800毫秒超时
                //if (pingReply.Status == IPStatus.Success)
                //{
                string pic = Common.picPath + "\\" + gName + ".jpg";
                    if (File.Exists(pic))
                    {
                        label10.Text = "";
                        label10.Enabled = false;
                        //Image img = Image.FromFile(pic);
                        //Clipboard.SetImage(img);
                        //Clipboard.SetDataObject(Image.FromFile(pic) + ".jpg", true);
                        pictureBox1.Load(Common.picPath + "\\" + gName + ".jpg");
                    }
                    else
                    {
                        label10.Enabled = true;
                        label10.Text = "图片文件不存在";
                        pictureBox1.Image = null;
                    }
                //}
                //else //超时提示
                //{
                //    label10.Enabled = true;
                //    label10.Text = "图片打开超时,检查图片配置";
                //    pictureBox1.Image = null;
                //}

            }
            catch (Exception showImagesERR)
            {
                label10.Text ="错误提示"+ showImagesERR.Message; 
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tips = new ToolTip();
            tips.InitialDelay = 800;
            tips.ReshowDelay = 300;
            tips.ShowAlways = true;
            tips.IsBalloon = true;
            tips.SetToolTip(this.pictureBox1, "图片会自动复制到剪贴板");
        }

        //更新行号
        private void dataGridView1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = this.dataGridView1.Rows[i];
                //r.Row.HeaderCell.Value = e.Row.Index + 1;
                //r.HeaderCell.Value =  i + 1;
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.dataGridView1.Refresh();
        }

        //输入数据清除格式
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (checkBox2.Checked)
                {
                    richTextBox2.Text = richTextBox1.Text;
                }
                e.SuppressKeyPress = true;
                richTextBox1.Paste(DataFormats.GetFormat(DataFormats.Text));
            }
        }

        //输入数据清除格式
        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.V)
            //{
            //    e.SuppressKeyPress = true;
            //    richTextBox2.Paste(DataFormats.GetFormat(DataFormats.Text));
            //}
        }

        //自动补全备注为石位信息
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        //测试rtf互转  r3 r5 测试后隐藏
        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {
            //richTextBox3.Rtf = richTextBox5.Text;
        }

        private void checkBox3_MouseHover(object sender, EventArgs e)
        {
            ToolTip removeInput = new ToolTip();
            removeInput.InitialDelay = 200;
            removeInput.ReshowDelay = 300;
            removeInput.ShowAlways = true;
            removeInput.IsBalloon = true;
            removeInput.SetToolTip(this.checkBox3, "快速模式说明: 复制带格式的石位信息到备注栏 \r\n 自动将备注栏带格式的文字添加到石位信息栏,减少复制次数 ");
        }
    }
}
