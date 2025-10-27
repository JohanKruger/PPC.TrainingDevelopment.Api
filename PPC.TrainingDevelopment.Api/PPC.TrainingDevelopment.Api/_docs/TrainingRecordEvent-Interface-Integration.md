# Training Record Event Interface Integration

## Overview

The Training Record Event interface provides comprehensive CRUD operations and specialized queries for managing detailed training session data including dates, duration, and cost breakdowns.

## Entity Structure

The TrainingRecordEvent entity represents detailed training session records with the following key components:

- **Session Information**: Start/end dates, duration tracking
- **Cost Management**: Detailed cost breakdown across 8 categories
- **Evidence Tracking**: Documentation availability indicator
- **Participant Linkage**: Connection to personnel and training events

## Key Features

### 1. Comprehensive Cost Tracking

- Training Materials
- Trainers
- Training Facilities (including catering)
- Scholarships and Bursaries
- Course Fees
- Accommodation (separate tracking)
- Travel (separate tracking)
- Administration Costs
- Equipment Depreciation

### 2. Advanced Querying Capabilities

- Filter by training event, personnel, or date range
- Evidence-based filtering (with/without evidence)
- Cost aggregation by various criteria
- Duration calculations

### 3. Business Rule Validation

- Date range validation (end date ≥ start date)
- Training event existence validation
- Positive cost value validation
- Related entity integrity checks

## Service Methods

### Core CRUD Operations

- `GetAllAsync()` - Retrieve all training record events
- `GetByIdAsync(int)` - Get specific training record event
- `CreateAsync(TrainingRecordEvent)` - Create new training record event
- `UpdateAsync(int, TrainingRecordEvent)` - Update existing training record event
- `DeleteAsync(int)` - Delete training record event

### Specialized Query Methods

- `GetByTrainingEventIdAsync(int)` - Filter by training event
- `GetByPersonnelNumberAsync(string)` - Filter by employee
- `GetByDateRangeAsync(DateTime, DateTime)` - Filter by date range
- `GetWithEvidenceAsync()` - Records with evidence
- `GetWithoutEvidenceAsync()` - Records without evidence

### Cost Analysis Methods

- `GetTotalCostsByTrainingEventIdAsync(int)` - Costs per training event
- `GetTotalCostsByPersonnelNumberAsync(string)` - Costs per employee
- `GetTotalCostsByDateRangeAsync(DateTime, DateTime)` - Costs per period

### Validation Methods

- `ValidateDateRangeAsync(DateTime, DateTime)` - Date range validation
- `ValidateTrainingEventExistsAsync(int)` - Training event existence check
- `ExistsAsync(int)` - Training record event existence check

## Controller Endpoints

### Data Retrieval

- `GET /api/TrainingRecordEvent` - All training record events
- `GET /api/TrainingRecordEvent/{id}` - Specific training record event
- `GET /api/TrainingRecordEvent/training-event/{trainingEventId}` - By training event
- `GET /api/TrainingRecordEvent/personnel/{personnelNumber}` - By personnel
- `GET /api/TrainingRecordEvent/date-range` - By date range
- `GET /api/TrainingRecordEvent/with-evidence` - With evidence
- `GET /api/TrainingRecordEvent/without-evidence` - Without evidence

### Cost Analysis

- `GET /api/TrainingRecordEvent/costs/training-event/{trainingEventId}` - Costs by training event
- `GET /api/TrainingRecordEvent/costs/personnel/{personnelNumber}` - Costs by personnel
- `GET /api/TrainingRecordEvent/costs/date-range` - Costs by date range

### Data Management

- `POST /api/TrainingRecordEvent` - Create training record event
- `PUT /api/TrainingRecordEvent/{id}` - Update training record event
- `DELETE /api/TrainingRecordEvent/{id}` - Delete training record event

## Request/Response Models

### CreateTrainingRecordEventRequest

