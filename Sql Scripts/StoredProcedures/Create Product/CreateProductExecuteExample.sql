DECLARE @GameVariants AS [dbo].[VariantTableType];

-- Insert variants for the game
INSERT INTO @GameVariants ([Name], [Slug], [Image], [Price], [Stock], [IsPrimary])
VALUES
    ('Standard Edition', 'taco-cat-standard', 'taco-cat-standard.jpg', 19.99, 50, 1),
    ('Taco Puck Maple Syrup Canoe', 'taco-puck-maple-syrup-canoe', 'taco-puck-maple-syrup-canoe.jpg', 24.99, 30, 0),
    ('Santa Cookie Elf Candy Snowman', 'santa-cookie-elf-candy-snowman', 'santa-cookie-elf-candy-snowman.jpg', 24.99, 20, 0),
    ('Taco Cat Goat Cheese Pizza Halloween Edition', 'taco-cat-goat-cheese-pizza-halloween-edition', 'taco-cat-goat-cheese-pizza-halloween-edition.jpg', 29.99, 10, 0),
    ('Taco Hat Cake Gift Pizza', 'taco-hat-cake-gift-pizza', 'taco-hat-cake-gift-pizza.jpg', 19.99, 10, 0),
    ('Taco Cat Goat Cheese Pizza On The Flip Side', 'taco-cat-goat-cheese-pizza-on-the-flip-side', 'taco-cat-goat-cheese-pizza-on-the-flip-side.jpg', 14.99, 10, 0);

-- Execute the stored procedure
EXEC [dbo].[CreateProduct]
    @ProductName = 'Taco Cat Goat Cheese Pizza',
    @ProductSlug = 'taco-cat-goat-cheese-pizza',
    @Description = 'A chaotic card game where players race to slap matching cards, combining elements of tacos, cats, goats, cheese, and pizza! Fast-paced fun for 2-8 players.',
    @CategoryId = 3,
    @Variants = @GameVariants;