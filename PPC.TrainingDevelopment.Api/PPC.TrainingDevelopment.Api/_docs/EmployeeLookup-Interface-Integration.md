# EmployeeLookup Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the EmployeeLookup API endpoints for frontend development.

## Data Model

### EmployeeLookup Object

```json
{
  "personnelNumber": "string (max 20 chars)",
  "firstName": "string (max 50 chars)",
  "lastName": "string (max 50 chars)",
  "knownName": "string (max 50 chars, optional)",
  "initials": "string (max 10 chars, optional)",
  "race": "string (max 50 chars, optional)",
  "gender": "string (max 20 chars, optional)",
  "disability": true,
  "eeLevel": "string (max 50 chars, optional)",
  "eeCategory": "string (max 50 chars, optional)"
}
```

### Properties

| Property          | Type    | Required | Description                                    |
| ----------------- | ------- | -------- | ---------------------------------------------- |
| `personnelNumber` | string  | Yes      | Unique personnel number (max 20 characters)    |
| `firstName`       | string  | Yes      | Employee's first name (max 50 characters)      |
| `lastName`        | string  | Yes      | Employee's last name (max 50 characters)       |
| `knownName`       | string  | No       | Known/preferred name (max 50 characters)       |
| `initials`        | string  | No       | Employee's initials (max 10 characters)        |
| `race`            | string  | No       | Employee's race category (max 50 characters)   |
| `gender`          | string  | No       | Employee's gender (max 20 characters)          |
| `disability`      | boolean | No       | Whether employee has a disability              |
| `eeLevel`         | string  | No       | Employment Equity level (max 50 characters)    |
| `eeCategory`      | string  | No       | Employment Equity category (max 50 characters) |

## API Endpoints

### Base URL

```
/api/EmployeeLookup
```

### Authentication

Most endpoints require authorization. Include JWT token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

**Note:** The `GET /{personnelNumber}` endpoint allows anonymous access.

### Endpoints

#### 1. Get All Employee Lookup Records

```http
GET /api/EmployeeLookup
```

**Authentication:** Required

**Response:** Array of EmployeeLookup objects

#### 2. Get Employee by Personnel Number

```http
GET /api/EmployeeLookup/{personnelNumber}
```

**Authentication:** Anonymous (no token required)

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number

**Response:** Single EmployeeLookup object

#### 3. Search Employees

```http
GET /api/EmployeeLookup/search/{searchTerm}
```

**Authentication:** Required

**Parameters:**

- `searchTerm` (string) - Search term to match against first name, last name, known name, or personnel number

**Response:** Array of matching EmployeeLookup objects

#### 4. Get Employees by Race

```http
GET /api/EmployeeLookup/race/{race}
```

**Authentication:** Required

**Parameters:**

- `race` (string) - The race category

**Response:** Array of EmployeeLookup objects

#### 5. Get Employees by Gender

```http
GET /api/EmployeeLookup/gender/{gender}
```

**Authentication:** Required

**Parameters:**

- `gender` (string) - The gender category

**Response:** Array of EmployeeLookup objects

#### 6. Get Employees by Employment Equity Level

```http
GET /api/EmployeeLookup/ee-level/{eeLevel}
```

**Authentication:** Required

**Parameters:**

- `eeLevel` (string) - The Employment Equity level

**Response:** Array of EmployeeLookup objects

#### 7. Get Employees by Employment Equity Category

```http
GET /api/EmployeeLookup/ee-category/{eeCategory}
```

**Authentication:** Required

**Parameters:**

- `eeCategory` (string) - The Employment Equity category

**Response:** Array of EmployeeLookup objects

#### 8. Get Employees by Disability Status

```http
GET /api/EmployeeLookup/disability/{hasDisability}
```

**Authentication:** Required

**Parameters:**

- `hasDisability` (boolean) - Disability status (true/false)

**Response:** Array of EmployeeLookup objects

#### 9. Create New Employee Lookup Record

```http
POST /api/EmployeeLookup
```

**Authentication:** Required

**Request Body:**

```json
{
  "personnelNumber": "string (required, max 20 chars)",
  "firstName": "string (required, max 50 chars)",
  "lastName": "string (required, max 50 chars)",
  "knownName": "string (optional, max 50 chars)",
  "initials": "string (optional, max 10 chars)",
  "race": "string (optional, max 50 chars)",
  "gender": "string (optional, max 20 chars)",
  "disability": true,
  "eeLevel": "string (optional, max 50 chars)",
  "eeCategory": "string (optional, max 50 chars)"
}
```

