cd %~dp0Core\Kore
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Domain\EF\Kore.Domain.EF
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Domain\RavenDb\Kore.Domain.RavenDb
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Caching\Kore.Providers.Caching.Memory
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Containers\Kore.Providers.Containers.Unity
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Containers\Kore.Providers.Containers.TinyIoC
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Logging\Kore.Providers.Logging.Log4Net
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Logging\Kore.Providers.Logging.NLog
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Messages\Kore.Providers.Messages.Resource
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Pdf\Kore.Providers.Pdf.ITextSharp
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Reporting\Kore.Providers.Reporting.SSRS
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0Providers\Serialization\Kore.Providers.Serialization.Newtonsoft
nuget pack Package.nuspec
copy Kore.*.nupkg %~dp0Nuget\.

cd %~dp0
