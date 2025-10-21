# TrainingEvent Interface Integration Guide

## Overview

This document provides the interface specification for integrating with the TrainingEvent API endpoints for frontend development.

## Data Model

### TrainingEvent Object

```json
{
  "trainingEventId": 1,
  "personnelNumber": "string (max 20 chars, optional)",
  "idNumber": "string (max 13 chars, optional)",
  "eventType": "string (max 50 chars)",
  "trainingEventName": "string (max 100 chars)",
  "regionId": 1,
  "provinceId": 1,
  "municipalityId": 1,
  "siteId": 1,
  "employee": {
    "personnelNumber": "string",
    "firstName": "string",
    "lastName": "string"
  },
  "nonEmployee": {
    "idNumber": "string"
  },
  "region": {
    "lookupId": 1,
    "value": "string"
  },
  "province": {
    "lookupId": 1,
    "value": "string"
  },
  "municipality": {
    "lookupId": 1,
    "value": "string"
  },
  "site": {
    "lookupId": 1,
    "value": "string"
  }
}
```

### Properties

| Property            | Type   | Required | Description                                   |
| ------------------- | ------ | -------- | --------------------------------------------- |
| `trainingEventId`   | int    | Yes      | Auto-generated unique identifier              |
| `personnelNumber`   | string | No\*     | Employee personnel number (max 20 characters) |
| `idNumber`          | string | No\*     | Non-employee ID number (max 13 characters)    |
| `eventType`         | string | Yes      | Training event type (max 50 characters)       |
| `trainingEventName` | string | Yes      | Training event name (max 100 characters)      |
| `regionId`          | int    | Yes      | Region lookup ID                              |
| `provinceId`        | int    | Yes      | Province lookup ID                            |
| `municipalityId`    | int    | Yes      | Municipality lookup ID                        |
| `siteId`            | int    | Yes      | Site lookup ID                                |

\*Either `personnelNumber` OR `idNumber` must be provided, but not both.

## API Endpoints

### Base URL

```
/api/TrainingEvent
```

### Authentication

All endpoints require authorization. Include JWT token in the Authorization header:

```
Authorization: Bearer {your-jwt-token}
```

### Endpoints

#### 1. Get All Training Event Records

```http
GET /api/TrainingEvent
```

**Authentication:** Required

**Response:** Array of TrainingEvent objects with navigation properties

#### 2. Get Training Event by ID

```http
GET /api/TrainingEvent/{trainingEventId}
```

**Authentication:** Required

**Parameters:**

- `trainingEventId` (int) - The training event ID

**Response:** Single TrainingEvent object

#### 3. Get Training Events by Personnel Number

```http
GET /api/TrainingEvent/employee/{personnelNumber}
```

**Authentication:** Required

**Parameters:**

- `personnelNumber` (string) - The employee's personnel number

**Response:** Array of TrainingEvent objects

#### 4. Get Training Events by ID Number

```http
GET /api/TrainingEvent/non-employee/{idNumber}
```

**Authentication:** Required

**Parameters:**

- `idNumber` (string) - The non-employee's South African ID number

**Response:** Array of TrainingEvent objects

#### 5. Get Training Events by Event Type

```http
GET /api/TrainingEvent/event-type/{eventType}
```

**Authentication:** Required

**Parameters:**

- `eventType` (string) - The event type

**Response:** Array of TrainingEvent objects

#### 6. Get Training Events by Region

```http
GET /api/TrainingEvent/region/{regionId}
```

**Authentication:** Required

**Parameters:**

- `regionId` (int) - The region lookup ID

**Response:** Array of TrainingEvent objects

#### 7. Get Training Events by Province

```http
GET /api/TrainingEvent/province/{provinceId}
```

**Authentication:** Required

**Parameters:**

- `provinceId` (int) - The province lookup ID

**Response:** Array of TrainingEvent objects

#### 8. Get Training Events by Municipality

```http
GET /api/TrainingEvent/municipality/{municipalityId}
```

**Authentication:** Required

**Parameters:**

- `municipalityId` (int) - The municipality lookup ID

**Response:** Array of TrainingEvent objects

#### 9. Get Training Events by Site

```http
GET /api/TrainingEvent/site/{siteId}
```

**Authentication:** Required

**Parameters:**

- `siteId` (int) - The site lookup ID

**Response:** Array of TrainingEvent objects

#### 10. Search Training Events

```http
GET /api/TrainingEvent/search/{searchTerm}
```

