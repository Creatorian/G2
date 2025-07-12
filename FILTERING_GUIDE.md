# Comprehensive Filtering Guide

## Overview
The API now supports comprehensive filtering for both Products and Categories with multiple filter types including text search, numeric ranges, boolean filters, and relationship-based filters.

## Product Filtering

### Filter Object Structure
```json
{
  "filter": {
    "name": "string",
    "slug": "string", 
    "description": "string",
    "short-description": "string",
    "number-of-players": "string",
    "playing-time": "string",
    "community-age": "string",
    "complexity": "string",
    "min-rating": 0.0,
    "max-rating": 5.0,
    "min-price": 0.00,
    "max-price": 999.99,
    "min-stock": 0,
    "max-stock": 1000,
    "awards": ["award1", "award2"],
    "category-ids": [1, 2, 3],
    "category-names": ["Strategy", "Family"],
    "has-images": true,
    "in-stock-only": true,
    "date-from": "2024-01-01T00:00:00Z",
    "date-to": "2024-12-31T23:59:59Z"
  }
}
```

### Filter Types

#### 1. Text-Based Filters (Case-Insensitive Contains Search)
- **name**: Search in product name
- **slug**: Search in product slug
- **description**: Search in product description
- **short-description**: Search in short description
- **number-of-players**: Search in number of players field
- **playing-time**: Search in playing time field
- **community-age**: Search in community age field
- **complexity**: Search in complexity field

#### 2. Numeric Range Filters
- **min-rating** / **max-rating**: Filter by rating range (0.0 - 5.0)
- **min-price** / **max-price**: Filter by price range
- **min-stock** / **max-stock**: Filter by stock quantity range

#### 3. Awards Filter
- **awards**: Array of award names to search for (any match)

#### 4. Category Filters
- **category-ids**: Array of category IDs to filter by
- **category-names**: Array of category names to search for (any match)

#### 5. Boolean Filters
- **has-images**: Filter products with/without images
- **in-stock-only**: Filter only products with stock > 0

#### 6. Date Range Filters
- **date-from**: Filter products created after this date
- **date-to**: Filter products created before this date

### Product Sorting Options
- `created-date-time` - Sort by creation date
- `name` - Sort by product name
- `price` - Sort by price
- `rating` - Sort by rating
- `stock` - Sort by stock quantity
- `complexity` - Sort by complexity
- `playing-time` - Sort by playing time

## Category Filtering

### Filter Object Structure
```json
{
  "filter": {
    "name": "string",
    "slug": "string",
    "has-products": true,
    "min-products-count": 0,
    "max-products-count": 100,
    "date-from": "2024-01-01T00:00:00Z",
    "date-to": "2024-12-31T23:59:59Z"
  }
}
```

### Filter Types

#### 1. Text-Based Filters (Case-Insensitive Contains Search)
- **name**: Search in category name
- **slug**: Search in category slug

#### 2. Boolean Filters
- **has-products**: Filter categories with/without products

#### 3. Numeric Range Filters
- **min-products-count** / **max-products-count**: Filter by number of products in category

#### 4. Date Range Filters
- **date-from**: Filter categories created after this date
- **date-to**: Filter categories created before this date

### Category Sorting Options
- `created-date-time` - Sort by creation date
- `name` - Sort by category name
- `slug` - Sort by category slug
- `products-count` - Sort by number of products

## API Usage Examples

### Example 1: Find all strategy games with rating above 4.0
```json
{
  "page": 1,
  "pageSize": 20,
  "sortBy": "rating",
  "sortOrder": "desc",
  "filter": {
    "category-names": ["Strategy"],
    "min-rating": 4.0
  }
}
```

### Example 2: Find family games under $50 with images
```json
{
  "page": 1,
  "pageSize": 20,
  "sortBy": "price",
  "sortOrder": "asc",
  "filter": {
    "category-names": ["Family"],
    "max-price": 50.00,
    "has-images": true
  }
}
```

### Example 3: Find award-winning games
```json
{
  "page": 1,
  "pageSize": 20,
  "sortBy": "name",
  "sortOrder": "asc",
  "filter": {
    "awards": ["Spiel des Jahres", "Golden Geek"]
  }
}
```

### Example 4: Find categories with more than 10 products
```json
{
  "page": 1,
  "pageSize": 20,
  "sortBy": "products-count",
  "sortOrder": "desc",
  "filter": {
    "min-products-count": 10
  }
}
```

### Example 5: Find products in stock with specific complexity
```json
{
  "page": 1,
  "pageSize": 20,
  "sortBy": "stock",
  "sortOrder": "desc",
  "filter": {
    "complexity": "Medium",
    "in-stock-only": true
  }
}
```

## Performance Considerations

### Indexes
The following database indexes are configured for optimal filtering performance:
- Product: `Slug`, `CreatedDateTime`, `Awards`
- Category: `Slug`, `CreatedDateTime`
- ProductCategory: `ProductId`, `CategoryId`
- Image: `ProductId`, `IsPrimary`, `CreatedDateTime`

### Query Optimization
- All text searches use `Contains()` for partial matching
- Numeric ranges use efficient `>=` and `<=` operators
- Boolean filters use `Any()` for relationship checks
- Date filters are optimized with proper indexing

### Best Practices
1. **Use specific filters** rather than broad text searches when possible
2. **Combine filters** to narrow down results efficiently
3. **Use pagination** to limit result sets
4. **Sort by indexed columns** for better performance
5. **Avoid very broad date ranges** unless necessary

## Error Handling
- Invalid filter values will be ignored (graceful degradation)
- Empty or null filter properties are safely handled
- Non-existent category IDs or names will return empty results
- Invalid date formats will be ignored

## Migration Notes
- The new filtering system is backward compatible
- Old API calls will continue to work with default filter values
- New filter properties are optional and can be omitted
- Existing pagination and sorting parameters remain unchanged 