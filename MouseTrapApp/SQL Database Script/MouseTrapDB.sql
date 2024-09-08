drop database if exists mousetrapdb;
create database mousetrapdb;
use mousetrapdb;

DELIMITER $$

CREATE PROCEDURE CreateGameTables()
BEGIN
    
    -- Create User Table
    CREATE TABLE `User` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        username VARCHAR(100) NOT NULL,
        `password` VARCHAR(15) NOT NULL,
        score INT NULL,
        login_attempt INT DEFAULT 0,
        locked_account BOOLEAN DEFAULT 0,
        is_admin BOOLEAN DEFAULT 0,
        health INT DEFAULT 100,
        Inventoryid INT NULL
	);
    
    -- Create Game Table
    CREATE TABLE `Game` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        `status` BOOLEAN NOT NULL,
        start_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        end_time TIMESTAMP NULL,
        Mapid INT NOT NULL
	);
    
    -- Create Map Table
    CREATE TABLE `Map` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        home_tile_row_p1 INT NOT NULL,
        home_tile_column_p1 INT NOT NULL,
        home_tile_row_p2 INT NOT NULL,
        home_tile_column_p2 INT NOT NULL,
        `max_rows` INT NOT NULL,
        `max_columns` INT NOT NULL
	);
    
    -- Create Tile Table
    CREATE TABLE `Tile` (
		id INT AUTO_INCREMENT PRIMARY KEY,
		position_y INT NOT NULL,
        position_x INT NOT NULL,
        Mapid INT NOT NULL,
        TileTypeid INT NOT NULL,
        Itemid INT NULL,
        Userid INT NULL
	);
    
    -- Create TileType table
    CREATE TABLE `TileType` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        `description` VARCHAR(255) NOT NULL,
        `value` INT NOT NULL
	);
    
    CREATE TABLE `ItemType` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        `type` VARCHAR(255),
        `value` INT(10)
	);
    
    -- Create Item Table
    CREATE TABLE `Item` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        `description` VARCHAR(255) NOT NULL,
        `value` INT NOT NULL,
        ItemTypeid INT NOT NULL
	);
    
    -- Create Inventory table
    CREATE TABLE `Inventory` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        item_count INT DEFAULT 0,
        Itemid INT NULL
	);
    
    -- Create Chat table
    CREATE TABLE `Chat` (
		id INT AUTO_INCREMENT PRIMARY KEY,
        message VARCHAR(255) NOT NULL,
        Gameid INT NOT NULL
	);
    
    -- User_Game linking tbale for many-to-many relation
    CREATE TABLE `User_Game` (
		Userid INT NOT NULL,
        Gameid INT NOT NULL,
        PRIMARY KEY (Userid, Gameid),
        FOREIGN KEY (Userid) REFERENCES `User`(id),
        FOREIGN KEY (Gameid) REFERENCES `Game`(id)
	);
    
    -- User_Chat linking table for many-to-many relation
    CREATE TABLE `User_Chat` (
		Userid INT NOT NULL,
        Chatid INT NOT NULL,
        PRIMARY KEY (Userid, Chatid),
        FOREIGN KEY (Userid) REFERENCES `User`(id),
        FOREIGN KEY (Chatid) REFERENCES `Chat`(id)
	);

	-- Foreign Key Constraints
    ALTER TABLE `User`
		ADD CONSTRAINT FOREIGN KEY (Inventoryid) REFERENCES `Inventory`(id);
        
	ALTER TABLE `Game`
		ADD CONSTRAINT FOREIGN KEY (Mapid) REFERENCES Map(id);
	
    ALTER TABLE `Tile`
		ADD CONSTRAINT FOREIGN KEY (Mapid) REFERENCES Map(id),
        ADD CONSTRAINT FOREIGN KEY (TileTypeid) REFERENCES TileType(id),
        ADD CONSTRAINT FOREIGN KEY (Userid) REFERENCES `User`(id),
        ADD CONSTRAINT FOREIGN KEY (Itemid) REFERENCES Item(id);
	
    ALTER TABLE `Item`
		ADD CONSTRAINT FOREIGN KEY (ItemTypeid) REFERENCES ItemType(id);
	
    ALTER TABLE `Inventory`
		ADD CONSTRAINT FOREIGN KEY (Itemid) REFERENCES Item(id);

