# Data Models

**Entity: LookupValues**

| Column     | Type          | Constraints                          | Description                                      |
| ---------- | ------------- | ------------------------------------ | ------------------------------------------------ |
| LookupId   | int           | IDENTITY(1,1), PRIMARY KEY, NOT NULL | Unique identifier for the lookup value           |
| LookupType | nvarchar(50)  | NOT NULL                             | Category or type of the lookup value             |
| Value      | nvarchar(100) | NOT NULL                             | The display value                                |
| Code       | nvarchar(20)  | NULL                                 | Optional code for the lookup value               |
| ParentId   | int           | NULL                                 | Reference to parent lookup for hierarchical data |
| SortOrder  | int           | NULL                                 | Order for displaying lookup values               |
| IsActive   | bit           | NOT NULL                             | Indicates if the lookup value is active          |

**Entity: EmployeeLookup**

| Column               | Type          | Constraints           | Description                            |
| -------------------- | ------------- | --------------------- | -------------------------------------- |
| PersonnelNumber      | nvarchar(20)  | PRIMARY KEY, NOT NULL | Unique personnel identifier            |
| FirstName            | nvarchar(50)  | NOT NULL              | Employee's first name                  |
| LastName             | nvarchar(50)  | NOT NULL              | Employee's last name                   |
| KnownName            | nvarchar(50)  | NULL                  | Employee's preferred or known name     |
| Initials             | nvarchar(10)  | NULL                  | Employee's initials                    |
| Race                 | nvarchar(50)  | NULL                  | Employee's race                        |
| Gender               | nvarchar(20)  | NULL                  | Employee's gender                      |
| Disability           | bit           | NULL                  | Indicates if employee has a disability |
| EELevel              | nvarchar(50)  | NULL                  | Employment equity level                |
| EECategory           | nvarchar(50)  | NULL                  | Employment equity category             |
| JobTitle             | nvarchar(100) | NULL                  | Employee's job title                   |
| JobGrade             | nvarchar(20)  | NULL                  | Employee's job grade                   |
| IDNumber             | nvarchar(13)  | NULL                  | Employee's South African ID number     |
| Site                 | nvarchar(100) | NULL                  | Employee's work site                   |
| HighestQualification | nvarchar(100) | NULL                  | Employee's highest qualification       |

**Entity: Employee**

| Column               | Type          | Constraints           | Description                            |
| -------------------- | ------------- | --------------------- | -------------------------------------- |
| PersonnelNumber      | nvarchar(20)  | PRIMARY KEY, NOT NULL | Unique personnel identifier            |
| FirstName            | nvarchar(50)  | NOT NULL              | Employee's first name                  |
| LastName             | nvarchar(50)  | NOT NULL              | Employee's last name                   |
| KnownName            | nvarchar(50)  | NULL                  | Employee's preferred or known name     |
| Initials             | nvarchar(10)  | NULL                  | Employee's initials                    |
| Race                 | nvarchar(50)  | NULL                  | Employee's race                        |
| Gender               | nvarchar(20)  | NULL                  | Employee's gender                      |
| Disability           | bit           | NULL                  | Indicates if employee has a disability |
| EELevel              | nvarchar(50)  | NULL                  | Employment equity level                |
| EECategory           | nvarchar(50)  | NULL                  | Employment equity category             |
| JobTitle             | nvarchar(100) | NULL                  | Employee's job title                   |
| JobGrade             | nvarchar(20)  | NULL                  | Employee's job grade                   |
| IDNumber             | nvarchar(13)  | NULL                  | Employee's South African ID number     |
| Site                 | nvarchar(100) | NULL                  | Employee's work site                   |
| HighestQualification | nvarchar(100) | NULL                  | Employee's highest qualification       |

**Entity: NonEmployee**

| Field    | Data Type | Length | Required | Description                           |
| -------- | --------- | ------ | -------- | ------------------------------------- |
| IDNumber | string    | 13     | Yes      | South African ID number (primary key) |

**Constraints & Notes:**

- IDNumber: Primary key, 13-digit South African ID number format
- No biographical information captured for non-employees
- ID number validation using South African ID number algorithm

**Business Rules:**

- IDNumber must be valid South African ID number format
- Only training data is captured for non-employees
- No BBBEE or Employment Equity data required for external participants

**Entity: TrainingEvent**

| Field             | Data Type | Length | Required | Description                              |
| ----------------- | --------- | ------ | -------- | ---------------------------------------- |
| TrainingEventId   | int       | -      | Yes      | Primary key, auto-increment              |
| PersonnelNumber   | string    | 8      | No       | Foreign key to Employee (nullable)       |
| IDNumber          | string    | 13     | No       | Foreign key to NonEmployee (nullable)    |
| EventType         | string    | 50     | Yes      | Type/category of training event          |
| TrainingEventName | string    | 100    | Yes      | Name/title of the training intervention  |
| RegionId          | int       | -      | Yes      | Foreign key to Region lookup table       |
| ProvinceId        | int       | -      | Yes      | Foreign key to Province lookup table     |
| MunicipalityId    | int       | -      | Yes      | Foreign key to Municipality lookup table |
| SiteId            | int       | -      | Yes      | Foreign key to Site lookup table         |

