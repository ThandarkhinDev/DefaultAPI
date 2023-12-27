-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: sampledb
-- ------------------------------------------------------
-- Server version	5.7.11-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_admin`
--

DROP TABLE IF EXISTS `tbl_admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_admin` (
  `AdminID` int(11) NOT NULL AUTO_INCREMENT,
  `AdminLevelID` int(11) NOT NULL,
  `AdminName` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `LoginName` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `address` text COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `nrc` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `state` bigint(20) NOT NULL,
  `salt` text COLLATE utf8_unicode_ci NOT NULL,
  `ImagePath` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `login_fail_count` int(11) NOT NULL,
  `access_status` tinyint(2) NOT NULL COMMENT '0 - access allow, 1 - Inactive, 2 - Blocked',
  `Email` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`AdminID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_admin`
--

LOCK TABLES `tbl_admin` WRITE;
/*!40000 ALTER TABLE `tbl_admin` DISABLE KEYS */;
INSERT INTO `tbl_admin` VALUES (1,1,'Admin','admin','wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt','','','',1,'/SApKtKXpIa6YnHCjKLxQJAeb279BlX8','1.png',0,0,'thandalay@gmail.com','2018-08-24 16:11:14','2017-11-07 03:38:35'),(2,2,'naing','naing','GuoyIxUncxmk0VTdOsl2YI3+yYV4IA4S','yangon','grgr','grg',1,'xvDZB+tGOCfx6JChprk/SwW+LzHwQZd+',NULL,2,0,'gr@gmai.com','2018-07-25 07:58:05','2018-07-25 07:58:05'),(7,2,'HEllo','ee','qe69g7hiYOOgXJACdMgh6205NRAYWbFR','yangon','ee','12',1,'2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q',NULL,0,0,'permissionday@gmail.com','2018-07-29 11:33:17','2018-07-28 05:14:58'),(9,2,'1','f','kH6SdJaEY2ILSYb3xAcwqOHe8lcNq5js','f','f','f',1,'m+uzlOCmVpzC1fdhHalLigMZhqL1vbCE',NULL,0,0,'g@gmail.com','2018-07-28 06:19:53','2018-07-28 06:19:53'),(10,2,'ef','fefe','uTeU5RbxEfzEyzxOKiPiujwaELwJxKxR','efe','fef','fefe',1,'5ieZTWd8bOK2a8GNJIjSYFflnEEMRrRh',NULL,0,0,'g@gmail.com','2018-07-28 06:22:18','2018-07-28 06:22:18'),(11,2,'grgerg','gregeger','K2m72ZYCA2KVy95tObyFZvqee0PXXdYe','gegr','gerge','gergeg',1,'Z0ltruMywwl5EMfNufpghAv2OxNKDTKG',NULL,0,0,'fewfi@gwijw.com','2018-07-28 06:23:13','2018-07-28 06:23:13'),(12,2,'bregg','grg','lN9QMvmsdqxsKjaBnr2o41q2bUqFCi03','greger','grege','gerger',1,'lb2wKlTO+moF/Ix5cMdNWs7chB9bH4Bs',NULL,0,0,'gr@gegi.com','2018-07-28 06:23:59','2018-07-28 06:23:59');
/*!40000 ALTER TABLE `tbl_admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_adminlevel`
--

DROP TABLE IF EXISTS `tbl_adminlevel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_adminlevel` (
  `AdminLevelID` int(11) NOT NULL AUTO_INCREMENT,
  `AdminLevel` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `restricted_iplist` text COLLATE utf8_unicode_ci NOT NULL,
  `Description` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `Remark` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `IsAdministrator` tinyint(1) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`AdminLevelID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_adminlevel`
--

LOCK TABLES `tbl_adminlevel` WRITE;
/*!40000 ALTER TABLE `tbl_adminlevel` DISABLE KEYS */;
INSERT INTO `tbl_adminlevel` VALUES (1,'Administrator','','Admin','can access all function',1,'0000-00-00 00:00:00','2018-01-19 05:12:47'),(2,'test','','test','testkk',0,'2018-07-25 10:06:02','2018-07-27 12:30:39');
/*!40000 ALTER TABLE `tbl_adminlevel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_adminlevelmenu`
--

DROP TABLE IF EXISTS `tbl_adminlevelmenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_adminlevelmenu` (
  `AdminLevelID` int(11) NOT NULL,
  `AdminMenuID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_adminlevelmenu`
--

LOCK TABLES `tbl_adminlevelmenu` WRITE;
/*!40000 ALTER TABLE `tbl_adminlevelmenu` DISABLE KEYS */;
INSERT INTO `tbl_adminlevelmenu` VALUES (2,2),(2,8),(2,7),(2,6),(2,1),(2,4),(2,0),(2,5),(2,9),(2,10),(2,11),(3,11),(3,10),(3,9),(3,5),(3,8),(3,7),(3,6),(3,1),(3,4),(3,0);
/*!40000 ALTER TABLE `tbl_adminlevelmenu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_adminmenu`
--

DROP TABLE IF EXISTS `tbl_adminmenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_adminmenu` (
  `AdminMenuID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL,
  `AdminMenuName` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `SrNo` int(11) NOT NULL,
  `ControllerName` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `Icon` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`AdminMenuID`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_adminmenu`
--

LOCK TABLES `tbl_adminmenu` WRITE;
/*!40000 ALTER TABLE `tbl_adminmenu` DISABLE KEYS */;
INSERT INTO `tbl_adminmenu` VALUES (1,0,'Admin Management',1,'#','glyphicon glyphicon-user icon text-info-lter'),(2,0,'Setup',2,'#','glyphicon glyphicon-cog icon text-info-lter'),(4,1,'Admin Level',101,'apps.adminlevel',''),(5,1,'Admin',102,'apps.admin',''),(6,4,'list',1001,'#',''),(7,4,'edit',1002,'#',''),(8,4,'delete',1003,'#',''),(9,5,'list',1001,'#',''),(10,5,'edit',1002,'#',''),(11,5,'delete',1003,'#',''),(12,0,'Report',4,'#','glyphicons glyphicons-list-alt text-success-lt'),(40,0,'Transaction',3,'#','glyphicons glyphicons-adjust-alt text-success-lt');
/*!40000 ALTER TABLE `tbl_adminmenu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_adminmenudetails`
--

DROP TABLE IF EXISTS `tbl_adminmenudetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_adminmenudetails` (
  `MenuID` int(11) NOT NULL,
  `ControllerName` varchar(50) CHARACTER SET utf8 NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_adminmenudetails`
