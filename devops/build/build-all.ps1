Install-Module VSSetup -Scope CurrentUser
Install-Module BuildUtils -Scope CurrentUser

$msbuildLocation = Get-LatestMsbuildLocation
set-alias msb $msbuildLocation
msb "../src/LogoFX.Client.Mvvm.View.sln" -property:Configuration=Release /t:Clean,Build