using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //mysql 

namespace stonemgr
{
    public partial class MainForm : Form
    {
   
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//启动居中
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Common.setName !="管理员")
                {
                    menuDisable();//2-1先隐藏菜单
                    loadMenu();//2-2后加载
                }
            }
            catch (Exception mainFormLoadERR)
            {
                MessageBox.Show("主窗体菜单加载异常"+mainFormLoadERR.Message);
            }
        }

        private void aboutmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"程序兼容说明 :
		指令集架构:IA-32, Itanium[2],x86-64
		客户端:XP SP3, Vista SP1, 7, 8, 8.1, 10
		服务器:	2003 SP2, 2003 R2 SP2, 2008, 2008 R2, 2012, 2012 R2, 2016
程序运行依赖文件:
		config.ini(文件名不可改)(必须)
		mysql.data.dll(文件名不可改)(必须)
		stonemgr.exe(可改名)(必须)
		说明.txt
		备注说明:以上三个文件需要放在同级目录");
        }

       //搜索石位信息
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchStone formB = (searchStone)Application.OpenForms["searchStone"];
            if (formB == null)
            {
                searchStone form = new searchStone();
                //form.FormBorderStyle = FormBorderStyle.None;
                //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                formB.WindowState = FormWindowState.Maximized;
            }
           
            //FormCollection collection = Application.OpenForms;
            //foreach (Form subWindow in collection)
            //{
            //    //MessageBox.Show(subWindow.Text.ToString());

            //    if ("搜索商品石位信息" == subWindow.Text.ToString())
            //    {
            //        MessageBox.Show(subWindow.Text.ToString());
            //        subWindow.WindowState = FormWindowState.Maximized;
            //        //this.panel1.Controls.Add(subWindow);
            //    }
            //    else
            //    {
                   
               // }
                //textBox1.Text += (subWindow.Name.ToString()) + "\r\n";
                //textBox1.Text += (subWindow.Text.ToString()) + "\r\n";
                //form.WindowState = showAll();
                //if (form.Visible == false)
                //    form.Visible = true;
         //   }
   
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //添加石位窗体
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addStone formB = (addStone)Application.OpenForms["addStone"];
            if (formB == null)
            {
                addStone form = new addStone();
                //form.FormBorderStyle = FormBorderStyle.None;
                //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                formB.WindowState = FormWindowState.Maximized;
            }
        }

        //显示所有窗体名称信息
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAll form = new showAll();
            form.Show();
            //FormCollection collection = Application.OpenForms;
            //foreach (Form form in collection)
            //{
            //    MessageBox.Show(form.ToString());
            //    //if (form.Visible == false)
            //    //    form.Visible = true;
            //}
        }


        private void testsqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                MessageBox.Show(Common.connStr);
                string sql = "SELECT * FROM `s_goods` where goodsno  ='1222233';";
                int result;
                MySqlConnection link = new MySqlConnection(Common.connStr);
                link.Open();
                MySqlCommand mycmd = new MySqlCommand(sql, link);

                MySqlDataReader myReader;
                myReader = mycmd.ExecuteReader();
                // Always call Read before accessing data.
                if (myReader.Read())//读取goodsid
                {
                   // return myReader.GetInt32(0);
                    MessageBox.Show(myReader.GetInt32(0).ToString());
                }
                else 
                {
                    MessageBox.Show("记录不存在");
                }
                //result = mycmd.ExecuteNonQuery();
                //id = Convert.ToInt32(mycmd.ExecuteScalar());
                link.Close();

                //return result;//返回影响行数
            }
            catch (MySqlException doErr)//
            {
                //return 0;
                //return doErr.Message + doErr.ErrorCode;
            }
        }

        private void devlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //aboutToolStripMenuItem.OwnerItem.
            if (this.devlogToolStripMenuItem.Text == "开发日志")
            {
                this.stoneToolStripMenuItem.Visible = false;
                //MessageBox.Show( menuStrip1.Items.Count.ToString());
                //MessageBox.Show("123");
            }
            else
            {
                //string str =<example>;
                MessageBox.Show("");

            }
            
            MessageBox.Show(@"2017-?-? 
1 , 输入货号 / 石位 / 备注过滤(货号:只保留数字 ,注入关键字过滤)

2 ,按钮提交处理
		--检测货号是否存在
			--不存在直接插入记录
		--存在
			--获取存在的goodsid
				----事务处理sql操作
					-- 将存在的goodsid 记录插入到goodslog表
					--根据goodsid 使用事务 update存在的信息
				--(事务状态检测)

3 , 添加 添加/更新二合一操作 (选中后, 对于已存在的数据更新并将旧数据放到log表)

4 ,依赖文件mysql.data.dll (缺少依赖/数据库用户无法连接会提示  instance xx )

5 , 石位信息 去除粘贴格式

6 ,  datagridview 行头字符显示乱码

7 , 上万行数据载入慢 , 使用datagridview1.datasource = dt ; 直接载入数据(循环载入太慢)

8 , 解决图片加载慢 ,先查库再导入图片

9 , 添加权限管理
				
	1,添加用户组
	2,添加用户,并分配用户组
	3,分配用户组权限
	4,用户登录自动获取权限内的操作菜单
10 ,2018-10-12 修复更新石位信息,没有更新更新者信息
"

                );
        }

        //石位列表窗体显示
        private void stoneListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stoneList formB = (stoneList)Application.OpenForms["stoneList"]; //搜索石位窗体
            if (formB == null)
            {
                stoneList form = new stoneList();
                //form.FormBorderStyle = FormBorderStyle.None;
                //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                formB.WindowState = FormWindowState.Maximized;
            }
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.1.1 release");
        }

        //添加用户窗体加载
        private void adduserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addUser adduser = (addUser)Application.OpenForms["adduser"]; //搜索窗体
            if (adduser == null)
            {
                addUser form = new addUser();
                //form.FormBorderStyle = FormBorderStyle.None;
                //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                adduser.WindowState = FormWindowState.Maximized;
            }
        }

        //用户组管理窗体加载
        private void groupmgrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            group gp = (group)Application.OpenForms["group"]; //搜索窗体
            if (gp == null)
            {
                group form = new group();
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                gp.WindowState = FormWindowState.Maximized;
            }

        }

        //权限窗体加载
        private void permissionmgrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auth permission = (auth)Application.OpenForms["auth"]; //搜索窗体
            if (permission == null)
            {
                auth form = new auth();
                form.TopLevel = false;//指示子窗体非顶级窗体
                this.panel1.Controls.Add(form);//将子窗体载入panel
                form.Show();
            }
            else
            {
                permission.WindowState = FormWindowState.Maximized;
            }
        }

        private void loadMenu() 
        {
            try
            {
                 string permission ="";
                //获取当前用户的用户组信息
                 string sql = " SELECT permission FROM `s_user` LEFT JOIN `s_menu` on `s_user`.usergroup = `s_menu`.group_name WHERE  user ='" + Common.setName + "' LIMIT 1;";

                DataTable dt = Common.getData(sql);
                if (dt.Rows.Count == 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        permission = dt.Rows[i]["permission"].ToString();
                    }
                    //MessageBox.Show(permission);
                    string[] arr = permission.Split(',');

                    for (int j = 0; j < arr.Length; j++)//动态显示权限菜单
                    {
                        //richTextBox1.Text += arr[j];
                        //int key;
                        //key = Convert.ToInt32(arr[j]);
                        switch (arr[j])
                        {
                            case "0": searchToolStripMenuItem.Visible = true; break;
                            case "1": addToolStripMenuItem.Visible = true; break;
                            case "2": stoneListToolStripMenuItem.Visible = true; break;
                            case "3": adduserToolStripMenuItem.Visible = true; break;
                            case "4": groupmgrToolStripMenuItem.Visible = true; break;
                            case "5": permissionmgrToolStripMenuItem.Visible = true; break;
                        }
                    }

                }

             }
            catch (Exception menuERR)
            {

                MessageBox.Show("加载菜单信息错误提示" + menuERR.Message);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本软件\r\nCopyright 秀恩秀首饰有限公司 © 2016-2019  。仅限内部使用\r\n著作权所有者为\"GA17\"。\r\n");
        }

        //窗体加载后,隐藏没有权限的6个菜单
        private void menuDisable() 
        {
            searchToolStripMenuItem.Visible = false;
            addToolStripMenuItem.Visible = false;
            stoneListToolStripMenuItem.Visible = false;
            adduserToolStripMenuItem.Visible = false;
            groupmgrToolStripMenuItem.Visible = false;
            permissionmgrToolStripMenuItem.Visible = false;
            permissionmgrToolStripMenuItem.Visible = false;

        }

    }
}
