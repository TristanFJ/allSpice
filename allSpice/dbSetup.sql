CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS recipes(
  id int NOT NULL primary key AUTO_INCREMENT,
  title TEXT NOT NULL,
  subtitle TEXT NOT NULL,
  category TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS ingredients(
  id int NOT NULL primary key AUTO_INCREMENT,
  name TEXT NOT NULL,
  quantity TEXT NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS steps(
  id int NOT NULL primary key AUTO_INCREMENT,
  sequence INT NOT NULL,
  body TEXT NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';


CREATE TABLE favorites(
  favoriteId int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL,
  id INT NOT NULL,
  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE, 

  FOREIGN KEY (id)
    REFERENCES recipes(id)
    ON DELETE CASCADE
)DEFAULT CHARSET UTF8 COMMENT '';

ALTER TABLE recipes
ADD imgUrl TEXT NOT NULL;