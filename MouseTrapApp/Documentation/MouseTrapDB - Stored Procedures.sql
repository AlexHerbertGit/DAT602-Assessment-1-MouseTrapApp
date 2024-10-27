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
    (0, 0, 13, 12, 12, 13),
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
    -- Home Tiles (Start points)
INSERT INTO `Tile` (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
VALUES
(0, 0, 1, 1, NULL, NULL),      -- Home Tile at top-left corner
(11, 12, 1, 1, NULL, NULL);    -- Home Tile at bottom-right corner

-- Finish Tile (Goal point)
INSERT INTO `Tile` (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
VALUES
(5, 6, 1, 4, NULL, NULL);      -- Finish Tile at center

-- Barrier Tiles (Obstacles)
INSERT INTO `Tile` (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
VALUES
(2, 2, 1, 2, NULL, NULL), (2, 3, 1, 2, NULL, NULL), (3, 5, 1, 2, NULL, NULL),
(6, 4, 1, 2, NULL, NULL), (7, 8, 1, 2, NULL, NULL), (10, 2, 1, 2, NULL, NULL),
(8, 9, 1, 2, NULL, NULL), (5, 10, 1, 2, NULL, NULL);

-- Open Tiles (All other positions)
INSERT INTO `Tile` (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
VALUES
-- Row 0
(0, 1, 1, 3, NULL, NULL), (0, 2, 1, 3, NULL, NULL), (0, 3, 1, 3, NULL, NULL),
(0, 4, 1, 3, NULL, NULL), (0, 5, 1, 3, NULL, NULL), (0, 6, 1, 3, NULL, NULL),
(0, 7, 1, 3, NULL, NULL), (0, 8, 1, 3, NULL, NULL), (0, 9, 1, 3, NULL, NULL),
(0, 10, 1, 3, NULL, NULL), (0, 11, 1, 3, NULL, NULL), (0, 12, 1, 3, NULL, NULL),

-- Row 1
(1, 0, 1, 3, NULL, NULL), (1, 1, 1, 3, NULL, NULL), (1, 2, 1, 3, NULL, NULL),
(1, 3, 1, 3, NULL, NULL), (1, 4, 1, 3, NULL, NULL), (1, 5, 1, 3, NULL, NULL),
(1, 6, 1, 3, NULL, NULL), (1, 7, 1, 3, NULL, NULL), (1, 8, 1, 3, NULL, NULL),
(1, 9, 1, 3, NULL, NULL), (1, 10, 1, 3, NULL, NULL), (1, 11, 1, 3, NULL, NULL),
(1, 12, 1, 3, NULL, NULL),

-- Row 2
(2, 0, 1, 3, NULL, NULL), (2, 1, 1, 3, NULL, NULL), (2, 4, 1, 3, NULL, NULL),
(2, 5, 1, 3, NULL, NULL), (2, 6, 1, 3, NULL, NULL), (2, 7, 1, 3, NULL, NULL),
(2, 8, 1, 3, NULL, NULL), (2, 9, 1, 3, NULL, NULL), (2, 10, 1, 3, NULL, NULL),
(2, 11, 1, 3, NULL, NULL), (2, 12, 1, 3, NULL, NULL),

-- Row 3
(3, 0, 1, 3, NULL, NULL), (3, 1, 1, 3, NULL, NULL), (3, 2, 1, 3, NULL, NULL),
(3, 3, 1, 3, NULL, NULL), (3, 4, 1, 3, NULL, NULL), (3, 6, 1, 3, NULL, NULL),
(3, 7, 1, 3, NULL, NULL), (3, 8, 1, 3, NULL, NULL), (3, 9, 1, 3, NULL, NULL),
(3, 10, 1, 3, NULL, NULL), (3, 11, 1, 3, NULL, NULL), (3, 12, 1, 3, NULL, NULL),

-- Row 4
(4, 0, 1, 3, NULL, NULL), (4, 1, 1, 3, NULL, NULL), (4, 2, 1, 3, NULL, NULL),
(4, 3, 1, 3, NULL, NULL), (4, 4, 1, 3, NULL, NULL), (4, 5, 1, 3, NULL, NULL),
(4, 6, 1, 3, NULL, NULL), (4, 7, 1, 3, NULL, NULL), (4, 8, 1, 3, NULL, NULL),
(4, 9, 1, 3, NULL, NULL), (4, 10, 1, 3, NULL, NULL), (4, 11, 1, 3, NULL, NULL),
(4, 12, 1, 3, NULL, NULL),

-- Row 5 (excluding Finish and Barrier tiles)
(5, 0, 1, 3, NULL, NULL), (5, 1, 1, 3, NULL, NULL), (5, 2, 1, 3, NULL, NULL),
(5, 3, 1, 3, NULL, NULL), (5, 4, 1, 3, NULL, NULL), (5, 5, 1, 3, NULL, NULL),
(5, 7, 1, 3, NULL, NULL), (5, 8, 1, 3, NULL, NULL), (5, 9, 1, 3, NULL, NULL),
(5, 11, 1, 3, NULL, NULL), (5, 12, 1, 3, NULL, NULL),

-- Row 6
(6, 0, 1, 3, NULL, NULL), (6, 1, 1, 3, NULL, NULL), (6, 2, 1, 3, NULL, NULL),
(6, 3, 1, 3, NULL, NULL), (6, 4, 1, 2, NULL, NULL), -- Barrier
(6, 5, 1, 3, NULL, NULL), (6, 6, 1, 3, NULL, NULL),
(6, 7, 1, 3, NULL, NULL), (6, 8, 1, 3, NULL, NULL),
(6, 9, 1, 3, NULL, NULL), (6, 10, 1, 3, NULL, NULL),
(6, 11, 1, 3, NULL, NULL), (6, 12, 1, 3, NULL, NULL),

-- Row 7
(7, 0, 1, 3, NULL, NULL), (7, 1, 1, 3, NULL, NULL), (7, 2, 1, 3, NULL, NULL),
(7, 3, 1, 3, NULL, NULL), (7, 4, 1, 3, NULL, NULL),
(7, 5, 1, 3, NULL, NULL), (7, 6, 1, 3, NULL, NULL),
(7, 7, 1, 3, NULL, NULL), (7, 8, 1, 2, NULL, NULL), -- Barrier
(7, 9, 1, 3, NULL, NULL), (7, 10, 1, 3, NULL, NULL),
(7, 11, 1, 3, NULL, NULL), (7, 12, 1, 3, NULL, NULL),

-- Row 8
(8, 0, 1, 3, NULL, NULL), (8, 1, 1, 3, NULL, NULL), (8, 2, 1, 3, NULL, NULL),
(8, 3, 1, 3, NULL, NULL), (8, 4, 1, 3, NULL, NULL),
(8, 5, 1, 3, NULL, NULL), (8, 6, 1, 3, NULL, NULL),
(8, 7, 1, 3, NULL, NULL), (8, 8, 1, 3, NULL, NULL),
(8, 9, 1, 2, NULL, NULL), -- Barrier
(8, 10, 1, 3, NULL, NULL), (8, 11, 1, 3, NULL, NULL),
(8, 12, 1, 3, NULL, NULL),

-- Row 9
(9, 0, 1, 3, NULL, NULL), (9, 1, 1, 3, NULL, NULL), (9, 2, 1, 3, NULL, NULL),
(9, 3, 1, 3, NULL, NULL), (9, 4, 1, 3, NULL, NULL),
(9, 5, 1, 3, NULL, NULL), (9, 6, 1, 3, NULL, NULL),
(9, 7, 1, 3, NULL, NULL), (9, 8, 1, 3, NULL, NULL),
(9, 9, 1, 3, NULL, NULL), (9, 10, 1, 3, NULL, NULL),
(9, 11, 1, 3, NULL, NULL), (9, 12, 1, 3, NULL, NULL),

-- Row 10
(10, 0, 1, 3, NULL, NULL), (10, 1, 1, 3, NULL, NULL), (10, 2, 1, 2, NULL, NULL), -- Barrier
(10, 3, 1, 3, NULL, NULL), (10, 4, 1, 3, NULL, NULL),
(10, 5, 1, 3, NULL, NULL), (10, 6, 1, 3, NULL, NULL),
(10, 7, 1, 3, NULL, NULL), (10, 8, 1, 3, NULL, NULL),
(10, 9, 1, 3, NULL, NULL), (10, 10, 1, 3, NULL, NULL),
(10, 11, 1, 3, NULL, NULL), (10, 12, 1, 3, NULL, NULL),

-- Row 11
(11, 0, 1, 3, NULL, NULL), (11, 1, 1, 3, NULL, NULL), (11, 2, 1, 3, NULL, NULL),
(11, 3, 1, 3, NULL, NULL), (11, 4, 1, 3, NULL, NULL), (11, 5, 1, 3, NULL, NULL),
(11, 6, 1, 3, NULL, NULL), (11, 7, 1, 3, NULL, NULL), (11, 8, 1, 3, NULL, NULL),
(11, 9, 1, 3, NULL, NULL), (11, 10, 1, 3, NULL, NULL), (11, 11, 1, 3, NULL, NULL);

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

DELIMITER $$
-- Create procedure for player login and lock out functionality, delcare parameter in and out fields
CREATE PROCEDURE PlayerLogin(
    IN p_username VARCHAR(100),
    IN p_password VARCHAR(15),
    OUT p_login_successful BOOLEAN,
    OUT p_account_locked BOOLEAN
)
BEGIN
    DECLARE login_attempts INT;
    DECLARE is_locked BOOLEAN;
    DECLARE db_password VARCHAR(15);
    
    -- Initialize output parameters
    SET p_login_successful = FALSE;
    SET p_account_locked = FALSE;
    
    -- Check if account is locked and retrieve login attempts and password
    SELECT locked_account, login_attempt, `password` INTO is_locked, login_attempts, db_password
    FROM `User`
    WHERE username = p_username;
    
    -- If account is locked, set p_account_locked to TRUE and skip further checks
    IF is_locked THEN
        SET p_account_locked = TRUE;
    ELSE
        -- Check if password matches the one stored in the db
        IF db_password = p_password THEN
            SET p_login_successful = TRUE;
            
            -- Reset login attempts on successful login
            UPDATE `User`
            SET login_attempt = 0
            WHERE username = p_username;
        ELSE
            -- Increment login attempt count on failed login
            SET login_attempts = login_attempts + 1;
            
            -- Check if login attempts have reached the limit
            IF login_attempts >= 5 THEN
                -- Lock the account
                UPDATE `User`
                SET locked_account = TRUE, login_attempt = login_attempts
                WHERE username = p_username;
                
                SET p_account_locked = TRUE;
            ELSE
                -- Update the login attempt count
                UPDATE `User`
                SET login_attempt = login_attempts
                WHERE username = p_username;
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;

DELIMITER $$
-- Stored Procedure for creating a new user account
CREATE PROCEDURE RegisterPlayer(
	IN p_username VARCHAR(100),
    IN p_password VARCHAR(15),
    OUT p_registration_successful BOOLEAN
)
BEGIN
	DECLARE user_exists INT DEFAULT 0;
    
    -- Initialzie Output parameter
    SET p_registration_successful = FALSE;
    
    -- Check if the username already exists in the database
    SELECT COUNT(*) INTO user_exists
    FROM `User`
    WHERE username = p_username;
    
    -- If the username does not exist, insert new user into database
    IF user_exists = 0 THEN
		INSERT INTO `User` (username, `password`, score, login_attempt, locked_account, is_admin, health)
        VALUES (p_username, p_password, 0, 0, 0, 0, 100);
        
        -- Set registration success to true
        SET p_registration_successful = TRUE;
	END IF;
END$$

DELIMITER ;

DELIMITER $$
-- Stored Procedure for creating a game, assigning a map id to the game record, and create/assigin tiles to the map co-ordinates based on max row/column values
CREATE PROCEDURE InitializeNewGameAndBoard(
	OUT p_game_id INT
)
BEGIN
	DECLARE map_id INT DEFAULT 1;
    DECLARE `max_rows` INT;
    DECLARE `max_columns` INT;
    DECLARE `row` INT DEFAULT 0;
    DECLARE col INT DEFAULT 0;
    
    -- Insert new game record with default map ID and start time
    INSERT INTO Game (`status`, start_time, end_time, Mapid)
    VALUES (1, NOW(), NULL, 1);
    
    -- Retrieve the recently created game ID
    SET p_game_id = LAST_INSERT_ID();
    
    -- Retrieve the max_rows and max_columns values for the map from the Map table using the ID
    SELECT `max_rows`, max_columns INTO `max_rows`, max_columns
    FROM Map
	WHERE id = map_id;
    
    -- While Loop to iterate through each row and column and create tiles
    WHILE `row` < `max_rows` DO
		SET col = 0;
        WHILE col < max_columns DO
			-- Insert a tile for each co-ordiante on the grid with default TileType
            INSERT INTO Tile (position_y, position_x, Mapid, TileTypeid, Itemid, Userid)
            VALUES (`row`, col, map_id, 1, NULL, NULL);
            -- Increment the col and row count by 1 with each pass, while loop will end once max row and column values have been reached
            SET col = col + 1;
		END WHILE;
        SET `row` = `row` + 1;
	END WHILE;
END$$

DELIMITER ;

DELIMITER $$
-- Stored Procedure that adds an item to a tile
CREATE PROCEDURE AddItemToTile(
	IN p_tile_id INT,
    IN p_item_type_id INT
)
BEGIN
	-- Check the tile exists and can hold an item
    IF EXISTS (SELECT 1 FROM Tile WHERE id = p_tile_id) THEN
		UPDATE Tile
        SET Itemid = p_item_type_id
        WHERE id = p_tile_id;
	ELSE 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "Tile does not exist or cannot hold an item";
	END IF;
END$$

DELIMITER ;

DELIMITER $$
-- Stored Procedure for moving a players character to another tile
CREATE PROCEDURE MovePlayerToTile(
	IN p_user_id INT,
    IN p_current_x INT,
    IN p_current_y INT,
    IN p_target_x INT,
    IN p_target_y INT
)
BEGIN
	DECLARE p_target_tile_type INT;
    
    -- Verfiy that the target tile is adjacent to the current tile
    IF ABS(p_target_x - p_current_x) + ABS(p_target_y - p_current_y) = 1 THEN
		SELECT TileTypeid INTO p_target_tile_type
        FROM Tile
        WHERE position_x = p_target_x AND position_y = p_target_y;
        
	-- Check if target tile is a barrier
		IF p_target_tile_type = 2 THEN
			SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = "Cannot move into a barrier tile.";
		ELSE 
			-- Update Player position
            UPDATE Tile SET Userid = NULL WHERE Userid = p_user_id; -- Clears old user position
            UPDATE Tile SET Userid = p_user_id WHERE position_x = p_target_x AND position_y = p_target_y; -- Sets new position
		END IF;
	ELSE 
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = "Invalid move: target tile in not adjacent to current position.";
	END IF;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure that Increases the players score when a Tile with a Cheese Wedge has been landed on\
CREATE PROCEDURE UpdatePlayerScoreForCheese(
	IN p_user_id INT,
    IN p_position_x INT,
    IN p_position_y INT
)
BEGIN
	DECLARE item_type INT;
    
    -- Check if the players position contains a cheese wedge item type
    SELECT ItemTypeid INTO item_type
    FROM Tile
    JOIN Item ON Tile.Itemid = Item.id
	WHERE Tile.position_x = p_position_x
        AND Tile.position_y = p_positon_y
        AND Item.ItemTypeid - 1;
	
    -- If tile does contain a cheese wedge, increase players score by 1
    IF item_type = 1 THEN
		UPDATE `User`
        SET score = score + 10
		WHERE id = p_user_id;
	END IF;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure to handle adding or using items when player lands on a tile containing an item
CREATE PROCEDURE AddItemToPlayerInventory(
	IN p_user_id INT,
    IN p_position_x INT,
    IN p_position_y INT 
)
BEGIN
	DECLARE item_type INT;
    DECLARE item_id INT;
    DECLARE map_id INT;
    
    -- Retrieve the Mapid for the user's current game
    SELECT Game.Mapid INTO map_id
    FROM `User`
    JOIN Game ON `User`.Gameid = Game.id
    WHERE `User`.id = p_user_id;
    
    -- Check the Tile for an Item Type and Item Id on the Tile
    SELECT Item.ItemTpeid, Item.id INTO item_type, item_id
    FROM Tile
    JOIN Item ON Tile.Itemid = Item.id
    WHERE Tile.Mapid = map_id
		AND Tile.position_x = p_postion_x
        AND Tile.postion_y = p_postion_y;
	
	-- Check the item type and update the player inventory or health
    IF item_type = 2 THEN
		UPDATE `User`
        SET Inventoryid = item_id
        WHERE id = p_user_id;
        
        -- Remove the item from the tile
        UPDATE Tile
        SET Itemid = NULL 
        WHERE Mapid = map_id
			AND position_x = p_position_x
            AND position_y = p_postion_y;
		
	ELSEIF item_type = 3 THEN
		-- Increase player health by 50 if the item type is 3 (peanuts)
        UPDATE `User`
        SET health = LEAST(health + 50, 100) -- Caps health at 100
        WHERE id = p_user_id;
        
        -- Remove the Item from the Tile
		UPDATE Tile
        SET Itemid = NULL
        WHERE Mapid = map_id
			AND position_x = p_position_x
            AND position_y = p_postion_y;
	END IF;
END $$

DELIMITER ;

DELIMITER $$

-- Procedure that handles when a player lands on a tile containing a Mouse Trap, it is then removed from that tile and placed in another available tile on the map
CREATE PROCEDURE HandleMouseTrapMovement(
	IN p_user_id INT,
    IN p_position_x INT,
    IN p_position_y INT 
)
BEGIN
	DECLARE item_type INT;
    DECLARE item_id INT;
    DECLARE map_id INT;
    DECLARE new_position_x INT;
    DECLARE new_position_y INT;
    
    -- Get the Mapid for the user's current game.
    SELECT Game.Mapid INTO map_id
    FROM `User`
    JOIN Game ON `User`.Gameid = Game.id
	WHERE `User`.id = p_user_id;
    
    -- Check if the tile contains a Mouse Trap Item Type
    SELECT Item.ItemTypeid, Item.id INTO item_type, item_id
    FROM Tile
    JOIN Item ON Tile.Itemid = Item.id
    WHERE Tile.Mapid = map_id
		AND Tile.position_x = p_position_x
        AND Tile.position_y = p_position_y
        AND Item.ItemTypeid = 4;
        
	-- If the item is a Mouse Trap, remove it from the current tile and move it
    IF item_type = 4 THEN
		UPDATE Tile
        SET Itemid = NULL 
		WHERE Mapid= map_id
			AND position_x = p_position_x
            AND position_y = p_postion_y;
            
		SELECT position_x, position_y INTO new_position_x, new_position_y
		FROM Tile
        WHERE Mapid = map_id
			AND Itemid IS NULL
		ORDER BY RAND()
        LIMIT 1;
        
        -- Place Mouse Trap on the new random empty tile
        UPDATE Tile
        SET Itemid = item_id
        WHERE Mapid = map_id
			AND position_x = new_position_x
            AND position_y = new_position_y;
	END IF;
END $$

DELIMITER ;

DELIMITER $$
-- Procedcure that terminates active games based on the ID that has been received from the front end application

CREATE PROCEDURE TerminateGame(
	IN p_game_id INT 
)
BEGIN
	DECLARE game_exists INT;
    
    -- Check if game exisits and is active
    SELECT COUNT(*) INTO game_exists
    FROM Game
	WHERE id = p_game_id AND status = 1;
    
    -- If game does exist and is active, proceed with termination and removal of record
    IF game_exists > 0 THEN
		DELETE FROM User_Game WHERE Gameid = p_game_id;
        
        DELETE FROM Game WHERE id = p_game_id;
        
        SELECT CONCAT('Game with ID ', p_game_id, 'has been terminated.') AS TerminationMessage;
	ELSE 
		SELECT CONCAT('No active game wfound with ID ', p_game_id) AS TerminationMessage;
	END IF;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to add a new player from the admin dashboard

CREATE PROCEDURE AdminAddNewPlayer(
	IN p_admin_user_id INT,
    IN p_username VARCHAR(100),
    IN p_password VARCHAR(15),
    IN p_is_admin BOOLEAN
)
BEGIN
	DECLARE is_admin INT;
    DECLARE user_exists INT;
    
    -- Check if the user making the request is an admin
    SELECT is_admin INTO is_admin
    FROM `User`
    WHERE id = p_admin_user_id;
    
    -- If user is not an admin then end the procedure
    IF is_admin = 0 THEN 
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Permission Denied, only admins can create new players in this way.';
	ELSE 
		SELECT COUNT(*) INTO user_exists
        FROM `User`
        WHERE username = p_username;
        
        -- If no existing user with credentials, proceed with registration
        IF user_exists = 0 THEN
			INSERT INTO `User` (username, `password`, score, login_attempt, locked_account, is_admin, health)
            VALUES (p_username, p_password, 0, 0, 0, p_is_admin, 100);
            
            -- Success message
            SELECT CONCAT('New Player "', p_username, '" has been successfully.') AS SuccessMessage;
		ELSE 
			SELECT CONCAT('A user with the username "', p_username, '" already exists.') AS ErrorMessage;
		END IF;
	END IF;
END $$

DELIMITER ; 

DELIMITER $$
-- Procedure to update a players data from inputs made from the front end application admin dashboard
CREATE PROCEDURE UpdatePlayerData(
	IN p_user_id INT,
    IN p_username VARCHAR(100),
    IN p_password VARCHAR(15),
    IN p_score INT,
    IN p_login_attempt INT,
    IN p_locked_account BOOLEAN,
    IN p_is_admin BOOLEAN,
    IN p_health INT 
)
BEGIN
	DECLARE user_exists INT;
    
    SELECT COUNT(*) INTO user_exists
    FROM `User`
	WHERE id = p_user_id;
    
    -- If user exists, proceed with the update
    IF user_exists > 0 THEN
		UPDATE `User`
        SET
			username = COALESCE(p_username, username),
            `password` = COALESCE(p_password, `password`),
            score = COALESCE(p_score, score),
            login_attempt = COALESCE(p_login_attempt, login_attempt),
            locked_account = COALESCE(p_locked_account, locked_account),
            is_admin = COALESCE(p_is_admin, is_admin),
            health = COALESCE(p_health, health)
		WHERE id = p_user_id;
        
        SELECT CONCAT('Player with ID ', p_user_id, ' has been updated successfully.') AS SuccessMessage;
	ELSE
    
		SELECT CONCAT('Player with ID ', p_user_id, ' does not exist.') as ErrorMessage;
	END IF;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to delete a player from the User table from the admin dashboard
CREATE PROCEDURE DeleteplayerByUsername(
	IN p_username VARCHAR(100)
)
BEGIN
	DECLARE user_exists INT;
    
    SELECT COUNT(*) INTO user_exists
    FROM `User`
    WHERE username = p_username;
    
    IF user_exists > 0 THEN
		DELETE FROM	`User`
        WHERE username = p_username;
        
		SELECT CONCAT('Player "', p_username, '" has been deleted.') AS SuccessMessage;
    ELSE
		SELECT CONCAT('Player with username "', p_username, '" does not exist.') AS ErrorMessage;
	END IF;
END $$

DELIMITER ;