--

LOCK TABLES `tbl_adminmenudetails` WRITE;
/*!40000 ALTER TABLE `tbl_adminmenudetails` DISABLE KEYS */;
INSERT INTO `tbl_adminmenudetails` VALUES (10,'apps.admin/editAdmin');
/*!40000 ALTER TABLE `tbl_adminmenudetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_adminmenuurl`
--

DROP TABLE IF EXISTS `tbl_adminmenuurl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_adminmenuurl` (
  `AdminMenuID` int(11) NOT NULL,
  `ServiceUrl` varchar(200) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_adminmenuurl`
--

LOCK TABLES `tbl_adminmenuurl` WRITE;
/*!40000 ALTER TABLE `tbl_adminmenuurl` DISABLE KEYS */;
INSERT INTO `tbl_adminmenuurl` VALUES (4,'adminlevel/GetAdminLevel'),(4,'adminlevel/GetAdminLevelMenu'),(7,'adminlevel/AddAdminLevel'),(7,'adminlevel/AddAdminLevelMenu'),(8,'adminlevel/DeleteAdminLevel'),(5,'admin/GetAdminSetup'),(11,'admin/DeleteAdminSetup'),(10,'admin/AddAdminSetup'),(10,'admin/SaveImagePath'),(9,'admin/GetAdminSetup'),(10,'admin/GetAdminSetup'),(45,'general/GetGeneralTypeComboData'),(45,'general/GetGeneralType'),(47,'general/AddGeneral'),(13,'eventLog/GetEventLog'),(6,'adminlevel/GetAdminLevel'),(6,'adminlevel/GetAdminLevelMenu');
/*!40000 ALTER TABLE `tbl_adminmenuurl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_email_template`
--

DROP TABLE IF EXISTS `tbl_email_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_email_template` (
  `EmailTemplateID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `template_name` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `template_content` text COLLATE utf8_unicode_ci,
  `subject` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `from_email` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `variable` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`EmailTemplateID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_email_template`
--

LOCK TABLES `tbl_email_template` WRITE;
/*!40000 ALTER TABLE `tbl_email_template` DISABLE KEYS */;
INSERT INTO `tbl_email_template` VALUES (1,'Account Lock Notification','Dear [Account Name],\r \r We Found you are trying many times with account name: [Login Name]  and wrong password. So we lock your account.\r If you want to unlock your account, click <a href=\"[Unlock URL]\">here</a>.\r \r Sincerely,\r sample','Account Lock Notification','gwtojt@gmail.com','[Account Name],[Login Name],[Admin Email]','2018-07-29 06:05:31'),(2,'Forgot Password Notification','Dear [Account Name],\r \r You have requested that a new password be sent to your email address at [Account Email].\r \r To confirm reset password click <a href=\"[Reset URL]\">here</a>\r \r New Password: [Generate Password] \r \r Sincerely,\r Sample Mail','Forgot Password Notification','gwtojt@gmail.com','[Account Name],[Account Email],[Reset URL],[Generate Password]','2018-07-29 06:04:59'),(3,'Contact Email','Dear Admin,\n\n[Message]\n\nSincerely,\n[Name]\n[Email]','Quick Contact Email','gwtojt@gmail.com','[Name],[Email],[Message]','2018-07-13 06:57:05');
/*!40000 ALTER TABLE `tbl_email_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_eventlog`
--

DROP TABLE IF EXISTS `tbl_eventlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_eventlog` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `LogType` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `LogDateTime` datetime NOT NULL,
  `Source` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `LogMessage` text COLLATE utf8_unicode_ci NOT NULL,
  `UserID` int(11) NOT NULL,
  `UserType` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `ipAddress` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=213 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_eventlog`
--

LOCK TABLES `tbl_eventlog` WRITE;
/*!40000 ALTER TABLE `tbl_eventlog` DISABLE KEYS */;
INSERT INTO `tbl_eventlog` VALUES (90,'Info','2018-07-28 11:55:58','Add Admin','8: Naing Thura Aung is created.\r\nNew data are as follow:\r\nAdminID: 8\r\nAdminLevelID: 2\r\nAdminName: Naing Thura Aung\r\nLoginName: 33\r\nPassword: meu1BvcdpZWEUskpzsCdjkk0z8eKl6em\r\naddress: ygn\r\nphone: 33\r\nnrc: 12/\r\nstate: 2\r\nsalt: JQqSfrqxURB4oUxpSFnnS/3zypIb1WjQ\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 11:55:57 AM\r\nmodified_date: 7/28/2018 11:55:57 AM',7,'admin','127.0.0.1'),(91,'Info','2018-07-28 11:56:40','Delete Admin','8 is deleted.\r\nOld data are as follow:\r\nAdminID: 8\r\nAdminLevelID: 2\r\nAdminName: Naing Thura Aung\r\nLoginName: 33\r\nPassword: meu1BvcdpZWEUskpzsCdjkk0z8eKl6em\r\naddress: ygn\r\nphone: 33\r\nnrc: 12/\r\nstate: 2\r\nsalt: JQqSfrqxURB4oUxpSFnnS/3zypIb1WjQ\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 11:55:57 AM\r\nmodified_date: 7/28/2018 11:55:57 AM',1,'admin','127.0.0.1'),(92,'Info','2018-07-28 12:17:28','Update Admin','7: test admin is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: SDFKYbWvbnmPeij7AnhC2OXFACH6jeJt\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 2\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/28/2018 11:44:58 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(93,'Info','2018-07-28 12:17:28','Update Admin','7: test admin is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: SDFKYbWvbnmPeij7AnhC2OXFACH6jeJt\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 2\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/28/2018 11:44:58 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(94,'Info','2018-07-28 12:17:50','Update Admin','2: naing is updated.\r\nOld data are as follow:\r\nAdminID: 2\r\nAdminLevelID: 2\r\nAdminName: naing\r\nLoginName: naing\r\nPassword: GuoyIxUncxmk0VTdOsl2YI3+yYV4IA4S\r\naddress: yangon\r\nphone: grgr\r\nnrc: grg\r\nstate: 2\r\nsalt: xvDZB+tGOCfx6JChprk/SwW+LzHwQZd+\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: gr@gmai.com\r\ncreated_date: 7/25/2018 2:28:05 PM\r\nmodified_date: 7/25/2018 2:28:05 PM',1,'admin','127.0.0.1'),(95,'Info','2018-07-28 12:49:53','Add Admin','9: 1 is created.\r\nNew data are as follow:\r\nAdminID: 9\r\nAdminLevelID: 2\r\nAdminName: 1\r\nLoginName: f\r\nPassword: kH6SdJaEY2ILSYb3xAcwqOHe8lcNq5js\r\naddress: f\r\nphone: f\r\nnrc: f\r\nstate: 2\r\nsalt: m+uzlOCmVpzC1fdhHalLigMZhqL1vbCE\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 12:49:53 PM\r\nmodified_date: 7/28/2018 12:49:53 PM',1,'admin','127.0.0.1'),(96,'Info','2018-07-28 12:52:18','Add Admin','10: ef is created.\r\nNew data are as follow:\r\nAdminID: 10\r\nAdminLevelID: 2\r\nAdminName: ef\r\nLoginName: fefe\r\nPassword: uTeU5RbxEfzEyzxOKiPiujwaELwJxKxR\r\naddress: efe\r\nphone: fef\r\nnrc: fefe\r\nstate: 1\r\nsalt: 5ieZTWd8bOK2a8GNJIjSYFflnEEMRrRh\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 12:52:18 PM\r\nmodified_date: 7/28/2018 12:52:18 PM',1,'admin','127.0.0.1'),(97,'Info','2018-07-28 12:53:13','Add Admin','11: grgerg is created.\r\nNew data are as follow:\r\nAdminID: 11\r\nAdminLevelID: 2\r\nAdminName: grgerg\r\nLoginName: gregeger\r\nPassword: K2m72ZYCA2KVy95tObyFZvqee0PXXdYe\r\naddress: gegr\r\nphone: gerge\r\nnrc: gergeg\r\nstate: 1\r\nsalt: Z0ltruMywwl5EMfNufpghAv2OxNKDTKG\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: fewfi@gwijw.com\r\ncreated_date: 7/28/2018 12:53:13 PM\r\nmodified_date: 7/28/2018 12:53:13 PM',1,'admin','127.0.0.1'),(98,'Info','2018-07-28 12:53:59','Add Admin','12: bregg is created.\r\nNew data are as follow:\r\nAdminID: 12\r\nAdminLevelID: 2\r\nAdminName: bregg\r\nLoginName: grg\r\nPassword: lN9QMvmsdqxsKjaBnr2o41q2bUqFCi03\r\naddress: greger\r\nphone: grege\r\nnrc: gerger\r\nstate: 2\r\nsalt: lb2wKlTO+moF/Ix5cMdNWs7chB9bH4Bs\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: gr@gegi.com\r\ncreated_date: 7/28/2018 12:53:59 PM\r\nmodified_date: 7/28/2018 12:53:59 PM',1,'admin','127.0.0.1'),(99,'Info','2018-07-28 13:16:01','Update Admin','12: bregg is updated.\r\nOld data are as follow:\r\nAdminID: 12\r\nAdminLevelID: 2\r\nAdminName: bregg\r\nLoginName: grg\r\nPassword: lN9QMvmsdqxsKjaBnr2o41q2bUqFCi03\r\naddress: greger\r\nphone: grege\r\nnrc: gerger\r\nstate: 2\r\nsalt: lb2wKlTO+moF/Ix5cMdNWs7chB9bH4Bs\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: gr@gegi.com\r\ncreated_date: 7/28/2018 12:53:59 PM\r\nmodified_date: 7/28/2018 12:53:59 PM',1,'admin','127.0.0.1'),(100,'Info','2018-07-28 13:16:07','Update Admin','9: 1 is updated.\r\nOld data are as follow:\r\nAdminID: 9\r\nAdminLevelID: 2\r\nAdminName: 1\r\nLoginName: f\r\nPassword: kH6SdJaEY2ILSYb3xAcwqOHe8lcNq5js\r\naddress: f\r\nphone: f\r\nnrc: f\r\nstate: 2\r\nsalt: m+uzlOCmVpzC1fdhHalLigMZhqL1vbCE\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 12:49:53 PM\r\nmodified_date: 7/28/2018 12:49:53 PM',1,'admin','127.0.0.1'),(101,'Info','2018-07-28 13:16:13','Update Admin','9: 1 is updated.\r\nOld data are as follow:\r\nAdminID: 9\r\nAdminLevelID: 2\r\nAdminName: 1\r\nLoginName: f\r\nPassword: kH6SdJaEY2ILSYb3xAcwqOHe8lcNq5js\r\naddress: f\r\nphone: f\r\nnrc: f\r\nstate: 2\r\nsalt: m+uzlOCmVpzC1fdhHalLigMZhqL1vbCE\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: g@gmail.com\r\ncreated_date: 7/28/2018 12:49:53 PM\r\nmodified_date: 7/28/2018 12:49:53 PM',1,'admin','127.0.0.1'),(102,'Info','2018-07-28 13:16:18','Update Admin','7: test admin is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: SDFKYbWvbnmPeij7AnhC2OXFACH6jeJt\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 2\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/28/2018 11:44:58 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(103,'Info','2018-07-28 13:16:27','Update Admin','2: naing is updated.\r\nOld data are as follow:\r\nAdminID: 2\r\nAdminLevelID: 2\r\nAdminName: naing\r\nLoginName: naing\r\nPassword: GuoyIxUncxmk0VTdOsl2YI3+yYV4IA4S\r\naddress: yangon\r\nphone: grgr\r\nnrc: grg\r\nstate: 2\r\nsalt: xvDZB+tGOCfx6JChprk/SwW+LzHwQZd+\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: gr@gmai.com\r\ncreated_date: 7/25/2018 2:28:05 PM\r\nmodified_date: 7/25/2018 2:28:05 PM',1,'admin','127.0.0.1'),(104,'Info','2018-07-29 00:04:30','Admin Change Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(105,'Info','2018-07-29 00:41:22','Admin Change Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'9\'',9,'admin','127.0.0.1'),(106,'Info','2018-07-29 00:41:56','Admin Change Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'9\'',9,'admin','127.0.0.1'),(107,'Info','2018-07-29 00:42:34','Admin Change Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'9\'',9,'admin','127.0.0.1'),(108,'Info','2018-07-29 10:19:59','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 2\r\naccess_status: 2\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(109,'Info','2018-07-29 10:20:20','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(110,'Info','2018-07-29 10:21:15','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(111,'Info','2018-07-29 10:32:15','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(112,'Info','2018-07-29 10:33:03','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(113,'Info','2018-07-29 10:55:16','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(114,'Info','2018-07-29 10:56:22','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(115,'Info','2018-07-29 10:57:06','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: +vbQgM+0xlWt/BTo7TNQD/K44gSThVCP\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 10:17:34 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(116,'Info','2018-07-29 11:04:02','Admin Reset Password','Reset Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',1,'admin','127.0.0.1'),(117,'Info','2018-07-29 11:05:01','Admin Reset Password','Reset Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',1,'admin','127.0.0.1'),(118,'Info','2018-07-29 11:08:46','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: 5nzqtaMDZ4uW7EPCw8vT6R/5jsV3ryQI\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 0\r\naccess_status: 2\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 11:07:12 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(119,'Info','2018-07-29 11:37:22','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: 5nzqtaMDZ4uW7EPCw8vT6R/5jsV3ryQI\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 5\r\naccess_status: 2\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 11:07:12 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(120,'Info','2018-07-29 12:26:53','Update Admin','7: test admin is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: 5nzqtaMDZ4uW7EPCw8vT6R/5jsV3ryQI\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 1\r\naccess_status: 0\r\nEmail: e@gmail.com\r\ncreated_date: 7/29/2018 11:07:12 AM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(121,'Info','2018-07-29 12:27:01','Admin Reset Password','Reset Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',1,'admin','127.0.0.1'),(122,'Info','2018-07-29 16:54:45','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(123,'Info','2018-07-29 16:55:42','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(124,'Info','2018-07-29 17:04:18','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(125,'Info','2018-07-29 17:04:58','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(126,'Info','2018-07-29 17:05:29','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(127,'Info','2018-07-29 17:08:27','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(128,'Error','2018-07-29 17:10:06','Send Mail','No such host is known',0,'public',''),(129,'Info','2018-07-29 17:20:08','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(130,'Info','2018-07-29 17:28:21','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(131,'Info','2018-07-29 17:33:26','Admin Un-Block','7 is updated.\r\nOld data are as follow:\r\nAdminID: 7\r\nAdminLevelID: 2\r\nAdminName: test admin\r\nLoginName: ee\r\nPassword: gJN7PJb8FcvcZSR2M3nPIkIVcaL/+Q6J\r\naddress: yangon\r\nphone: ee\r\nnrc: 12\r\nstate: 1\r\nsalt: 2Enj5KORWrPusaQmY+cwV+xLiAtf1D9Q\r\nImagePath: \r\nlogin_fail_count: 4\r\naccess_status: 0\r\nEmail: permissionday@gmail.com\r\ncreated_date: 7/29/2018 5:29:58 PM\r\nmodified_date: 7/28/2018 11:44:58 AM',1,'admin','127.0.0.1'),(132,'Info','2018-07-29 17:40:38','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(133,'Info','2018-07-29 17:41:05','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(134,'Info','2018-07-29 17:44:16','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(135,'Info','2018-07-29 17:45:00','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(136,'Info','2018-07-29 17:45:16','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(137,'Info','2018-07-29 17:46:22','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(138,'Info','2018-07-29 17:48:45','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(139,'Info','2018-07-29 17:49:08','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(140,'Info','2018-07-29 17:50:12','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(141,'Info','2018-07-29 18:02:23','Admin Change Password From Forgot Password','Change Password\r\nGiven data are as follow:\r\nAdminID = \'7\'',7,'admin','127.0.0.1'),(142,'Error','2018-07-29 18:47:24','Send Mail','No such host is known',0,'public',''),(143,'Error','2018-07-29 18:49:34','Send Mail','No such host is known',0,'public',''),(144,'Info','2018-08-19 12:59:10','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(145,'Info','2018-08-19 13:14:08','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(146,'Info','2018-08-19 13:18:24','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(147,'Info','2018-08-19 13:24:03','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(148,'Info','2018-08-19 18:43:23','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(149,'Info','2018-08-19 18:57:46','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(150,'Info','2018-08-19 19:00:23','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(151,'Info','2018-08-19 21:54:47','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(152,'Info','2018-08-19 21:58:58','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(153,'Info','2018-08-19 22:12:35','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(154,'Info','2018-08-19 22:29:12','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(155,'Info','2018-08-19 22:32:27','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(156,'Info','2018-08-19 22:41:28','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(157,'Info','2018-08-19 22:45:38','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(158,'Info','2018-08-19 23:23:05','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(159,'Info','2018-08-19 23:29:59','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(160,'Info','2018-08-19 23:34:34','Admin Login','AdminID : 1\r\naccess_status : 0\r\nAdminLevelID : 1\r\nAdminName : Admin\r\ncreated_date : 20/07/2018 9:22:27 AM\r\nEmail : thandalay@gmail.com\r\nImagePath : 1.png\r\nlogin_fail_count : 0\r\nLoginName : admin\r\nmodified_date : 07/11/2017 10:08:35 AM\r\nPassword : wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt\r\nSalt : /SApKtKXpIa6YnHCjKLxQJAeb279BlX8\r\naddress : \r\nphone : \r\nnrc : \r\nstate : 1\r\n',1,'admin','127.0.0.1'),(161,'Info','2018-08-20 10:38:31','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(162,'Info','2018-08-20 10:41:12','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(163,'Info','2018-08-20 10:45:53','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(164,'Info','2018-08-20 10:49:45','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(165,'Info','2018-08-20 10:54:41','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(166,'Info','2018-08-21 07:06:18','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(167,'Info','2018-08-21 07:08:43','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(168,'Info','2018-08-21 07:20:57','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(169,'Info','2018-08-21 09:26:38','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(170,'Info','2018-08-24 16:32:13','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(171,'Info','2018-08-24 18:03:51','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(172,'Info','2018-08-24 18:09:41','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(173,'Info','2018-08-24 20:27:05','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(174,'Info','2018-08-24 22:40:26','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(175,'Info','2018-08-24 22:40:45','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(176,'Info','2018-08-24 22:41:18','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(177,'Info','2018-08-24 22:46:34','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(178,'Info','2018-08-24 23:02:19','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(179,'Info','2018-08-24 23:08:27','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(180,'Info','2018-08-25 10:14:51','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(181,'Info','2018-08-25 14:13:28','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(182,'Info','2018-08-25 14:26:26','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(183,'Info','2018-08-25 14:40:47','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(184,'Info','2018-08-25 14:59:16','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(185,'Info','2018-08-25 15:00:51','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(186,'Info','2018-08-25 15:02:00','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(187,'Info','2018-08-25 15:06:16','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(188,'Info','2018-08-25 15:07:53','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(189,'Info','2018-08-25 15:09:01','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(190,'Info','2018-08-25 15:19:41','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(191,'Info','2018-08-25 15:23:41','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(192,'Info','2018-08-25 15:42:02','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(193,'Info','2018-08-25 16:01:39','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(194,'Info','2018-08-25 16:11:11','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(195,'Info','2018-08-25 16:24:46','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(196,'Info','2018-08-25 16:28:34','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(197,'Info','2018-08-25 16:29:57','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(198,'Info','2018-08-25 16:45:30','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(199,'Info','2018-08-26 18:52:56','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(200,'Info','2018-08-26 18:55:12','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(201,'Info','2018-08-26 18:58:24','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(202,'Info','2018-08-26 19:05:30','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(203,'Info','2018-08-26 19:07:49','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(204,'Info','2018-08-26 19:12:01','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(205,'Info','2018-08-26 19:16:47','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(206,'Info','2018-08-26 19:20:15','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(207,'Info','2018-08-26 23:53:12','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(208,'Info','2018-08-28 22:36:57','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(209,'Info','2018-08-28 22:40:23','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(210,'Info','2018-08-29 06:47:13','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(211,'Info','2018-08-29 10:53:22','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1'),(212,'Info','2018-08-29 13:48:31','TokenProviderMiddleWare','Successful login for this account UserName : admin , Password : gwtsoft',1,'admin','127.0.0.1');
/*!40000 ALTER TABLE `tbl_eventlog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_sample`
--

DROP TABLE IF EXISTS `tbl_sample`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_sample` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_sample`
--

LOCK TABLES `tbl_sample` WRITE;
/*!40000 ALTER TABLE `tbl_sample` DISABLE KEYS */;
INSERT INTO `tbl_sample` VALUES (2,'Thura1','sample'),(3,'Aung Aung','sample'),(4,'Hello','sample4'),(5,'Naing','sample11');
/*!40000 ALTER TABLE `tbl_sample` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_setting`
--

DROP TABLE IF EXISTS `tbl_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_setting` (
  `SettingID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `Value` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`SettingID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_setting`
--

LOCK TABLES `tbl_setting` WRITE;
/*!40000 ALTER TABLE `tbl_setting` DISABLE KEYS */;
INSERT INTO `tbl_setting` VALUES (1,'Allow Login Failure Count','2'),(2,'Password Validation','8'),(3,'Admin Email','gwtojt@gmail.com'),(4,'SMTP','smtp.gmail.com'),(5,'Email','gwtojt@gmail.com'),(6,'Email Password','globalwave'),(7,'Server Port','587');
/*!40000 ALTER TABLE `tbl_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_state`
--

DROP TABLE IF EXISTS `tbl_state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_state` (
  `stateid` bigint(20) NOT NULL AUTO_INCREMENT,
  `statename` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `isactive` tinyint(4) NOT NULL,
  PRIMARY KEY (`stateid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_state`
--

LOCK TABLES `tbl_state` WRITE;
/*!40000 ALTER TABLE `tbl_state` DISABLE KEYS */;
INSERT INTO `tbl_state` VALUES (1,'Yangon',1),(2,'Mandalay',1);
/*!40000 ALTER TABLE `tbl_state` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-08-30 10:32:22
