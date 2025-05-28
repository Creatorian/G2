USE [Gnome]
GO

/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 5/27/2025 9:03:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[UpdateProduct]
    @ProductId INT,
    @ProductName NVARCHAR(MAX),
    @ProductSlug NVARCHAR(MAX),
    @Description NVARCHAR(MAX) = NULL,
    @CategoryId INT,
    @Variants VariantUpdateTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verify product exists
        IF NOT EXISTS(SELECT 1 FROM [dbo].[Products] WHERE [Id] = @ProductId)
        BEGIN
            RAISERROR('Product does not exist.', 16, 1);
            RETURN;
        END

        -- Verify category exists
        IF NOT EXISTS(SELECT 1 FROM [dbo].[Categories] WHERE [Id] = @CategoryId)
        BEGIN
            RAISERROR('Category does not exist.', 16, 1);
            RETURN;
        END

        -- Validate variant ownership (if any provided)
        IF EXISTS (
            SELECT 1 FROM @Variants tv
            WHERE tv.VariantId IS NOT NULL
            AND NOT EXISTS (
                SELECT 1 FROM [dbo].[Variants] v
                WHERE v.[Id] = tv.[VariantId]
                AND v.[ProductId] = @ProductId
            )
        )
        BEGIN
            RAISERROR('Invalid variant IDs provided.', 16, 1);
            RETURN;
        END

        -- Update product details
        UPDATE [dbo].[Products]
        SET 
            [Name] = @ProductName,
            [Slug] = @ProductSlug,
            [Description] = @Description,
            [CategoryId] = @CategoryId
        WHERE 
            [Id] = @ProductId;

        -- Remove variants
        DELETE FROM [dbo].[Variants]
        WHERE 
            [ProductId] = @ProductId
            AND [Id] NOT IN (SELECT [VariantId] FROM @Variants WHERE [VariantId] IS NOT NULL);

        -- Update existing variants
        UPDATE v
        SET 
            v.[Name] = tv.[Name],
            v.[Slug] = tv.[Slug],
            v.[Image] = tv.[Image],
            v.[Price] = tv.[Price],
            v.[Stock] = tv.[Stock],
            v.[IsPrimary] = tv.[IsPrimary]
        FROM 
            [dbo].[Variants] v
        INNER JOIN 
            @Variants tv ON v.[Id] = tv.[VariantId]
        WHERE 
            v.[ProductId] = @ProductId;

        -- Add new variants
        INSERT INTO [dbo].[Variants] (
            [Name],
            [Slug],
            [Image],
            [Price],
            [Stock],
            [IsPrimary],
            [CreatedDateTime],
            [ProductId]
        )
        SELECT 
            [Name],
            [Slug],
            [Image],
            [Price],
            [Stock],
            [IsPrimary],
            GETDATE(),
            @ProductId
        FROM 
            @Variants
        WHERE 
            [VariantId] IS NULL;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO