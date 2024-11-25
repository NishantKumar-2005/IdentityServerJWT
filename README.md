﻿# IdentityServerJWT
Register User 
$user = @{
    username = "testuser"
    email = "testuser@example.com"
    password = "Test@1234"
}
 
Invoke-RestMethod -Uri "http://localhost:5140/api/account/register" -Method Post -ContentType "application/json" -Body ($user | ConvertTo-Json)

# get JWT token
$body = @{
    grant_type = "password"
    username = "testuser"
    password = "Test@1234"
    client_id = "client_id"
    client_secret = "client_secret"
    scope = "api1"
}

$tokenResponse = Invoke-RestMethod -Uri "http://localhost:5140/connect/token" -Method Post -ContentType "application/x-www-form-urlencoded" -Body $body
$tokenResponse.access_token
