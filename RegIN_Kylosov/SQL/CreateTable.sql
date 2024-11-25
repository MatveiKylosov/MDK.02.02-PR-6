CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Image` blob,
  `DateUpdate` datetime NOT NULL,
  `DateCreate` datetime NOT NULL,
  `Pincode` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci