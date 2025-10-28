# UserPermissions Interface Integration Guide

This guide provides comprehensive information for integrating with the UserPermissions API endpoints in your frontend applications.

## Data Model

### UserPermission Object

```json
{
  "permissionId": 1,
  "username": "john.doe",
  "permissionCode": "READ_EMPLOYEES",
  "createdDate": "2024-10-28T10:30:00Z"
}
```

### Properties

| Property         | Type     | Required             | Description                                           |
| ---------------- | -------- | -------------------- | ----------------------------------------------------- |
| `permissionId`   | integer  | Yes (auto-generated) | Unique identifier for the permission                  |
| `username`       | string   | Yes                  | Username (max 100 characters)                         |
| `permissionCode` | string   | Yes                  | Code representing the permission (max 100 characters) |
| `createdDate`    | datetime | Yes (auto-generated) | Timestamp when the permission was created             |

## API Endpoints

### Base URL

```
https://localhost:7257/api/UserPermission
```

### Endpoints

#### 1. Get All User Permissions

```http
GET /api/UserPermission
```

**Response:** Array of UserPermission objects

#### 2. Get User Permission by ID

```http
GET /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID

**Response:** Single UserPermission object

#### 3. Get User Permissions by Username

```http
GET /api/UserPermission/username/{username}
```

**Parameters:**

- `username` (string) - The username

**Response:** Array of UserPermission objects for the specified user

#### 4. Get User Permissions by Permission Code

```http
GET /api/UserPermission/permission-code/{permissionCode}
```

**Parameters:**

- `permissionCode` (string) - The permission code

**Response:** Array of UserPermission objects with the specified permission code

#### 5. Check if User Has Permission

```http
GET /api/UserPermission/check/{username}/{permissionCode}
```

**Parameters:**

- `username` (string) - The username
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

- `searchTerm` (string) - Search term (searches username and permission code)

**Response:** Array of matching UserPermission objects

#### 7. Create New User Permission

```http
POST /api/UserPermission
```

**Request Body:**

```json
{
  "username": "john.doe",
  "permissionCode": "READ_EMPLOYEES"
}
```

**Response:** Created UserPermission object with generated ID

**Error Scenarios:**

- `409 Conflict` - Permission already exists for this user

#### 8. Update User Permission

```http
PUT /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID to update

**Request Body:**

```json
{
  "username": "john.doe",
  "permissionCode": "WRITE_EMPLOYEES"
}
```

**Response:** Updated UserPermission object

#### 9. Delete User Permission

```http
DELETE /api/UserPermission/{id}
```

**Parameters:**

- `id` (integer) - The permission ID to delete

**Response:** Success message with status 200

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface UserPermission {
  permissionId: number;
  username: string;
  permissionCode: string;
  createdDate: string;
}

interface CreateUserPermissionRequest {
  username: string;
  permissionCode: string;
}

interface UpdateUserPermissionRequest {
  username: string;
  permissionCode: string;
}

interface PermissionCheckResponse {
  hasPermission: boolean;
}
```

### Sample API Calls

#### Get all permissions for a specific user:

```javascript
const response = await fetch("/api/UserPermission/username/john.doe", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const permissions = await response.json();
```

#### Check if user has specific permission:

```javascript
const response = await fetch(
  "/api/UserPermission/check/john.doe/READ_EMPLOYEES",
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
  username: "john.doe",
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
  username: "john.doe",
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

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `201 Created` - Successfully created
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Missing or invalid authorization
- `404 Not Found` - Resource not found
- `409 Conflict` - Permission already exists for user
- `500 Internal Server Error` - Server error

Error responses include a message field:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Business Rules

- Username and permission code combination must be unique
- CreatedDate is automatically set when creating a permission and cannot be modified
- Permission codes are case-sensitive
- Search functionality searches across username and permission code

## Frontend Implementation Tips

### Permission-Based UI

```javascript
// Check multiple permissions at once
async function checkPermissions(username, requiredPermissions) {
  const checks = await Promise.all(
    requiredPermissions.map((permission) =>
      fetch(`/api/UserPermission/check/${username}/${permission}`, {
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
const permissions = await checkPermissions("john.doe", [
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
async function getUserPermissions(username) {
  const response = await fetch(`/api/UserPermission/username/${username}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return await response.json();
}

// Add permission to user
async function grantPermission(username, permissionCode) {
  const response = await fetch("/api/UserPermission", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username, permissionCode }),
  });

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.message);
  }

  return await response.json();
}
```

### Bulk Permission Operations

```javascript
// Grant multiple permissions to a user
async function grantMultiplePermissions(username, permissionCodes) {
  const results = await Promise.allSettled(
    permissionCodes.map((code) => grantPermission(username, code))
  );

  const successful = results
    .filter((r) => r.status === "fulfilled")
    .map((r) => r.value);
  const failed = results
    .filter((r) => r.status === "rejected")
    .map((r) => r.reason);

  return { successful, failed };
}

// Revoke multiple permissions from a user
async function revokeUserPermissions(username) {
  const userPermissions = await getUserPermissions(username);

  const results = await Promise.allSettled(
    userPermissions.map((permission) =>
      fetch(`/api/UserPermission/${permission.permissionId}`, {
        method: "DELETE",
        headers: { Authorization: `Bearer ${token}` },
      })
    )
  );

  return results;
}
```

## Authentication Requirements

All API endpoints require JWT authentication. Ensure that your requests include the authorization header:

```javascript
const headers = {
  Authorization: `Bearer ${jwtToken}`,
  "Content-Type": "application/json",
};
```

## Rate Limiting

Be mindful of API rate limits when making bulk operations. Consider implementing:

- Request batching
- Retry logic with exponential backoff
- User feedback for long-running operations