-- MySQL dump 10.13  Distrib 9.1.0, for Win64 (x86_64)
--
-- Host: localhost    Database: final_library_db
-- ------------------------------------------------------
-- Server version	9.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary view structure for view `active_book_rentals`
--

DROP TABLE IF EXISTS `active_book_rentals`;
/*!50001 DROP VIEW IF EXISTS `active_book_rentals`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `active_book_rentals` AS SELECT 
 1 AS `Book History ID`,
 1 AS `Book Title`,
 1 AS `Member Name`,
 1 AS `Issue Date`,
 1 AS `Return Date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `books_by_category`
--

DROP TABLE IF EXISTS `books_by_category`;
/*!50001 DROP VIEW IF EXISTS `books_by_category`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `books_by_category` AS SELECT 
 1 AS `Book ID`,
 1 AS `Book Title`,
 1 AS `Author Name`,
 1 AS `Published Year`,
 1 AS `Category`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `ymb01`
--

DROP TABLE IF EXISTS `ymb01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ymb01` (
  `ymb01f01` int NOT NULL AUTO_INCREMENT,
  `ymb01f02` varchar(255) NOT NULL,
  `ymb01f03` varchar(255) DEFAULT NULL,
  `ymb01f04` int DEFAULT NULL,
  `ymb01f05` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ymb01f01`),
  UNIQUE KEY `ymb01f02` (`ymb01f02`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ymb01`
--

LOCK TABLES `ymb01` WRITE;
/*!40000 ALTER TABLE `ymb01` DISABLE KEYS */;
INSERT INTO `ymb01` VALUES (1,'To Kill a Mockingbird','Harper Lee',1960,'Fiction'),(2,'1984','George Orwell',1949,'Dystopian'),(3,'The Great Gatsby','F. Scott Fitzgerald',1925,'Fiction'),(4,'Neuromancer','William Gibson',1984,'Science Fiction'),(5,'Dune','Frank Herbert',1965,'Science Fiction'),(6,'The Invisible Man','H.G. Wells',1897,'Science Fiction'),(7,'Educated','Tara Westover',2018,'Memoir'),(8,'Sapiens','Yuval Noah Harari',2011,'Non-Fiction');
/*!40000 ALTER TABLE `ymb01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ymh01`
--

DROP TABLE IF EXISTS `ymh01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ymh01` (
  `ymh01f01` int NOT NULL AUTO_INCREMENT,
  `ymh01f02` int NOT NULL,
  `ymh01f03` int NOT NULL,
  `ymh01f04` date NOT NULL,
  `ymh01f05` date DEFAULT NULL,
  PRIMARY KEY (`ymh01f01`),
  KEY `fk_book` (`ymh01f02`),
  KEY `fk_member` (`ymh01f03`),
  CONSTRAINT `fk_book` FOREIGN KEY (`ymh01f02`) REFERENCES `ymb01` (`ymb01f01`) ON DELETE CASCADE,
  CONSTRAINT `fk_member` FOREIGN KEY (`ymh01f03`) REFERENCES `ymm01` (`ymm01f01`) ON DELETE CASCADE,
  CONSTRAINT `ymh01_chk_1` CHECK (((`ymh01f05` is null) or (`ymh01f05` > `ymh01f04`)))
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ymh01`
--

LOCK TABLES `ymh01` WRITE;
/*!40000 ALTER TABLE `ymh01` DISABLE KEYS */;
INSERT INTO `ymh01` VALUES (1,1,1,'2023-11-01',NULL),(2,2,2,'2023-12-01','2023-12-15'),(3,3,3,'2023-10-01','2023-11-01'),(4,4,4,'2023-09-01','2023-09-15'),(5,5,5,'2023-12-01',NULL);
/*!40000 ALTER TABLE `ymh01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ymm01`
--

DROP TABLE IF EXISTS `ymm01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ymm01` (
  `ymm01f01` int NOT NULL AUTO_INCREMENT,
  `ymm01f02` varchar(255) NOT NULL,
  `ymm01f03` varchar(255) DEFAULT NULL,
  `ymm01f04` date NOT NULL,
  PRIMARY KEY (`ymm01f01`),
  UNIQUE KEY `ymm01f03` (`ymm01f03`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ymm01`
--

LOCK TABLES `ymm01` WRITE;
/*!40000 ALTER TABLE `ymm01` DISABLE KEYS */;
INSERT INTO `ymm01` VALUES (1,'Alice Johnson','alice.johnson@example.com','2023-01-01'),(2,'Bob Smith','bob.smith@example.com','2023-02-01'),(3,'Charlie Brown','charlie.brown@example.com','2023-03-01'),(4,'Daisy Lee','daisy.lee@example.com','2023-04-01'),(5,'Evan White','evan.white@example.com','2023-05-01');
/*!40000 ALTER TABLE `ymm01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `active_book_rentals`
--

/*!50001 DROP VIEW IF EXISTS `active_book_rentals`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `active_book_rentals` AS select `ymh01`.`ymh01f01` AS `Book History ID`,`ymb01`.`ymb01f02` AS `Book Title`,`ymm01`.`ymm01f02` AS `Member Name`,`ymh01`.`ymh01f04` AS `Issue Date`,`ymh01`.`ymh01f05` AS `Return Date` from ((`ymh01` join `ymb01` on((`ymh01`.`ymh01f02` = `ymb01`.`ymb01f01`))) join `ymm01` on((`ymh01`.`ymh01f03` = `ymm01`.`ymm01f01`))) where (`ymh01`.`ymh01f05` is null) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `books_by_category`
--

/*!50001 DROP VIEW IF EXISTS `books_by_category`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `books_by_category` AS select `ymb01`.`ymb01f01` AS `Book ID`,`ymb01`.`ymb01f02` AS `Book Title`,`ymb01`.`ymb01f03` AS `Author Name`,`ymb01`.`ymb01f04` AS `Published Year`,`ymb01`.`ymb01f05` AS `Category` from `ymb01` order by `ymb01`.`ymb01f05` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-29 22:11:32
