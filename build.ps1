# Stop on error, see http://redsymbol.net/articles/unofficial-bash-strict-mode/
$ErrorActionPreference = 'Stop'

### Args
if ($args.Count -ne 1) { write-host "Usage is $($MyInvocation.MyCommand.Path) version"; exit 1 }
# Version
[string]$version = $args[0]


write-host "### Is environment available"
if (!(test-path 'c:\program files (x86)\microsoft visual studio\2017\*\msbuild\15.0\bin\msbuild.exe')) { write-host 'msbuild not available !'; exit 1 }


write-host "### Paths"
$nugetSpecFileName = 'Metric.Client.nuspec'
$rootDirectoryPath = (split-path ($MyInvocation.MyCommand.Path))
$solutionFilePath = join-path $rootDirectoryPath Metrics.sln
$packagesDirectoryPath = join-path $rootDirectoryPath packages
$nugetSpecFilePath = join-path $rootDirectoryPath $nugetSpecFileName
$nugetPackageFilePath = join-path $packagesDirectoryPath $nugetSpecFileName.Replace('.nuspec', '.' + $version + '.nupkg')


echo "### nuget ensure we have our dependencies"
nuget restore $solutionFilePath -verbosity detailed


write-host "### Compile"
# Build
& 'c:\program files (x86)\microsoft visual studio\2017\*\msbuild\15.0\bin\msbuild.exe' $solutionFilePath /target:ReBuild /property:Configuration=Release /property:Version=$version
if ($LastExitCode -ne 0) { write-host 'Compile failure !'; exit 1 }


write-host "### nuget pack"
# Ensure we have an empty packages directory
if (test-path $packagesDirectoryPath) { rm -recurse -force $packagesDirectoryPath }
mkdir $packagesDirectoryPath | out-null
# nuget pack
nuget pack $nugetSpecFilePath -outputdirectory $packagesDirectoryPath -version $version -verbosity detailed


write-host "### Push instruction"
write-host "You can push the package just created to the default source (nuget.org) with the following command`n`t`$apiKey=key_from_nuget.org`n`tnuget push $nugetPackageFilePath -apikey `$apiKey -verbosity detailed"
