param([switch]$Clean)

if ($Clean) {
    if (Test-Path $pwd\bin) {
        Remove-Item $pwd\bin -Recurse -Force
    }
    dotnet clean
}
dotnet restore
if ($LASTEXITCODE -ne 0) { throw 'restore' }
dotnet build -c Release 
if ($LASTEXITCODE -ne 0) { throw 'build' }
dotnet publish -f net462 -o $pwd\bin\clr -c Release 
if ($LASTEXITCODE -ne 0) { throw 'publish net46' }
dotnet publish -f netstandard2.0 -o $pwd\bin\coreclr -c Release
if ($LASTEXITCODE -ne 0) { throw 'publish netcore' }
#cp $PSScriptRoot\DataProt.ps?1 $pwd\bin\
#powershell.exe -Command {ipmo X:\PackageManager\bin\PackageManager.psd1; Test-PackageManager}