# Employee Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the Employee API endpoints for frontend development.

## Data Model

### Employee Object

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
  "eeCategory": "string (max 50 chars, optional)",
  "jobTitle": "string (max 100 chars, optional)",
  "jobGrade": "string (max 20 chars, optional)",
  "idNumber": "string (max 13 chars, optional)",
  "site": "string (max 100 chars, optional)",
  "highestQualification": "string (max 100 chars, optional)"
}
```

### Properties

| Property               | Type    | Required | Description                                           |
| ---------------------- | ------- | -------- | ----------------------------------------------------- |
| `personnelNumber`      | string  | Yes      | Unique personnel number (max 20 characters)           |
| `firstName`            | string  | Yes      | Employee's first name (max 50 characters)             |
| `lastName`             | string  | Yes      | Employee's last name (max 50 characters)              |
| `knownName`            | string  | No       | Known/preferred name (max 50 characters)              |
| `initials`             | string  | No       | Employee's initials (max 10 characters)               |
| `race`                 | string  | No       | Employee's race category (max 50 characters)          |
| `gender`               | string  | No       | Employee's gender (max 20 characters)                 |
| `disability`           | boolean | No       | Whether employee has a disability                     |
| `eeLevel`              | string  | No       | Employment Equity level (max 50 characters)           |
| `eeCategory`           | string  | No       | Employment Equity category (max 50 characters)        |
| `jobTitle`             | string  | No       | Employee's job title (max 100 characters)             |
| `jobGrade`             | string  | No       | Employee's job grade (max 20 characters)              |
| `idNumber`             | string  | No       | South African ID number (max 13 characters)           |
| `site`                 | string  | No       | Employee's work site location (max 100 characters)    |
| `highestQualification` | string  | No       | Employee's highest qualification (max 100 characters) |

## API Endpoints

### Base URL

```
/api/Employee
```

### Authentication

Most endpoints require authorization. Include JWT token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

**Note:** The `GET /{personnelNumber}` endpoint allows anonymous access.

### Endpoints

#### 1. Get All Employee Records

```http
GET /api/Employee
```

**Authentication:** Required

**Response:** Array of Employee objects

#### 2. Get Employee by Personnel Number

```http
GET /api/Employee/{personnelNumber}
```

**Authentication:** Anonymous (no token required)

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number

**Response:** Single Employee object

#### 3. Search Employees

```http
GET /api/Employee/search/{searchTerm}
```

**Authentication:** Required

**Parameters:**

- `searchTerm` (string) - Search term to match against first name, last name, known name, personnel number, job title, job grade, ID number, or site

**Response:** Array of matching Employee objects

#### 4. Get Employees by Race

```http
GET /api/Employee/race/{race}
```

**Authentication:** Required

**Parameters:**

- `race` (string) - The race category

**Response:** Array of Employee objects

#### 5. Get Employees by Gender

```http
GET /api/Employee/gender/{gender}
```

**Authentication:** Required

**Parameters:**

- `gender` (string) - The gender category

**Response:** Array of Employee objects

#### 6. Get Employees by Employment Equity Level

```http
GET /api/Employee/ee-level/{eeLevel}
```

**Authentication:** Required

**Parameters:**

- `eeLevel` (string) - The Employment Equity level

**Response:** Array of Employee objects

#### 7. Get Employees by Employment Equity Category

```http
GET /api/Employee/ee-category/{eeCategory}
```

**Authentication:** Required

**Parameters:**

- `eeCategory` (string) - The Employment Equity category

**Response:** Array of Employee objects

#### 8. Get Employees by Disability Status

```http
GET /api/Employee/disability/{hasDisability}
```

**Authentication:** Required

**Parameters:**

- `hasDisability` (boolean) - Disability status (true/false)

**Response:** Array of Employee objects

#### 9. Get Employees by Job Title

```http
GET /api/Employee/job-title/{jobTitle}
```

**Authentication:** Required

**Parameters:**

- `jobTitle` (string) - The job title

**Response:** Array of Employee objects

#### 10. Get Employees by Job Grade

```http
GET /api/Employee/job-grade/{jobGrade}
```

**Authentication:** Required

**Parameters:**

- `jobGrade` (string) - The job grade

**Response:** Array of Employee objects

#### 11. Get Employees by Site

```http
GET /api/Employee/site/{site}
```

**Authentication:** Required

**Parameters:**

- `site` (string) - The site location

**Response:** Array of Employee objects

#### 12. Get Employee by ID Number

```http
GET /api/Employee/id-number/{idNumber}
```

**Authentication:** Required

**Parameters:**

- `idNumber` (string) - The South African ID number

**Response:** Single Employee object

#### 13. Create New Employee Record

```http
POST /api/Employee
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
  "eeCategory": "string (optional, max 50 chars)",
  "jobTitle": "string (optional, max 100 chars)",
  "jobGrade": "string (optional, max 20 chars)",
  "idNumber": "string (optional, max 13 chars)",
  "site": "string (optional, max 100 chars)",
  "highestQualification": "string (optional, max 100 chars)"
}
```

**Response:** Created Employee object

#### 14. Update Employee Record

```http
PUT /api/Employee/{personnelNumber}
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
  "eeCategory": "string (optional, max 50 chars)",
  "jobTitle": "string (optional, max 100 chars)",
  "jobGrade": "string (optional, max 20 chars)",
  "idNumber": "string (optional, max 13 chars)",
  "site": "string (optional, max 100 chars)",
  "highestQualification": "string (optional, max 100 chars)"
}
```

**Response:** Updated Employee object

#### 15. Delete Employee Record

```http
DELETE /api/Employee/{personnelNumber}
```

**Authentication:** Required

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number to delete

**Response:** Success message

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface Employee {
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
  jobTitle?: string;
  jobGrade?: string;
  idNumber?: string;
  site?: string;
  highestQualification?: string;
}

interface CreateEmployeeRequest {
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
  jobTitle?: string;
  jobGrade?: string;
  idNumber?: string;
  site?: string;
  highestQualification?: string;
}

interface UpdateEmployeeRequest {
  firstName: string;
  lastName: string;
  knownName?: string;
  initials?: string;
  race?: string;
  gender?: string;
  disability?: boolean;
  eeLevel?: string;
  eeCategory?: string;
  jobTitle?: string;
  jobGrade?: string;
  idNumber?: string;
  site?: string;
  highestQualification?: string;
}
```