**Response:** Created EmployeeLookup object

#### 10. Update Employee Lookup Record

```http
PUT /api/EmployeeLookup/{personnelNumber}
```

**Authentication:** Required

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number to update

**Request Body:**

```json
{
  "firstName": "string (required, max 50 chars)",
  "lastName": "string (required, max 50 chars)",
  "knownName": "string (optional, max 50 chars)",
  "initials": "string (optional, max 10 chars)",
  "race": "string (optional, max 50 chars)",
  "gender": "string (optional, max 20 chars)",
  "disability": true,
  "eeLevel": "string (optional, max 50 chars)",
  "eeCategory": "string (optional, max 50 chars)"
}
```

**Response:** Updated EmployeeLookup object

#### 11. Delete Employee Lookup Record

```http
DELETE /api/EmployeeLookup/{personnelNumber}
```

**Authentication:** Required

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number to delete

**Response:** Success message

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface EmployeeLookup {
  personnelNumber: string;
  firstName: string;
  lastName: string;
  knownName?: string;
  initials?: string;
  race?: string;
  gender?: string;
  disability?: boolean;
  eeLevel?: string;
  eeCategory?: string;
}

interface CreateEmployeeLookupRequest {
  personnelNumber: string;
  firstName: string;
  lastName: string;
  knownName?: string;
  initials?: string;
  race?: string;
  gender?: string;
  disability?: boolean;
  eeLevel?: string;
  eeCategory?: string;
}

interface UpdateEmployeeLookupRequest {
  firstName: string;
  lastName: string;
  knownName?: string;
  initials?: string;
  race?: string;
  gender?: string;
  disability?: boolean;
  eeLevel?: string;
  eeCategory?: string;
}
```

### Sample API Calls

#### Get employee by personnel number (anonymous access):

```javascript
const response = await fetch("/api/EmployeeLookup/EMP001", {
  headers: {
    "Content-Type": "application/json",
  },
});
const employee = await response.json();
```

#### Search for employees:

```javascript
const response = await fetch("/api/EmployeeLookup/search/John", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const employees = await response.json();
```

#### Get employees by gender:

```javascript
const response = await fetch("/api/EmployeeLookup/gender/Female", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const femaleEmployees = await response.json();
```

#### Create a new employee lookup record:

```javascript
const newEmployee = {
  personnelNumber: "EMP002",
  firstName: "Jane",
  lastName: "Smith",
  knownName: "Jane",
  initials: "J.S.",
  race: "White",
  gender: "Female",
  disability: false,
  eeLevel: "Senior",
  eeCategory: "Management",
};

const response = await fetch("/api/EmployeeLookup", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newEmployee),
});
const created = await response.json();
```

#### Update an employee lookup record:

```javascript
const updatedEmployee = {
  firstName: "Jane",
  lastName: "Smith-Johnson",
  knownName: "Jane",
  initials: "J.S.J.",
  race: "White",
  gender: "Female",
  disability: false,
  eeLevel: "Executive",
  eeCategory: "Senior Management",
};

const response = await fetch("/api/EmployeeLookup/EMP002", {
  method: "PUT",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(updatedEmployee),
});
const updated = await response.json();
```

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `201 Created` - Successfully created
- `400 Bad Request` - Invalid request data or employee already exists
- `401 Unauthorized` - Missing or invalid authorization (for secured endpoints)
- `404 Not Found` - Employee not found
- `500 Internal Server Error` - Server error

Error responses include a message field:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Business Rules

- Personnel numbers must be unique across all employee records
- When creating a new employee, the system checks for existing personnel numbers
- The `GET /{personnelNumber}` endpoint is accessible without authentication for integration purposes
- All other endpoints require proper JWT authentication
- Search functionality matches against first name, last name, known name, and personnel number
- Employment Equity (EE) data is optional but important for compliance reporting

## Notes

- Personnel number serves as the primary key for employee lookup records
- All string fields respect maximum length constraints as specified
- The disability field is a nullable boolean (true/false/null)
- Race, gender, EE level, and EE category values should ideally be standardized across the system
- Consider using LookupValue API for standardized values for dropdown fields
