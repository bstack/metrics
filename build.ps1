# Stop on error, see http://redsymbol.net/articles/unofficial-bash-strict-mode/
$ErrorActionPreference = 'Stop'

### Args
if ($args.Count -ne 1) { write-host "Usage is $($MyInvocation.MyCommand.Path) version"; exit 1 }
# Version
[string]$version = $args[0]

write-host "### Paths"
$rootDirectoryPath = (split-path ($MyInvocation.MyCommand.Path))
$solutionFilePath = join-path $rootDirectoryPath Metrics.sln
$packagesDirectoryPath = join-path $rootDirectoryPath packages

write-host "### Empty packages directory"
if (test-path $packagesDirectoryPath) { Remove-Item -recurse -force $packagesDirectoryPath }

write-host "### Build and pack"
dotnet clean $solutionFilePath
dotnet pack $solutionFilePath --configuration Release /p:PackageVersion=$version /p:Version=$version --output $packagesDirectoryPath
if ($LastExitCode -ne 0) { write-host 'Build and pack failure !'; exit 1 }

write-host "### Push instruction"
write-host "Copy-Item '$packagesDirectoryPath\Metric.Client.*.nupkg' YOUR_NUGET_LOCATION"