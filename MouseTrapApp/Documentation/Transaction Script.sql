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
    ('admin', 'adminpass', 100, 0, 0, 1, 100, NULL); 

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
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		-- Rollback transaction in case of error
        ROLLBACK;
        SELECT 'An error occured during player login.' AS ErrorMessage;
	END;
    
    -- Initialize output parameters
    SET p_login_successful = FALSE;
    SET p_account_locked = FALSE;
    
    -- Start Transaction
    START TRANSACTION;
    
    BEGIN
		-- Check if account is locked and retrieve login attempts and password
		SELECT locked_account, login_attempt, `password`
		INTO is_locked, login_attempts, db_password
		FROM `User`
		WHERE username = p_username;
		
		-- If account is locked, set p_account_locked to TRUE and skip further checks
		IF is_locked THEN
			SET p_account_locked = TRUE;
            ROLLBACK;
		ELSE
			-- Check if password matches the one stored in the db
			IF db_password = p_password THEN
				SET p_login_successful = TRUE;
				
				-- Reset login attempts on successful login
				UPDATE `User`
				SET login_attempt = 0
				WHERE username = p_username;
                
                COMMIT;
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
                    ROLLBACK;
				ELSE
					-- Update the login attempt count
					UPDATE `User`
					SET login_attempt = login_attempts
					WHERE username = p_username;
                    
                    ROLLBACK;
				END IF;
			END IF;
		END IF;
	END;
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
	-- Error handling in case of unexpected errors
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
		SELECT 'An error occurred during player registration.' AS ErrorMessage;
	END;
	
    
    -- Initialzie Output parameter
    SET p_registration_successful = FALSE;
    
    -- Start Transaction
    START TRANSACTION;
    
    BEGIN
		-- Check if the username already exists in the database
		SELECT COUNT(*) INTO user_exists
		FROM `User`
		WHERE username = p_username
		FOR UPDATE;
		
		-- If the username does not exist, insert new user into database
		IF user_exists = 0 THEN
			INSERT INTO `User` (username, `password`, score, login_attempt, locked_account, is_admin, health)
			VALUES (p_username, p_password, 0, 0, 0, 0, 100);
			
			-- Set registration success to true
			SET p_registration_successful = TRUE;
			COMMIT;
		ELSE
			-- Rollback incase of error
			ROLLBACK;
		END IF;
	END;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure to Create a new Game
