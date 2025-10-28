# UserPermissions Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the UserPermissions API endpoints for frontend development.

## Data Model

### UserPermission Object

```json
{
  "permissionId": 1,
  "personnelNo": "string (max 20 chars)",
  "permissionCode": "string (max 100 chars)",
  "createdDate": "2024-10-28T10:30:00Z",
  "employee": {
    "personnelNumber": "string",
    "firstName": "string",
    "lastName": "string",
    "knownName": "string",
    "jobTitle": "string",
    "site": "string"
  }
}
```

### Properties

| Property         | Type     | Required             | Description                                           |
| ---------------- | -------- | -------------------- | ----------------------------------------------------- |
| `permissionId`   | integer  | Yes (auto-generated) | Unique identifier for the permission                  |
| `personnelNo`    | string   | Yes                  | Employee personnel number (max 20 characters)         |
| `permissionCode` | string   | Yes                  | Code representing the permission (max 100 characters) |
| `createdDate`    | datetime | Yes (auto-generated) | Timestamp when the permission was created             |
| `employee`       | Employee | No                   | Employee object with basic information                |

## API Endpoints

### Base URL

```
/api/UserPermission
```

### Authentication

All endpoints require authorization. Include JWT token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

### Endpoints

#### 1. Get All User Permissions

```http
GET /api/UserPermission
```

**Response:** Array of UserPermission objects with employee details

#### 2. Get User Permission by ID

```http
GET /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID

**Response:** Single UserPermission object

#### 3. Get User Permissions by Personnel Number

```http
GET /api/UserPermission/personnel/{personnelNo}
```

**Parameters:**

- `personnelNo` (string) - The employee personnel number

**Response:** Array of UserPermission objects for the specified employee

#### 4. Get User Permissions by Permission Code

```http
GET /api/UserPermission/permission-code/{permissionCode}
```

**Parameters:**

- `permissionCode` (string) - The permission code

**Response:** Array of UserPermission objects with the specified permission code

#### 5. Check if Personnel Has Permission

```http
GET /api/UserPermission/check/{personnelNo}/{permissionCode}
```

**Parameters:**

- `personnelNo` (string) - The employee personnel number
- `permissionCode` (string) - The permission code to check

**Response:**

```json
{
  "hasPermission": true
}
```

#### 6. Search User Permissions

```http
GET /api/UserPermission/search?searchTerm={term}
```

**Query Parameters:**

- `searchTerm` (string) - Search term (searches personnel number, permission code, employee name)

**Response:** Array of matching UserPermission objects

#### 7. Create New User Permission

```http
POST /api/UserPermission
```

**Request Body:**

```json
{
  "personnelNo": "string (required, max 20 chars)",
  "permissionCode": "string (required, max 100 chars)"
}
```

**Response:** Created UserPermission object with generated ID

**Error Scenarios:**

- `400 Bad Request` - Employee does not exist
- `409 Conflict` - Permission already exists for this employee

#### 8. Update User Permission

```http
PUT /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID to update

**Request Body:**

```json
{
  "personnelNo": "string (required, max 20 chars)",
  "permissionCode": "string (required, max 100 chars)"
}
```

**Response:** Updated UserPermission object

#### 9. Delete User Permission

```http
DELETE /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID to delete

**Response:** Success message

```json
{
  "message": "User permission deleted successfully."
}
```

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface UserPermission {
  permissionId: number;
  personnelNo: string;
  permissionCode: string;
  createdDate: string;
  employee?: Employee;
}

interface Employee {
  personnelNumber: string;
  firstName: string;
  lastName: string;
  knownName?: string;
  jobTitle?: string;
  site?: string;
}

interface CreateUserPermissionRequest {
  personnelNo: string;
  permissionCode: string;
}

interface UpdateUserPermissionRequest {
  personnelNo: string;
  permissionCode: string;
}

interface PermissionCheckResponse {
  hasPermission: boolean;
}
```

### Sample API Calls

#### Get all permissions for a specific employee:

```javascript
const response = await fetch("/api/UserPermission/personnel/EMP001", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const permissions = await response.json();
```

#### Check if employee has specific permission:

```javascript
const response = await fetch(
  "/api/UserPermission/check/EMP001/READ_EMPLOYEES",
  {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  }
);
const result = await response.json();
const hasPermission = result.hasPermission;
```

#### Create a new user permission:

```javascript
const newPermission = {
  personnelNo: "EMP001",
  permissionCode: "READ_EMPLOYEES",
};

const response = await fetch("/api/UserPermission", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newPermission),
});
const created = await response.json();
```

