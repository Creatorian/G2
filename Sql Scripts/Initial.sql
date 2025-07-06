USE Gnome;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- ======================
    -- 0. Drop existing data and recreate tables
    -- ======================

    -- Drop existing data (in reverse dependency order)
    DELETE FROM ProductCategories;
    DELETE FROM Variants;
    DELETE FROM Products;
    DELETE FROM Categories;

    -- Reset ID counters for all tables
    DBCC CHECKIDENT ('Categories', RESEED, 0);
    DBCC CHECKIDENT ('Products', RESEED, 0);
    DBCC CHECKIDENT ('Variants', RESEED, 0);

    -- ======================
    -- 1. Insert Categories
    -- ======================

    DECLARE @StrategyId INT, 
            @FamilyId INT,
            @PartyId INT,
            @CooperativeId INT,
            @DeckBuildingId INT,
            @EurogameId INT;

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Strategy', 'strategy', GETDATE());
    SELECT @StrategyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Family', 'family', GETDATE());
    SELECT @FamilyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Party', 'party', GETDATE());
    SELECT @PartyId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Cooperative', 'cooperative', GETDATE());
    SELECT @CooperativeId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Deck-building', 'deck-building', GETDATE());
    SELECT @DeckBuildingId = SCOPE_IDENTITY();

    INSERT INTO Categories (Name, Slug, CreatedDateTime)
    VALUES ('Eurogame', 'eurogame', GETDATE());
    SELECT @EurogameId = SCOPE_IDENTITY();

    -- =====================
    -- 2. Insert Products
    -- =====================

    DECLARE @GloomhavenId INT,
            @TerraformingMarsId INT,
            @TwilightImperiumId INT,
            @ScytheId INT,
            @RootId INT,
            @BrassBirminghamId INT,
            @SpiritIslandStrategyId INT,
            @ArkNovaId INT,
            @GreatWesternTrailId INT,
            @DuneImperiumId INT,
            @TicketToRideId INT,
            @CarcassonneId INT,
            @AzulId INT,
            @KingdominoId INT,
            @PandemicFamilyId INT,
            @SplendorId INT,
            @CatanId INT,
            @DixitId INT,
            @PatchworkId INT,
            @WingspanId INT,
            @CodenamesId INT,
            @JustOneId INT,
            @TelestrationsId INT,
            @CardsAgainstHumanityId INT,
            @WavelengthId INT,
            @DecryptoId INT,
            @TheResistanceId INT,
            @MonikersId INT,
            @ConceptId INT,
            @JokingHazardId INT,
            @PandemicLegacyId INT,
            @JawsOfLionId INT,
            @SpiritIslandCoopId INT,
            @MarvelChampionsId INT,
            @TheCrewId INT,
            @MysteriumId INT,
            @ForbiddenIslandId INT,
            @HanabiId INT,
            @RobinsonCrusoeId INT,
            @TheMindId INT,
            @DominionId INT,
            @ClankId INT,
            @AeonsEndId INT,
            @StarRealmsId INT,
            @LegendaryMarvelId INT,
            @AgricolaId INT,
            @TerraMysticaId INT,
            @PuertoRicoId INT,
            @CastlesBurgundyId INT,
            @ViticultureId INT;

    -- Strategy Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Gloomhaven', 'gloomhaven', 'Cooperative strategy legacy game', GETDATE());
    SELECT @GloomhavenId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Terraforming Mars', 'terraforming-mars', 'Engineer Mars ecosystem', GETDATE());
    SELECT @TerraformingMarsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Twilight Imperium IV', 'twilight-imperium-4', 'Epic space empire building', GETDATE());
    SELECT @TwilightImperiumId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Scythe', 'scythe', 'Alternate-history 1920s Europe', GETDATE());
    SELECT @ScytheId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Root', 'root', 'Asymmetric woodland warfare', GETDATE());
    SELECT @RootId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Brass: Birmingham', 'brass-birmingham', 'Industrial revolution strategy', GETDATE());
    SELECT @BrassBirminghamId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Spirit Island', 'spirit-island', 'Defend island from invaders', GETDATE());
    SELECT @SpiritIslandStrategyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Ark Nova', 'ark-nova', 'Zoo management simulation', GETDATE());
    SELECT @ArkNovaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Great Western Trail', 'great-western-trail', 'Cattle drive strategy', GETDATE());
    SELECT @GreatWesternTrailId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Dune: Imperium', 'dune-imperium', 'Bluffing and worker placement', GETDATE());
    SELECT @DuneImperiumId = SCOPE_IDENTITY();

    -- Family Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Ticket to Ride', 'ticket-to-ride', 'Railway adventure game', GETDATE());
    SELECT @TicketToRideId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Carcassonne', 'carcassonne', 'Tile-placement medieval game', GETDATE());
    SELECT @CarcassonneId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Azul', 'azul', 'Abstract mosaic-building', GETDATE());
    SELECT @AzulId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Kingdomino', 'kingdomino', 'Domino kingdom-building', GETDATE());
    SELECT @KingdominoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Pandemic', 'pandemic', 'Save the world from diseases', GETDATE());
    SELECT @PandemicFamilyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Splendor', 'splendor', 'Gemstone trading engine', GETDATE());
    SELECT @SplendorId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Catan', 'catan', 'Classic resource trading', GETDATE());
    SELECT @CatanId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Dixit', 'dixit', 'Creative storytelling game', GETDATE());
    SELECT @DixitId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Patchwork', 'patchwork', 'Tetris-like quilting game', GETDATE());
    SELECT @PatchworkId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Wingspan', 'wingspan', 'Bird-collection engine builder', GETDATE());
    SELECT @WingspanId = SCOPE_IDENTITY();

    -- Party Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Codenames', 'codenames', 'Word association game', GETDATE());
    SELECT @CodenamesId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Just One', 'just-one', 'Cooperative clue-giving', GETDATE());
    SELECT @JustOneId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Telestrations', 'telestrations', 'Pictionary telephone', GETDATE());
    SELECT @TelestrationsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Cards Against Humanity', 'cards-against-humanity', 'Adult party game', GETDATE());
    SELECT @CardsAgainstHumanityId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Wavelength', 'wavelength', 'Psychic guessing game', GETDATE());
    SELECT @WavelengthId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Decrypto', 'decrypto', 'Code-breaking team game', GETDATE());
    SELECT @DecryptoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('The Resistance', 'the-resistance', 'Social deduction game', GETDATE());
    SELECT @TheResistanceId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Monikers', 'monikers', 'Progressive charades', GETDATE());
    SELECT @MonikersId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Concept', 'concept', 'Visual guessing game', GETDATE());
    SELECT @ConceptId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Joking Hazard', 'joking-hazard', 'Comic creation game', GETDATE());
    SELECT @JokingHazardId = SCOPE_IDENTITY();

    -- Cooperative Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Pandemic Legacy: Season 1', 'pandemic-legacy-s1', 'Campaign-style Pandemic', GETDATE());
    SELECT @PandemicLegacyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Gloomhaven: Jaws of the Lion', 'jaws-of-lion', 'Entry-level Gloomhaven', GETDATE());
    SELECT @JawsOfLionId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Spirit Island', 'spirit-island-coop', 'Cooperative island defense', GETDATE());
    SELECT @SpiritIslandCoopId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Marvel Champions', 'marvel-champions', 'Superhero card game', GETDATE());
    SELECT @MarvelChampionsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('The Crew', 'the-crew', 'Trick-taking space game', GETDATE());
    SELECT @TheCrewId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Mysterium', 'mysterium', 'Ghostly murder mystery', GETDATE());
    SELECT @MysteriumId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Forbidden Island', 'forbidden-island', 'Treasure recovery game', GETDATE());
    SELECT @ForbiddenIslandId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Hanabi', 'hanabi', 'Firework card game', GETDATE());
    SELECT @HanabiId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Robinson Crusoe', 'robinson-crusoe', 'Survival adventure', GETDATE());
    SELECT @RobinsonCrusoeId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('The Mind', 'the-mind', 'Synchronized number play', GETDATE());
    SELECT @TheMindId = SCOPE_IDENTITY();

    -- Deck-building Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Dominion', 'dominion', 'Original deck-builder', GETDATE());
    SELECT @DominionId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Clank!', 'clank', 'Deck-building adventure', GETDATE());
    SELECT @ClankId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Aeon''s End', 'aeons-end', 'Cooperative deck-builder', GETDATE());
    SELECT @AeonsEndId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Star Realms', 'star-realms', 'Space combat deck-builder', GETDATE());
    SELECT @StarRealmsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Legendary: Marvel', 'legendary-marvel', 'Superhero deck-builder', GETDATE());
    SELECT @LegendaryMarvelId = SCOPE_IDENTITY();

    -- Eurogames
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Agricola', 'agricola', 'Farming strategy game', GETDATE());
    SELECT @AgricolaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Terra Mystica', 'terra-mystica', 'Fantasy territory control', GETDATE());
    SELECT @TerraMysticaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Puerto Rico', 'puerto-rico', 'Colonial strategy game', GETDATE());
    SELECT @PuertoRicoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Castles of Burgundy', 'castles-burgundy', 'Tile-placement strategy', GETDATE());
    SELECT @CastlesBurgundyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime)
    VALUES ('Viticulture', 'viticulture', 'Wine-making strategy', GETDATE());
    SELECT @ViticultureId = SCOPE_IDENTITY();

    -- =====================
    -- 3. Create Product-Category Relationships (Many-to-Many)
    -- =====================

    -- Strategy Games
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@GloomhavenId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TerraformingMarsId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TwilightImperiumId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ScytheId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@RootId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@BrassBirminghamId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@SpiritIslandStrategyId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ArkNovaId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@GreatWesternTrailId, @StrategyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@DuneImperiumId, @StrategyId);

    -- Family Games
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TicketToRideId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CarcassonneId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@AzulId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@KingdominoId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@PandemicFamilyId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@SplendorId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CatanId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@DixitId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@PatchworkId, @FamilyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@WingspanId, @FamilyId);

    -- Party Games
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CodenamesId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@JustOneId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TelestrationsId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CardsAgainstHumanityId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@WavelengthId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@DecryptoId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TheResistanceId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@MonikersId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ConceptId, @PartyId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@JokingHazardId, @PartyId);

    -- Cooperative Games
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@PandemicLegacyId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@JawsOfLionId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@SpiritIslandCoopId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@MarvelChampionsId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TheCrewId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@MysteriumId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ForbiddenIslandId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@HanabiId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@RobinsonCrusoeId, @CooperativeId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TheMindId, @CooperativeId);

    -- Deck-building Games
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@DominionId, @DeckBuildingId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ClankId, @DeckBuildingId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@AeonsEndId, @DeckBuildingId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@StarRealmsId, @DeckBuildingId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@LegendaryMarvelId, @DeckBuildingId);

    -- Eurogames
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@AgricolaId, @EurogameId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@TerraMysticaId, @EurogameId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@PuertoRicoId, @EurogameId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@CastlesBurgundyId, @EurogameId);
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@ViticultureId, @EurogameId);

    -- Multi-category games (some games belong to multiple categories)
    -- Gloomhaven is both Strategy and Cooperative
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@GloomhavenId, @CooperativeId);
    
    -- Spirit Island is both Strategy and Cooperative
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@SpiritIslandStrategyId, @CooperativeId);
    
    -- Pandemic is both Family and Cooperative
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@PandemicFamilyId, @CooperativeId);
    
    -- Wingspan is both Family and Eurogame
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@WingspanId, @EurogameId);
    
    -- Aeon's End is both Deck-building and Cooperative
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@AeonsEndId, @CooperativeId);
    
    -- Legendary: Marvel is both Deck-building and Cooperative
    INSERT INTO ProductCategories (ProductId, CategoryId) VALUES (@LegendaryMarvelId, @CooperativeId);

    -- =====================
    -- 4. Insert Variants
    -- =====================

    -- Gloomhaven Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Standard Edition', 'gloomhaven-standard', 'gloomhaven.jpg', 139.99, 15, 1, GETDATE(), @GloomhavenId),
    ('Frosthaven', 'gloomhaven-frost', 'frosthaven.jpg', 169.99, 8, 0, GETDATE(), @GloomhavenId);

    -- Terraforming Mars Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Ares Expedition', 'mars-ares', 'ares.jpg', 49.99, 20, 0, GETDATE(), @TerraformingMarsId),
    ('Prelude Expansion', 'mars-prelude', 'prelude.jpg', 29.99, 30, 0, GETDATE(), @TerraformingMarsId);

    -- Ticket to Ride Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Europe Edition', 'ticket-europe', 'ticket_europe.jpg', 59.99, 25, 1, GETDATE(), @TicketToRideId),
    ('Nordic Countries', 'ticket-nordic', 'ticket_nordic.jpg', 54.99, 18, 0, GETDATE(), @TicketToRideId);

    -- Codenames Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Duet', 'codenames-duet', 'codenames_duet.jpg', 19.99, 40, 0, GETDATE(), @CodenamesId),
    ('Disney Edition', 'codenames-disney', 'codenames_disney.jpg', 29.99, 15, 0, GETDATE(), @CodenamesId);

	-- Pandemic Legacy Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Season 1', 'pandemic-s1', 'pandemic_s1.jpg', 79.99, 8, 1, GETDATE(), @PandemicLegacyId),
    ('Season 2', 'pandemic-s2', 'pandemic_s2.jpg', 79.99, 6, 0, GETDATE(), @PandemicLegacyId);

    -- Marvel Champions Expansions
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Red Skull', 'marvel-red-skull', 'marvel_redskull.jpg', 39.99, 15, 0, GETDATE(), @MarvelChampionsId),
    ('Galaxy''s Most Wanted', 'marvel-galaxy', 'marvel_galaxy.jpg', 44.99, 12, 0, GETDATE(), @MarvelChampionsId);

    -- Azul Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Summer Pavilion', 'azul-summer', 'azul_summer.jpg', 49.99, 20, 0, GETDATE(), @AzulId),
    ('Stained Glass', 'azul-glass', 'azul_glass.jpg', 44.99, 18, 0, GETDATE(), @AzulId);

    -- Wingspan Expansions
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('European', 'wingspan-europe', 'wingspan_europe.jpg', 29.99, 25, 0, GETDATE(), @WingspanId),
    ('Oceania', 'wingspan-oceania', 'wingspan_oceania.jpg', 34.99, 20, 0, GETDATE(), @WingspanId);

    -- Catan Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Seafarers', 'catan-seafarers', 'catan_seafarers.jpg', 44.99, 15, 0, GETDATE(), @CatanId),
    ('Cities & Knights', 'catan-cities', 'catan_cities.jpg', 49.99, 12, 0, GETDATE(), @CatanId);

    -- Scythe Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Wind Gambit', 'scythe-wind', 'scythe_wind.jpg', 29.99, 10, 0, GETDATE(), @ScytheId),
    ('Invaders from Afar', 'scythe-invaders', 'scythe_invaders.jpg', 34.99, 8, 0, GETDATE(), @ScytheId);

    -- Spirit Island Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Branch & Claw', 'spirit-branch', 'branch_claw.jpg', 49.99, 12, 0, GETDATE(), @SpiritIslandStrategyId),
    ('Jagged Earth', 'spirit-jagged', 'jagged_earth.jpg', 59.99, 9, 0, GETDATE(), @SpiritIslandStrategyId);

    -- Dominion Expansions
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Seaside', 'dominion-seaside', 'dominion_seaside.jpg', 44.99, 15, 0, GETDATE(), @DominionId),
    ('Prosperity', 'dominion-prosperity', 'dominion_prosperity.jpg', 49.99, 12, 0, GETDATE(), @DominionId);

    -- Clank! Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Sunken Treasures', 'clank-sunken', 'clank_sunken.jpg', 34.99, 18, 0, GETDATE(), @ClankId),
    ('Mummy''s Curse', 'clank-mummy', 'clank_mummy.jpg', 39.99, 14, 0, GETDATE(), @ClankId);

    -- Agricola Variants
    INSERT INTO Variants (Name, Slug, Image, Price, Stock, IsPrimary, CreatedDateTime, ProductId)
    VALUES 
    ('Farmers of the Moor', 'agricola-farmers', 'agricola_farmers.jpg', 49.99, 10, 0, GETDATE(), @AgricolaId),
    ('Revised Edition', 'agricola-revised', 'agricola_revised.jpg', 69.99, 20, 1, GETDATE(), @AgricolaId);

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