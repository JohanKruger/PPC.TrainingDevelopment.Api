# Employee API Documentation

## Overview

The Employee API provides CRUD operations for managing employee records in the Training Development system. This API handles full employee data including personal information, Employment Equity details, and demographic information.

## Base URL

```
https://localhost:7071/api/employee
```

## Authentication

Most endpoints require JWT Bearer token authentication. The `GetByPersonnelNumber` endpoint allows anonymous access for basic employee lookups.

## Endpoints

### 1. Get All Employees

**GET** `/api/employee`

- **Description**: Retrieves all employee records ordered by last name, then first name
- **Authentication**: Required
- **Response**: Array of Employee objects

### 2. Get Employee by Personnel Number

**GET** `/api/employee/{personnelNumber}`

- **Description**: Retrieves a specific employee by personnel number
- **Authentication**: Anonymous (public access)
- **Parameters**:
  - `personnelNumber` (string): Employee's personnel number
- **Response**: Employee object or 404 if not found

### 3. Search Employees

**GET** `/api/employee/search/{searchTerm}`

- **Description**: Searches employees by first name, last name, known name, or personnel number
- **Authentication**: Required
- **Parameters**:
  - `searchTerm` (string): Search term (case-insensitive)
- **Response**: Array of matching Employee objects

### 4. Get Employees by Race

**GET** `/api/employee/race/{race}`

- **Description**: Retrieves employees filtered by race category
- **Authentication**: Required
- **Parameters**:
  - `race` (string): Race category
- **Response**: Array of Employee objects

### 5. Get Employees by Gender

**GET** `/api/employee/gender/{gender}`

- **Description**: Retrieves employees filtered by gender
- **Authentication**: Required
- **Parameters**:
  - `gender` (string): Gender category
- **Response**: Array of Employee objects

### 6. Get Employees by Employment Equity Level

**GET** `/api/employee/ee-level/{eeLevel}`

- **Description**: Retrieves employees filtered by Employment Equity level
- **Authentication**: Required
- **Parameters**:
  - `eeLevel` (string): Employment Equity level
- **Response**: Array of Employee objects

### 7. Get Employees by Employment Equity Category

**GET** `/api/employee/ee-category/{eeCategory}`

- **Description**: Retrieves employees filtered by Employment Equity category
- **Authentication**: Required
- **Parameters**:
  - `eeCategory` (string): Employment Equity category
- **Response**: Array of Employee objects

### 8. Get Employees by Disability Status

**GET** `/api/employee/disability/{hasDisability}`

- **Description**: Retrieves employees filtered by disability status
- **Authentication**: Required
- **Parameters**:
  - `hasDisability` (boolean): true for employees with disabilities, false for those without
- **Response**: Array of Employee objects

### 9. Create Employee

**POST** `/api/employee`

- **Description**: Creates a new employee record
- **Authentication**: Required
- **Request Body**: CreateEmployeeRequest object
- **Response**: Created Employee object with 201 status

### 10. Update Employee

**PUT** `/api/employee/{personnelNumber}`

- **Description**: Updates an existing employee record
- **Authentication**: Required
- **Parameters**:
  - `personnelNumber` (string): Employee's personnel number
- **Request Body**: UpdateEmployeeRequest object
- **Response**: Updated Employee object

### 11. Delete Employee

**DELETE** `/api/employee/{personnelNumber}`

- **Description**: Deletes an employee record
- **Authentication**: Required
- **Parameters**:
  - `personnelNumber` (string): Employee's personnel number
- **Response**: Success message or 404 if not found

## Data Models

### Employee

```json
{
  "personnelNumber": "string (20 chars, required)",
  "firstName": "string (50 chars, required)",
  "lastName": "string (50 chars, required)",
  "knownName": "string (50 chars, optional)",
  "initials": "string (10 chars, optional)",
  "race": "string (50 chars, optional)",
  "gender": "string (20 chars, optional)",
  "disability": "boolean (optional)",
  "eeLevel": "string (50 chars, optional)",
  "eeCategory": "string (50 chars, optional)"
}
```

### CreateEmployeeRequest

```json
{
  "personnelNumber": "string (20 chars, required)",
  "firstName": "string (50 chars, required)",
  "lastName": "string (50 chars, required)",
  "knownName": "string (50 chars, optional)",
  "initials": "string (10 chars, optional)",
  "race": "string (50 chars, optional)",
  "gender": "string (20 chars, optional)",
  "disability": "boolean (optional)",
  "eeLevel": "string (50 chars, optional)",
  "eeCategory": "string (50 chars, optional)"
}
```

### UpdateEmployeeRequest

```json
{
  "firstName": "string (50 chars, required)",
  "lastName": "string (50 chars, required)",
  "knownName": "string (50 chars, optional)",
  "initials": "string (10 chars, optional)",
  "race": "string (50 chars, optional)",
  "gender": "string (20 chars, optional)",
  "disability": "boolean (optional)",
  "eeLevel": "string (50 chars, optional)",
  "eeCategory": "string (50 chars, optional)"
}
```

## Error Responses

### 400 Bad Request

- Invalid model state
- Employee already exists (for create operations)
- Empty search term

### 401 Unauthorized

- Missing or invalid JWT token (for protected endpoints)

### 404 Not Found

- Employee not found

### 500 Internal Server Error

- Database connection issues
- Unexpected server errors

## Business Rules

1. **Personnel Number**: Must be unique across all employees
2. **Required Fields**: Personnel number, first name, and last name are mandatory
3. **Field Lengths**: All fields have maximum length constraints as specified in the data model
4. **Case Sensitivity**: Search operations are case-insensitive
5. **Ordering**: Results are ordered by last name, then first name for consistency

## Usage Examples

### Creating an Employee

```bash
curl -X POST "https://localhost:7071/api/employee" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "personnelNumber": "EMP001",
    "firstName": "John",
    "lastName": "Doe",
    "knownName": "Johnny",
    "race": "White",
    "gender": "Male",
    "disability": false,
    "eeLevel": "Senior",
    "eeCategory": "Management"
  }'
```

### Searching for Employees

```bash
curl -X GET "https://localhost:7071/api/employee/search/john" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### Getting Employee by Personnel Number

```bash
curl -X GET "https://localhost:7071/api/employee/EMP001"
```

## Integration Notes

- The Employee entity is separate from EmployeeLookup and serves different business purposes
- Employee records can be linked to training events through the PersonnelNumber field
- All demographic and Employment Equity fields support South African BBBEE requirements
- The API supports both internal employee management and training event association
