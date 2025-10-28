# Reports Interface Integration

This document outlines the Reports API integration for exporting TrainingRecordEvents with their related Training Event and Employee data in a flat structure for CSV export.

## Overview

The Reports API provides a comprehensive solution for exporting training data in a flattened structure that combines:

- Training Record Event details (costs, dates, duration)
- Training Event information (type, name, location)
- Employee demographic and job data (when available)
- Non-employee ID information (when no employee match exists)

## API Endpoints

### Base URL

```
/api/Reports
```

### Available Endpoints

1. **GET** `/training-records/export` - Get all training records
2. **GET** `/training-records/export/by-date` - Filter by date range
3. **GET** `/training-records/export/by-personnel/{personnelNumber}` - Filter by personnel number
4. **GET** `/training-records/export/by-event/{trainingEventId}` - Filter by training event
5. **GET** `/training-records/export/filtered` - Multiple optional filters
6. **GET** `/training-records/export/csv` - Direct CSV download

## Response Model

### TrainingReportResponse

```csharp
public class TrainingReportResponse
{
    // Training Record Event fields
    public int TrainingRecordEventId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Hours { get; set; }
    public int? Minutes { get; set; }
    public bool? Evidence { get; set; }
    public string? ServiceProviderExternal { get; set; }
    public decimal? CostTrainingMaterials { get; set; }
    public decimal? CostTrainers { get; set; }
    public decimal? CostTrainingFacilities { get; set; }
    public decimal? ScholarshipsBursaries { get; set; }
    public decimal? CourseFees { get; set; }
    public decimal? Accommodation { get; set; }
    public decimal? Travel { get; set; }
    public decimal? Meal { get; set; }
    public decimal? AdministrationCosts { get; set; }
    public decimal? EquipmentDepreciation { get; set; }
    public decimal TotalCosts { get; set; }
    public int? TotalDurationMinutes { get; set; }

    // Training Event fields
    public int TrainingEventId { get; set; }
    public string? EventType { get; set; }
    public string? TrainingEventName { get; set; }
    public string? Region { get; set; }
    public string? Province { get; set; }
    public string? Municipality { get; set; }
    public string? Site { get; set; }

    // Employee fields
    public string? PersonnelNumber { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public string? EmployeeKnownName { get; set; }
    public string? EmployeeInitials { get; set; }
    public string? EmployeeRace { get; set; }
    public string? EmployeeGender { get; set; }
    public bool? EmployeeDisability { get; set; }
    public string? EmployeeEELevel { get; set; }
    public string? EmployeeEECategory { get; set; }
    public string? EmployeeJobTitle { get; set; }
    public string? EmployeeJobGrade { get; set; }
    public string? EmployeeIDNumber { get; set; }
    public string? EmployeeSite { get; set; }
    public string? EmployeeHighestQualification { get; set; }
    public string? EmployeeNotes { get; set; }

    // Non-Employee fields
    public string? NonEmployeeIDNumber { get; set; }
}
```

## Service Architecture

### IReportsService Interface

```csharp
public interface IReportsService
{
    Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync();
    Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(string personnelNumber);
    Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsByEventForExportAsync(int trainingEventId);
    Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? personnelNumber = null,
        int? trainingEventId = null,
        bool? hasEvidence = null);
}
```

### ReportsService Implementation

The service uses Entity Framework with Include statements to load related data:

- TrainingRecordEvent → TrainingEvent → Employee
- TrainingRecordEvent → TrainingEvent → LookupValues (EventType, TrainingEventName, Region, Province, Municipality, Site)

The service projects the data into a flat `TrainingReportResponse` structure using LINQ Select.

## Database Relationships

### Core Entities

1. **TrainingRecordEvents** (Primary)

   - Foreign Key: TrainingEventId → TrainingEvents
   - Contains: Cost data, dates, duration, evidence flag

2. **TrainingEvents** (Related)

   - Foreign Key: PersonnelNumber → Employees (optional)
   - Foreign Keys: EventTypeId, TrainingEventNameId, RegionId, ProvinceId, MunicipalityId, SiteId → LookupValues
   - Contains: Event details, location references

3. **Employees** (Related, Optional)

   - Primary Key: PersonnelNumber
   - Contains: Demographic and job information

4. **LookupValues** (Related)
   - Contains: Descriptive text for event types, names, and locations