**Authentication:** Required

**Parameters:**

- `searchTerm` (string) - Search term to match against training event name or event type

**Response:** Array of matching TrainingEvent objects

#### 11. Create New Training Event Record

```http
POST /api/TrainingEvent
```

**Authentication:** Required

**Request Body:**

```json
{
  "personnelNumber": "string (optional, max 20 chars)",
  "idNumber": "string (optional, max 13 chars)",
  "eventType": "string (required, max 50 chars)",
  "trainingEventName": "string (required, max 100 chars)",
  "regionId": 1,
  "provinceId": 1,
  "municipalityId": 1,
  "siteId": 1
}
```

**Response:** Created TrainingEvent object

#### 12. Update Training Event Record

```http
PUT /api/TrainingEvent/{trainingEventId}
```

**Authentication:** Required

**Parameters:**

- `trainingEventId` (int) - The training event ID to update

**Request Body:**

```json
{
  "personnelNumber": "string (optional, max 20 chars)",
  "idNumber": "string (optional, max 13 chars)",
  "eventType": "string (required, max 50 chars)",
  "trainingEventName": "string (required, max 100 chars)",
  "regionId": 1,
  "provinceId": 1,
  "municipalityId": 1,
  "siteId": 1
}
```

**Response:** Updated TrainingEvent object

#### 13. Delete Training Event Record

```http
DELETE /api/TrainingEvent/{trainingEventId}
```

**Authentication:** Required

**Parameters:**

- `trainingEventId` (int) - The training event ID to delete

**Response:** Success message

## Frontend Integration Examples

### JavaScript/TypeScript Interface

```typescript
interface TrainingEvent {
  trainingEventId: number;
  personnelNumber?: string;
  idNumber?: string;
  eventType: string;
  trainingEventName: string;
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
  employee?: Employee;
  nonEmployee?: NonEmployee;
  region?: LookupValue;
  province?: LookupValue;
  municipality?: LookupValue;
  site?: LookupValue;
}

interface CreateTrainingEventRequest {
  personnelNumber?: string;
  idNumber?: string;
  eventType: string;
  trainingEventName: string;
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
}

interface UpdateTrainingEventRequest {
  personnelNumber?: string;
  idNumber?: string;
  eventType: string;
  trainingEventName: string;
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
}

interface Employee {
  personnelNumber: string;
  firstName: string;
  lastName: string;
  // ... other properties
}

interface NonEmployee {
  idNumber: string;
}

interface LookupValue {
  lookupId: number;
  value: string;
  // ... other properties
}
```

### Sample API Calls

#### Get all training events:

```javascript
const response = await fetch("/api/TrainingEvent", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const trainingEvents = await response.json();
```

#### Get training events for an employee:

```javascript
const response = await fetch("/api/TrainingEvent/employee/EMP001", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const employeeTrainingEvents = await response.json();
```

#### Get training events by region:

```javascript
const response = await fetch("/api/TrainingEvent/region/1", {
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
});
const regionalTrainingEvents = await response.json();
```

#### Create a new training event for an employee:

```javascript
const newTrainingEvent = {
  personnelNumber: "EMP001",
  idNumber: null,
  eventType: "Technical Training",
  trainingEventName: "Advanced Safety Training",
  regionId: 1,
  provinceId: 1,
  municipalityId: 1,
  siteId: 1,
};

const response = await fetch("/api/TrainingEvent", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newTrainingEvent),
});
const created = await response.json();
```

#### Create a new training event for a non-employee:

```javascript
const newTrainingEvent = {
  personnelNumber: null,
  idNumber: "1234567890123",
  eventType: "Safety Training",
  trainingEventName: "Basic Safety Orientation",
  regionId: 1,
  provinceId: 1,
  municipalityId: 1,
  siteId: 1,
};

const response = await fetch("/api/TrainingEvent", {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(newTrainingEvent),
});
const created = await response.json();
```

#### Update a training event:

```javascript
const updatedTrainingEvent = {
  personnelNumber: "EMP001",
  idNumber: null,
  eventType: "Technical Training",
  trainingEventName: "Advanced Safety Training - Updated",
  regionId: 1,
  provinceId: 1,
  municipalityId: 1,
  siteId: 2,
};

const response = await fetch("/api/TrainingEvent/1", {
  method: "PUT",
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  },
  body: JSON.stringify(updatedTrainingEvent),
});
const updated = await response.json();
```

