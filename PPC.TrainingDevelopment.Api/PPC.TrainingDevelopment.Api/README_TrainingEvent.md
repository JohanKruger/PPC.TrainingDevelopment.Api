# TrainingEvent API Documentation

## Overview

The TrainingEvent API provides CRUD operations for managing training event records in the Training Development system. This API handles training events for both employees and non-employees, including geographic location data and event details.

## Base URL

```
https://localhost:7071/api/trainingevent
```

## Authentication

All endpoints require JWT Bearer token authentication.

## Endpoints

### 1. Get All Training Events

**GET** `/api/trainingevent`

- **Description**: Retrieves all training event records with related data
- **Authentication**: Required
- **Response**: Array of TrainingEvent objects with navigation properties

### 2. Get Training Event by ID

**GET** `/api/trainingevent/{trainingEventId}`

- **Description**: Retrieves a specific training event by ID
- **Authentication**: Required
- **Parameters**:
  - `trainingEventId` (int): Training event ID
- **Response**: TrainingEvent object or 404 if not found

### 3. Get Training Events by Personnel Number

**GET** `/api/trainingevent/employee/{personnelNumber}`

- **Description**: Retrieves all training events for a specific employee
- **Authentication**: Required
- **Parameters**:
  - `personnelNumber` (string): Employee's personnel number
- **Response**: Array of TrainingEvent objects

### 4. Get Training Events by ID Number

**GET** `/api/trainingevent/non-employee/{idNumber}`

- **Description**: Retrieves all training events for a specific non-employee
- **Authentication**: Required
- **Parameters**:
  - `idNumber` (string): South African ID number
- **Response**: Array of TrainingEvent objects

### 5. Get Training Events by Event Type

**GET** `/api/trainingevent/event-type/{eventType}`

- **Description**: Retrieves training events filtered by event type
- **Authentication**: Required
- **Parameters**:
  - `eventType` (string): Event type category
- **Response**: Array of TrainingEvent objects

### 6. Get Training Events by Region

**GET** `/api/trainingevent/region/{regionId}`

- **Description**: Retrieves training events filtered by region
- **Authentication**: Required
- **Parameters**:
  - `regionId` (int): Region lookup ID
- **Response**: Array of TrainingEvent objects

### 7. Get Training Events by Province

**GET** `/api/trainingevent/province/{provinceId}`

- **Description**: Retrieves training events filtered by province
- **Authentication**: Required
- **Parameters**:
  - `provinceId` (int): Province lookup ID
- **Response**: Array of TrainingEvent objects

### 8. Get Training Events by Municipality

**GET** `/api/trainingevent/municipality/{municipalityId}`

- **Description**: Retrieves training events filtered by municipality
- **Authentication**: Required
- **Parameters**:
  - `municipalityId` (int): Municipality lookup ID
- **Response**: Array of TrainingEvent objects

### 9. Get Training Events by Site

**GET** `/api/trainingevent/site/{siteId}`

- **Description**: Retrieves training events filtered by site
- **Authentication**: Required
- **Parameters**:
  - `siteId` (int): Site lookup ID
- **Response**: Array of TrainingEvent objects

### 10. Search Training Events

**GET** `/api/trainingevent/search/{searchTerm}`

- **Description**: Searches training events by name or event type
- **Authentication**: Required
- **Parameters**:
  - `searchTerm` (string): Search term (case-insensitive)
- **Response**: Array of matching TrainingEvent objects

### 11. Create Training Event

**POST** `/api/trainingevent`

- **Description**: Creates a new training event record
- **Authentication**: Required
- **Request Body**: CreateTrainingEventRequest object
- **Response**: Created TrainingEvent object with 201 status

### 12. Update Training Event

**PUT** `/api/trainingevent/{trainingEventId}`

- **Description**: Updates an existing training event record
- **Authentication**: Required
- **Parameters**:
  - `trainingEventId` (int): Training event ID
- **Request Body**: UpdateTrainingEventRequest object
- **Response**: Updated TrainingEvent object

### 13. Delete Training Event

**DELETE** `/api/trainingevent/{trainingEventId}`

- **Description**: Deletes a training event record
- **Authentication**: Required
- **Parameters**:
  - `trainingEventId` (int): Training event ID
- **Response**: Success message or 404 if not found

## Data Models

### TrainingEvent