CREATE PROCEDURE CreateNewGame(
	OUT p_game_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		-- Rollback transaction in case of error
        ROLLBACK;
        SET p_game_id = NULL;
        SELECT 'An error occurred while creating a new game.' AS ErrorMessage;
	END;
	-- Start Transaction
    START TRANSACTION;
    
    BEGIN
		INSERT INTO `Game` (`status`, start_time, end_time, Mapid)
		VALUES (1, NOW(), NULL, 1);
		
		SET p_game_id = LAST_INSERT_ID();
        
        COMMIT;
	END;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to get map dimensions for the game
CREATE PROCEDURE GetMapDimensionsForGame(
    IN p_game_id INT,
    OUT p_max_rows INT,
    OUT p_max_columns INT,
    OUT p_map_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        
        SET p_map_id = NULL;
		SET p_max_rows = NULL;
		SET p_max_columns = NULL;
        
        SELECT 'An error occured while fetching map dimensions.' as ErrorMessage;
	END;
	-- Start transaction
    START TRANSACTION;
    
    BEGIN
		DECLARE map_id INT;

		-- Fetch the Map ID associated with the given game
		SELECT Mapid 
        INTO map_id 
        FROM `Game` 
        WHERE id = p_game_id;
		SET p_map_id = map_id;
		
        IF map_id IS NOT NULL THEN
			SET p_map_id = map_id;
            
			-- Fetch max rows and max columns
			SELECT max_rows, max_columns 
            INTO p_max_rows, p_max_columns
			FROM `Map`
			WHERE id = map_id;
		ELSE
			-- If not valid map id is found, set output parameters to NULL
            SET p_map_id = NULL;
            SET p_max_rows = NULL;
            SET p_max_columns = NULL;
            
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Game ID does not have a valid Map ID.';
		END IF;
        
        COMMIT;
	END;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure to collect Tile data and send it to the front end for Grid Cell population
CREATE PROCEDURE GetTilesForGame(
	IN p_game_id INT
)
BEGIN
	    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        
        SELECT 'An error occured while fetching the tile data.' AS ErrorMessage;
	END;

	-- Start Transaction
    START TRANSACTION;
    
    BEGIN
		DECLARE map_id INT;
		
		-- Fetch the Mapid associated with the specific game
		SELECT Mapid 
        INTO map_id 
        FROM `Game` 
        WHERE id = p_game_id;
		
        -- Verify the map id exists
        IF map_id IS NOT NULL THEN
			SELECT position_y, position_x, TileTypeid, Itemid, Userid
            FROM `Tile`
            WHERE Mapid = map_id;
		ELSE
			SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No valid map id assoicated with the game id.';
		END IF;
        
        COMMIT;
	END;
END $$

DELIMITER ;

DELIMITER $$
-- Stored Procedure that adds an item to a tile
CREATE PROCEDURE AddItemToTile(
    IN p_tile_id INT,
    IN p_item_type_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        
        SELECT 'An error occured while adding the item to the tile.' AS ErrorMessage;
	END;
    
-- Start Transaction
	START TRANSACTION;
    
    BEGIN
		-- Check if the tile exists and is eligible to hold an item
		IF EXISTS (SELECT 1 FROM Tile WHERE id = p_tile_id) THEN
			UPDATE Tile
			SET Itemid = p_item_type_id
			WHERE id = p_tile_id;
            
            COMMIT;
		ELSE 
			SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = "Tile does not exist or cannot hold an item";
		END IF;
	END;
END$$

DELIMITER $$

CREATE PROCEDURE PopulateItemsOnBoard(
    IN p_map_id INT
)
BEGIN
    DECLARE item_count INT;
    DECLARE tile_id INT;
    DECLARE item_type_id INT;
    DECLARE attempts INT DEFAULT 0;

    DECLARE max_cheese INT DEFAULT 5;
    DECLARE max_paper_clips INT DEFAULT 5;
    DECLARE max_peanuts INT DEFAULT 5;
	
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred during the item population on the board.';
	END;
    
    START TRANSACTION;
    
    -- Cheese placement loop
    SET item_type_id = 1; -- Cheese
    SET item_count = 0;
    SET attempts = 0;

    cheese_loop: WHILE item_count < max_cheese DO
        SELECT id INTO tile_id
        FROM Tile
        WHERE Mapid = p_map_id
            AND TileTypeid NOT IN (1, 2, 4) -- Excludes home, barrier, and finish tiles
            AND Itemid IS NULL
            AND NOT EXISTS (
                SELECT 1 FROM Tile AS t2
                WHERE t2.Itemid IS NOT NULL
                    AND ABS(t2.position_x - Tile.position_x) <= 1
                    AND ABS(t2.position_y - Tile.position_y) <= 1
            )
        ORDER BY RAND()
        LIMIT 1;
            
        -- Attempt to place the item
        IF tile_id IS NOT NULL THEN
            CALL AddItemToTile(tile_id, item_type_id);
            SET item_count = item_count + 1;
        END IF;

        SET attempts = attempts + 1;
        IF attempts > 50 THEN
            LEAVE cheese_loop;
        END IF;
    END WHILE;

    -- Paper Clip placement loop
    SET item_type_id = 2; -- Paper Clip
    SET item_count = 0;
    SET attempts = 0;

    paper_clip_loop: WHILE item_count < max_paper_clips DO
        SELECT id INTO tile_id
        FROM Tile
        WHERE Mapid = p_map_id
            AND TileTypeid NOT IN (1, 2, 4) -- Excludes home, barrier, and finish tiles
            AND Itemid IS NULL
            AND NOT EXISTS (
                SELECT 1 FROM Tile AS t2
                WHERE t2.Itemid IS NOT NULL
                    AND ABS(t2.position_x - Tile.position_x) <= 1
                    AND ABS(t2.position_y - Tile.position_y) <= 1
            )
        ORDER BY RAND()
        LIMIT 1;
            
        -- Attempt to place the item
        IF tile_id IS NOT NULL THEN
            CALL AddItemToTile(tile_id, item_type_id);
            SET item_count = item_count + 1;
        END IF;

        SET attempts = attempts + 1;
        IF attempts > 50 THEN
            LEAVE paper_clip_loop;
        END IF;
    END WHILE;

    -- Peanut placement loop
    SET item_type_id = 3; -- Peanut
    SET item_count = 0;
    SET attempts = 0;

    peanut_loop: WHILE item_count < max_peanuts DO
        SELECT id INTO tile_id
        FROM Tile
        WHERE Mapid = p_map_id
            AND TileTypeid NOT IN (1, 2, 4) -- Excludes home, barrier, and finish tiles
            AND Itemid IS NULL
            AND NOT EXISTS (
                SELECT 1 FROM Tile AS t2
                WHERE t2.Itemid IS NOT NULL
                    AND ABS(t2.position_x - Tile.position_x) <= 1
                    AND ABS(t2.position_y - Tile.position_y) <= 1
            )
        ORDER BY RAND()
        LIMIT 1;
            
        -- Attempt to place the item
        IF tile_id IS NOT NULL THEN
            CALL AddItemToTile(tile_id, item_type_id);
            SET item_count = item_count + 1;
        END IF;

        SET attempts = attempts + 1;
        IF attempts > 50 THEN
            LEAVE peanut_loop;
        END IF;
    END WHILE;
    
    COMMIT;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to collect user data to be used in the game
CREATE PROCEDURE GetUserDetails(
	IN p_username VARCHAR(100),
    OUT p_user_id INT,
    OUT p_score INT,
    OUT p_is_admin BOOLEAN,
    OUT p_health INT,
    OUT p_inventory_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while fetching user details.';
	END;
    
    START TRANSACTION;
    
	SELECT id, score, is_admin, health, Inventoryid
    INTO p_user_id, p_score, p_is_admin, p_health, p_inventory_id
    FROM `User`
    WHERE username = p_username;
    
    COMMIT;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure to add a user to a game, creating a User-Game record to link the User id to the Game id
CREATE PROCEDURE AddUserToGame(
	IN p_user_id INT,
    IN p_game_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while adding user to the game.';
	END;
    
    START TRANSACTION;
    
	-- Prevent duplicate records
	IF NOT EXISTS (
		SELECT 1 FROM `User_Game`
		WHERE Userid = p_user_id AND Gameid = p_game_id
	) THEN
		INSERT INTO `User_Game` (Userid, Gameid)
        VALUES (p_user_id, p_game_id);
	END IF;
    
    COMMIT;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to add a user to a home tile
CREATE PROCEDURE AssignUserToHomeTile(
	IN p_user_id INT,
    IN p_map_id INT
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occured when assigning a player to the home tile.';
	END;
    
    START TRANSACTION;
    
    IF EXISTS (
		SELECT 1 FROM Tile
        WHERE position_x = 0 AND position_y = 0 AND Mapid = p_map_id
	) THEN
        
		-- Update the home tile to assign user ID
		UPDATE Tile
		SET Userid = p_user_id
		WHERE position_x = 0 AND position_y = 0 AND mapId = p_map_id;
	ELSE
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Home tile does not exist for the specified map id.';
	END IF;
    
    COMMIT;
END$$

DELIMITER ;

DELIMITER $$
CREATE PROCEDURE MovePlayerToTile(
    IN p_user_id INT,
    IN p_current_x INT,
    IN p_current_y INT,
    IN p_target_x INT,
    IN p_target_y INT
)
BEGIN
	DECLARE p_target_tile_type INT;
    DECLARE p_item_id INT;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while moving the player.';
	END;
	
    START TRANSACTION;
    
    -- Verify that the target tile is adjacent to the current tile
    IF ABS(p_target_x - p_current_x) + ABS(p_target_y - p_current_y) = 1 THEN
        SELECT TileTypeid, Itemid INTO p_target_tile_type, p_item_id
        FROM Tile
        WHERE position_x = p_target_x AND position_y = p_target_y
        FOR UPDATE;

        -- Check if the target tile is a barrier
        IF p_target_tile_type = 2 THEN
			ROLLBACK;
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = "Cannot move into a barrier tile.";
        ELSE
            -- Update player position
            UPDATE Tile 
            SET Userid = NULL 
            WHERE Userid = p_user_id; -- Clear old position
            
            UPDATE Tile 
            SET Userid = p_user_id 
            WHERE position_x = p_target_x AND position_y = p_target_y;

            -- If there's an item, add it to the player's inventory and clear the item
            IF p_item_id IS NOT NULL THEN
                -- Add item to inventory (implement your inventory update here)
                UPDATE Tile SET Itemid = NULL WHERE position_x = p_target_x AND position_y = p_target_y;
            END IF;
        END IF;
    ELSE
    ROLLBACK;
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = "Invalid move: target tile is not adjacent.";
    END IF;
    
    COMMIT;
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
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while updateing the player score for cheese collected.';
	END;
    
    START TRANSACTION;
    
    -- Check if the players position contains a cheese wedge item type
    SELECT ItemTypeid INTO item_type
    FROM Tile
    JOIN Item ON Tile.Itemid = Item.id
	WHERE Tile.position_x = p_position_x
        AND Tile.position_y = p_positon_y
        AND Item.ItemTypeid = 1
	FOR UPDATE;
    
    -- If tile does contain a cheese wedge, increase players score by 1
    IF item_type = 1 THEN
		UPDATE `User`
        SET score = score + 10
		WHERE id = p_user_id;
        
        UPDATE Tile
        SET Itemid = NULL
        WHERE position_x = p_position_x
        AND position_y = p_position_y;
	END IF;
    
    COMMIT;
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
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while adding items to player inventory.';
	END;
    
    START TRANSACTION;
    
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
        AND Tile.postion_y = p_postion_y
	FOR UPDATE;
    
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
    
    COMMIT;
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
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while handling mouse trap movement.';
	END;
    
    START TRANSACTION;
    
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
        AND Item.ItemTypeid = 4
	FOR UPDATE;
    
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
        LIMIT 1
        FOR UPDATE;
        
        -- Place Mouse Trap on the new random empty tile
        UPDATE Tile
        SET Itemid = item_id
        WHERE Mapid = map_id
			AND position_x = new_position_x
            AND position_y = new_position_y;
	END IF;
    
    COMMIT;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to get all users ID and usernames
CREATE PROCEDURE GetAllUsers()
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while fetching user data.';
	END;
    
    START TRANSACTION;
    
    -- Selects all user IDs and usernames to be read directly as a dataset in C#
    SELECT id AS UserID, username AS Username
    FROM `User`
    FOR SHARE;
    
    COMMIT;
END$$

DELIMITER $$

CREATE PROCEDURE GetAllUserGames()
BEGIN
    -- Error handling and rollback on exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while fetching User_Game records.';
    END;

    -- Start transaction for concurrency-safe reads
    START TRANSACTION;

    -- Select all game IDs and associated user IDs from User_Game table
    SELECT Gameid AS game_id, Userid AS user_id
    FROM `User_Game`
    FOR SHARE; -- Allow concurrent reads, prevent writes during the transaction

    -- Commit transaction
    COMMIT;
END$$

DELIMITER ;

DELIMITER $$
-- Procedcure that terminates active games based on the ID that has been received from the front end application
CREATE PROCEDURE TerminateGame(
	IN p_game_id INT 
)
BEGIN
	DECLARE game_exists INT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred while terminating the game.';
	END;
    
    START TRANSACTION;
    
    -- Check if game exisits and is active
    SELECT COUNT(*) INTO game_exists
    FROM Game
	WHERE id = p_game_id AND status = 1
    FOR UPDATE;
    
    -- If game does exist and is active, proceed with termination and removal of record
    IF game_exists > 0 THEN
		DELETE FROM User_Game WHERE Gameid = p_game_id;
        
        DELETE FROM Game WHERE id = p_game_id;
        
        SELECT CONCAT('Game with ID ', p_game_id, 'has been terminated.') AS TerminationMessage;
	ELSE 
		SELECT CONCAT('No active game wfound with ID ', p_game_id) AS TerminationMessage;
	END IF;
    
    COMMIT;
END $$

DELIMITER ;

DELIMITER $$
-- Procedure to update a players data from inputs made from the front end application admin dashboard
CREATE PROCEDURE UpdateUserInfo(
    IN p_user_id INT,
    IN p_username VARCHAR(100),
    IN p_score INT,
    IN p_health INT,
    IN p_is_admin BOOLEAN
)
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occured while updateing user information.';
	END;
    
    START TRANSACTION;
    
    UPDATE `User`
    SET 
        username = p_username,
        score = p_score,
        health = p_health,
        is_admin = p_is_admin
    WHERE id = p_user_id;
    
    IF ROW_COUNT() = 0 THEN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No user found with the specified ID or no changes made.';
	END IF;
    
    COMMIT;
END$$

DELIMITER ;

DELIMITER $$
-- Procedure to delete a player from the User table from the admin dashboard
CREATE PROCEDURE DeletePlayerByUsername(
	IN p_username VARCHAR(100)
)
BEGIN
	DECLARE user_exists INT;
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
		ROLLBACK;
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error occurred wile attempting to delete the player.';
	END;
    
    START TRANSACTION;
    
    SELECT COUNT(*) INTO user_exists
    FROM `User`
    WHERE username = p_username;
    
    IF user_exists > 0 THEN
		DELETE FROM	`User`
        WHERE username = p_username;
		
        COMMIT;
        
		SELECT CONCAT('Player "', p_username, '" has been deleted.') AS SuccessMessage;
    ELSE
		ROLLBACK;
        SIGNAL SQLSTATE '45000';
		SELECT CONCAT('Player with username "', p_username, '" does not exist.');
	END IF;
END $$

DELIMITER ;

-- Test Calls for Transactional Procedures

-- Player Login
CALL PlayerLogin('player1', 'pass123', @login_successful, @account_locked);
SELECT @login_successful, @account_locked;

-- Registration
CALL RegisterPlayer('testUser1', 'testpass123', @registration_successful);
SELECT @registration_successful;

-- Create New Game
CALL CreateNewGame(@game_id);
SELECT @game_id;

-- Get Map Dimensions for Game Board
CALL GetMapDimensionsForGame(1, @max_rows, @max_columns, @map_id);
SELECT @max_rows, @max_columns, @map_id;

-- Get Tiles for Game
CALL GetTilesForGame(1);

-- Add Item to Tile
CALL AddItemToTile(1, 2);

-- Populate Items On Board
CALL PopulateItemsOnBoard(1);

-- Get User Details
CALL GetUserDetails('player1', @user_id, @score, @is_admin, @health, @inventory_id);
SELECT @user_id, @score, @is_admin, @health, @inventory_id;

-- Add User To Game
CALL AddUserToGame(1, 1);

-- Assign User To Home Tile
CALL AssignUserToHomeTile(1, 1);

-- Move Player To Tile
CALL MovePlayerToTile(1, 0, 0, 0, 1);

-- Update Player Score for Cheese
CALL UpdatePlayerScoreForCheese(1, 0, 1);

-- Add Item to player inventory
CALL AddItemToPlayerInventory(1, 0, 1);

-- Handle Mouse Trap Movement
CALL HandleMouseTrapMovement(1, 0, 0);

-- Get All Users
CALL GetAllUsers();

-- Get All User Games
CALL GetAllUserGames();

-- Terminate Game
CALL TerminateGame(1);

-- Update User Info
CALL UpdateUserInfo(1, 'player1', 100, 80, TRUE);

-- Delete Player by username
CALL DeletePlayerByUsername('player1');