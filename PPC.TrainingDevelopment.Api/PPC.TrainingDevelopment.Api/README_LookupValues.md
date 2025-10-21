# LookupValues API Documentation

## Overview

The LookupValues API provides CRUD operations for managing lookup values in the PPC Training Development system. Lookup values are used to store reference data like employee statuses, departments, gender options, race categories, and training types.

## Database Schema

The `LookupValues` table has the following structure:

| Column     | Type          | Constraints                          | Description                                      |
| ---------- | ------------- | ------------------------------------ | ------------------------------------------------ |
| LookupId   | int           | IDENTITY(1,1), PRIMARY KEY, NOT NULL | Unique identifier for the lookup value           |
| LookupType | nvarchar(50)  | NOT NULL                             | Category or type of the lookup value             |
| Value      | nvarchar(100) | NOT NULL                             | The display value                                |
| Code       | nvarchar(20)  | NULL                                 | Optional code for the lookup value               |
| ParentId   | int           | NULL                                 | Reference to parent lookup for hierarchical data |
| SortOrder  | int           | NULL                                 | Order for displaying lookup values               |
| IsActive   | bit           | NOT NULL                             | Indicates if the lookup value is active          |

## API Endpoints

### Base URL

`https://localhost:7001/api/lookupvalues`

### Authentication

All endpoints require JWT Bearer token authentication.

### Endpoints

#### 1. Get All Lookup Values

```http
GET /api/lookupvalues
```

Returns all lookup values with their hierarchical relationships.

#### 2. Get Lookup Value by ID

```http
GET /api/lookupvalues/{id}
```

Returns a specific lookup value by its ID.

#### 3. Get Lookup Values by Type

```http
GET /api/lookupvalues/type/{type}
```

Returns all lookup values for a specific type (e.g., "Department", "Gender").

#### 4. Get Active Lookup Values by Type

```http
GET /api/lookupvalues/type/{type}/active
```

Returns only active lookup values for a specific type.

#### 5. Get Children of Parent Lookup

```http
GET /api/lookupvalues/parent/{parentId}/children
```

Returns all child lookup values for a specific parent.

#### 6. Create Lookup Value

```http
POST /api/lookupvalues
Content-Type: application/json

{
  "lookupType": "string",
  "value": "string",
  "code": "string (optional)",
  "parentId": number (optional),
  "sortOrder": number (optional),
  "isActive": boolean
}
```

#### 7. Update Lookup Value

```http
PUT /api/lookupvalues/{id}
Content-Type: application/json

{
  "lookupType": "string",
  "value": "string",
  "code": "string (optional)",
  "parentId": number (optional),
  "sortOrder": number (optional),
  "isActive": boolean
}
```

#### 8. Delete Lookup Value

```http
DELETE /api/lookupvalues/{id}
```

Note: Cannot delete lookup values that have children.

## Pre-seeded Data

The system automatically seeds the following lookup types on startup:

### Employee Status

- Active (ACTIVE)
- Inactive (INACTIVE)
- Terminated (TERM)

### Gender

- Male (M)
- Female (F)
- Other (O)

### Race

- African (AFR)
- Coloured (COL)
- Indian (IND)
- White (WHT)

### Department

- Information Technology (IT)
- Human Resources (HR)
- Finance (FIN)
- Operations (OPS)

### EE Level

- Top Management (TM)
- Senior Management (SM)
- Professionally Qualified (PQ)
- Skilled Technical (ST)
- Semi-skilled (SS)
- Unskilled (US)

### Training Status

- Not Started (NS)
- In Progress (IP)
- Completed (COMP)
- Cancelled (CANC)

### Training Type

- Skills Development (SD)
- Learnership (LRN)
- Internship (INT)
- Bursary (BUR)

## Error Handling

The API returns appropriate HTTP status codes:

- `200 OK` - Successful operation
- `201 Created` - Resource created successfully
- `400 Bad Request` - Invalid request data or validation errors
- `401 Unauthorized` - Missing or invalid authentication token
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

## Testing

Use the included `LookupValues.http` file to test the API endpoints with VS Code REST Client extension or similar tools.

## Dependencies

- Entity Framework Core 9.0.9
- Microsoft.EntityFrameworkCore.SqlServer 9.0.9
- ASP.NET Core 8.0

## Database Connection

The API uses SQL Server LocalDB by default. Connection strings are configured in:

- `appsettings.json` - Production database
- `appsettings.Development.json` - Development database
