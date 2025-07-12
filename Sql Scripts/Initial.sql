USE Gnome;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- ======================
    -- 0. Drop existing data and recreate tables
    -- ======================

    -- Drop existing data (in reverse dependency order)
    DELETE FROM ProductCategories;
    DELETE FROM Products;
    DELETE FROM Categories;
    DELETE FROM Images;
    DELETE FROM AdminUsers;

    -- Reset ID counters for all tables
    DBCC CHECKIDENT ('Categories', RESEED, 0);
    DBCC CHECKIDENT ('Products', RESEED, 0);
    DBCC CHECKIDENT ('AdminUsers', RESEED, 0);
    DBCC CHECKIDENT ('Images', RESEED, 0);

    -- ======================
    -- 1. Insert Categories
    -- ======================

    DECLARE @StrategyId INT, 
            @FamilyId INT,
            @PartyId INT,
            @CooperativeId INT,
            @CardId INT,
            @EducationalId INT,
			@FantasyId INT;

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Strategy Games', 'strategy-games', GETDATE());
    SELECT @StrategyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Family Games', 'family-games', GETDATE());
    SELECT @FamilyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Party Games', 'party-games', GETDATE());
    SELECT @PartyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Cooperative Games', 'cooperative-games', GETDATE());
    SELECT @CooperativeId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Card Games', 'card-games', GETDATE());
    SELECT @CardId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Educational Games', 'educational-games', GETDATE());
    SELECT @EducationalId = SCOPE_IDENTITY();
	
    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Fantasy Games', 'fantasy-games', GETDATE());
    SELECT @FantasyId = SCOPE_IDENTITY();

    -- =====================
    -- 2. Insert Products
    -- =====================

    DECLARE @CatanId INT,
            @TicketToRideId INT,
            @CarcassonneId INT,
            @7WondersId INT,
            @KingOfTokyoId INT;


    INSERT INTO Products (Name, Slug, Description, ShortDescription, NumberOfPlayers, PlayingTime, CommunityAge, Complexity, Price, Rating, Stock, Awards, CreatedDateTime)
    VALUES ('Catan', 'catan', 'In CATAN (formerly The Settlers of Catan), players try to be the dominant force on the island of Catan by building settlements, cities and roads. On each turn dice are rolled to determine which resources the island produces. Players build structures by ''spending'' resources (sheep, wheat, wood, brick and ore) which are represented by the relevant resource cards; each land type, with the exception of the unproductive desert, produces a specific resource: hills produce brick, forests produce wood, mountains produce ore, fields produce wheat, and pastures produce sheep.

Set-up includes randomly placing large hexagonal tiles (each depicting one of the five resource-producing terrain types--or the desert) in a honeycomb shape and surrounding them with water tiles, some of which contain ports of exchange. A number disk, the value of which will correspond to the roll of two 6-sided dice, are placed on each terrain tile. Each player is given two settlements (think: houses) and roads (sticks) which are placed on intersections and borders of the terrain tiles. Players collect a hand of resource cards based on which terrain tiles their last-placed settlement is adjacent to. A robber pawn is placed on the desert tile.

A turn consists of rolling the dice, collecting resource cards based on this dice roll and the position of settlements (or upgraded cities—think: hotels), turning in resource cards (if possible and desired) for improvements, trading cards at a port, possibly playing a development card, or trading resource cards with other players. If the dice roll is a 7, the active player moves the robber to a new terrain tile and steals a resource card from another player who has a settlement adjacent to that tile.

Points are accumulated by building settlements and cities, having the longest road or the largest army (from some of the development cards), and gathering certain development cards that simply award victory points. When a player has gathered 10 points (some of which may be held in secret), s/he announces this and claims the win.

', 'Collect and trade resources to build up the island of Catan in this modern classic.', '3-4', '60-120 Min', '10+', '2.29', 49.99, 7.1, 15, '2025 BoardGameGeek Hall of Fame Inductee,2012 JoTa Best Game Released in Brazil Nominee,2012 JoTa Best Game Released in Brazil Critic Award,2011 Ludo Award Best Board Game Editor''s Choice Winner,2011 Jocul Anului în România Best Game in Romanian Winner,2011 Jocul Anului în România Best Game in Romanian Finalist,2005 Gra Roku Game of the Year Winner,2004 Hra roku Winner,2004 Hra roku Nominee,2002 Japan Boardgame Prize Best Japanese Game Nominee,2001 Origins Awards Hall of Fame Inductee,1996 Origins Awards Best Fantasy or Science Fiction Board Game Winner,1995 Spiel des Jahres Winner,1995 Meeples Choice Award Winner,1995 Meeples Choice Award Nominee,1995 Essener Feder Best Written Rules Winner,1995 Deutscher Spiele Preis Best Family/Adult Game Winner', GETDATE());
    SELECT @CatanId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, ShortDescription, NumberOfPlayers, PlayingTime, CommunityAge, Complexity, Price, Rating, Stock, Awards, CreatedDateTime)
    VALUES ('Ticket to Ride', 'ticket-to-ride', 'With elegantly simple gameplay, Ticket to Ride can be learned in under 15 minutes. Players collect cards of various types of train cars they then use to claim railway routes in North America. The longer the routes, the more points they earn. Additional points come to those who fulfill Destination Tickets – goal cards that connect distant cities; and to the player who builds the longest continuous route.

"The rules are simple enough to write on a train ticket – each turn you either draw more cards, claim a route, or get additional Destination Tickets," says Ticket to Ride author, Alan R. Moon. "The tension comes from being forced to balance greed – adding more cards to your hand, and fear – losing a critical route to a competitor."

Ticket to Ride continues in the tradition of Days of Wonder''s big format board games featuring high-quality illustrations and components including: an oversize board map of North America, 225 custom-molded train cars, 144 illustrated cards, and wooden scoring markers.

Since its introduction and numerous subsequent awards, Ticket to Ride has become the BoardGameGeek epitome of a "gateway game" -- simple enough to be taught in a few minutes, and with enough action and tension to keep new players involved and in the game for the duration.

Part of the Ticket to Ride series.', 'All aboard! Collect trains, choo-choo-choose your routes, fulfill your destination!', '2-5', '30-60 Min', '8+', '1.82', 39.99, 7.4, 20, '2025 BoardGameGeek Hall of Fame Inductee,2010 Hungarian Board Game Award Nominee,2008 Ludoteca Ideale Official Selection Winner,2008 Gra Roku Game of the Year Nominee,2006 Japan Boardgame Prize Best Japanese Game Winner,2006 Japan Boardgame Prize Best Japanese Game Nominee,2006 Hra roku Winner,2006 Hra roku Nominee,2005 Vuoden Peli Family Game of the Year Winner,2005 Vuoden Peli Family Game of the Year Nominee,2005 Juego del Año Winner,2005 Juego del Año Finalist,2005 Diana Jones Award for Excellence in Gaming Winner,2005 Diana Jones Award for Excellence in Gaming Nominee,2005 As d''Or - Jeu de l''Année Winner,2005 As d''Or - Jeu de l''Année Nominee,2005 Årets Spill Best Family Game Nominee,2005 Årets Spel Best Family Game Winner,2004 Tric Trac Nominee,2004 Spiel des Jahres Winner,2004 Spiel des Jahres Nominee,2004 Origins Awards Best Board Game Winner,2004 Nederlandse Spellenprijs Nominee,2004 Meeples Choice Award Winner,2004 Meeples Choice Award Nominee,2004 Japan Boardgame Prize Best Advanced Game Winner,2004 Japan Boardgame Prize Best Advanced Game Nominee,2004 International Gamers Awards - General Strategy; Multi-player Nominee', GETDATE());
    SELECT @TicketToRideId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, ShortDescription, NumberOfPlayers, PlayingTime, CommunityAge, Complexity, Price, Rating, Stock, Awards, CreatedDateTime)
    VALUES ('Carcassonne', 'carcassonne', 'Carcassonne is a tile placement game in which the players draw and place a tile with a piece of southern French landscape represented on it. The tile might feature a city, a road, a cloister, grassland or some combination thereof, and it must be placed adjacent to tiles that have already been played, in such a way that cities are connected to cities, roads to roads, et cetera. Having placed a tile, the player can then decide to place one of their meeples in one of the areas on it: in the city as a knight, on the road as a robber, in the cloister as a monk, or in the field as a farmer. When that area is complete that meeple scores points for its owner.

During a game of Carcassonne, players are faced with decisions like: "Is it really worth putting my last meeple there?" or "Should I use this tile to expand my city, or should I place it near my opponent instead, thus making it a harder for them to complete it and score points?" Since players place only one tile and have the option to place one meeple on it, turns proceed quickly even if it is a game full of options and possibilities.

First game in the Carcassonne series.

', 'Shape the medieval landscape of France, claiming cities, monasteries and farms.', '2-5', '30-45 Min', '7+', '1.89', 29.99, 7.4, 18, '2025 BoardGameGeek Hall of Fame Inductee,2012 Ludo Award Best Board Game Editor''s Choice Winner,2011 Jocul Anului în România Best Game in Romanian Finalist,2004 Vuoden Peli Family Game of the Year Winner,2004 Vuoden Peli Family Game of the Year Nominee,2004 Hra roku Nominee,2002 Årets Spel Best Family Game Winner,2001 Spiel des Jahres Winner,2001 Spiel des Jahres Nominee,2001 Spiel der Spiele Hit mit Freunden Recommended,2001 Nederlandse Spellenprijs Nominee,2001 International Gamers Awards - General Strategy; Multi-player Nominee,2001 Deutscher Spiele Preis Best Family/Adult Game Winner,2000 Meeples Choice Award Winner,2000 Meeples Choice Award Nominee', GETDATE());
    SELECT @CarcassonneId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, ShortDescription, NumberOfPlayers, PlayingTime, CommunityAge, Complexity, Price, Rating, Stock, Awards, CreatedDateTime)
    VALUES ('7 Wonders', '7-wonders', 'You are the leader of one of the 7 great cities of the Ancient World. Gather resources, develop commercial routes, and affirm your military supremacy. Build your city and erect an architectural wonder which will transcend future times.

7 Wonders lasts three ages. In each age, players receive seven cards from a particular deck, choose one of those cards, then pass the remainder to an adjacent player. Players reveal their cards simultaneously, paying resources if needed or collecting resources or interacting with other players in various ways. (Players have individual boards with special powers on which to organize their cards, and the boards are double-sided). Each player then chooses another card from the deck they were passed, and the process repeats until players have six cards in play from that age. After three ages, the game ends.

In essence, 7 Wonders is a card development game. Some cards have immediate effects, while others provide bonuses or upgrades later in the game. Some cards provide discounts on future purchases. Some provide military strength to overpower your neighbors and others give nothing but victory points. Each card is played immediately after being drafted, so you''ll know which cards your neighbor is receiving and how her choices might affect what you''ve already built up. Cards are passed left-right-left over the three ages, so you need to keep an eye on the neighbors in both directions.

Though the box of earlier editions is listed as being for 3–7 players, there is an official 2-player variant included in the instructions.', 'Draft cards to develop your ancient civilization and build its Wonder of the World.', '2-7', '30 Min', '10+', '2.32', 49.99, 7.7, 16, '2012 Ludoteca Ideale Winner,2011/2012 Boardgames Australia Awards Best International Game Nominee,2011 Vuoden Peli Adult Game of the Year Winner,2011 Vuoden Peli Adult Game of the Year Nominee,2011 Spiel des Jahres Kennerspiel Game of the Year Nominee,2011 Spiel des Jahres Kennerspiel des Jahres Winner,2011 Nederlandse Spellenprijs Nominee,2011 Lys Passioné Winner,2011 Lys Passioné Finalist,2011 Lucca Games Best Boardgame Winner,2011 Lucca Games Best Boardgame Nominee,2011 Juego del Año Tico Winner,2011 Juego del Año Tico Nominee,2011 Juego del Año Finalist,2011 JoTa Best Light Board Game Nominee,2011 JoTa Best Light Board Game Critic Award,2011 JoTa Best Light Board Game Audience Award,2011 JoTa Best Card Game Nominee,2011 JoTa Best Card Game Critic Award,2011 JoTa Best Card Game Audience Award,2011 JoTa Best Artwork Nominee,2011 JoTa Best Artwork Critic Award,2011 Japan Boardgame Prize Voters'' Selection Winner,2011 Japan Boardgame Prize Voters'' Selection Nominee,2011 International Gamers Award - General Strategy: Multi-player Winner,2011 International Gamers Award - General Strategy; Multi-player Nominee,2011 Hra roku Winner,2011 Hra roku Nominee,2011 Guldbrikken Best Adult Game Winner,2011 Guldbrikken Best Adult Game Nominee,2011 Gra Roku Game of the Year Nominee,2011 Gouden Ludo Winner,2011 Gouden Ludo Nominee,2011 Golden Geek Most Innovative Board Game Nominee,2011 Golden Geek Best Strategy Board Game Nominee,2011 Golden Geek Best Party Board Game Nominee,2011 Golden Geek Best Family Board Game Winner,2011 Golden Geek Best Family Board Game Nominee,2011 Golden Geek Best Card Game Winner,2011 Golden Geek Best Card Game Nominee,2011 Golden Geek Best Board Game Artwork/Presentation Nominee,2011 Fairplay À la carte Winner,2011 Deutscher Spiele Preis Best Family/Adult Game Winner,2011 As d''Or - Jeu de l''Année Prix du Jury Winner,2011 As d''Or - Jeu de l''Année Nominee,2010 Tric Trac Nominee,2010 Tric Trac d''Or,2010 Swiss Gamers Award Winner,2010 Meeples Choice Award Winner,2010 Meeples Choice Award Nominee', GETDATE());
    SELECT @7WondersId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, ShortDescription, NumberOfPlayers, PlayingTime, CommunityAge, Complexity, Price, Rating, Stock, Awards, CreatedDateTime)
    VALUES ('King of Tokyo', 'king-of-tokyo', 'In King of Tokyo, you play mutant monsters, gigantic robots, and strange aliens—all of whom are destroying Tokyo and whacking each other in order to become the one and only King of Tokyo.

At the start of each turn, you roll six dice, which show the following six symbols: 1, 2, or 3 Victory Points, Energy, Heal, and Attack. Over three successive throws, choose whether to keep or discard each die in order to win victory points, gain energy, restore health, or attack other players into understanding that Tokyo is YOUR territory.

The fiercest player will occupy Tokyo, and earn extra victory points, but that player can''t heal and must face all the other monsters alone!

Top this off with special cards purchased with energy that have a permanent or temporary effect, such as the growing of a second head which grants you an additional die, body armor, nova death ray, and more.... and it''s one of the most explosive games of the year!

In order to win the game, one must either destroy Tokyo by accumulating 20 victory points, or be the only surviving monster once the fighting has ended.

First Game in the King of Tokyo series

', 'Prove your dominance by destroying Tokyo or by being the last monster left standing.', '2-6', '30 Min', '8+', '1.48', 39.99, 7.1, 22, '2014 Gra Roku Game of the Year Winner,2014 Gra Roku Game of the Year Nominee,2013 Nederlandse Spellenprijs Best Family Game Winner,2013 Nederlandse Spellenprijs Best Family Game Nominee,2013 Juego del Año Tico Nominee,2013 Hra roku Nominee,2013 Guldbrikken Best Family Game Winner,2013 Guldbrikken Best Family Game Nominee,2013 Boardgames Australia Awards Best International Game Nominee,2012 Ludoteca Ideale Winner,2012 Gouden Ludo Nominee,2012 Golden Geek Best Thematic Board Game Nominee,2012 Golden Geek Best Party Game Winner,2012 Golden Geek Best Party Board Game Nominee,2012 Golden Geek Best Family Board Game Winner,2012 Golden Geek Best Family Board Game Nominee,2012 Golden Geek Best Children''s Game Winner,2012 Golden Geek Best Children''s Board Game Nominee,2012 Golden Geek Best Board Game Artwork/Presentation Nominee,2012 As d''Or - Jeu de l''Année Nominee,2011 Meeples Choice Award Nominee,2011 Lys Grand Public Finalist,2011 Lucca Games Best Family Game Nominee,2011 Japan Boardgame Prize Voters'' Selection Nominee,2011 Golden Geek Best Party Board Game Nominee', GETDATE());
    SELECT @KingOfTokyoId = SCOPE_IDENTITY();

    -- =====================
    -- 3. Create Product-Category Relationships (Many-to-Many)
	-- For testing purposes, some products will be in all categories
    -- =====================

    -- Catan
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CatanId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CatanId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CatanId, @PartyId);

    -- Ticket to Ride
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TicketToRideId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TicketToRideId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TicketToRideId, @CardId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TicketToRideId, @EducationalId);

    -- Carcassone
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @CardId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @EducationalId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @FantasyId);

    -- 7 Wonders
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @CardId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @EducationalId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@7WondersId, @FantasyId);

    -- King Of Tokyo
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @CardId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @EducationalId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingOfTokyoId, @FantasyId);



    -- =====================
    -- 4. Insert Images
    -- =====================

	-- Catan Images
	INSERT INTO Images (Url, IsPrimary, CreatedDateTime, ProductId) VALUES
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332145/gnome/products/3DBox_CATAN_BaseGame_NE_nk1iz7.png', 1, GETDATE(), 1),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332151/gnome/products/Board-Full-on-wood-copy_gjcwy3.webp', 0, GETDATE(), 1),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332157/gnome/products/deluxe-02-boxback_wwegli.jpg', 0, GETDATE(), 1);

	-- Ticket To Ride Images
	INSERT INTO Images (Url, IsPrimary, CreatedDateTime, ProductId) VALUES
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332448/gnome/products/91YNJM4oyhL._UF894_1000_QL80__e86ula.jpg', 1, GETDATE(), 2),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332455/gnome/products/Opened_ppspzi.webp', 0, GETDATE(), 2),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332451/gnome/products/images_cn5xeg.jpg', 0, GETDATE(), 2);

	-- Carcassone Images
	INSERT INTO Images (Url, IsPrimary, CreatedDateTime, ProductId) VALUES
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332679/gnome/products/ZM7810_box-right_lsok4z.webp', 1, GETDATE(), 3),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332676/gnome/products/images_ln8p3f.jpg', 0, GETDATE(), 3),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332675/gnome/products/Carcassonne_back_pgsaci.png', 0, GETDATE(), 3);

	-- 7 Wonders Images
	INSERT INTO Images (Url, IsPrimary, CreatedDateTime, ProductId) VALUES
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332957/gnome/products/images_1_bg74gr.jpg', 1, GETDATE(), 4),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332958/gnome/products/opened_mo821c.jpg', 0, GETDATE(), 4),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752332956/gnome/products/7-wonders-board-game-back_1000x_aidbcu.webp', 0, GETDATE(), 4);
	
	-- King Of Tokyo Images
	INSERT INTO Images (Url, IsPrimary, CreatedDateTime, ProductId) VALUES
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752333136/gnome/products/3760175513145_epp7fk.webp', 1, GETDATE(), 5),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752333143/gnome/products/Opened_joviza.jpg', 0, GETDATE(), 5),
	('https://res.cloudinary.com/dtwi4umvq/image/upload/v1752333147/gnome/products/ZBG-IEL002_BACK_yn1ohp.jpg', 0, GETDATE(), 5);

    -- =====================
    -- 5. Insert Admin User
    -- =====================

    INSERT INTO [Gnome].[dbo].[AdminUsers] 
    (
        [Username],
        [Email], 
        [PasswordHash],
        [FirstName],
        [LastName],
        [IsActive],
        [CreatedDateTime],
        [LastLoginDateTime],
        [LastPasswordChangeDateTime]
    )
    VALUES 
    (
        'gnome',
        'gnome@mailinator.com',
        'sD3fPKLnFKZUjnSV4qA/XoJOqsmDfNfxWcZ7kPtLc0I=',
        'Ajs',
        'Nigrutin',
        1,
        GETDATE(),
        NULL,
        GETDATE()
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH