# jewelryinfo
c# mysql


石位管理系统:文件说明

此三个文件是必须,且放在同级目录,(文件可以放在一个文件夹内, 文件夹最好不要包含中文,使用英文路径!!)

Mysql.data.dll 数据连接库
config.ini配置文件
石位管理系统.exe   程序

如图
 

数据库存放目录

D:\phpStudy\MySQL\data\stonedb

1 数据备份还原操作说明
1) , 备份 还原操作 请使用该软件(navicat)
 

2) 备份操作 ,选中stonedb, 转储sql文件->结构和数据


 


2  Config.ini文件说明

[config]
;数据库地址
server=192.168.1.95
;数据库名称
DataBase=stonedb
;数据库用户
UserId=user
;数据库密码
pwd=user
;图片文件夹
picPath=\\192.168.1.90\images










3数据库用户及权限管理
数据库账号 

普通用户user 密user (拥有极少权限,即select,update,insert,可操作数据库 db2/stonedb)

3-1添加用户,点击用户  输入
用户名
密码
主机  %    (备注%代表局域网内可访问连接数据库)
保存
 
















3-2配置用户权限
	用户->选中用户-编辑用户
 

3-3 添加权限 选中数据库  ->选中select insert update 权限 确定即可
			
 


 



4 , 

问题:为何要新建一个低权限的用户?(仅仅拥有对stonedb数据库 insert select update的权限)
由于配置文件可被人查看,故创建一个低权限防止数据库被其他用户恶意删除!!
减低数据丢失风险

Mysql数据库开启binlog
