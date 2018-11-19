using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//ini file need
using System.Runtime.InteropServices;//ini file need

using MySql.Data;//mysql 
using MySql.Data.MySqlClient; //mysql 


namespace stonemgr
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//如何启动居中
        }

        public string str = "";//该变量保存INI文件所在的具体物理位置
        public string strOne = "";//区域内容
        public string con = "";
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string server, database, user, pwd ,picPath,picIp;
                str = Application.StartupPath + "\\config.ini";
                // other method  str = System.AppDomain.CurrentDomain.BaseDirectory + @"config.ini";
                strOne = System.IO.Path.GetFileNameWithoutExtension(str);
                textBox2.Text = strOne.ToString();
                if (File.Exists(str))
                {
                    server = ContentReader(strOne, "server", "");
                    database = ContentReader(strOne, "DataBase", "");
                    user = ContentReader(strOne, "UserId", "");
                    pwd = ContentReader(strOne, "Pwd", "");
                    con = "server=" + server + ";UserId=" + user + ";pwd=" + pwd + ";DataBase=" + database + ";";
                    picPath = ContentReader(strOne, "picPath", "");
                    picIp = ContentReader(strOne, "picIp", "");
                    Common.picIp = picIp;
                    Common.picturePath = picPath;
                    Common.conn = con;//保存连接信息

                }
                Common c1 = new Common();
                string[] result = c1.getUserList();//获取用户载入combox
                for (int i = 0; i < result.Count(); i++)
                {
                    comboBox1.Items.Add(result[i]);
                }
                comboBox1.SelectedIndex = 0;

                string status = c1.testConnect(con);//test connect..
                if (status != "true")//检测数据库连接状态
                {
                    MessageBox.Show(status);
                    this.Close();
                }
            }
            catch (Exception loginForm)
            {

                MessageBox.Show("登录初始化失异常 :" + loginForm.Message + "\r\n提示 object  reference not set to an instance of an object ,\r\n解决方法 ,1 添加依赖文件mysql.data.dll到程序运行目录,2  确定数据库用户  host 的权限为 %,即所有地址可访问");
            }
        }

        //内置读取ini文件
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        //获取配置文件内容
        public string ContentReader(string area, string key, string def)
        {
            StringBuilder stringBuilder = new StringBuilder(1024);
            GetPrivateProfileString(area, key, def, stringBuilder, 1024, str);
            return stringBuilder.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = "",pwd="";
                username = this.comboBox1.Text;
                Common c1 = new Common();
                MySqlConnection mycon2 = new MySqlConnection (Common.conn);
                mycon2.Open();
                //MessageBox.Show("SELECT `password` FROM `s_user`  where 1=1 and user = '" + username + "';");
                MySqlCommand mycmd = new MySqlCommand("SELECT `password` FROM `s_user`  where 1=1 and user = '"+username + "';", mycon2);
                MySqlDataReader reader = mycmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        pwd = reader.GetString(0); 
                    }
                }
                if (pwd == textBox1.Text) //验证登录跳转
                {  
                    Common.setName = username; //保存登录用户
                    this.DialogResult = DialogResult.OK;    //returan status  and load main form
                    this.Close();    //close login window
                }
            }

            catch (Exception err)
            {
                MessageBox.Show("错误信息: "+err.Message);//catch login err
            }      
        }

        private void insertSql() { 
            
        }

        private void selectSql()
        {

        }

        private void updateSql()
        {

        }



        //泛型测试2017-9-23 15:50:33

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> res = new List<string>();
            for (int j = 0; j < 10; j++)
            {
                res.Add("123" + j);
                MessageBox.Show(res[j]);

            }
            MessageBox.Show(res.Count().ToString());
            for (int i = res.Count()-1; i >0 ; i--)
            {
                MessageBox.Show(res[i]+"___\r\n");
            }

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
               
            }
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //     MessageBox.Show (this.comboBox1.Text);
        //     MessageBox.Show(this);
        //}

        //private string[] getUserList(string[] str)
        //{
        //    string[] info=null;
        //    try
        //    {
        //        //MessageBox.Show(str);
        //        MySqlConnection mycon = new MySqlConnection(con);
        //        mycon.Open();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        switch (ex.Number)
        //        {
        //            case 0:
        //                info[0] =("Cannot connect to server.  Contact administrator " + ex.Number);break;
        //            case 1045:
        //                info[1] =("Invalid username/password, please try again " + ex.Number);break;
        //            default: info[2] = ("error code " + ex.Number); break;
        //        }

        //    }

        //    return str;
        //}
    }
}
