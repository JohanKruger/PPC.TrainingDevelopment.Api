# LookupValue Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the LookupValue API endpoints for frontend development.

## Data Model

### LookupValue Object

```json
{
  "lookupId": 1,
  "lookupType": "string (max 50 chars)",
  "value": "string (max 100 chars)",
  "code": "string (max 20 chars, optional)",
  "parentId": 1,
  "sortOrder": 1,
  "isActive": true,
  "parent": null,
  "children": []
}
```

### Properties

| Property     | Type          | Required             | Description                                            |
| ------------ | ------------- | -------------------- | ------------------------------------------------------ |
| `lookupId`   | integer       | Yes (auto-generated) | Unique identifier for the lookup value                 |
| `lookupType` | string        | Yes                  | Category/type of the lookup value (max 50 characters)  |
| `value`      | string        | Yes                  | Display value (max 100 characters)                     |
| `code`       | string        | No                   | Optional code for the lookup value (max 20 characters) |
| `parentId`   | integer       | No                   | ID of parent lookup value for hierarchical data        |
| `sortOrder`  | integer       | No                   | Sort order for display purposes                        |
| `isActive`   | boolean       | Yes                  | Whether the lookup value is active (default: true)     |
| `parent`     | LookupValue   | No                   | Parent lookup value object                             |
| `children`   | LookupValue[] | No                   | Array of child lookup values                           |

## API Endpoints

### Base URL

```
/api/LookupValues
```

### Authentication

All endpoints require authorization. Include JWT token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

### Endpoints

#### 1. Get All Lookup Values

```http
GET /api/LookupValues
```

**Response:** Array of LookupValue objects

#### 2. Get Lookup Value by ID

```http
GET /api/LookupValues/{id}
```

**Parameters:**

- `id` (integer) - The lookup value ID

**Response:** Single LookupValue object

#### 3. Get Lookup Values by Type

```http
GET /api/LookupValues/type/{type}
```

**Parameters:**

- `type` (string) - The lookup type

**Response:** Array of LookupValue objects

#### 4. Get Active Lookup Values by Type

```http
GET /api/LookupValues/type/{type}/active
```

**Parameters:**

- `type` (string) - The lookup type

**Response:** Array of active LookupValue objects

#### 5. Get Children of Parent Lookup Value

```http
GET /api/LookupValues/parent/{parentId}/children
```

**Parameters:**

- `parentId` (integer) - The parent lookup value ID

**Response:** Array of child LookupValue objects

#### 6. Create New Lookup Value

```http
POST /api/LookupValues
```

**Request Body:**

```json
{
  "lookupType": "string (required, max 50 chars)",
  "value": "string (required, max 100 chars)",
  "code": "string (optional, max 20 chars)",
  "parentId": 1,
  "sortOrder": 1,
  "isActive": true
}
```

**Response:** Created LookupValue object with generated ID

#### 7. Update Lookup Value

```http
PUT /api/LookupValues/{id}
```

**Parameters:**

- `id` (integer) - The lookup value ID to update

**Request Body:**

```json
{
  "lookupType": "string (required, max 50 chars)",
  "value": "string (required, max 100 chars)",
  "code": "string (optional, max 20 chars)",
  "parentId": 1,
  "sortOrder": 1,
  "isActive": true
}
```

**Response:** Updated LookupValue object

#### 8. Delete Lookup Value

```http
DELETE /api/LookupValues/{id}
```

**Parameters:**

- `id` (integer) - The lookup value ID to delete

**Response:** Success message

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface LookupValue {
  lookupId: number;
  lookupType: string;
  value: string;
  code?: string;
  parentId?: number;
  sortOrder?: number;
  isActive: boolean;
  parent?: LookupValue;
  children?: LookupValue[];
}

interface CreateLookupValueRequest {
  lookupType: string;
  value: string;
  code?: string;
  parentId?: number;
  sortOrder?: number;
  isActive: boolean;
}
```

### Sample API Calls

#### Get all active values for a specific type:

```javascript
const response = await fetch("/api/LookupValues/type/Department/active", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const departments = await response.json();
```

#### Create a new lookup value:

```javascript
const newLookupValue = {
  lookupType: "Department",
  value: "Human Resources",
  code: "HR",
  isActive: true,
};

const response = await fetch("/api/LookupValues", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newLookupValue),
});
const created = await response.json();
```

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `201 Created` - Successfully created
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Missing or invalid authorization
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

Error responses include a message field:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Notes

- All string fields are required unless marked as optional
- Maximum length constraints must be respected
- A lookup value cannot be its own parent
- Deleting a lookup value with children will fail
- The API supports hierarchical data through parentId relationships
