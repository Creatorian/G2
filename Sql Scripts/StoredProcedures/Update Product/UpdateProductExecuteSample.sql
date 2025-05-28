DECLARE @UpdateVariants AS VariantUpdateTableType;

-- Update Europe Edition (VariantId 5)
INSERT INTO @UpdateVariants (VariantId, Name, Slug, Image, Price, Stock, IsPrimary)
VALUES (5, 'Europe Edition', 'ticket-europe', 'ticket_europe.jpg', 69.99, 20, 1);

-- Delete Nordic variant by OMITTING it from the list
-- Add new Asia Edition (VariantId = NULL)
INSERT INTO @UpdateVariants (VariantId, Name, Slug, Image, Price, Stock, IsPrimary)
VALUES (NULL, 'Asia Edition', 'ticket-asia', 'ticket_asia.jpg', 74.99, 15, 0);

EXEC [dbo].[UpdateProduct]
    @ProductId = 11,
    @ProductName = 'Ticket to Ride: Modern Edition',
    @ProductSlug = 'ticket-to-ride-modern',
    @Description = 'Updated railway adventure game',
    @CategoryId = 1,
    @Variants = @UpdateVariants;