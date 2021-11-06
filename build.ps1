Write-Host "------------------ Restoring packages ------------------"
Write-Host "--------------------------------------------------------"
dotnet restore Employees.sln
Write-Host ""

Write-Host "------------------ Building solution ------------------"
Write-Host "-------------------------------------------------------"
dotnet build Employees.sln
Write-Host ""

Write-Host "------------------ Building image for platformsservice ------------------"
Write-Host "--------------------------------------------------------------------------"
docker build -t piotrkowalski93/platformsservice:latest -f "Dockerfile.Platforms" .
Write-Host ""

Write-Host "------------------ Building image for commandsservice ------------------"
Write-Host "-------------------------------------------------------------------------"
docker build  -t piotrkowalski93/commandsservice:latest -f "Dockerfile.Commands" .
Write-Host ""

Write-Host "------------------ Pushing images ------------------"
Write-Host "----------------------------------------------------"
docker push piotrkowalski93/commandsservice
docker push piotrkowalski93/platformsservice
Write-Host ""