#!/usr/bin/env pwsh

# PowerShell script to test audit logging functionality

$baseUrl = "http://localhost:5048"

Write-Host "Testing Audit Logging Functionality" -ForegroundColor Green

# Step 1: Get authentication token
Write-Host "Step 1: Getting authentication token..." -ForegroundColor Yellow
$loginRequest = @{
    username = "admin"
    password = "admin"
} | ConvertTo-Json

try {
    $loginResponse = Invoke-RestMethod -Uri "$baseUrl/api/authentication/login" -Method POST -Body $loginRequest -ContentType "application/json"
    $token = $loginResponse.token
    Write-Host "✓ Token obtained successfully" -ForegroundColor Green
} catch {
    Write-Host "✗ Failed to get token: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Step 2: Make a test API call that should be audited
Write-Host "Step 2: Making test API call (GET /api/employee)..." -ForegroundColor Yellow
$headers = @{
    "Authorization" = "Bearer $token"
}

try {
    $employees = Invoke-RestMethod -Uri "$baseUrl/api/employee" -Method GET -Headers $headers
    Write-Host "✓ Employees retrieved successfully" -ForegroundColor Green
} catch {
    Write-Host "✗ Failed to get employees: $($_.Exception.Message)" -ForegroundColor Red
}

# Step 3: Check audit logs
Write-Host "Step 3: Checking audit logs..." -ForegroundColor Yellow

try {
    $auditLogs = Invoke-RestMethod -Uri "$baseUrl/api/auditlogs?pageSize=10" -Method GET -Headers $headers
    Write-Host "✓ Audit logs retrieved successfully" -ForegroundColor Green
    Write-Host "Found $($auditLogs.Count) audit log entries" -ForegroundColor Cyan
    
    if ($auditLogs.Count -gt 0) {
        Write-Host "Latest audit log entries:" -ForegroundColor Cyan
        $auditLogs | Select-Object -First 5 | ForEach-Object {
            Write-Host "  - [$($_.Timestamp)] $($_.HttpMethod) $($_.RequestPath) - Status: $($_.StatusCode) - User: $($_.UserName) - Duration: $($_.DurationMs)ms" -ForegroundColor White
        }
    }
} catch {
    Write-Host "✗ Failed to get audit logs: $($_.Exception.Message)" -ForegroundColor Red
}

# Step 4: Test creating an employee (should be audited)
Write-Host "Step 4: Testing employee creation (should be audited)..." -ForegroundColor Yellow
$newEmployee = @{
    personnelNumber = "TEST$(Get-Random -Minimum 1000 -Maximum 9999)"
    firstName = "Test"
    lastName = "User"
    knownName = "Tester"
    race = "Test Race"
    gender = "Test Gender"
    disability = $false
    eeLevel = "Test Level"
    eeCategory = "Test Category"
    jobTitle = "Test Position"
    jobGrade = "T1"
    idNumber = "$(Get-Random -Minimum 1000000000000 -Maximum 9999999999999)"
    site = "Test Site"
} | ConvertTo-Json

try {
    $createdEmployee = Invoke-RestMethod -Uri "$baseUrl/api/employee" -Method POST -Body $newEmployee -ContentType "application/json" -Headers $headers
    Write-Host "✓ Employee created successfully: $($createdEmployee.personnelNumber)" -ForegroundColor Green
    
    # Clean up - delete the test employee
    try {
        Invoke-RestMethod -Uri "$baseUrl/api/employee/$($createdEmployee.personnelNumber)" -Method DELETE -Headers $headers
        Write-Host "✓ Test employee deleted successfully" -ForegroundColor Green
    } catch {
        Write-Host "⚠ Failed to delete test employee: $($_.Exception.Message)" -ForegroundColor Yellow
    }
} catch {
    Write-Host "✗ Failed to create employee: $($_.Exception.Message)" -ForegroundColor Red
}

# Step 5: Check audit logs again to see new entries
Write-Host "Step 5: Checking updated audit logs..." -ForegroundColor Yellow

try {
    $updatedAuditLogs = Invoke-RestMethod -Uri "$baseUrl/api/auditlogs?pageSize=15" -Method GET -Headers $headers
    Write-Host "✓ Updated audit logs retrieved successfully" -ForegroundColor Green
    Write-Host "Found $($updatedAuditLogs.Count) total audit log entries" -ForegroundColor Cyan
    
    if ($updatedAuditLogs.Count -gt 0) {
        Write-Host "Latest audit log entries after testing:" -ForegroundColor Cyan
        $updatedAuditLogs | Select-Object -First 8 | ForEach-Object {
            $statusColor = if ($_.StatusCode -ge 200 -and $_.StatusCode -lt 300) { "Green" } elseif ($_.StatusCode -ge 400) { "Red" } else { "Yellow" }
            Write-Host "  - [$($_.Timestamp)] $($_.HttpMethod) $($_.RequestPath) - " -NoNewline -ForegroundColor White
            Write-Host "Status: $($_.StatusCode)" -NoNewline -ForegroundColor $statusColor
            Write-Host " - User: $($_.UserName) - Duration: $($_.DurationMs)ms" -ForegroundColor White
        }
    }
} catch {
    Write-Host "✗ Failed to get updated audit logs: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`nAudit logging test completed!" -ForegroundColor Green
Write-Host "Check the database AuditLogs table for complete audit trail." -ForegroundColor Cyan