## Error Handling

All endpoints return appropriate HTTP status codes:

- `200 OK` - Success
- `201 Created` - Successfully created
- `400 Bad Request` - Invalid request data or participant validation errors
- `401 Unauthorized` - Missing or invalid authorization
- `404 Not Found` - Training event not found
- `500 Internal Server Error` - Server error

Error responses include a message field:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Business Rules

- Either `personnelNumber` or `idNumber` must be provided, but not both
- Personnel numbers must reference existing Employee records
- ID numbers must reference existing NonEmployee records
- All geographic lookup IDs must reference valid LookupValue records
- Event types should be standardized values
- Training event names can be free text but may reference catalogue entries
- Geographic hierarchy: Region > Province > Municipality > Site

## Validation

### Client-Side Validation

```javascript
function validateTrainingEventRequest(request) {
  const errors = [];

  // Validate participant (XOR validation)
  const hasPersonnelNumber =
    request.personnelNumber && request.personnelNumber.trim();
  const hasIDNumber = request.idNumber && request.idNumber.trim();

  if (!hasPersonnelNumber && !hasIDNumber) {
    errors.push("Either Personnel Number or ID Number must be provided");
  }

  if (hasPersonnelNumber && hasIDNumber) {
    errors.push("Cannot provide both Personnel Number and ID Number");
  }

  // Validate required fields
  if (!request.eventType || !request.eventType.trim()) {
    errors.push("Event Type is required");
  }

  if (!request.trainingEventName || !request.trainingEventName.trim()) {
    errors.push("Training Event Name is required");
  }

  if (!request.regionId || request.regionId <= 0) {
    errors.push("Region is required");
  }

  if (!request.provinceId || request.provinceId <= 0) {
    errors.push("Province is required");
  }

  if (!request.municipalityId || request.municipalityId <= 0) {
    errors.push("Municipality is required");
  }

  if (!request.siteId || request.siteId <= 0) {
    errors.push("Site is required");
  }

  return errors;
}
```

## Notes

- Training events support both internal employees and external participants
- Navigation properties provide rich context including participant and location details
- Geographic data supports detailed reporting and analysis capabilities
- Event types should be managed through the LookupValue system for consistency
- The API supports comprehensive filtering by participant, location, and event characteristics
- Search functionality covers both event names and event types for flexible discovery

## BREAKING CHANGES - Frontend Integration Update Required

⚠️ **IMPORTANT**: The TrainingEvent API has been updated with breaking changes that require frontend updates.

### What Changed

The `eventType` and `trainingEventName` properties have been changed from string values to foreign key references to the LookupValue system for better data consistency and normalization.

#### Before (Old Structure):

```json
{
  "trainingEventId": 1,
  "eventType": "Technical Training",
  "trainingEventName": "Advanced Safety Training"
  // ... other properties
}
```

#### After (New Structure):

```json
{
  "trainingEventId": 1,
  "eventTypeId": 5,
  "trainingEventNameId": 12,
  "eventType": {
    "lookupId": 5,
    "value": "Technical Training",
    "lookupType": "EventType"
  },
  "trainingEventName": {
    "lookupId": 12,
    "value": "Advanced Safety Training",
    "lookupType": "TrainingEventName"
  }
  // ... other properties
}
```

### Updated Data Model

#### TrainingEvent Object (Updated)

```json
{
  "trainingEventId": 1,
  "personnelNumber": "string (max 20 chars, optional)",
  "idNumber": "string (max 13 chars, optional)",
  "eventTypeId": 1,
  "trainingEventNameId": 1,
  "regionId": 1,
  "provinceId": 1,
  "municipalityId": 1,
  "siteId": 1,
  "employee": {
    "personnelNumber": "string",
    "firstName": "string",
    "lastName": "string"
  },
  "nonEmployee": {
    "idNumber": "string"
  },
  "eventType": {
    "lookupId": 1,
    "value": "string",
    "lookupType": "EventType"
  },
  "trainingEventName": {
    "lookupId": 1,
    "value": "string",
    "lookupType": "TrainingEventName"
  },
  "region": {
    "lookupId": 1,
    "value": "string"
  },
  "province": {
    "lookupId": 1,
    "value": "string"
  },
  "municipality": {
    "lookupId": 1,
    "value": "string"
  },
  "site": {
    "lookupId": 1,
    "value": "string"
  }
}
```

#### Updated Properties Table

