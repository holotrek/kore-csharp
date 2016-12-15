# Kore (Kaiser-Core) C# .NET library

## Synopsis

Core cross-cutting Providers and DDD Unit of Work / Repository patterns for .NET C# projects.

## Download

This is a library provided for use within a project and is not an independent application. The best way to include the library in your project is to use NuGet:

[Download Kore using NuGet](https://www.nuget.org/packages/Kore/)

## Features

### Domain Layer

Provides a common Unit of Work and Repository pattern that can be used by any ORM. Provides an IEntity and base for all entities to implement, along with a Domain Event Dispatcher that will allow you to create domain subsets that can broadcast events to other domains.

Current implementations are provided that follow the patterns using the following ORMs:
* **Entity Framework 6** [Download from NuGet](https://www.nuget.org/packages/Kore.Domain.EF/)
* **LiteDB** [Download from NuGet](https://www.nuget.org/packages/Kore.Domain.LiteDb/)

### Providers

Kore contains contracts for cross-cutting concerns that are commonly used by every application. Specific implementations of these contracts will be included in separate projects so that you can choose which implementation fits your project best. Implementations are explained in sub-bullet points beneath each provider.

Current list of providers:
* **Authentication**: Provides a common set of methods to use for authentication.
  * Forms [TBD]
* **Caching**: Provides a basic common set of methods for putting things in a cache and retrieving them by key.
  * Memory Cache [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Caching.Memory/)
* **Containers**: This is a wrapper for which IoC container you use, so that the Kore can use it to resolve objects. Mainly only used if you want to use the Domain Event Dispatching functionality.
  * Unity [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Containers.Unity/)
* **Email**: The Kore has a basic email provider that will use native .NET email libraries. It provides a contract however, for the common emailing methods that can be used in a specific implementation.
  * SendGrid [TBD]
* **Logging**: Methods for logging different severity messages to a logging provider. Can be injected into the Messages provider to have any messages be automatically logged.
  * log4net [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Logging.Log4Net/)
  * NLog [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Logging.NLog/)
* **Messages**: Methods for collecting messages from any layer of the application (Controller/Service/Domain) and providing the ability to link them to a certain property and/or GUID. This provider works out of the box with Kore using the base implementation, so a separate library below is not strictly required.
  * Resource integrated message provider [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Messages.Resource/)
    * Allows messages to be pulled from a resource file, so the "AddMessage", and similar, method accepts a resource key rather than the actual message.
* **PDF Manipulation**: Currently has a single method to merge PDFs into one file. May end up being expanded in the future.
  * iTextSharp [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Pdf.ITextSharp/)
* **Reporting**: Contains methods for rendering a report using a provided report definition, datasources, and other parameters.
  * SSRS client-side (RDLC) [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Reporting.SSRS/)
* **Serialization**: Methods to serialize/deserialize objects to and from JSON.
  * Newtonsoft JSON: [Download from NuGet](https://www.nuget.org/packages/Kore.Providers.Serialization.Newtonsoft/)

### Attributes

Provides a set of custom validation attributes that can be used to annotate your view models. Provides extensions for pulling down a JSON object containing information about each property in a given model, including:
* **Property Name**
* **Display Name**
* **Display Group Name**
* **Property name of primary key of the model**
* **Property Type**
* **Short Name**
* **Prompt**
* **Default Value**
* and **validations**, including any default .NET attribute such as [RequiredAttribute](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.requiredattribute(v=vs.110).aspx), as well as custom attributes...
  * **Two-property attributes that compare the value of one to another:**
    * MustBeGreaterThan
    * MustBeGreaterThanOrEqual
    * MustBeLesserThan
    * MustBeLesserThanOrEqual
  * **Attribute that requires the property decorated to be a certain thing when another property is another thing**
    * MustBeWhen
    * MustBeWhenNot
    * NotAllowedWhen
    * NotAllowedWhenNot
    * RequiredWhen
    * RequiredWhenNot

### Extensions

An ever-growing collection of extensions are provided in the Kore for tasks performed often in regards to:
* IEnumerable
* Enum
* Exceptions
* Expressions
* LinQ
* Math functions
* Response filtering/paging/sorting by request
* String functions
* Types

### Formatters

Allows for injecting a specific formatter that will provide consistency in how things are formatted across the application. When a string property on a view model is annotated with the FormattedDate, referencing a DateTime property, the string will be formatted according to the specific strategy injected.

Currently formatters are provided for:
* Date
* Full Name

### Composites

An ever-growing collection of models that are lightweight representations of real-world things that contain simple properties. Also known as "Value Objects", they are immutable (have properties that are set only upon initialization of the object).

Current composites are:
* Full Name (used by the Full Name formatter)
* GPS Point

### Comparers

An ever-growing collection of custom equality comparers.
* Lambda: Provides for the ability to compare a particular property in two different objects. Used by the LinQ extensions.

## Contributing

All pull requests must adhere to the following rules:
* Must pass StyleCop 100% (settings files are provided in every project)
* The Kore project must contain no 3rd party references other than System.Linq.Dynamic, unless approved by the owner.
* If creating a new project for a different implementation, the following rules should be adhered to:
  * Follow naming conventions and folder structure that the other implementation projects use
  * Add the Settings.StyleCop file to the project
  * Any 3rd party library must be included through NuGet
  * AssemblyInfo.cs should contain a description and proper assembly info identifying the copmany/copyright that wrote the library. You may use "Holotrek" if you do not wish to include your name/company name.
  * All projects added are assumed to be licensed under the MIT license that the overall project uses.

## License

See [LICENSE](LICENSE)
