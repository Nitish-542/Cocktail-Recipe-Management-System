# Cocktail Recipe App API Documentation

## Overview
The **Cocktail Recipe App** provides an API for managing cocktail recipes, bartenders, and cocktail categories. This API allows users to create, read, update, and delete cocktail recipes, with related information about bartenders and categories.

## Base URL
The base URL for all API endpoints is:

https://localhost:7093/api/

yaml
Copy
Edit


## Endpoints

### 1. Create Cocktail
- **URL**: `/cocktail/create`
- **Method**: `POST`
- **Description**: Creates a new cocktail in the system.
- **Request Body**:

```json
{
  "BartenderId": 2,
  "DrinkName": "Mojito",
  "DrinkRecipe": "Muddle mint leaves with lime juice, add rum & soda.",
  "LiqIns": "White Rum",
  "MixIns": "Mint, Lime, Sugar, Soda",
  "CategoryId": 1,
  "DatePosted": "2024-02-10T00:00:00"
}
Response:
json
Copy
Edit
```
{
  "DrinkId": 18,
  "BartenderId": 2,
  "DrinkName": "Mojito",
  "DrinkRecipe": "Muddle mint leaves with lime juice, add rum & soda.",
  "LiqIns": "White Rum",
  "MixIns": "Mint, Lime, Sugar, Soda",
  "CategoryName": "Refreshing",
  "DatePosted": "2024-02-10T00:00:00"
}
```
###2. Get Cocktail by ID
URL: /cocktail/get/{id}
Method: GET
Description: Retrieves a specific cocktail by its ID.
Parameters:
id (required): The ID of the cocktail.
Response:
json
Copy
Edit
```
{
  "DrinkId": 18,
  "BartenderId": 2,
  "DrinkName": "Mojito",
  "DrinkRecipe": "Muddle mint leaves with lime juice, add rum & soda.",
  "LiqIns": "White Rum",
  "MixIns": "Mint, Lime, Sugar, Soda",
  "CategoryName": "Refreshing",
  "DatePosted": "2024-02-10T00:00:00"
}
```
###3. Get All Cocktails
URL: /cocktail/list
Method: GET
Description: Retrieves a list of all cocktails in the system.
Response:
json
Copy
Edit
```
[
  {
    "DrinkId": 18,
    "BartenderId": 2,
    "DrinkName": "Mojito",
    "DrinkRecipe": "Muddle mint leaves with lime juice, add rum & soda.",
    "LiqIns": "White Rum",
    "MixIns": "Mint, Lime, Sugar, Soda",
    "CategoryName": "Refreshing",
    "DatePosted": "2024-02-10T00:00:00"
  },
  {
    "DrinkId": 24,
    "BartenderId": 3,
    "DrinkName": "Martini",
    "DrinkRecipe": "Stir gin and vermouth with ice, strain into a glass.",
    "LiqIns": "Gin, Dry Vermouth",
    "MixIns": "Olive",
    "CategoryName": "Classic",
    "DatePosted": "2024-02-12T00:00:00"
  }
```
]
###4. Update Cocktail
URL: /cocktail/update/{id}
Method: PUT
Description: Updates an existing cocktail's details.
Parameters:
id (required): The ID of the cocktail to update.
Request Body:
json
Copy
Edit
```
{
  "BartenderId": 2,
  "DrinkName": "Mojito",
  "DrinkRecipe": "Muddle mint leaves with lime juice, add rum & soda.",
  "LiqIns": "White Rum",
  "MixIns": "Mint, Lime, Sugar, Soda",
  "CategoryId": 1,
  "DatePosted": "2024-02-10T00:00:00"
}
```
Response: 204 No Content
###5. Delete Cocktail by ID
URL: /cocktail/delete/{id}
Method: DELETE
Description: Deletes a specific cocktail by ID.
Parameters:
id (required): The ID of the cocktail to delete.
Response: 204 No Content
##Data Models
CocktailDTO
DrinkId: The ID of the cocktail.
BartenderId: The ID of the bartender who created the cocktail.
DrinkName: The name of the cocktail.
DrinkRecipe: The recipe for preparing the cocktail.
LiqIns: The liquor ingredients.
MixIns: The mixing ingredients.
CategoryName: The category of the cocktail (e.g., "Classic", "Refreshing").
DatePosted: The date when the cocktail was posted.
##Error Handling
400 Bad Request: The request is malformed or missing required fields.
404 Not Found: The requested resource could not be found.
500 Internal Server Error: A server-side error occurred.

###Conclusion
This API allows easy management of cocktail recipes, ensuring that bartenders and their drinks can be efficiently added, updated, retrieved, and deleted.