| Property              | Type   | Required | Description                                   |
| --------------------- | ------ | -------- | --------------------------------------------- |
| `trainingEventId`     | int    | Yes      | Auto-generated unique identifier              |
| `personnelNumber`     | string | No\*     | Employee personnel number (max 20 characters) |
| `idNumber`            | string | No\*     | Non-employee ID number (max 13 characters)    |
| `eventTypeId`         | int    | Yes      | **NEW**: Foreign key to LookupValue           |
| `trainingEventNameId` | int    | Yes      | **NEW**: Foreign key to LookupValue           |
| `regionId`            | int    | Yes      | Region lookup ID                              |
| `provinceId`          | int    | Yes      | Province lookup ID                            |
| `municipalityId`      | int    | Yes      | Municipality lookup ID                        |
| `siteId`              | int    | Yes      | Site lookup ID                                |

### Updated API Endpoints

#### Changed Endpoint: Get Training Events by Event Type

**OLD:**

```http
GET /api/TrainingEvent/event-type/{eventType}
```

Where `eventType` was a string value like "Technical Training"

**NEW:**

```http
GET /api/TrainingEvent/event-type/{eventTypeId}
```

Where `eventTypeId` is an integer ID from the LookupValue table

### Updated Request/Response Models

#### TypeScript Interfaces (Updated)

```typescript
interface TrainingEvent {
  trainingEventId: number;
  personnelNumber?: string;
  idNumber?: string;
  eventTypeId: number; // CHANGED: was string eventType
  trainingEventNameId: number; // CHANGED: was string trainingEventName
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
  employee?: Employee;
  nonEmployee?: NonEmployee;
  eventType?: LookupValue; // NEW: navigation property
  trainingEventName?: LookupValue; // NEW: navigation property
  region?: LookupValue;
  province?: LookupValue;
  municipality?: LookupValue;
  site?: LookupValue;
}

interface CreateTrainingEventRequest {
  personnelNumber?: string;
  idNumber?: string;
  eventTypeId: number; // CHANGED: was string eventType
  trainingEventNameId: number; // CHANGED: was string trainingEventName
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
}

interface UpdateTrainingEventRequest {
  personnelNumber?: string;
  idNumber?: string;
  eventTypeId: number; // CHANGED: was string eventType
  trainingEventNameId: number; // CHANGED: was string trainingEventName
  regionId: number;
  provinceId: number;
  municipalityId: number;
  siteId: number;
}
```

### Frontend Migration Guide

#### 1. Update Data Access Layer

**OLD:**

```javascript
// Creating a training event (old way)
const newTrainingEvent = {
  personnelNumber: "EMP001",
  eventType: "Technical Training", // String value
  trainingEventName: "Advanced Safety Training", // String value
  regionId: 1,
  // ... other properties
};
```

**NEW:**

```javascript
// Creating a training event (new way)
const newTrainingEvent = {
  personnelNumber: "EMP001",
  eventTypeId: 5, // Lookup ID from LookupValue table
  trainingEventNameId: 12, // Lookup ID from LookupValue table
  regionId: 1,
  // ... other properties
};
```

#### 2. Update Display Logic

**OLD:**

```javascript
// Displaying event type (old way)
const displayEventType = trainingEvent.eventType; // Direct string value
const displayEventName = trainingEvent.trainingEventName; // Direct string value
```

**NEW:**

```javascript
// Displaying event type (new way)
const displayEventType = trainingEvent.eventType?.value || "Unknown Event Type";
const displayEventName =
  trainingEvent.trainingEventName?.value || "Unknown Event Name";
```

#### 3. Update Form Components

You'll need to implement dropdown/select components that populate from the LookupValue API:

