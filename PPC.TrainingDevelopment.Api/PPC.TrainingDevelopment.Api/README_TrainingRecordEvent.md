# Training Record Event API

This document describes the Training Record Event API endpoints for managing detailed training session data including dates, duration, and cost breakdowns.

## Overview

The Training Record Event entity stores detailed information about training sessions linked to training events. It includes:

- Session timing (start/end dates, duration)
- Cost breakdowns across multiple categories
- Evidence tracking
- Personnel information

## Authentication

All endpoints require JWT authentication. Include the Bearer token in the Authorization header:

```
Authorization: Bearer YOUR_JWT_TOKEN_HERE
```

## Endpoints

### 1. Get All Training Record Events

- **GET** `/api/TrainingRecordEvent`
- **Description**: Retrieve all training record events with related training event data
- **Response**: Array of training record event objects

### 2. Get Training Record Event by ID

- **GET** `/api/TrainingRecordEvent/{trainingRecordEventId}`
- **Description**: Retrieve a specific training record event by its ID
- **Parameters**:
  - `trainingRecordEventId` (path): The training record event ID
- **Response**: Training record event object or 404 if not found

### 3. Get Training Record Events by Training Event ID

- **GET** `/api/TrainingRecordEvent/training-event/{trainingEventId}`
- **Description**: Retrieve all training record events for a specific training event
- **Parameters**:
  - `trainingEventId` (path): The training event ID
- **Response**: Array of training record event objects

### 4. Get Training Record Events by Personnel Number

- **GET** `/api/TrainingRecordEvent/personnel/{personnelNumber}`
- **Description**: Retrieve all training record events for a specific employee
- **Parameters**:
  - `personnelNumber` (path): The employee's personnel number
- **Response**: Array of training record event objects

### 5. Get Training Record Events by Date Range

- **GET** `/api/TrainingRecordEvent/date-range?startDate={startDate}&endDate={endDate}`
- **Description**: Retrieve training record events within a specific date range
- **Parameters**:
  - `startDate` (query): Start date (yyyy-MM-dd format)
  - `endDate` (query): End date (yyyy-MM-dd format)
- **Response**: Array of training record event objects

### 6. Get Training Record Events with Evidence

- **GET** `/api/TrainingRecordEvent/with-evidence`
- **Description**: Retrieve all training record events that have evidence
- **Response**: Array of training record event objects

### 7. Get Training Record Events without Evidence

- **GET** `/api/TrainingRecordEvent/without-evidence`
- **Description**: Retrieve all training record events that do not have evidence
- **Response**: Array of training record event objects

### 8. Get Total Costs by Training Event ID

- **GET** `/api/TrainingRecordEvent/costs/training-event/{trainingEventId}`
- **Description**: Calculate total costs for all training record events of a specific training event
- **Parameters**:
  - `trainingEventId` (path): The training event ID
- **Response**: Object containing training event ID and total costs

### 9. Get Total Costs by Personnel Number

- **GET** `/api/TrainingRecordEvent/costs/personnel/{personnelNumber}`
- **Description**: Calculate total costs for all training record events of a specific employee
- **Parameters**:
  - `personnelNumber` (path): The employee's personnel number
- **Response**: Object containing personnel number and total costs

### 10. Get Total Costs by Date Range

- **GET** `/api/TrainingRecordEvent/costs/date-range?startDate={startDate}&endDate={endDate}`
- **Description**: Calculate total costs for all training record events within a date range
- **Parameters**:
  - `startDate` (query): Start date (yyyy-MM-dd format)
  - `endDate` (query): End date (yyyy-MM-dd format)
- **Response**: Object containing date range and total costs

### 11. Create Training Record Event

- **POST** `/api/TrainingRecordEvent`
- **Description**: Create a new training record event
- **Request Body**: CreateTrainingRecordEventRequest object
- **Response**: Created training record event object with 201 status

### 12. Update Training Record Event

- **PUT** `/api/TrainingRecordEvent/{trainingRecordEventId}`
- **Description**: Update an existing training record event
- **Parameters**:
  - `trainingRecordEventId` (path): The training record event ID
- **Request Body**: UpdateTrainingRecordEventRequest object
- **Response**: Updated training record event object

### 13. Delete Training Record Event

- **DELETE** `/api/TrainingRecordEvent/{trainingRecordEventId}`
- **Description**: Delete a training record event
- **Parameters**:
  - `trainingRecordEventId` (path): The training record event ID
- **Response**: Success message or 404 if not found

## Data Models

### TrainingRecordEvent

```json
{
  "trainingRecordEventId": 1,
  "trainingEventId": 1,
  "startDate": "2024-01-15T09:00:00Z",
  "endDate": "2024-01-15T17:00:00Z",
  "hours": 8,
  "minutes": 0,
  "personnelNumber": "12345678",
  "evidence": true,
  "costTrainingMaterials": 150.0,
  "costTrainers": 500.0,
  "costTrainingFacilities": 200.0,
  "scholarshipsBursaries": 0.0,
  "courseFees": 300.0,
  "accommodationTravel": 100.0,
  "administrationCosts": 50.0,
  "equipmentDepreciation": 25.0,
  "totalDurationMinutes": 480,
  "totalCosts": 1325.0
}
```

### CreateTrainingRecordEventRequest

```json
{
  "trainingEventId": 1,
  "startDate": "2024-01-15T09:00:00Z",
  "endDate": "2024-01-15T17:00:00Z",
  "hours": 8,
  "minutes": 0,
  "personnelNumber": "12345678",
  "evidence": true,
  "costTrainingMaterials": 150.0,
  "costTrainers": 500.0,
  "costTrainingFacilities": 200.0,
  "scholarshipsBursaries": 0.0,
  "courseFees": 300.0,
  "accommodationTravel": 100.0,
  "administrationCosts": 50.0,
  "equipmentDepreciation": 25.0
}
```

## Cost Categories

The API supports the following cost categories:

- **Training Materials**: Cost of training materials and resources
- **Trainers**: Cost of trainers and facilitators
- **Training Facilities**: Cost of facilities including catering
- **Scholarships/Bursaries**: Scholarships and bursaries amount
- **Course Fees**: Course fees and tuition
- **Accommodation/Travel**: Accommodation and travel costs
- **Administration Costs**: Administration and overhead costs
- **Equipment Depreciation**: Equipment depreciation costs

## Business Rules

1. **Date Validation**: End date must be greater than or equal to start date
2. **Training Event Reference**: Training event must exist before creating a training record event
3. **Cost Validation**: All cost fields must be positive values (if provided)
4. **Duration Tracking**: Hours and minutes can be used together for precise duration tracking
5. **Evidence Tracking**: Evidence field indicates whether completion documentation is available
6. **Personnel Linkage**: Personnel number links to the employee who participated in the training

## Error Handling

The API returns appropriate HTTP status codes:

- **200 OK**: Successful retrieval or update
- **201 Created**: Successful creation
- **400 Bad Request**: Invalid request data or validation errors
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server error

Error responses include a descriptive message:

```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```
