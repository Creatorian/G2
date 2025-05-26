CREATE OR ALTER PROCEDURE [dbo].[CreateProduct]
    @ProductName NVARCHAR(MAX),
    @ProductSlug NVARCHAR(MAX),
    @Description NVARCHAR(MAX) = NULL,
    @CategoryId INT,
    @Variants VariantTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewProductId INT;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verify category exists
        IF NOT EXISTS(SELECT 1 FROM [dbo].[Categories] WHERE [Id] = @CategoryId)
        BEGIN
            RAISERROR('Category does not exist', 16, 1);
            RETURN;
        END

        -- Insert new product
        INSERT INTO [dbo].[Products] (
            [Name],
            [Slug],
            [Description],
            [CreatedDateTime],
            [CategoryId]
        ) VALUES (
            @ProductName,
            @ProductSlug,
            @Description,
            GETDATE(),
            @CategoryId
        );

        SET @NewProductId = SCOPE_IDENTITY();

        -- Insert variants if any exist
        IF EXISTS(SELECT 1 FROM @Variants)
        BEGIN
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
                @NewProductId
            FROM @Variants;
        END

        COMMIT TRANSACTION;
        
        -- Return the new product ID
        SELECT @NewProductId AS Id;
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