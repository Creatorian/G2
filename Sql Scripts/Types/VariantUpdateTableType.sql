CREATE TYPE [dbo].[VariantUpdateTableType] AS TABLE(
    [VariantId] INT NULL,  -- NULL = new variant, NOT NULL = existing variant
    [Name] NVARCHAR(MAX),
    [Slug] NVARCHAR(MAX),
    [Image] NVARCHAR(MAX),
    [Price] DECIMAL(18, 2),
    [Stock] INT,
    [IsPrimary] BIT
)
GO