#### Search permissions by term:

```javascript
const searchTerm = "admin";
const response = await fetch(
  `/api/UserPermission/search?searchTerm=${encodeURIComponent(searchTerm)}`,
  {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  }
);
const searchResults = await response.json();
```

#### Get all users with a specific permission:

```javascript
const response = await fetch(
  "/api/UserPermission/permission-code/ADMIN_ACCESS",
  {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  }
);
const adminUsers = await response.json();
```

#### Update a user permission:

```javascript
const updatedPermission = {
  personnelNo: "EMP001",
  permissionCode: "WRITE_EMPLOYEES",
};

const response = await fetch("/api/UserPermission/1", {
  method: "PUT",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(updatedPermission),
});
const updated = await response.json();
```

#### Delete a user permission:

```javascript
const response = await fetch("/api/UserPermission/1", {
  method: "DELETE",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const result = await response.json();
```

## Common Permission Codes

While permission codes are free-form strings, here are suggested standard codes for consistency:

### Employee Management

- `READ_EMPLOYEES` - View employee information
- `WRITE_EMPLOYEES` - Create/update employee information
- `DELETE_EMPLOYEES` - Delete employee records

### Training Management

- `READ_TRAINING_EVENTS` - View training events
- `WRITE_TRAINING_EVENTS` - Create/update training events
- `DELETE_TRAINING_EVENTS` - Delete training events
- `READ_TRAINING_RECORDS` - View training records
- `WRITE_TRAINING_RECORDS` - Create/update training records

### Reporting

- `READ_REPORTS` - View reports
- `GENERATE_REPORTS` - Generate custom reports
- `EXPORT_DATA` - Export data to external formats

### Administration

- `ADMIN_ACCESS` - Full administrative access
- `USER_MANAGEMENT` - Manage user permissions
- `SYSTEM_CONFIG` - Configure system settings
- `AUDIT_LOGS` - View audit logs

### Lookup Management

- `READ_LOOKUPS` - View lookup values
- `WRITE_LOOKUPS` - Create/update lookup values
- `DELETE_LOOKUPS` - Delete lookup values

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `201 Created` - Successfully created
- `400 Bad Request` - Invalid request data or employee does not exist
- `401 Unauthorized` - Missing or invalid authorization
- `404 Not Found` - Resource not found
- `409 Conflict` - Permission already exists for employee
- `500 Internal Server Error` - Server error

Error responses include a message field:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Business Rules

- Each permission must reference a valid employee (personnelNo must exist in Employee table)
- Personnel number and permission code combination must be unique
- CreatedDate is automatically set when creating a permission and cannot be modified
- Permissions support cascading delete (if employee is deleted, their permissions are removed)
- Permission codes are case-sensitive
- Search functionality searches across personnel number, permission code, and employee names

## Frontend Implementation Tips

### Permission-Based UI

```javascript
// Check multiple permissions at once
async function checkPermissions(personnelNo, requiredPermissions) {
  const checks = await Promise.all(
    requiredPermissions.map((permission) =>
      fetch(`/api/UserPermission/check/${personnelNo}/${permission}`, {
        headers: { Authorization: `Bearer ${token}` },
      }).then((r) => r.json())
    )
  );

  return requiredPermissions.reduce((acc, permission, index) => {
    acc[permission] = checks[index].hasPermission;
    return acc;
  }, {});
}

// Usage in component
const permissions = await checkPermissions("EMP001", [
  "READ_EMPLOYEES",
  "WRITE_EMPLOYEES",
  "DELETE_EMPLOYEES",
]);

// Conditionally render UI elements
if (permissions.WRITE_EMPLOYEES) {
  // Show edit button
}
```

### Permission Management Component

```javascript
// Get all permissions for user management interface
async function getUserPermissions(personnelNo) {
  const response = await fetch(`/api/UserPermission/personnel/${personnelNo}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return await response.json();
}

// Add permission to user
async function grantPermission(personnelNo, permissionCode) {
  const response = await fetch("/api/UserPermission", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ personnelNo, permissionCode }),
  });

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.message);
  }

  return await response.json();
}
```

## Notes

- All string fields are required unless marked as optional
- Maximum length constraints must be respected
- Permission codes are case-sensitive and should follow consistent naming conventions
- The API includes Employee navigation properties for easier frontend display
- Search functionality is optimized with database indexes for performance
- Unique constraint prevents duplicate permissions for the same employee
- CreatedDate provides audit trail for permission grants