```json
{
  "trainingEventId": "int (auto-generated)",
  "personnelNumber": "string (20 chars, optional)",
  "idNumber": "string (13 chars, optional)",
  "eventType": "string (50 chars, required)",
  "trainingEventName": "string (100 chars, required)",
  "regionId": "int (required)",
  "provinceId": "int (required)",
  "municipalityId": "int (required)",
  "siteId": "int (required)",
  "employee": "Employee object (navigation property)",
  "nonEmployee": "NonEmployee object (navigation property)",
  "region": "LookupValue object (navigation property)",
  "province": "LookupValue object (navigation property)",
  "municipality": "LookupValue object (navigation property)",
  "site": "LookupValue object (navigation property)"
}
```

### CreateTrainingEventRequest

```json
{
  "personnelNumber": "string (20 chars, optional)",
  "idNumber": "string (13 chars, optional)",
  "eventType": "string (50 chars, required)",
  "trainingEventName": "string (100 chars, required)",
  "regionId": "int (required)",
  "provinceId": "int (required)",
  "municipalityId": "int (required)",
  "siteId": "int (required)"
}
```

### UpdateTrainingEventRequest

```json
{
  "personnelNumber": "string (20 chars, optional)",
  "idNumber": "string (13 chars, optional)",
  "eventType": "string (50 chars, required)",
  "trainingEventName": "string (100 chars, required)",
  "regionId": "int (required)",
  "provinceId": "int (required)",
  "municipalityId": "int (required)",
  "siteId": "int (required)"
}
```

## Error Responses

### 400 Bad Request

- Invalid model state
- Participant validation errors (both or neither PersonnelNumber/IDNumber provided)
- Empty search term

### 401 Unauthorized

- Missing or invalid JWT token

### 404 Not Found

- Training event not found

### 500 Internal Server Error

- Database connection issues
- Unexpected server errors

## Business Rules

1. **Participant Requirement**: Either PersonnelNumber OR IDNumber must be provided, but not both
2. **Personnel Number**: Must reference an existing Employee record
3. **ID Number**: Must reference an existing NonEmployee record
4. **Geographic Hierarchy**: Region > Province > Municipality > Site (all required)
5. **Event Type**: Must be a predefined value from training catalogue
6. **Lookup References**: All geographic lookup IDs must reference valid LookupValue records
7. **Ordering**: Results are ordered by training event name, then event type

## Validation Rules

### Participant Validation

- Exactly one of PersonnelNumber or IDNumber must be provided
- Cannot provide both PersonnelNumber and IDNumber
- Cannot leave both PersonnelNumber and IDNumber empty

### Field Constraints

- EventType: Maximum 50 characters, required
- TrainingEventName: Maximum 100 characters, required
- PersonnelNumber: Maximum 20 characters if provided
- IDNumber: Maximum 13 characters if provided
- All geographic IDs are required integers

## Usage Examples

### Creating a Training Event for Employee

```bash
curl -X POST "https://localhost:7071/api/trainingevent" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "personnelNumber": "EMP001",
    "idNumber": null,
    "eventType": "Technical Training",
    "trainingEventName": "Advanced Safety Training",
    "regionId": 1,
    "provinceId": 1,
    "municipalityId": 1,
    "siteId": 1
  }'
```

### Creating a Training Event for Non-Employee

```bash
curl -X POST "https://localhost:7071/api/trainingevent" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "personnelNumber": null,
    "idNumber": "1234567890123",
    "eventType": "Safety Training",
    "trainingEventName": "Basic Safety Orientation",
    "regionId": 1,
    "provinceId": 1,
    "municipalityId": 1,
    "siteId": 1
  }'
```

### Getting Training Events for an Employee

```bash
curl -X GET "https://localhost:7071/api/trainingevent/employee/EMP001" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### Searching Training Events

```bash
curl -X GET "https://localhost:7071/api/trainingevent/search/safety" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

## Integration Notes

- Training events link participants (employees or non-employees) to specific training interventions
- Geographic hierarchy supports detailed location-based reporting and analysis
- Navigation properties provide full context including participant and location details
- Event types should be standardized using the LookupValue system
- The API supports both internal employee training and external participant training
- All geographic lookups reference the LookupValue table for consistency
- Multiple training events can be recorded per participant over time

## Database Migration Note

When implementing this API, you'll need to create a database migration to add the new TrainingEvent and NonEmployee tables with the appropriate foreign key relationships and indexes.
