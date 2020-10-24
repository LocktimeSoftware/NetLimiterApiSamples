# NetLimiterAPISamples

## About the NetLimiter API
The NetLimiter API is used to control machine which is running **NetLimiter 4.1.1** or later. All functinality available in NetLimiter is accessible via the API. It's possible to create filters, rules, monitor network activity etc. Our official **NetLimiter Client** (GUI) is whole built above the API too.
Currently, there is no documentation except this Readme file and the API samples. We will add more information on an ongoing basis.

## C# samples
- The samples were created using **Visual Studio 2019**. They work with earlier 2017 version too.
- Most of the samples are .NET framework console application.
- Samples are kept as simple as possible so the counstructs like try/catch etc. are usually omitted.
- NetLimiter nuget package (the API) is added to each project.
- For security reasons, application must run elevated in order to modify NetLimiter service settings. It's possible to disable this requirement (see below).

## How to create project in Visual Studio
1. Create .NET framework project
2. Add ***NetLimiter*** nuget package
3. Use the API (check our samples)

## About the NetLimiter nuget package
https://www.nuget.org/packages/NetLimiter
- Currently NetLimiter nuget package has dependency on NLog logging library. The dependency will be removed soon.
- **NetLimiter 4.1.1** or later must be installed on the machine you are connecting to (usually local machine).

## How to allow non-elevated client to modify NetLimiter settings
In NetLimiter [configuration file](https://netlimiter.com/docs/internals/xml-configuration-file) set ***RequireElevationLocal*** to false:

      <RequireElevationLocal>false</RequireElevationLocal>

For questions and comments: [support@netlimiter.com](mailto://support@netlimier.com)
