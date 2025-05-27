-- Create a user-defined table type for variants
CREATE TYPE [dbo].[VariantTableType] AS TABLE(
    [Name] NVARCHAR(MAX),
    [Slug] NVARCHAR(MAX),
    [Image] NVARCHAR(MAX),
    [Price] DECIMAL(18, 2),
    [Stock] INT,
    [IsPrimary] BIT
)
GO