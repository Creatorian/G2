USE Gnome;
GO

BEGIN TRY
    BEGIN TRANSACTION;

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
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Gloomhaven', 'gloomhaven', 'Cooperative strategy legacy game', GETDATE(), @StrategyId);
    SELECT @GloomhavenId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Terraforming Mars', 'terraforming-mars', 'Engineer Mars ecosystem', GETDATE(), @StrategyId);
    SELECT @TerraformingMarsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Twilight Imperium IV', 'twilight-imperium-4', 'Epic space empire building', GETDATE(), @StrategyId);
    SELECT @TwilightImperiumId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Scythe', 'scythe', 'Alternate-history 1920s Europe', GETDATE(), @StrategyId);
    SELECT @ScytheId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Root', 'root', 'Asymmetric woodland warfare', GETDATE(), @StrategyId);
    SELECT @RootId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Brass: Birmingham', 'brass-birmingham', 'Industrial revolution strategy', GETDATE(), @StrategyId);
    SELECT @BrassBirminghamId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Spirit Island', 'spirit-island', 'Defend island from invaders', GETDATE(), @StrategyId);
    SELECT @SpiritIslandStrategyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Ark Nova', 'ark-nova', 'Zoo management simulation', GETDATE(), @StrategyId);
    SELECT @ArkNovaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Great Western Trail', 'great-western-trail', 'Cattle drive strategy', GETDATE(), @StrategyId);
    SELECT @GreatWesternTrailId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Dune: Imperium', 'dune-imperium', 'Bluffing and worker placement', GETDATE(), @StrategyId);
    SELECT @DuneImperiumId = SCOPE_IDENTITY();


    -- Family Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Ticket to Ride', 'ticket-to-ride', 'Railway adventure game', GETDATE(), @FamilyId);
    SELECT @TicketToRideId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Carcassonne', 'carcassonne', 'Tile-placement medieval game', GETDATE(), @FamilyId);
    SELECT @CarcassonneId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Azul', 'azul', 'Abstract mosaic-building', GETDATE(), @FamilyId);
    SELECT @AzulId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Kingdomino', 'kingdomino', 'Domino kingdom-building', GETDATE(), @FamilyId);
    SELECT @KingdominoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Pandemic', 'pandemic', 'Save the world from diseases', GETDATE(), @FamilyId);
    SELECT @PandemicFamilyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Splendor', 'splendor', 'Gemstone trading engine', GETDATE(), @FamilyId);
    SELECT @SplendorId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Catan', 'catan', 'Classic resource trading', GETDATE(), @FamilyId);
    SELECT @CatanId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Dixit', 'dixit', 'Creative storytelling game', GETDATE(), @FamilyId);
    SELECT @DixitId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Patchwork', 'patchwork', 'Tetris-like quilting game', GETDATE(), @FamilyId);
    SELECT @PatchworkId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Wingspan', 'wingspan', 'Bird-collection engine builder', GETDATE(), @FamilyId);
    SELECT @WingspanId = SCOPE_IDENTITY();


    -- Party Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Codenames', 'codenames', 'Word association game', GETDATE(), @PartyId);
    SELECT @CodenamesId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Just One', 'just-one', 'Cooperative clue-giving', GETDATE(), @PartyId);
    SELECT @JustOneId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Telestrations', 'telestrations', 'Pictionary telephone', GETDATE(), @PartyId);
    SELECT @TelestrationsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Cards Against Humanity', 'cards-against-humanity', 'Adult party game', GETDATE(), @PartyId);
    SELECT @CardsAgainstHumanityId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Wavelength', 'wavelength', 'Psychic guessing game', GETDATE(), @PartyId);
    SELECT @WavelengthId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Decrypto', 'decrypto', 'Code-breaking team game', GETDATE(), @PartyId);
    SELECT @DecryptoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('The Resistance', 'the-resistance', 'Social deduction game', GETDATE(), @PartyId);
    SELECT @TheResistanceId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Monikers', 'monikers', 'Progressive charades', GETDATE(), @PartyId);
    SELECT @MonikersId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Concept', 'concept', 'Visual guessing game', GETDATE(), @PartyId);
    SELECT @ConceptId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Joking Hazard', 'joking-hazard', 'Comic creation game', GETDATE(), @PartyId);
    SELECT @JokingHazardId = SCOPE_IDENTITY();


    -- Cooperative Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Pandemic Legacy: Season 1', 'pandemic-legacy-s1', 'Campaign-style Pandemic', GETDATE(), @CooperativeId);
    SELECT @PandemicLegacyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Gloomhaven: Jaws of the Lion', 'jaws-of-lion', 'Entry-level Gloomhaven', GETDATE(), @CooperativeId);
    SELECT @JawsOfLionId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Spirit Island', 'spirit-island-coop', 'Cooperative island defense', GETDATE(), @CooperativeId);
    SELECT @SpiritIslandCoopId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Marvel Champions', 'marvel-champions', 'Superhero card game', GETDATE(), @CooperativeId);
    SELECT @MarvelChampionsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('The Crew', 'the-crew', 'Trick-taking space game', GETDATE(), @CooperativeId);
    SELECT @TheCrewId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Mysterium', 'mysterium', 'Ghostly murder mystery', GETDATE(), @CooperativeId);
    SELECT @MysteriumId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Forbidden Island', 'forbidden-island', 'Treasure recovery game', GETDATE(), @CooperativeId);
    SELECT @ForbiddenIslandId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Hanabi', 'hanabi', 'Firework card game', GETDATE(), @CooperativeId);
    SELECT @HanabiId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Robinson Crusoe', 'robinson-crusoe', 'Survival adventure', GETDATE(), @CooperativeId);
    SELECT @RobinsonCrusoeId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('The Mind', 'the-mind', 'Synchronized number play', GETDATE(), @CooperativeId);
    SELECT @TheMindId = SCOPE_IDENTITY();


    -- Deck-building Games
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Dominion', 'dominion', 'Original deck-builder', GETDATE(), @DeckBuildingId);
    SELECT @DominionId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Clank!', 'clank', 'Deck-building adventure', GETDATE(), @DeckBuildingId);
    SELECT @ClankId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Aeon''s End', 'aeons-end', 'Cooperative deck-builder', GETDATE(), @DeckBuildingId);
    SELECT @AeonsEndId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Star Realms', 'star-realms', 'Space combat deck-builder', GETDATE(), @DeckBuildingId);
    SELECT @StarRealmsId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Legendary: Marvel', 'legendary-marvel', 'Superhero deck-builder', GETDATE(), @DeckBuildingId);
    SELECT @LegendaryMarvelId = SCOPE_IDENTITY();


    -- Eurogames
    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Agricola', 'agricola', 'Farming strategy game', GETDATE(), @EurogameId);
    SELECT @AgricolaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Terra Mystica', 'terra-mystica', 'Fantasy territory control', GETDATE(), @EurogameId);
    SELECT @TerraMysticaId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Puerto Rico', 'puerto-rico', 'Colonial strategy game', GETDATE(), @EurogameId);
    SELECT @PuertoRicoId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Castles of Burgundy', 'castles-burgundy', 'Tile-placement strategy', GETDATE(), @EurogameId);
    SELECT @CastlesBurgundyId = SCOPE_IDENTITY();

    INSERT INTO Products (Name, Slug, Description, CreatedDateTime, CategoryId)
    VALUES ('Viticulture', 'viticulture', 'Wine-making strategy', GETDATE(), @EurogameId);
    SELECT @ViticultureId = SCOPE_IDENTITY();

    -- =====================
    -- 3. Insert Variants
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

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH