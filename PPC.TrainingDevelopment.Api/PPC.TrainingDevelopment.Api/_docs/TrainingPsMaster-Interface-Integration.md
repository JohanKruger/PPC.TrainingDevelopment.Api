# TrainingPsMaster Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the TrainingPsMaster API endpoints for frontend development. TrainingPsMaster is a read-only data source that provides comprehensive SAP employee master data for training and development purposes.

## Data Model

### TrainingPsMaster Object

```json
{
  "personnelNumber": "string",
  "title": "string",
  "lastName": "string",
  "firstName": "string",
  "initials": "string",
  "secondName": "string",
  "knownAs": "string",
  "gender": "string",
  "dateOfBirth": "2024-01-01T00:00:00.000Z",
  "idNumber": "string",
  "raceCode": "string",
  "raceDescription": "string",
  "companyCode": "string",
  "companyName": "string",
  "personnelArea": "string",
  "personnelAreaDescription": "string",
  "personnelSubArea": "string",
  "personnelSubAreaDescription": "string",
  "employeeGroup": "string",
  "employeeGroupDescription": "string",
  "employeeSubGroup": "string",
  "employeeSubGroupDescription": "string",
  "organisationUnit": "string",
  "organisationUnitDescription": "string",
  "position": "string",
  "positionDescription": "string",
  "startDate": "2024-01-01T00:00:00.000Z",
  "endDate": "2024-12-31T00:00:00.000Z",
  "costCenter": "string",
  "costCenterDescription": "string",
  "employmentStatus": "string",
  "employementStatusDescription": "string",
  "emailAddress": "string",
  "disability": "string",
  "eeLevel": "string",
  "eeCategory": "string",
  "jobGrade": "string",
  "managerPersonnelNumber": "string",
  "managerName": "string",
  "managerEmailAddress": "string",
  "managerKnownAs": "string",
  "managerCostCenter": "string"
}
```

### Properties

| Property                       | Type     | Description                                     |
| ------------------------------ | -------- | ----------------------------------------------- |
| `personnelNumber`              | string   | Unique personnel number from SAP                |
| `title`                        | string   | Employee title (Mr, Mrs, Ms, Dr, etc.)          |
| `lastName`                     | string   | Employee's last name                            |
| `firstName`                    | string   | Employee's first name                           |
| `initials`                     | string   | Employee's initials                             |
| `secondName`                   | string   | Employee's second/middle name                   |
| `knownAs`                      | string   | Employee's preferred/known name                 |
| `gender`                       | string   | Employee's gender                               |
| `dateOfBirth`                  | datetime | Employee's date of birth                        |
| `idNumber`                     | string   | South African ID number                         |
| `raceCode`                     | string   | Race classification code                        |
| `raceDescription`              | string   | Race classification description                 |
| `companyCode`                  | string   | Company code from SAP                           |
| `companyName`                  | string   | Company name                                    |
| `personnelArea`                | string   | Personnel area code                             |
| `personnelAreaDescription`     | string   | Personnel area description                      |
| `personnelSubArea`             | string   | Personnel sub-area code                         |
| `personnelSubAreaDescription`  | string   | Personnel sub-area description                  |
| `employeeGroup`                | string   | Employee group code                             |
| `employeeGroupDescription`     | string   | Employee group description                      |
| `employeeSubGroup`             | string   | Employee sub-group code                         |
| `employeeSubGroupDescription`  | string   | Employee sub-group description                  |
| `organisationUnit`             | string   | Organizational unit code                        |
| `organisationUnitDescription`  | string   | Organizational unit description                 |
| `position`                     | string   | Position code from SAP                          |
| `positionDescription`          | string   | Position description/job title                  |
| `startDate`                    | datetime | Employment start date                           |
| `endDate`                      | datetime | Employment end date (null for active employees) |
| `costCenter`                   | string   | Cost center code                                |
| `costCenterDescription`        | string   | Cost center description                         |
| `employmentStatus`             | string   | Employment status code                          |
| `employementStatusDescription` | string   | Employment status description                   |
| `emailAddress`                 | string   | Employee's email address                        |
| `disability`                   | string   | Disability status/description                   |
| `eeLevel`                      | string   | Employment Equity level                         |
| `eeCategory`                   | string   | Employment Equity category                      |
| `jobGrade`                     | string   | Job grade/level                                 |
| `managerPersonnelNumber`       | string   | Manager's personnel number                      |
| `managerName`                  | string   | Manager's full name                             |
| `managerEmailAddress`          | string   | Manager's email address                         |
| `managerKnownAs`               | string   | Manager's preferred/known name                  |
| `managerCostCenter`            | string   | Manager's cost center                           |

## API Endpoints

### Base URL

```
/api/TrainingPsMaster
```

### Authentication

**Note:** All endpoints are currently open access without authentication requirements.

### Endpoints

#### 1. Get All TrainingPsMaster Records

