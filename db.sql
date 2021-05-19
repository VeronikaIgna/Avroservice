-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: avtoservice
-- ------------------------------------------------------
-- Server version	5.5.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `car`
--

DROP TABLE IF EXISTS `car`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `car` (
  `id_Car` int(11) NOT NULL AUTO_INCREMENT,
  `StateNumber` varchar(10) NOT NULL,
  `Run` char(8) NOT NULL,
  `YearOfManufacture` varchar(10) NOT NULL,
  `id_Owner` int(11) NOT NULL,
  `id_Model` int(11) NOT NULL,
  `id_Marka` int(11) NOT NULL,
  PRIMARY KEY (`id_Car`),
  KEY `id_Owner` (`id_Owner`),
  KEY `id_Model` (`id_Model`),
  KEY `car_ibfk_2_idx` (`id_Marka`),
  CONSTRAINT `car_ibfk_2` FOREIGN KEY (`id_Marka`) REFERENCES `marka` (`id_Marka`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `car_ibfk_1` FOREIGN KEY (`id_Owner`) REFERENCES `owner` (`id_Owner`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `car_ibfk_3` FOREIGN KEY (`id_Model`) REFERENCES `model` (`id_Model`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `car`
--

LOCK TABLES `car` WRITE;
/*!40000 ALTER TABLE `car` DISABLE KEYS */;
INSERT INTO `car` VALUES (1,'Р764ТУ','200000','2004-11-20',2,24,2),(2,'Г905СТ','207  600','2025-09-20',5,2,2),(3,'У765РО','105 678','2011-01-20',6,3,3),(4,'Х779ПО','12000','2008-05-20',3,28,10),(6,'А718ТУ','167 900','2018-10-20',4,6,10),(35,'102 000','Р987РТ','13.08.2018',56,24,2);
/*!40000 ALTER TABLE `car` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contract`
--

DROP TABLE IF EXISTS `contract`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contract` (
  `id_Contract` int(11) NOT NULL AUTO_INCREMENT,
  `StartDate` varchar(15) NOT NULL,
  `EndDate` varchar(15) NOT NULL,
  `TotalCost` varchar(15) NOT NULL,
  `id_Owner` int(11) NOT NULL,
  `id_Services` int(11) NOT NULL,
  `id_master` int(11) DEFAULT NULL,
  `id_listDetails` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_Contract`),
  KEY `id_Owner` (`id_Owner`),
  KEY `id_Services` (`id_Services`),
  KEY `contract_ibfk_3_idx` (`id_master`),
  KEY `contract_ibfk_4_idx` (`id_listDetails`),
  CONSTRAINT `contract_ibfk_4` FOREIGN KEY (`id_listDetails`) REFERENCES `list_details` (`id_ListDetails`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `contract_ibfk_2` FOREIGN KEY (`id_Services`) REFERENCES `services` (`id_Services`) ON DELETE CASCADE,
  CONSTRAINT `contract_ibfk_3` FOREIGN KEY (`id_master`) REFERENCES `master` (`id_Master`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `contract_ibfk_1` FOREIGN KEY (`id_Owner`) REFERENCES `owner` (`id_Owner`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contract`
--

LOCK TABLES `contract` WRITE;
/*!40000 ALTER TABLE `contract` DISABLE KEYS */;
/*!40000 ALTER TABLE `contract` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detail`
--

DROP TABLE IF EXISTS `detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detail` (
  `id_Detail` int(11) NOT NULL AUTO_INCREMENT,
  `Name_Detail` varchar(50) NOT NULL,
  `Price_Detail` int(11) NOT NULL,
  PRIMARY KEY (`id_Detail`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detail`
--

LOCK TABLES `detail` WRITE;
/*!40000 ALTER TABLE `detail` DISABLE KEYS */;
INSERT INTO `detail` VALUES (1,'Амортизатор, левый',2200),(2,'Антифриз зелёный (5L) Megatrix',400),(3,'Боковина, левая',6500),(4,'Брызговик задний левый',150),(5,'Генератор (3 контакт) Asin',4500),(6,'Глушитель (длинный) Polmostrow ',1800),(7,'Датчик давления масла',400),(8,'Датчик уровня топлива отверстий',900),(9,'Колодки задние',600),(10,'Личинка замка зажигания (пластик)',1000);
/*!40000 ALTER TABLE `detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `list_details`
--

DROP TABLE IF EXISTS `list_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `list_details` (
  `id_ListDetails` int(11) NOT NULL AUTO_INCREMENT,
  `CountDetails` int(11) NOT NULL,
  `CostDetails` varchar(15) NOT NULL,
  `id_Detail` int(11) NOT NULL,
  PRIMARY KEY (`id_ListDetails`),
  KEY `id_Detail` (`id_Detail`),
  CONSTRAINT `list_details_ibfk_1` FOREIGN KEY (`id_Detail`) REFERENCES `detail` (`id_Detail`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `list_details`
--

LOCK TABLES `list_details` WRITE;
/*!40000 ALTER TABLE `list_details` DISABLE KEYS */;
INSERT INTO `list_details` VALUES (1,1,'2 200',1),(2,1,'1800',6),(3,4,'8800',1),(4,4,'26000',3),(5,3,'6600',1),(6,4,'1600',2),(7,5,'2000',2),(8,4,'8800',1),(9,5,'11000',1);
/*!40000 ALTER TABLE `list_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `managers`
--

DROP TABLE IF EXISTS `managers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `managers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `login` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `pass` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=koi8r;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `managers`
--

LOCK TABLES `managers` WRITE;
/*!40000 ALTER TABLE `managers` DISABLE KEYS */;
INSERT INTO `managers` VALUES (1,'Артем','1','1','Казань'),(3,'Ильхам','2','2','Оренбург');
/*!40000 ALTER TABLE `managers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `marka`
--

DROP TABLE IF EXISTS `marka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `marka` (
  `id_Marka` int(11) NOT NULL AUTO_INCREMENT,
  `Name_Marka` varchar(20) NOT NULL,
  PRIMARY KEY (`id_Marka`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marka`
--

LOCK TABLES `marka` WRITE;
/*!40000 ALTER TABLE `marka` DISABLE KEYS */;
INSERT INTO `marka` VALUES (1,'Audi'),(2,'BMW'),(3,'Chevrolet'),(4,'Citroen'),(5,'Ford'),(6,'Honda'),(7,'Hyundai'),(8,'Infiniti'),(9,'Jeep'),(10,'Kia'),(11,'Lexus'),(12,'Mazda'),(13,'Mercedes-Benz'),(14,'Mitsubishi'),(15,'Nissan'),(16,'Opel'),(17,'Skoda'),(18,'Toyota');
/*!40000 ALTER TABLE `marka` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master`
--

DROP TABLE IF EXISTS `master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `master` (
  `id_Master` int(11) NOT NULL AUTO_INCREMENT,
  `Surname_Mas` varchar(25) NOT NULL,
  `Name_Mas` varchar(25) NOT NULL,
  `MiddleName_Mas` varchar(25) NOT NULL,
  `PhoneNumber_Mas` varchar(20) NOT NULL,
  `Position` varchar(35) NOT NULL,
  PRIMARY KEY (`id_Master`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master`
--

LOCK TABLES `master` WRITE;
/*!40000 ALTER TABLE `master` DISABLE KEYS */;
INSERT INTO `master` VALUES (1,'Родионов','Александр','Павлович','89057646523','Автомеханик'),(2,'Капралов','Кирилл','Сергеевич','89600567329','Шиномонтажник'),(3,'Забалуев','Станислав','Михайлович','89176548790','Автомаляр'),(4,'Ахунов','Раиль','Рамилевич','89600897654','Автоэлектрик'),(5,'Мельников','Илья','Олегович','89179437593','Автомойщик');
/*!40000 ALTER TABLE `master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `model`
--

DROP TABLE IF EXISTS `model`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `model` (
  `id_Model` int(11) NOT NULL AUTO_INCREMENT,
  `Name_Model` varchar(20) NOT NULL,
  `id_Marka` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_Model`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `model`
--

LOCK TABLES `model` WRITE;
/*!40000 ALTER TABLE `model` DISABLE KEYS */;
INSERT INTO `model` VALUES (1,'A6',1),(2,'3-Series',2),(3,'Astra',3),(4,'C-Elysee',4),(5,'C-MAX',5),(6,'Avancier',6),(7,'Solaris',7),(8,'Grand Commander',8),(9,'RIO',10),(10,'Galant',NULL),(11,'ES',NULL),(12,'5',NULL),(13,'AMG GT',NULL),(14,'Fabia',NULL),(15,'Adam',NULL),(16,'Eclipse Cross',NULL),(17,'Ceed',NULL),(18,'A-Class',NULL),(19,'A3',1),(20,'A4',1),(21,'A5',1),(22,'X1',2),(23,'X2',2),(24,'X3',2),(25,'X4',2),(26,'Kuga',5),(27,'Picanto',10),(28,'Rio New',10),(29,'Ceed',10),(30,'Optima',10);
/*!40000 ALTER TABLE `model` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `owner`
--

DROP TABLE IF EXISTS `owner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `owner` (
  `id_Owner` int(11) NOT NULL AUTO_INCREMENT,
  `Surname_Owner` varchar(25) CHARACTER SET utf8 NOT NULL,
  `Name_Owner` varchar(25) CHARACTER SET utf8 NOT NULL,
  `MiddleName_Owner` varchar(25) CHARACTER SET utf8 NOT NULL,
  `PhoneNumber_Owner` varchar(11) CHARACTER SET utf8 NOT NULL,
  `id_manager` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_Owner`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=koi8r COLLATE=koi8r_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `owner`
--

LOCK TABLES `owner` WRITE;
/*!40000 ALTER TABLE `owner` DISABLE KEYS */;
INSERT INTO `owner` VALUES (2,'Гилязов','Ильнур','Радикович','89057654389',1),(3,'Герасимова','Резеда','Маратовна','89177438905',1),(4,'Сибгатуллин','Алмаз','Рафаэлович','89053146578',1),(5,'Миронова','Мария','Сергеевна','89600777873',1),(6,'Савушкин','Сергей','Артемович','89056783456',1),(56,'Долгова','Алина','Андреевна','89600424415',1);
/*!40000 ALTER TABLE `owner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `services`
--

DROP TABLE IF EXISTS `services`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `services` (
  `id_Services` int(11) NOT NULL AUTO_INCREMENT,
  `Name_Services` varchar(50) NOT NULL,
  `Price_Services` varchar(15) NOT NULL,
  PRIMARY KEY (`id_Services`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `services`
--

LOCK TABLES `services` WRITE;
/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services` VALUES (1,'Замена свечей зажигания','950'),(2,'Замена масла и масляного фильтра двигателя ','490'),(3,'Замена пыльника рулеврй рейки','690'),(4,'Замена рулевой тяги','790');
/*!40000 ALTER TABLE `services` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-12-07 15:50:46
