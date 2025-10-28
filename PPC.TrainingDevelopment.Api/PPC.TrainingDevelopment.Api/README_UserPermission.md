# User Permissions API

## Overview

The User Permissions API provides endpoints to manage user permissions within the Training Development system. This API allows you to create, read, update, and delete permission assignments for users.

## Entity Structure

### UserPermission

| Field          | Type     | Description                           |
| -------------- | -------- | ------------------------------------- |
| PermissionId   | int      | Unique identifier (Primary Key)       |
| Username       | string   | Username (max 100 characters)         |
| PermissionCode | string   | Code representing the permission      |
| CreatedDate    | DateTime | Timestamp when permission was granted |

## Business Rules

- Username and permission code combination must be unique
- CreatedDate is automatically set when creating a permission
- Permission codes are case-sensitive

## API Endpoints

### GET /api/UserPermission

Get all user permissions.

**Response:**

```json
[
  {
    "permissionId": 1,
    "username": "john.doe",
    "permissionCode": "READ_EMPLOYEES",
    "createdDate": "2024-10-28T10:30:00Z"
  }
]
```

### GET /api/UserPermission/{id}

Get a specific user permission by ID.

**Parameters:**

- `id` (int): Permission ID

**Response:** Single UserPermission object or 404 if not found

### GET /api/UserPermission/username/{username}

Get all permissions for a specific user.

**Parameters:**

- `username` (string): Username

**Response:** Array of UserPermission objects

### GET /api/UserPermission/permission-code/{permissionCode}

Get all users who have a specific permission.

**Parameters:**

- `permissionCode` (string): Permission code to search for

**Response:** Array of UserPermission objects

### GET /api/UserPermission/check/{username}/{permissionCode}

Check if a specific user has a specific permission.

**Parameters:**

- `username` (string): Username
- `permissionCode` (string): Permission code to check

**Response:**

```json
{
  "hasPermission": true
}
```

### GET /api/UserPermission/search?searchTerm={term}

Search permissions by username or permission code.

**Query Parameters:**

- `searchTerm` (string): Search term

**Response:** Array of matching UserPermission objects

### POST /api/UserPermission

Create a new user permission.

**Request Body:**

```json
{
  "username": "john.doe",
  "permissionCode": "READ_EMPLOYEES"
}
```

**Response:** Created UserPermission object with 201 status

**Error Scenarios:**

- 409: Permission already exists for this user

### PUT /api/UserPermission/{id}

Update an existing user permission.

**Parameters:**

- `id` (int): Permission ID

**Request Body:**

```json
{
  "username": "john.doe",
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
- Index on Username
- Index on PermissionCode
- Unique composite index on (Username, PermissionCode)
- Index on CreatedDate

## Usage Examples

### Grant admin access to a user

```http
POST /api/UserPermission
{
  "username": "john.doe",
  "permissionCode": "ADMIN_ACCESS"
}
```

### Check if user can read reports

```http
GET /api/UserPermission/check/john.doe/READ_REPORTS
```

### Get all users with admin access

```http
GET /api/UserPermission/permission-code/ADMIN_ACCESS
```

### Search for permissions related to a specific user

```http
GET /api/UserPermission/search?searchTerm=john.doe
```
