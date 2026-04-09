# Build script for RunHidden and RunHiddenWait executables

# Use Visual Studio's MSBuild instead of the old .NET Framework version
$msbuild = "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe"
if (-not (Test-Path $msbuild)) {
    $msbuild = "msbuild"  # Fallback to PATH
}

Write-Host "Building RunHidden.exe - Release configuration..." -ForegroundColor Cyan
& $msbuild RunHidden.csproj /p:Configuration=Release /p:Platform=AnyCPU /v:minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed for RunHidden.exe" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host "Successfully built RunHidden.exe" -ForegroundColor Green
Write-Host "  Output: bin\Release\RunHidden.exe" -ForegroundColor Gray
Write-Host ""

Write-Host "Building RunHiddenWait.exe - ReleaseWait configuration..." -ForegroundColor Cyan
& $msbuild RunHidden.csproj /p:Configuration=ReleaseWait /p:Platform=AnyCPU /v:minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed for RunHiddenWait.exe" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host "Successfully built RunHiddenWait.exe" -ForegroundColor Green
Write-Host "  Output: bin\ReleaseWait\RunHiddenWait.exe" -ForegroundColor Gray
Write-Host ""

Write-Host "All builds completed successfully!" -ForegroundColor Green