### Sample API Calls

#### Get employee by personnel number (anonymous access):

```javascript
const response = await fetch("/api/Employee/EMP001", {
  headers: {
    "Content-Type": "application/json",
  },
});
const employee = await response.json();
```

#### Search for employees:

```javascript
const response = await fetch("/api/Employee/search/John", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const employees = await response.json();
```

#### Get employees by gender:

```javascript
const response = await fetch("/api/Employee/gender/Female", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const femaleEmployees = await response.json();
```

#### Get employees by job title:

```javascript
const response = await fetch("/api/Employee/job-title/Software Developer", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const developers = await response.json();
```

#### Get employees by site:

```javascript
const response = await fetch("/api/Employee/site/Head Office", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const headOfficeEmployees = await response.json();
```

#### Get employee by ID number:

```javascript
const response = await fetch("/api/Employee/id-number/8001015009087", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const employee = await response.json();
```

#### Create a new employee record:

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
  jobTitle: "Software Developer",
  jobGrade: "P4",
  idNumber: "8001015009087",
  site: "Head Office",
  highestQualification: "Bachelor of Science in Computer Science",
};

const response = await fetch("/api/Employee", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newEmployee),
});
const created = await response.json();
```

#### Update an employee record:

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
  jobTitle: "Senior Software Developer",
  jobGrade: "P5",
  idNumber: "8001015009087",
  site: "Head Office",
  highestQualification: "Master of Science in Computer Science",
};

const response = await fetch("/api/Employee/EMP002", {
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
- Search functionality matches against first name, last name, known name, personnel number, job title, job grade, ID number, and site
- Employment Equity (EE) data is optional but important for compliance reporting

## Notes

- Personnel number serves as the primary key for employee records
- All string fields respect maximum length constraints as specified
- The disability field is a nullable boolean (true/false/null)
- Race, gender, EE level, and EE category values should ideally be standardized across the system
- Consider using LookupValue API for standardized values for dropdown fields
- The Employee entity is separate from EmployeeLookup and serves different business purposes
- Employee records can be linked to training events through the PersonnelNumber field
- All demographic and Employment Equity fields support South African BBBEE requirements
- ID number follows South African ID number format (13 digits)
- Job titles and grades should align with organizational structure
- Site information helps with geographical and organizational reporting
- Highest qualification field supports skills development and compliance reporting

## Changes Log

### Version 2.0 - October 2025

**New Fields Added:**

- `jobTitle` - Employee's job title (max 100 characters)
- `jobGrade` - Employee's job grade (max 20 characters)
- `idNumber` - South African ID number (max 13 characters)
- `site` - Employee's work site location (max 100 characters)
- `highestQualification` - Employee's highest qualification (max 100 characters)

**New API Endpoints:**

- `GET /api/Employee/job-title/{jobTitle}` - Get employees by job title
- `GET /api/Employee/job-grade/{jobGrade}` - Get employees by job grade
- `GET /api/Employee/site/{site}` - Get employees by site
- `GET /api/Employee/id-number/{idNumber}` - Get employee by ID number

**Enhanced Functionality:**

- Search endpoint now includes job title, job grade, ID number, and site in search criteria
- Create and Update endpoints support all new fields
- TypeScript interfaces updated to include new properties

**Frontend Impact:**

- All forms creating or updating employees must include the new optional fields
- Search functionality will return results based on expanded search criteria
- New filtering options available through dedicated endpoints
- Employee display components should accommodate additional fields

---

**Document Version:** 2.0  
**Last Updated:** October 21, 2025  
**API Version Compatibility:** v2.0+  
**Reference ID:** EMP-API-DOC-v2.0
