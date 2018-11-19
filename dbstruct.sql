/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50547
Source Host           : localhost:3306
Source Database       : db2

Target Server Type    : MYSQL
Target Server Version : 50547
File Encoding         : 65001

Date: 2018-11-19 14:55:18
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for s_goods
-- ----------------------------
DROP TABLE IF EXISTS `s_goods`;
CREATE TABLE `s_goods` (
  `goodsid` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'id',
  `goodsno` varchar(10) NOT NULL COMMENT '商品编号',
  `goods_name` varchar(255) NOT NULL COMMENT '商品名称,例:E-8000001',
  `goods_stone` text NOT NULL,
  `comment` text COMMENT '备注',
  `add_time` datetime NOT NULL COMMENT '添加时间',
  `add_user` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`goodsid`),
  KEY `gname` (`goods_name`) USING BTREE,
  KEY `add_time` (`add_time`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6086 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for s_goodslog
-- ----------------------------
DROP TABLE IF EXISTS `s_goodslog`;
CREATE TABLE `s_goodslog` (
  `goodsid` int(10) unsigned NOT NULL COMMENT '关联的商品id',
  `goods_name` varchar(255) NOT NULL,
  `goods_stone` text NOT NULL COMMENT '商品石位参数信息',
  `comment` text,
  `add_time` datetime NOT NULL COMMENT '添加时间',
  `add_user` varchar(255) NOT NULL,
  KEY `goods_name` (`goods_name`) USING BTREE COMMENT 'goods_name 索引'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for s_group
-- ----------------------------
DROP TABLE IF EXISTS `s_group`;
CREATE TABLE `s_group` (
  `gid` int(11) unsigned NOT NULL AUTO_INCREMENT COMMENT '用户组id',
  `group_name` varchar(20) NOT NULL COMMENT '用户组名称',
  `comment` varchar(255) DEFAULT NULL COMMENT '备注',
  `sort` varchar(200) CHARACTER SET utf8 DEFAULT NULL,
  `addtime` datetime DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`gid`),
  UNIQUE KEY `group_name` (`group_name`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for s_log
-- ----------------------------
DROP TABLE IF EXISTS `s_log`;
CREATE TABLE `s_log` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `message` text,
  `user` varchar(255) DEFAULT NULL,
  `comment` text,
  `add_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

-- ----------------------------
-- Table structure for s_menu
-- ----------------------------
DROP TABLE IF EXISTS `s_menu`;
CREATE TABLE `s_menu` (
  `permission` varchar(255) CHARACTER SET gbk NOT NULL COMMENT '菜单元素',
  `group_name` varchar(255) CHARACTER SET gbk NOT NULL COMMENT '权限名称',
  `comment` varchar(255) CHARACTER SET gbk DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`group_name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for s_user
-- ----------------------------
DROP TABLE IF EXISTS `s_user`;
CREATE TABLE `s_user` (
  `uid` int(11) unsigned NOT NULL AUTO_INCREMENT COMMENT '用户id',
  `user` varchar(20) CHARACTER SET gbk NOT NULL COMMENT '用户名',
  `password` varchar(20) CHARACTER SET gbk NOT NULL COMMENT '用户密码',
  `addtime` datetime DEFAULT NULL COMMENT '添加时间',
  `ucomment` varchar(255) CHARACTER SET gbk DEFAULT NULL COMMENT '备注',
  `usergroup` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