```javascript
// Fetch available event types
const fetchEventTypes = async () => {
  const response = await fetch("/api/LookupValues?lookupType=EventType", {
    headers: { Authorization: `Bearer ${token}` },
  });
  return await response.json();
};

// Fetch available training event names
const fetchTrainingEventNames = async () => {
  const response = await fetch(
    "/api/LookupValues?lookupType=TrainingEventName",
    {
      headers: { Authorization: `Bearer ${token}` },
    }
  );
  return await response.json();
};

// Form component example
function TrainingEventForm() {
  const [eventTypes, setEventTypes] = useState([]);
  const [trainingEventNames, setTrainingEventNames] = useState([]);
  const [formData, setFormData] = useState({
    eventTypeId: "",
    trainingEventNameId: "",
    // ... other fields
  });

  useEffect(() => {
    fetchEventTypes().then(setEventTypes);
    fetchTrainingEventNames().then(setTrainingEventNames);
  }, []);

  return (
    <form>
      <select
        value={formData.eventTypeId}
        onChange={(e) =>
          setFormData({ ...formData, eventTypeId: parseInt(e.target.value) })
        }
      >
        <option value="">Select Event Type</option>
        {eventTypes.map((type) => (
          <option key={type.lookupId} value={type.lookupId}>
            {type.value}
          </option>
        ))}
      </select>

      <select
        value={formData.trainingEventNameId}
        onChange={(e) =>
          setFormData({
            ...formData,
            trainingEventNameId: parseInt(e.target.value),
          })
        }
      >
        <option value="">Select Training Event Name</option>
        {trainingEventNames.map((name) => (
          <option key={name.lookupId} value={name.lookupId}>
            {name.value}
          </option>
        ))}
      </select>

      {/* ... other form fields */}
    </form>
  );
}
```

#### 4. Update Search/Filter Logic

**OLD:**

```javascript
// Filtering by event type (old way)
const filterByEventType = (events, eventType) => {
  return events.filter((event) => event.eventType === eventType);
};
```

**NEW:**

```javascript
// Filtering by event type (new way)
const filterByEventType = (events, eventTypeId) => {
  return events.filter((event) => event.eventTypeId === eventTypeId);
};

// Or if filtering by display value:
const filterByEventTypeValue = (events, eventTypeValue) => {
  return events.filter((event) => event.eventType?.value === eventTypeValue);
};
```

#### 5. Update API Calls

**OLD:**

```javascript
// Get events by type (old way)
const getEventsByType = async (eventType) => {
  const response = await fetch(
    `/api/TrainingEvent/event-type/${encodeURIComponent(eventType)}`,
    {
      headers: { Authorization: `Bearer ${token}` },
    }
  );
  return await response.json();
};
```

**NEW:**

```javascript
// Get events by type (new way)
const getEventsByType = async (eventTypeId) => {
  const response = await fetch(`/api/TrainingEvent/event-type/${eventTypeId}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return await response.json();
};
```

### Updated Validation

```javascript
function validateTrainingEventRequest(request) {
  const errors = [];

  // Validate participant (XOR validation)
  const hasPersonnelNumber =
    request.personnelNumber && request.personnelNumber.trim();
  const hasIDNumber = request.idNumber && request.idNumber.trim();

  if (!hasPersonnelNumber && !hasIDNumber) {
    errors.push("Either Personnel Number or ID Number must be provided");
  }

  if (hasPersonnelNumber && hasIDNumber) {
    errors.push("Cannot provide both Personnel Number and ID Number");
  }

  // Updated validation for new ID fields
  if (!request.eventTypeId || request.eventTypeId <= 0) {
    errors.push("Event Type is required");
  }

  if (!request.trainingEventNameId || request.trainingEventNameId <= 0) {
    errors.push("Training Event Name is required");
  }

  if (!request.regionId || request.regionId <= 0) {
    errors.push("Region is required");
  }

  if (!request.provinceId || request.provinceId <= 0) {
    errors.push("Province is required");
  }

  if (!request.municipalityId || request.municipalityId <= 0) {
    errors.push("Municipality is required");
  }

  if (!request.siteId || request.siteId <= 0) {
    errors.push("Site is required");
  }

  return errors;
}
```

### Benefits of the Change

1. **Data Consistency**: Event types and training event names are now standardized across the system
2. **Better Data Integrity**: Foreign key constraints prevent invalid references
3. **Easier Maintenance**: Changes to event types/names are centralized in the LookupValue system
4. **Improved Reporting**: Better support for analytics and reporting with normalized data
5. **Multi-language Support**: Future support for localization through the LookupValue system

### Migration Checklist for Frontend Teams

- [ ] Update TypeScript interfaces and type definitions
- [ ] Replace string-based event type/name properties with ID-based properties
- [ ] Update form components to use dropdown/select controls with LookupValue data
- [ ] Modify display logic to use navigation property values
- [ ] Update API calls for filtering by event type (now uses ID instead of string)
- [ ] Update validation logic for the new ID-based fields
- [ ] Test all CRUD operations with the new data structure
- [ ] Update any search/filter functionality
- [ ] Review and update unit tests
- [ ] Update documentation and user guides
