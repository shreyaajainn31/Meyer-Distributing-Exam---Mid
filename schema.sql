show databases;
CREATE USER 'username'@'hostname' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON 'databasename'.* TO 'username'@'hostname';

use databasename;

CREATE TABLE Customer (
    customerId INT PRIMARY KEY AUTO_INCREMENT,
    customerName VARCHAR(200),
    customerEmail VARCHAR(200),
    customerPhoneNumber VARCHAR(100),
    customerAddress VARCHAR(300)
);

CREATE TABLE Orders (
    orderId INT PRIMARY KEY AUTO_INCREMENT,
    customerId INT,
    orderNumber VARCHAR(200),
    orderDate DATETIME,
    FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);

CREATE TABLE Product (
    productId INT PRIMARY KEY AUTO_INCREMENT,
    productName VARCHAR(200),
    productDescription VARCHAR(10000),
    productPrice FLOAT
);

CREATE TABLE Returns (
	returnId INT PRIMARY KEY AUTO_INCREMENT,
    orderId INT,
    returnNumber VARCHAR(1000),
    returnDate DATETIME,
    FOREIGN KEY (orderId) REFERENCES Orders(orderId)
);