**Constraints & Notes:**

- TrainingEventId: Primary key, auto-increment
- Either PersonnelNumber OR IDNumber must be provided (mutually exclusive)
- PersonnelNumber: Foreign key relationship to Employee table (nullable)
- IDNumber: Foreign key relationship to NonEmployee table (nullable)
- One-to-many relationship: One Employee/NonEmployee can have multiple TrainingEvents
- All lookup fields (Region, Province, Municipality, Site) reference separate lookup tables
- EventType: Predefined values from training catalogue
- TrainingEventName: Free text but may reference catalogue entries

**Business Rules:**

- Each training event must be linked to either an employee OR a non-employee
- PersonnelNumber and IDNumber are mutually exclusive (only one can have a value)
- Geographic hierarchy: Region > Province > Municipality > Site
- EventType must exist in training catalogue
- All geographic lookups must be valid references
- Multiple training events can be recorded per participant (employee or non-employee)

### 3. Training Information

**Purpose**: Stores detailed training session data including dates, duration, and cost breakdowns for each training event.

**Entity: TrainingRecordEvents**

| Field                  | Data Type     | Length | Required | Description                           |
| ---------------------- | ------------- | ------ | -------- | ------------------------------------- |
| TrainingRecordEventId  | int           | -      | Yes      | Primary key, auto-increment           |
| TrainingEventId        | int           | -      | Yes      | Foreign key to TrainingEvent          |
| StartDate              | datetime      | -      | Yes      | Training start date and time          |
| EndDate                | datetime      | -      | Yes      | Training end date and time            |
| Hours                  | int           | -      | No       | Training duration in hours            |
| Minutes                | int           | -      | No       | Training duration in minutes          |
| PersonnelNumber        | nvarchar(20)  | -      | No       | Personnel number (from TrainingEvent) |
| Evidence               | bit           | -      | No       | Indicates if evidence is available    |
| CostTrainingMaterials  | decimal(18,2) | -      | No       | Cost of training materials            |
| CostTrainers           | decimal(18,2) | -      | No       | Cost of trainers                      |
| CostTrainingFacilities | decimal(18,2) | -      | No       | Cost of facilities including catering |
| ScholarshipsBursaries  | decimal(18,2) | -      | No       | Scholarships and bursaries amount     |
| CourseFees             | decimal(18,2) | -      | No       | Course fees                           |
| AccommodationTravel    | decimal(18,2) | -      | No       | Accommodation and travel costs        |
| AdministrationCosts    | decimal(18,2) | -      | No       | Administration costs                  |
| EquipmentDepreciation  | decimal(18,2) | -      | No       | Equipment depreciation costs          |

**Constraints & Notes:**

- TrainingRecordEventId: Primary key, auto-increment
- TrainingEventId: Foreign key relationship to TrainingEvent table
- One-to-many relationship: One TrainingEvent can have multiple TrainingRecordEvents
- All cost fields use decimal(18,2) for monetary values
- Hours and Minutes fields can be used together for precise duration tracking
- PersonnelNumber duplicated for easier reporting (denormalized from TrainingEvent)

**Business Rules:**

- Each training record event must be linked to a valid TrainingEvent
- StartDate must be before or equal to EndDate
- Cost fields are optional and default to NULL if not provided
- Evidence field tracks completion documentation availability
- Multiple training record events can exist per training event (for multi-session courses)

**Entity: UserPermissions**

| Column         | Type          | Constraints                          | Description                                |
| -------------- | ------------- | ------------------------------------ | ------------------------------------------ |
| PermissionId   | int           | IDENTITY(1,1), PRIMARY KEY, NOT NULL | Unique identifier for the permission       |
| PersonnelNo    | nvarchar(20)  | NOT NULL                             | Personnel number (foreign key to Employee) |
| PermissionCode | nvarchar(100) | NOT NULL                             | Code/string representing the permission    |
| CreatedDate    | datetime      | NOT NULL, DEFAULT GETDATE()          | Timestamp when the permission was created  |

**Constraints & Notes:**

- PermissionId: Primary key, auto-increment.
- PersonnelNo: Required; should reference Employee.PersonnelNumber.
- PermissionCode: Free-form string; consider using a lookup table if codes become standardized.
- CreatedDate: Automatically set on insert.

**Business Rules:**

- Each UserPermissions row must reference a valid employee PersonnelNo.
- PersonnelNo and PermissionCode are required.
- A personnel can have multiple permission records.
- CreatedDate indicates when the permission was granted and must not be altered except for audit purposes.
- Consider enforcing uniqueness if duplicate permission codes per personnel are not allowed (e.g., UNIQUE(PersonnelNo, PermissionCode)).