```http
GET /api/TrainingPsMaster
```

**Authentication:** None required

**Response:** Array of TrainingPsMaster objects

**Description:** Retrieves all employee master data records from the SAP training view. This endpoint returns comprehensive employee information including organizational structure, position details, and manager information.

#### 2. Get TrainingPsMaster by Personnel Number or ID Number

```http
GET /api/TrainingPsMaster/{personnelNumber}
```

**Authentication:** None required

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number or South African ID number

**Response:** Single TrainingPsMaster object or 404 if not found

**Description:** Retrieves employee master data by personnel number or ID number. The search will match against both fields, making it flexible for different types of employee lookups.

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface TrainingPsMaster {
  personnelNumber?: string;
  title?: string;
  lastName?: string;
  firstName?: string;
  initials?: string;
  secondName?: string;
  knownAs?: string;
  gender?: string;
  dateOfBirth?: Date;
  idNumber?: string;
  raceCode?: string;
  raceDescription?: string;
  companyCode?: string;
  companyName?: string;
  personnelArea?: string;
  personnelAreaDescription?: string;
  personnelSubArea?: string;
  personnelSubAreaDescription?: string;
  employeeGroup?: string;
  employeeGroupDescription?: string;
  employeeSubGroup?: string;
  employeeSubGroupDescription?: string;
  organisationUnit?: string;
  organisationUnitDescription?: string;
  position?: string;
  positionDescription?: string;
  startDate?: Date;
  endDate?: Date;
  costCenter?: string;
  costCenterDescription?: string;
  employmentStatus?: string;
  employementStatusDescription?: string;
  emailAddress?: string;
  disability?: string;
  eeLevel?: string;
  eeCategory?: string;
  jobGrade?: string;
  managerPersonnelNumber?: string;
  managerName?: string;
  managerEmailAddress?: string;
  managerKnownAs?: string;
  managerCostCenter?: string;
}
```

### Sample API Calls

#### Get all employee master data:

```javascript
const response = await fetch("/api/TrainingPsMaster", {
  headers: {
    "Content-Type": "application/json",
  },
});
const employees = await response.json();
```

#### Get employee by personnel number:

```javascript
const response = await fetch("/api/TrainingPsMaster/EMP001", {
  headers: {
    "Content-Type": "application/json",
  },
});

if (response.ok) {
  const employee = await response.json();
  console.log("Employee found:", employee);
} else if (response.status === 404) {
  console.log("Employee not found");
} else {
  console.error("Error fetching employee");
}
```

#### Get employee by ID number:

```javascript
const response = await fetch("/api/TrainingPsMaster/8001015009087", {
  headers: {
    "Content-Type": "application/json",
  },
});

if (response.ok) {
  const employee = await response.json();
  console.log("Employee found:", employee);
} else {
  console.log("Employee not found");
}
```

#### React Component Example:

```typescript
import React, { useState, useEffect } from "react";

interface EmployeeMasterProps {
  personnelNumber: string;
}

const EmployeeMasterDetails: React.FC<EmployeeMasterProps> = ({
  personnelNumber,
}) => {
  const [employee, setEmployee] = useState<TrainingPsMaster | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchEmployee = async () => {
      try {
        setLoading(true);
        const response = await fetch(
          `/api/TrainingPsMaster/${personnelNumber}`
        );

        if (response.ok) {
          const data = await response.json();
          setEmployee(data);
        } else if (response.status === 404) {
          setError("Employee not found");
        } else {
          setError("Failed to fetch employee data");
        }
      } catch (err) {
        setError("Network error occurred");
      } finally {
        setLoading(false);
      }
    };

    if (personnelNumber) {
      fetchEmployee();
    }
  }, [personnelNumber]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;
  if (!employee) return <div>No employee data available</div>;

  return (
    <div className="employee-details">
      <h2>Employee Master Data</h2>
      <div className="employee-info">
        <p>
          <strong>Personnel Number:</strong> {employee.personnelNumber}
        </p>
        <p>
          <strong>Name:</strong> {employee.title} {employee.firstName}{" "}
          {employee.lastName}
        </p>
        <p>
          <strong>Known As:</strong> {employee.knownAs}
        </p>
        <p>
          <strong>Position:</strong> {employee.positionDescription}
        </p>
        <p>
          <strong>Cost Center:</strong> {employee.costCenterDescription}
        </p>
        <p>
          <strong>Manager:</strong> {employee.managerName} (
          {employee.managerKnownAs})
        </p>
        <p>
          <strong>Email:</strong> {employee.emailAddress}
        </p>
        <p>
          <strong>Employment Status:</strong>{" "}
          {employee.employementStatusDescription}
        </p>
      </div>
    </div>
  );
};
```

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `404 Not Found` - Employee not found
- `500 Internal Server Error` - Server error (database connection issues, etc.)

Error responses include standard ASP.NET Core error format:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "detail": "Employee not found"
}
```

## Data Source Information

### Database Details

- **Source:** Oracle Database (SAP BI)
- **View:** `SAPBIUSER.MV_EMP_TRAINING_PS_MASTER`
- **Access:** Read-only materialized view
- **Update Frequency:** Data is synchronized from SAP on a scheduled basis

### Data Characteristics

- **Master Data:** Contains authoritative employee information from SAP HR
- **Historical Data:** Includes start and end dates for employment periods
- **Organizational Structure:** Complete hierarchy from company to cost center level
- **Manager Information:** Direct manager details for reporting structure
- **Employment Equity:** Includes race, gender, disability, and EE categorization
- **Contact Information:** Email addresses and personal details

## Business Rules and Constraints

### Data Integrity

- Personnel numbers are unique across the system
- Records reflect the current state in SAP at last synchronization
- End dates are null for active employees
- Manager information may be null for top-level executives

### Search Capabilities

- Personnel number search is exact match
- ID number search is exact match
- Both searches use the same endpoint parameter

### Data Relationships

- TrainingPsMaster provides master data for employee lookups
- Can be cross-referenced with Employee API for training-specific data
- Manager relationships create hierarchical structures
- Cost centers and organizational units provide reporting groupings

## Integration Patterns

### Employee Lookup Pattern

```javascript
// Primary lookup by personnel number
const getEmployeeByPersonnelNumber = async (personnelNumber) => {
  const response = await fetch(`/api/TrainingPsMaster/${personnelNumber}`);
  return response.ok ? await response.json() : null;
};

// Fallback lookup by ID number
const getEmployeeByIdNumber = async (idNumber) => {
  const response = await fetch(`/api/TrainingPsMaster/${idNumber}`);
  return response.ok ? await response.json() : null;
};
```

### Manager Hierarchy Pattern

```javascript
const buildManagerHierarchy = async (personnelNumber) => {
  const hierarchy = [];
  let currentEmployee = await getEmployeeByPersonnelNumber(personnelNumber);

  while (currentEmployee && currentEmployee.managerPersonnelNumber) {
    hierarchy.push(currentEmployee);
    currentEmployee = await getEmployeeByPersonnelNumber(
      currentEmployee.managerPersonnelNumber
    );
  }

  return hierarchy;
};
```

### Department/Cost Center Filtering

```javascript
const filterByCostCenter = (employees, costCenter) => {
  return employees.filter((emp) => emp.costCenter === costCenter);
};

const filterByOrganizationUnit = (employees, orgUnit) => {
  return employees.filter((emp) => emp.organisationUnit === orgUnit);
};
```

## Performance Considerations

### Caching Strategy

- Consider implementing client-side caching for frequently accessed employee data
- Master data changes infrequently, making it suitable for longer cache periods
- Cache employee lists by cost center or organizational unit for better performance

### Pagination

- The "Get All" endpoint returns all records without pagination
- Consider implementing client-side filtering and pagination for large datasets
- Use specific lookups (by personnel number) when possible to avoid large data transfers

### Network Optimization

```javascript
// Efficient batch lookup pattern
const getMultipleEmployees = async (personnelNumbers) => {
  const promises = personnelNumbers.map((num) =>
    fetch(`/api/TrainingPsMaster/${num}`).then((r) => (r.ok ? r.json() : null))
  );

  const results = await Promise.allSettled(promises);
  return results
    .filter((result) => result.status === "fulfilled" && result.value)
    .map((result) => result.value);
};
```

## Security Considerations

### Data Sensitivity

- Contains personal information (names, ID numbers, birth dates)
- Includes organizational structure and salary-related information (job grades)
- Manager relationships may be sensitive for organizational reasons

### Access Control

- Currently no authentication required
- Consider implementing role-based access for sensitive fields
- Audit trails may be required for compliance purposes

### Data Protection

- Ensure HTTPS for all API calls
- Consider data masking for non-essential personal information
- Implement appropriate logging without exposing sensitive data

## Notes

- **Read-Only Nature:** This API provides read-only access to SAP master data
- **Data Freshness:** Data accuracy depends on SAP synchronization schedule
- **Comprehensive Coverage:** Includes detailed organizational and personal information
- **Manager Relationships:** Enables building of organizational hierarchies
- **Employment Equity Compliance:** Supports BBBEE and EE reporting requirements
- **Integration Point:** Serves as authoritative source for employee master data
- **Cross-Reference Capability:** Can be used to enrich data from other employee APIs
- **SAP Integration:** Direct connection to enterprise resource planning system
- **Audit Trail:** All access should be logged for compliance purposes
- **Performance Impact:** Large dataset - consider caching strategies for production use

---

**Document Version:** 1.0  
**Last Updated:** October 21, 2025  
**API Version Compatibility:** v1.0+  
**Reference ID:** TPS-API-DOC-v1.0  
**Data Source:** SAP BI - SAPBIUSER.MV_EMP_TRAINING_PS_MASTER
