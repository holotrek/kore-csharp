cd %~dp0Core\KoreAsp
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Domain\EF\KoreAsp.Domain.EF
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Domain\LiteDb\KoreAsp.Domain.LiteDb
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Caching\KoreAsp.Providers.Caching.Memory
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Containers\KoreAsp.Providers.Containers.Unity
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Logging\KoreAsp.Providers.Logging.Log4Net
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Logging\KoreAsp.Providers.Logging.NLog
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Messages\KoreAsp.Providers.Messages.Resource
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Pdf\KoreAsp.Providers.Pdf.ITextSharp
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Reporting\KoreAsp.Providers.Reporting.SSRS
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Serialization\KoreAsp.Providers.Serialization.Newtonsoft
nuget pack Package.nuspec
copy KoreAsp.*.nupkg %~dp0Nuget\.

cd %~dp0