### Data Flow

```
TrainingRecordEvent
├── TrainingEvent
│   ├── Employee (via PersonnelNumber)
│   ├── EventType (LookupValue)
│   ├── TrainingEventName (LookupValue)
│   ├── Region (LookupValue)
│   ├── Province (LookupValue)
│   ├── Municipality (LookupValue)
│   └── Site (LookupValue)
```

## Frontend Integration

### For React/Angular Applications

```typescript
interface TrainingReportResponse {
  trainingRecordEventId: number;
  trainingEventId: number;
  startDate: string;
  endDate: string;
  hours?: number;
  minutes?: number;
  evidence?: boolean;
  serviceProviderExternal?: string;
  // ... cost fields
  totalCosts: number;
  totalDurationMinutes?: number;

  // Training Event fields
  eventType?: string;
  trainingEventName?: string;
  region?: string;
  province?: string;
  municipality?: string;
  site?: string;

  // Employee fields
  personnelNumber?: string;
  employeeFirstName?: string;
  employeeLastName?: string;
  // ... other employee fields

  // Non-employee fields
  nonEmployeeIDNumber?: string;
}

// Service method
async function getTrainingRecords(filters?: {
  startDate?: string;
  endDate?: string;
  personnelNumber?: string;
  trainingEventId?: number;
  hasEvidence?: boolean;
}): Promise<TrainingReportResponse[]> {
  const params = new URLSearchParams();
  if (filters?.startDate) params.append("startDate", filters.startDate);
  if (filters?.endDate) params.append("endDate", filters.endDate);
  if (filters?.personnelNumber)
    params.append("personnelNumber", filters.personnelNumber);
  if (filters?.trainingEventId)
    params.append("trainingEventId", filters.trainingEventId.toString());
  if (filters?.hasEvidence !== undefined)
    params.append("hasEvidence", filters.hasEvidence.toString());

  const response = await fetch(
    `/api/Reports/training-records/export/filtered?${params}`,
    {
      headers: {
        Authorization: `Bearer ${authToken}`,
      },
    }
  );

  return response.json();
}

// CSV Export method
function exportToCsv(filters?: any) {
  const params = new URLSearchParams(filters);
  window.open(`/api/Reports/training-records/export/csv?${params}`);
}
```

### CSV Export Features

1. **Direct Download**: Use `/training-records/export/csv` endpoint
2. **Custom Filtering**: All filter parameters are supported
3. **Automatic Filename**: Format: `training_records_yyyyMMdd_HHmmss.csv`
4. **CSV Escaping**: Proper handling of quotes and special characters
5. **UTF-8 Encoding**: Full character set support

## Error Handling

### Validation Errors

- Date range validation (start date ≤ end date)
- Positive integer validation for IDs
- Non-empty string validation for personnel numbers

### HTTP Status Codes

- `200 OK`: Successful response
- `400 Bad Request`: Validation errors
- `401 Unauthorized`: Authentication required
- `500 Internal Server Error`: Server-side errors

## Performance Considerations

1. **Data Loading**: Uses Entity Framework Include to efficiently load related data
2. **Projection**: Projects directly to response model to minimize memory usage
3. **Filtering**: Database-level filtering for optimal performance
4. **Ordering**: Consistent ordering by StartDate, then TrainingRecordEventId
5. **CSV Generation**: Server-side generation for large datasets

## Security

1. **Authentication**: JWT Bearer token required for all endpoints
2. **Authorization**: Inherits from controller-level authorization
3. **Data Access**: No direct SQL injection risks (uses Entity Framework)
4. **File Download**: Secure CSV generation without file system access

## Testing

### Unit Testing

- Service methods with mocked DbContext
- CSV generation with various data scenarios
- Filter logic validation

### Integration Testing

- Full API endpoint testing
- Database integration testing
- Authentication flow testing

### Performance Testing

- Large dataset export testing
- Concurrent request handling
- Memory usage monitoring

## Deployment Considerations

1. **Database Performance**: Ensure proper indexing on frequently filtered columns
2. **Memory Management**: Monitor memory usage for large CSV exports
3. **Timeout Settings**: Configure appropriate timeouts for large exports
4. **Logging**: Comprehensive logging for troubleshooting
5. **Monitoring**: Track export frequency and performance metrics
