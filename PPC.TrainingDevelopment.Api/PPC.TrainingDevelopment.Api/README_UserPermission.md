# User Permissions API

## Overview

The User Permissions API provides endpoints to manage user permissions within the Training Development system. This API allows you to create, read, update, and delete permission assignments for employees.

## Entity Structure

### UserPermission

| Field          | Type     | Description                                         |
| -------------- | -------- | --------------------------------------------------- |
| PermissionId   | int      | Unique identifier (Primary Key)                     |
| PersonnelNo    | string   | Employee personnel number (Foreign Key to Employee) |
| PermissionCode | string   | Code representing the permission                    |
| CreatedDate    | DateTime | Timestamp when permission was granted               |

## Business Rules

- Each permission must reference a valid employee (PersonnelNo must exist in Employee table)
- Personnel number and permission code combination must be unique
- CreatedDate is automatically set when creating a permission
- Supports cascading delete (if employee is deleted, their permissions are removed)

## API Endpoints

### GET /api/UserPermission

Get all user permissions with employee details included.

**Response:**

```json
[
  {
    "permissionId": 1,
    "personnelNo": "EMP001",
    "permissionCode": "READ_EMPLOYEES",
    "createdDate": "2024-10-28T10:30:00Z",
    "employee": {
      "personnelNumber": "EMP001",
      "firstName": "John",
      "lastName": "Doe"
    }
  }
]
```

### GET /api/UserPermission/{id}

Get a specific user permission by ID.

**Parameters:**

- `id` (int): Permission ID

**Response:** Single UserPermission object or 404 if not found

### GET /api/UserPermission/personnel/{personnelNo}

Get all permissions for a specific employee.

**Parameters:**

- `personnelNo` (string): Employee personnel number

**Response:** Array of UserPermission objects

### GET /api/UserPermission/permission-code/{permissionCode}

Get all users who have a specific permission.

**Parameters:**

- `permissionCode` (string): Permission code to search for

**Response:** Array of UserPermission objects

### GET /api/UserPermission/check/{personnelNo}/{permissionCode}

Check if a specific employee has a specific permission.

**Parameters:**

- `personnelNo` (string): Employee personnel number
- `permissionCode` (string): Permission code to check

**Response:**

```json
{
  "hasPermission": true
}
```

### GET /api/UserPermission/search?searchTerm={term}

Search permissions by personnel number, permission code, or employee name.

**Query Parameters:**

- `searchTerm` (string): Search term

**Response:** Array of matching UserPermission objects

### POST /api/UserPermission

Create a new user permission.

**Request Body:**

```json
{
  "personnelNo": "EMP001",
  "permissionCode": "READ_EMPLOYEES"
}
```

**Response:** Created UserPermission object with 201 status

**Error Scenarios:**

- 400: Employee does not exist
- 409: Permission already exists for this employee

### PUT /api/UserPermission/{id}

Update an existing user permission.

**Parameters:**

- `id` (int): Permission ID

**Request Body:**

```json
{
  "personnelNo": "EMP001",
  "permissionCode": "WRITE_EMPLOYEES"
}
```

**Response:** Updated UserPermission object or 404 if not found

### DELETE /api/UserPermission/{id}

Delete a user permission.

**Parameters:**

- `id` (int): Permission ID

**Response:** 200 with success message or 404 if not found

## Common Permission Codes

While permission codes are free-form strings, here are some suggested standard codes:

- `READ_EMPLOYEES` - View employee information
- `WRITE_EMPLOYEES` - Create/update employee information
- `DELETE_EMPLOYEES` - Delete employee records
- `READ_TRAINING_EVENTS` - View training events
- `WRITE_TRAINING_EVENTS` - Create/update training events
- `DELETE_TRAINING_EVENTS` - Delete training events
- `READ_REPORTS` - View reports
- `ADMIN_ACCESS` - Full administrative access
- `USER_MANAGEMENT` - Manage user permissions

## Error Handling

All endpoints return appropriate HTTP status codes:

- 200: Success
- 201: Created
- 400: Bad Request (validation errors)
- 401: Unauthorized
- 404: Not Found
- 409: Conflict (duplicate permissions)
- 500: Internal Server Error

## Authentication

All endpoints require JWT authentication. Include the token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

## Database Indexes

The UserPermissions table includes the following indexes for optimal performance:

- Primary key on PermissionId
- Index on PersonnelNo
- Index on PermissionCode
- Unique composite index on (PersonnelNo, PermissionCode)
- Index on CreatedDate

## Usage Examples

### Grant admin access to an employee

```http
POST /api/UserPermission
{
  "personnelNo": "EMP001",
  "permissionCode": "ADMIN_ACCESS"
}
```

### Check if employee can read reports

```http
GET /api/UserPermission/check/EMP001/READ_REPORTS
```

### Get all employees with admin access

```http
GET /api/UserPermission/permission-code/ADMIN_ACCESS
```

### Search for permissions related to a specific employee

```http
GET /api/UserPermission/search?searchTerm=EMP001
```