END$$
DELIMITER ;

DELIMITER $$

DELIMITER $$
DELIMITER $$
CREATE PROCEDURE InsertTestData()
BEGIN
    -- Insert Test ItemType Data (Item types must exist before items)
    INSERT INTO `ItemType` (`type`, `value`)
    VALUES
    ('Cheese', 10),
    ('Paper Clip', 5),
    ('Peanut', 50),
    ('Mouse Trap', 50);

    -- Insert Test Item Data (Items must exist before inserting into Inventory)
    INSERT INTO `Item` (`description`, `value`, ItemTypeid)
    VALUES 
    ('Cheese', 10, 1),
    ('Paper Clip', 5, 2),
    ('Peanut', 50, 3),
    ('Mouse Trap', 50, 4);

    -- Insert Test Inventory Data (Inventory now references existing Itemid)
    INSERT INTO `Inventory` (item_count, Itemid)
    VALUES
    (1, 1), -- 1 Cheese in Inventory
    (2, 2); -- 2 Paper Clips in Inventory

    -- Insert Test User Data (User references existing Inventoryid)
    INSERT INTO `User` (username, `password`, score, login_attempt, locked_account, is_admin, health, Inventoryid)
    VALUES 
    ('player1', 'pass123', 50, 0, 0, 0, 100, 1), -- References Inventory id 1
    ('player2', 'pass456', 20, 0, 0, 0, 80, 2),  -- References Inventory id 2
    ('admin', 'adminpass', NULL, 0, 0, 1, 100, NULL); -- Admin with no inventory

    -- Insert Test Map Data
    INSERT INTO `Map` (home_tile_row_p1, home_tile_column_p1, home_tile_row_p2, home_tile_column_p2, `max_rows`, `max_columns`)
    VALUES 
    (0, 0, 20, 20, 20, 20),
    (0, 0, 30, 30, 30, 30);

    -- Insert Test Game Data
    INSERT INTO `Game` (`status`, start_time, end_time, Mapid)
    VALUES 
    (1, NOW(), NULL, 1),
    (1, NOW(), NULL, 2);

    -- Insert Test TileType Data
    INSERT INTO `TileType` (`description`, `value`)
    VALUES
    ('Home Tile', 0),
    ('Barrier', -1),
    ('Open', 0),
    ('Finish', 1);

    -- Insert Test Tile Data
    INSERT INTO `Tile` (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
    VALUES
    (0, 0, 1, 1, NULL, 1), -- Home Tile Player 1
    (20, 20, 1, 1, NULL, 2), -- Home Tile Player 2
    (0, 3, 1, 2, NULL, NULL), -- Barrier Tile
    (1, 1, 1, 3, 1, NULL), -- Cheese on Tile
    (4, 4, 1, 3, 2, NULL), -- Paper Clip on Tile
    (6, 5, 1, 3, 3, NULL), -- Peanut on Tile
    (7, 7, 1, 3, 4, NULL), -- Mouse Trap on Tile
    (10, 10, 1, 4, NULL, NULL); -- Finish Tile

    -- Insert Test Data into Chat Table
    INSERT INTO `Chat` (message, Gameid)
    VALUES
    ('Player 1 has entered the game!', 1),
    ('Player 2 has entered the game!', 2);

    -- Insert Test Data into User_Game table
    INSERT INTO `User_Game` (Userid, Gameid)
    VALUES
    (1, 1), -- Player 1 in game 1
    (2, 1); -- Player 2 in game 1

    -- Insert Test Data into User_Chat table
    INSERT INTO `User_Chat` (Userid, Chatid)
    VALUES 
    (1, 1), -- Player 1 in chat 1
    (2, 1); -- Player 2 in chat 1

END $$
DELIMITER ;


CALL CreateGameTables();

CALL InsertTestData();