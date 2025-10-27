# Audit Logging Implementation

This document describes the audit logging implementation that tracks all API calls made to the application.

## Features

The audit logging system captures the following information for each API call:

### Captured Data

- **User Information**: User ID and Username (from JWT token claims)
- **Request Details**: HTTP method, request path, query string, controller, action
- **Request/Response**: Request body and response body (with size limits)
- **Performance**: Response time in milliseconds
- **Network**: Client IP address and User-Agent
- **Status**: HTTP status code
- **Errors**: Exception details if any errors occur
- **Timestamp**: UTC timestamp of the request

### Database Schema

The audit logs are stored in the `AuditLogs` table with the following structure:

```sql
CREATE TABLE [AuditLogs] (
    [AuditLogId] int NOT NULL IDENTITY,
    [UserId] nvarchar(50) NOT NULL,
    [UserName] nvarchar(100) NOT NULL,
    [HttpMethod] nvarchar(10) NOT NULL,
    [RequestPath] nvarchar(500) NOT NULL,
    [QueryString] nvarchar(2000) NULL,
    [Controller] nvarchar(100) NOT NULL,
    [Action] nvarchar(100) NOT NULL,
    [RequestBody] nvarchar(max) NULL,
    [ResponseBody] nvarchar(max) NULL,
    [StatusCode] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [DurationMs] bigint NOT NULL,
    [IpAddress] nvarchar(45) NULL,
    [UserAgent] nvarchar(500) NULL,
    [ExceptionDetails] nvarchar(2000) NULL,
    [AdditionalInfo] nvarchar(500) NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([AuditLogId])
);
```

### Indexes

The following indexes are created for optimal query performance:

- `IX_AuditLogs_UserId`
- `IX_AuditLogs_Controller`
- `IX_AuditLogs_Action`
- `IX_AuditLogs_Timestamp`
- `IX_AuditLogs_StatusCode`
- `IX_AuditLogs_Controller_Action`
- `IX_AuditLogs_UserId_Timestamp`

## Implementation Components

### 1. AuditLog Model (`Models/AuditLog.cs`)

Defines the data structure for audit log entries.

### 2. IAuditLogService Interface (`Services/Interfaces/IAuditLogService.cs`)

Defines the contract for audit log operations.

### 3. AuditLogService (`Services/AuditLogService.cs`)

Implements the audit log service with methods to:

- Save audit log entries
- Retrieve audit logs with pagination
- Query audit logs by user, controller, date range, etc.

### 4. AuditLoggingMiddleware (`Middleware/AuditLoggingMiddleware.cs`)

ASP.NET Core middleware that intercepts HTTP requests and responses to capture audit data.

### 5. AuditLogsController (`Controllers/AuditLogsController.cs`)

REST API endpoints to view and query audit logs.

## API Endpoints

### Get All Audit Logs

```
GET /api/auditlogs?page=1&pageSize=50
```

### Get Audit Log by ID

```
GET /api/auditlogs/{id}
```

### Get Audit Logs by User

```
GET /api/auditlogs/user/{userId}?page=1&pageSize=50
```

### Get Audit Logs by Controller

```
GET /api/auditlogs/controller/{controller}?page=1&pageSize=50
```

### Get Audit Logs by Date Range

```
GET /api/auditlogs/daterange?fromDate=2024-10-27T00:00:00Z&toDate=2024-10-28T23:59:59Z&page=1&pageSize=50
```

## Configuration

### Excluded Paths

The following paths are excluded from audit logging to prevent noise:

- `/swagger/*`
- `/health`
- `/metrics`
- `/favicon.ico`
- `/api/auditlogs` (prevents recursive logging)

### Size Limits

- Request body: Limited to 50KB
- Response body: Limited to 10KB
- Only JSON and form-encoded content types are logged

### User Identification

The middleware attempts to extract user information from JWT token claims in the following order:

1. `ClaimTypes.NameIdentifier` (standard claim)
2. `sub` (subject claim)
3. `user_id` (custom claim)
4. Falls back to "Anonymous" if no user claims are found

## Security Considerations

### Data Sensitivity

- Request and response bodies are logged, which may contain sensitive data
- Consider implementing data masking for sensitive fields (passwords, PII, etc.)
- Response bodies are only logged for certain status codes (2xx, 4xx+)

### Performance Impact

- Audit logging runs asynchronously to minimize impact on request processing
- Failed audit log writes do not affect the main application flow
- Database indexes are optimized for common query patterns

### Storage Management

- Consider implementing log retention policies to manage database size
- Archive old audit logs to separate storage if needed
- Monitor database growth and implement cleanup procedures

## Error Handling

The audit logging system is designed to be non-intrusive:

- If audit logging fails, the main application continues to function
- Audit logging errors are logged separately but do not throw exceptions
- Failed audit writes are caught and logged to the application log

## Usage Examples

### View Recent Activity

```csharp
var recentLogs = await auditLogService.GetAuditLogsAsync(1, 20);
```

### Track User Activity

```csharp
var userActivity = await auditLogService.GetAuditLogsByUserAsync("admin", 1, 50);
```

### Monitor Specific Controllers

```csharp
var employeeControllerLogs = await auditLogService.GetAuditLogsByControllerAsync("Employee", 1, 100);
```

### Investigate Issues in Time Range

```csharp
var logs = await auditLogService.GetAuditLogsByDateRangeAsync(
    DateTime.UtcNow.AddHours(-24),
    DateTime.UtcNow,
    1, 50);
```

## Future Enhancements

Consider implementing the following features:

1. **Data Masking**: Automatically mask sensitive fields in request/response bodies
2. **Real-time Monitoring**: WebSocket or SignalR integration for real-time audit log streaming
3. **Alerting**: Automatic alerts for suspicious activity patterns
4. **Analytics**: Dashboard with audit log analytics and reporting
5. **Export**: Export audit logs to external systems (SIEM, log aggregators)
6. **Compliance**: Additional fields for regulatory compliance (SOX, GDPR, etc.)
7. **User Session Tracking**: Link audit logs to user sessions
8. **API Rate Limiting**: Track and limit API usage per user