```csharp
public class CreateTrainingRecordEventRequest
{
    [Required] public int TrainingEventId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    public int? Hours { get; set; }
    public int? Minutes { get; set; }
    [MaxLength(20)] public string? PersonnelNumber { get; set; }
    public bool? Evidence { get; set; }
    [Range(0, double.MaxValue)] public decimal? CostTrainingMaterials { get; set; }
    [Range(0, double.MaxValue)] public decimal? CostTrainers { get; set; }
    [Range(0, double.MaxValue)] public decimal? CostTrainingFacilities { get; set; }
    [Range(0, double.MaxValue)] public decimal? ScholarshipsBursaries { get; set; }
    [Range(0, double.MaxValue)] public decimal? CourseFees { get; set; }
    [Range(0, double.MaxValue)] public decimal? Accommodation { get; set; }
    [Range(0, double.MaxValue)] public decimal? Travel { get; set; }
    [Range(0, double.MaxValue)] public decimal? AdministrationCosts { get; set; }
    [Range(0, double.MaxValue)] public decimal? EquipmentDepreciation { get; set; }
}
```

### TrainingRecordEvent Response

```csharp
public class TrainingRecordEvent
{
    public int TrainingRecordEventId { get; set; }
    public int TrainingEventId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Hours { get; set; }
    public int? Minutes { get; set; }
    public string? PersonnelNumber { get; set; }
    public bool? Evidence { get; set; }
    public decimal? CostTrainingMaterials { get; set; }
    public decimal? CostTrainers { get; set; }
    public decimal? CostTrainingFacilities { get; set; }
    public decimal? ScholarshipsBursaries { get; set; }
    public decimal? CourseFees { get; set; }
    public decimal? Accommodation { get; set; }
    public decimal? Travel { get; set; }
    public decimal? AdministrationCosts { get; set; }
    public decimal? EquipmentDepreciation { get; set; }
    public TrainingEvent? TrainingEvent { get; set; }
    public int? TotalDurationMinutes { get; set; } // Calculated
    public decimal TotalCosts { get; set; } // Calculated
}
```

## Database Configuration

### Entity Configuration

- Primary Key: `TrainingRecordEventId` (auto-increment)
- Foreign Key: `TrainingEventId` (cascade delete)
- Indexes: TrainingEventId, PersonnelNumber, StartDate, EndDate, Evidence
- Decimal Precision: 18,2 for all cost fields

### Relationships

- **TrainingEvent**: Many-to-One (required)
- **Navigation**: TrainingEvent.TrainingRecordEvents collection

## Business Logic

### Validation Rules

1. **Date Constraints**: EndDate >= StartDate
2. **Training Event**: Must exist in TrainingEvents table
3. **Cost Values**: Must be positive if provided
4. **Duration**: Hours and minutes are optional but complementary

### Calculated Properties

1. **TotalDurationMinutes**: (Hours × 60) + Minutes
2. **TotalCosts**: Sum of all cost categories

### Data Integrity

- Cascade delete: Removing a TrainingEvent removes its TrainingRecordEvents
- Foreign key constraints ensure referential integrity
- Indexed fields improve query performance

## Integration Patterns

### Service Layer

- Repository pattern with Entity Framework
- Async/await for all database operations
- Comprehensive error handling and validation
- Include related entities for complete data

### Controller Layer

- RESTful endpoint design
- JWT authentication required
- Model validation with data annotations
- Standardized error responses
- Proper HTTP status codes

### Error Handling

- ArgumentException for business rule violations
- 404 for not found resources
- 400 for validation errors
- 500 for unexpected errors
- Descriptive error messages

## Usage Examples

### Creating a Training Record Event

```http
POST /api/TrainingRecordEvent
{
  "trainingEventId": 1,
  "startDate": "2024-01-15T09:00:00Z",
  "endDate": "2024-01-15T17:00:00Z",
  "hours": 8,
  "minutes": 0,
  "personnelNumber": "12345678",
  "evidence": true,
  "costTrainingMaterials": 150.00,
  "costTrainers": 500.00
}
```

### Querying Costs by Date Range

```http
GET /api/TrainingRecordEvent/costs/date-range?startDate=2024-01-01&endDate=2024-12-31
```

### Filtering by Evidence Status

```http
GET /api/TrainingRecordEvent/without-evidence
```

## Performance Considerations

### Optimizations

- Database indexes on frequently queried fields
- Include strategies for related data loading
- Efficient cost calculation queries
- Pagination support for large result sets

### Monitoring

- Query performance tracking
- Cost calculation efficiency
- Memory usage for large datasets
- Response time optimization
