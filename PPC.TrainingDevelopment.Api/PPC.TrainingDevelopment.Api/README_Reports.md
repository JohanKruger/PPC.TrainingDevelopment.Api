# Reports API Documentation

This API provides endpoints for exporting TrainingRecordEvents with their related Training Event and Employee data in a flat structure suitable for CSV export on the frontend.

## Base URL

```
/api/Reports
```

## Authentication

All endpoints require JWT Bearer token authentication.

## Endpoints

### 1. Get All Training Records for Export

**GET** `/training-records/export`

Returns all training record events with their related training event and employee data in a flat structure.

**Response:** `200 OK`

```json
[
  {
    "trainingRecordEventId": 1,
    "trainingEventId": 1,
    "startDate": "2024-01-15T09:00:00Z",
    "endDate": "2024-01-15T17:00:00Z",
    "hours": 8,
    "minutes": 0,
    "evidence": true,
    "serviceProviderExternal": "ABC Training",
    "costTrainingMaterials": 150.0,
    "costTrainers": 500.0,
    "costTrainingFacilities": 200.0,
    "scholarshipsBursaries": 0.0,
    "courseFees": 300.0,
    "accommodation": 50.0,
    "travel": 50.0,
    "meal": 25.0,
    "administrationCosts": 50.0,
    "equipmentDepreciation": 25.0,
    "totalCosts": 1350.0,
    "totalDurationMinutes": 480,
    "eventType": "Skills Development",
    "trainingEventName": "Leadership Training",
    "region": "Gauteng",
    "province": "Gauteng",
    "municipality": "City of Johannesburg",
    "site": "Head Office",
    "personnelNumber": "12345678",
    "employeeFirstName": "John",
    "employeeLastName": "Doe",
    "employeeKnownName": "Johnny",
    "employeeInitials": "J.D.",
    "employeeRace": "African",
    "employeeGender": "Male",
    "employeeDisability": false,
    "employeeEELevel": "Professional",
    "employeeEECategory": "Category A",
    "employeeJobTitle": "Manager",
    "employeeJobGrade": "M1",
    "employeeIDNumber": "8001015009088",
    "employeeSite": "Head Office",
    "employeeHighestQualification": "Bachelor's Degree",
    "employeeNotes": "High performer",
    "nonEmployeeIDNumber": null
  }
]
```

### 2. Get Training Records by Date Range for Export

**GET** `/training-records/export/by-date`

Returns training record events filtered by date range.

**Query Parameters:**

- `startDate` (required): Start date filter (yyyy-MM-dd)
- `endDate` (required): End date filter (yyyy-MM-dd)

**Example:** `/training-records/export/by-date?startDate=2024-01-01&endDate=2024-12-31`

**Response:** `200 OK` - Same structure as above, filtered by date range.

**Error Responses:**

- `400 Bad Request` - Start date cannot be greater than end date

### 3. Get Training Records by Personnel Number for Export

**GET** `/training-records/export/by-personnel/{personnelNumber}`

Returns training record events filtered by personnel number.

**Path Parameters:**

- `personnelNumber` (required): Personnel number to filter by

**Example:** `/training-records/export/by-personnel/12345678`

**Response:** `200 OK` - Same structure as above, filtered by personnel number.

**Error Responses:**

- `400 Bad Request` - Personnel number cannot be empty

### 4. Get Training Records by Training Event ID for Export

**GET** `/training-records/export/by-event/{trainingEventId}`

Returns training record events filtered by training event ID.

**Path Parameters:**

- `trainingEventId` (required): Training event ID to filter by

**Example:** `/training-records/export/by-event/1`

**Response:** `200 OK` - Same structure as above, filtered by training event ID.

**Error Responses:**

- `400 Bad Request` - Training event ID must be greater than 0

### 5. Get Training Records with Multiple Filters for Export

**GET** `/training-records/export/filtered`

Returns training record events with optional multiple filters.

**Query Parameters (all optional):**

- `startDate`: Start date filter (yyyy-MM-dd)
- `endDate`: End date filter (yyyy-MM-dd)
- `personnelNumber`: Personnel number filter
- `trainingEventId`: Training event ID filter
- `hasEvidence`: Evidence filter (true/false)

**Example:** `/training-records/export/filtered?startDate=2024-01-01&endDate=2024-12-31&hasEvidence=true`

**Response:** `200 OK` - Same structure as above, filtered by specified criteria.

**Error Responses:**

- `400 Bad Request` - Start date cannot be greater than end date
- `400 Bad Request` - Training event ID must be greater than 0

### 6. Download Training Records as CSV

**GET** `/training-records/export/csv`

Downloads training record events as a CSV file with optional filters.

**Query Parameters (all optional):**

- `startDate`: Start date filter (yyyy-MM-dd)
- `endDate`: End date filter (yyyy-MM-dd)
- `personnelNumber`: Personnel number filter
- `trainingEventId`: Training event ID filter
- `hasEvidence`: Evidence filter (true/false)

**Example:** `/training-records/export/csv?startDate=2024-01-01&endDate=2024-12-31`

**Response:** `200 OK`

- **Content-Type:** `text/csv`
- **Content-Disposition:** `attachment; filename="training_records_yyyyMMdd_HHmmss.csv"`

**Error Responses:**

- `400 Bad Request` - Start date cannot be greater than end date
- `400 Bad Request` - Training event ID must be greater than 0

## CSV Format

The CSV export includes the following columns:

### Training Record Event Data

- TrainingRecordEventId
- TrainingEventId
- StartDate
- EndDate
- Hours
- Minutes
- Evidence
- ServiceProviderExternal
- CostTrainingMaterials
- CostTrainers
- CostTrainingFacilities
- ScholarshipsBursaries
- CourseFees
- Accommodation
- Travel
- Meal
- AdministrationCosts
- EquipmentDepreciation
- TotalCosts
- TotalDurationMinutes

### Training Event Data

- EventType
- TrainingEventName
- Region
- Province
- Municipality
- Site

### Employee Data

- PersonnelNumber
- EmployeeFirstName
- EmployeeLastName
- EmployeeKnownName
- EmployeeInitials
- EmployeeRace
- EmployeeGender
- EmployeeDisability
- EmployeeEELevel
- EmployeeEECategory
- EmployeeJobTitle
- EmployeeJobGrade
- EmployeeIDNumber
- EmployeeSite
- EmployeeHighestQualification
- EmployeeNotes

### Non-Employee Data

- NonEmployeeIDNumber (populated when no employee match exists)

## Data Relationships

The flat structure combines data from three main entities:

1. **TrainingRecordEvent**: Core training record information including dates, costs, and duration
2. **TrainingEvent**: Training event details including type, name, and location information
3. **Employee**: Employee demographic and job information (when available)

When a training record is linked to an employee (via PersonnelNumber), all employee fields are populated. When no employee match exists, the NonEmployeeIDNumber field contains the ID number from the training event.

## Frontend Integration

### For JSON Export

Use the `/training-records/export/filtered` endpoint with appropriate filters and process the JSON response on the frontend to generate CSV.

### For Direct CSV Download

Use the `/training-records/export/csv` endpoint which returns a ready-to-download CSV file.

### Example Frontend Usage

```javascript
// Get filtered data for frontend processing
const response = await fetch(
  "/api/Reports/training-records/export/filtered?startDate=2024-01-01&endDate=2024-12-31",
  {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  }
);
const data = await response.json();

// Or download CSV directly
window.open(
  "/api/Reports/training-records/export/csv?startDate=2024-01-01&endDate=2024-12-31"
);
```

## Error Handling

All endpoints return consistent error responses:

- `400 Bad Request` - Invalid input parameters
- `401 Unauthorized` - Missing or invalid authentication token
- `500 Internal Server Error` - Server-side error

Error responses include descriptive messages to help with troubleshooting.
