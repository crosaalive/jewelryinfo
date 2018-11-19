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
using System.Text.RegularExpressions; //

namespace stonemgr
{
    class Common
    {
        private static string name = "";//用户名
        public static string connStr = "";//登入保存连接信息
        public static string picPath = "";//图片路径
        public static string picIp = "";//图片路径
        //private static string[] userList = null;//用户列表
        public static string setName
        {
            set { name = value; }
            get { return name; }
        }

        public static string conn
        {
            set { connStr = value; }
            get { return connStr; }
        }

        public static string picturePath
        {
            set { picPath = value; }
            get { return picPath; }
        }

        public static string pictureIp
        {
            set { picIp = value; }
            get { return picIp; }
        }

        public string testConnect(string str)
        {
            try
            {
                //MessageBox.Show(connStr);
                MySqlConnection mycon = new MySqlConnection(connStr);
                mycon.Open();
                mycon.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        return ("Cannot connect to server.  Contact administrator " + ex.Number);
                    case 1045:
                        return ("Invalid username/password, please try again " + ex.Number);
                    default: return ("unknow err" + ex.Number);
                }
            }
            return ("true");
        }

        //返回用户名列表
        public string[] getUserList()
        {
            string[] errInfo = null;//错误信息保存
            List<string> uList = new List<string>();//用户列表保存至泛型
            try
            {     //先测试数据库连接
                MySqlConnection mycon = new MySqlConnection(connStr);
                mycon.Open();
                mycon.Close();
            }
            catch (MySqlException ex2)
            {
                errInfo[0] = ("数据库连接失败 : 错误代码 " + ex2.Number);
                return errInfo;
            }

            //获取用户信息
            try
            {
                MySqlConnection mycon2 = new MySqlConnection(connStr);
                mycon2.Open();
                MySqlCommand mycmd = new MySqlCommand("SELECT `user` FROM `s_user`;", mycon2);
                MySqlDataReader reader = mycmd.ExecuteReader();//读取查询记录
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        uList.Add(reader.GetString(0)); //用户装箱  
                    }
                }
                string[] userlist = uList.ToArray();
                return userlist;//返回用户列表数组

            }
            catch (MySqlException ex3)
            {
                errInfo[0] = "获取用户列表失败 : 错误代码 " + ex3.Number;
                return errInfo;
            }
        }
        //执行sql,Update、Insert和Delete语句，返回值为该命令所影响的行数。
        public int doSql(string sql)
        {
            int result;
            try
            {
                MySqlConnection link = new MySqlConnection(connStr);
                link.Open();
                MySqlCommand mycmd = new MySqlCommand(sql, link);
                result = mycmd.ExecuteNonQuery();
                //id = Convert.ToInt32(mycmd.ExecuteScalar());   
                link.Close();
                return result;//返回影响行数
            }
            catch (MySqlException doErr)//
            {
                return 0;
                //return doErr.Message + doErr.ErrorCode;
            }
        }

        //查询goodsname , 存在返回1 ,不存在返回0
        public  int checkExist(string sql)
        {
            try
            {
                //int num = 0;
                MySqlConnection link2 = new MySqlConnection(connStr);
                link2.Open();
                MySqlCommand cmd = new MySqlCommand(sql, link2);
                //MessageBox.Show(cmd.ExecuteScalar().ToString());
                //string res = cmd.ExecuteScalar().ToString();

                MySqlDataReader reader = cmd.ExecuteReader();//读取查询记录
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                       // MessageBox.Show(reader.GetString(0)); //装箱  
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                link2.Close();
                return 0;   
                
            }
            catch (MySqlException checkErr)//
            {
                return 0;
            }
        }

        //返回用户列表数组
        public string[] addToList(string sql)
        {
            List<string> resultList = new List<string>();//泛型
            string[] result = null;
            try
            {
                MySqlConnection link2 = new MySqlConnection(connStr);
                link2.Open();
                MySqlCommand cmd = new MySqlCommand(sql, link2);
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                link2.Close();
                MySqlDataReader reader = cmd.ExecuteReader();//读取查询记录
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        resultList.Add(reader.GetString(0)); //

                    }
                }
                string[] userlist = resultList.ToArray();
                return userlist;//返回用户列表数组
            }
            catch (MySqlException checkErr)//
            {
                result[0] = "false";
                return result;
            }
        }


        //过滤非法sql字符
        public static string filterSqlStr(string Str)
        {

            Str = Str.Replace("'", "");
            Str = Str.Replace("\"", "");

            Str = Str.Replace("\'", "");
            Str = Str.Replace("\\", "");

            Str = Str.Replace("&", "&amp");
            Str = Str.Replace("<", "&lt");
            Str = Str.Replace(">", "&gt");
            Str = Str.Replace("delete", "");
            Str = Str.Replace("update", "");
            Str = Str.Replace("insert", "");
            Str = Str.Replace("drop", "");
            Str = Str.Replace("dump", "");
            return Str;
        }

        //事务处理插入log /更新记录 ,1更新/插入log成功, 0更新/插入log失败
        public static int sqlTrans(string log, string update)
        {
            MySqlConnection link3 = new MySqlConnection(connStr);
            link3.Open();
            MySqlCommand myCommand = link3.CreateCommand();
            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = link3.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = link3;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = log;//log sql
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = update;//update sql
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                link3.Close();
                return 1;
                //Console.WriteLine("Both records are written to database.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        MessageBox.Show("An exception of type " + ex.GetType() +
                         " was encountered while attempting to roll back the transaction.");
                    }
                }
            return 0;
            }
        }

        public static string RemoveNotNumber(string key)
        {
            return Regex.Replace(key, @"[^\d]*", "");
        }


        //返回6位流水号
        public static string goodsNum(string str)
        {
            int len = str.ToString().Length;
            string num = "";
            if (len > 0)
            {
                switch (len)
                { //根据货号长度前置补0
                    case 1:
                        num = "00000" + str;
                        break;
                    case 2:
                        num = "0000" + str;
                        break;
                    case 3:
                        num = "000" + str;
                        break;
                    case 4:
                        num = "00" + str;
                        break;
                    case 5:
                        num = "0" + str;
                        break;
                    case 6:
                        num = str;
                        break;
                    //default:
                    //    die("货号长度输入错误!,请返回检查 !!!");
                    //break;
                }
                return num;
            }

            else { return ""; }
        }

        //返回货号前缀
        public static string strPrefix(string pre)
        {
            if (pre.Length > 0)
            {
                switch (pre.Substring(0, 1))
                {
                    case "1":
                        pre = "N-1";
                        break;
                    case "2":
                        pre = "HP-2";
                        break;
                    case "3":
                        pre = "B-3";
                        break;
                    case "4":
                        pre = "V-4";
                        break;
                    case "5":
                        pre = "P-5";
                        break;
                    case "6":
                        pre = "R-6";
                        break;
                    case "7":
                        pre = "H-7";
                        break;
                    case "8":
                        pre = "E-8";
                        break;
                    case "9":
                        pre = "S-9";
                        break;
                }
                return pre;
            }
            else
            {
                return "";
            }
        }

        //返回货号前缀 ,示例 E-
        public static string strPrefix2(string pre)
        {
            if (pre.Length > 0)
            {
                switch (pre.Substring(0, 1))
                {
                    case "1":
                        pre = "N-";
                        break;
                    case "2":
                        pre = "HP-";
                        break;
                    case "3":
                        pre = "B-";
                        break;
                    case "4":
                        pre = "V-";
                        break;
                    case "5":
                        pre = "P-";
                        break;
                    case "6":
                        pre = "R-";
                        break;
                    case "7":
                        pre = "H-";
                        break;
                    case "8":
                        pre = "E-";
                        break;
                    case "9":
                        pre = "S-";
                        break;
                }
                return pre;
            }
            else
            {
                return "";
            }
        }


        //查询goodsname石位信息,并返回数组列表
        public static DataTable getStoneInfo(string sql)
        {
            DataTable stoneInfo = null;
            try
            {
                MySqlConnection link2 = new MySqlConnection(connStr);
                link2.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, link2);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
            catch (MySqlException stongeERR)//
            {
                MessageBox.Show("获取石位信息列表错误,错误信息" + stongeERR.Message + "\r\n 点击确定继续使用");
                return stoneInfo;
            }
        }

        //执行查询 , 返回表()
        public static DataTable getData(string sql)
        {
            DataTable stoneInfo = null;
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
            catch (MySqlException stongeERR)//
            {
                MessageBox.Show("getdata 错误信息"+ stongeERR.Message+ "\r\n 点击确定继续使用");
                return stoneInfo;
            }
        }

        //过滤非 数字/字母/.   字符
        public static string fillterString(string key)
        {
            return Regex.Replace(key, @"[^\da-zA-Z\.\t]*", "");
        }

        public static void showERR(string str)
        {
            MessageBox.Show(str);
        }

    }
}
