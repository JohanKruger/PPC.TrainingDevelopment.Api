**Entity: TrainingPsMaster**

| Column                       | Type          | Constraints           | Description                        |
| ---------------------------- | ------------- | --------------------- | ---------------------------------- |
| PersonnelNumber              | nvarchar(24)  | PRIMARY KEY, NOT NULL | Unique personnel identifier        |
| Title                        | nvarchar(15)  | NULL                  | Title (e.g., Mr, Ms, Dr)           |
| LastName                     | nvarchar(120) | NOT NULL              | Employee's last name               |
| FirstName                    | nvarchar(120) | NOT NULL              | Employee's first name              |
| Initials                     | nvarchar(30)  | NULL                  | Employee's initials                |
| SecondName                   | nvarchar(120) | NULL                  | Employee's second name             |
| KnownAs                      | nvarchar(120) | NULL                  | Employee's preferred or known name |
| Gender                       | nvarchar(7)   | NULL                  | Employee's gender                  |
| DateOfBirth                  | date          | NULL                  | Employee's date of birth           |
| IdNumber                     | nvarchar(90)  | NULL                  | Employee's ID number               |
| RaceCode                     | nvarchar(6)   | NULL                  | Code for employee's race           |
| RaceDescription              | nvarchar(150) | NULL                  | Description of employee's race     |
| CompanyCode                  | nvarchar(12)  | NULL                  | Company code                       |
| CompanyName                  | nvarchar(75)  | NULL                  | Company name                       |
| PersonnelArea                | nvarchar(12)  | NULL                  | Personnel area code                |
| PersonnelAreaDescription     | nvarchar(90)  | NULL                  | Personnel area description         |
| PersonnelSubArea             | nvarchar(12)  | NULL                  | Personnel sub-area code            |
| PersonnelSubAreaDescription  | nvarchar(45)  | NULL                  | Personnel sub-area description     |
| EmployeeGroup                | nvarchar(3)   | NULL                  | Employee group code                |
| EmployeeGroupDescription     | nvarchar(60)  | NULL                  | Employee group description         |
| EmployeeSubGroup             | nvarchar(6)   | NULL                  | Employee sub-group code            |
| EmployeeSubGroupDescription  | nvarchar(60)  | NULL                  | Employee sub-group description     |
| OrganisationUnit             | nvarchar(24)  | NULL                  | Organisation unit code             |
| OrganisationUnitDescription  | nvarchar(120) | NULL                  | Organisation unit description      |
| Position                     | nvarchar(24)  | NULL                  | Position code                      |
| PositionDescription          | nvarchar(120) | NULL                  | Position description               |
| StartDate                    | date          | NULL                  | Employment start date              |
| EndDate                      | date          | NULL                  | Employment end date                |
| CostCenter                   | nvarchar(30)  | NULL                  | Cost center code                   |
| CostCenterDescription        | nvarchar(120) | NULL                  | Cost center description            |
| EmploymentStatus             | nvarchar(3)   | NULL                  | Employment status code             |
| EmployementStatusDescription | nvarchar(120) | NULL                  | Employment status description      |
| EmailAddress                 | nvarchar(723) | NULL                  | Employee's email address           |
| Disability                   | nvarchar(3)   | NULL                  | Disability status                  |
| EELevel                      | nvarchar(80)  | NULL                  | Employment equity level            |
| EECategory                   | nvarchar(300) | NULL                  | Employment equity category         |
| JobGrade                     | nvarchar(6)   | NULL                  | Job grade                          |
| ManagerPersonnelNumber       | nvarchar(135) | NULL                  | Manager's personnel number         |
| ManagerName                  | nvarchar(241) | NULL                  | Manager's name                     |
| ManagerEmailAddress          | nvarchar(723) | NULL                  | Manager's email address            |
| ManagerKnownAs               | nvarchar(120) | NULL                  | Manager's preferred or known name  |
| ManagerCostCenter            | nvarchar(30)  | NULL                  | Manager's cost center code         |

**Constraints & Notes:**

- Data is stored in an Oracle db table : SAPBIUSER.MV_EMP_TRAINING_PS_MASTER
- Connection details :
- Oracle server : sdnuxprt.ppc.co.za [10.2.251.17]
- port : 1527
- Oracle SID : PRD
- Oracle user : HR_TR_USER
- password : xXfhv7tWhISac6ay
