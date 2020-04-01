//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
// #addin "Cake.Npm"
#addin nuget:?package=Cake.VersionReader

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var buildNumber = Argument("buildNumber", "");
var versionSuffix = Argument("versionSuffix", "");
var configuration = Argument("configuration", "Release");
var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var slnPath = "WebApplicationIntegrationTesting.sln";
var temporaryFolder = "./temp";
var publishFolder = "./publish";


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

///define all tasks
Task("Publish-WebApplication1.Api")
	//.IsDependentOn("Build")
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
{	
    var settings = new DotNetCorePublishSettings
    {
        Configuration = "Release",
        OutputDirectory = "./WebApplication1.Api/published-app/"
    };
    DotNetCorePublish("./WebApplication1.Api/WebApplication1.Api.csproj", settings);

	var version = GetVersionWithBuildNumber("./WebApplication1.Api/published-app/WebApplication1.Api.dll", buildNumber);

	var nuGetPackSettings = new NuGetPackSettings {
		OutputDirectory = publishFolder,
		Suffix = versionSuffix,
		Version = version
	};
	NuGetPack("./WebApplication1.Api/WebApplication1.Api.nuspec", nuGetPackSettings);
});


Task("Run-Unit-Tests")
	.IsDependentOn("Build")
	.Does(() => 
{
     var settings = new DotNetCoreTestSettings
     {
         Configuration = configuration,
		 NoBuild = true
     };

     Information("UnitTests");
     var projectFiles = GetFiles("./**/*.UnitTests.csproj");
     foreach(var file in projectFiles)
     {
         DotNetCoreTest(file.FullPath, settings);
     }
});

Task("Run-Integration-Tests")
	//.IsDependentOn("Build")
	.IsDependentOn("Publish-WebApplication1.Api")
	.Does(() => 
{
     var settings = new DotNetCoreTestSettings
     {
         Configuration = configuration,
		 NoBuild = true
     };

     Information("IntegrationTests");
     var projectFiles = GetFiles("./**/IntegrationTests.csproj");	 
     foreach(var file in projectFiles)
     {		 
         DotNetCoreTest(file.FullPath, settings);
     }
});

Task("Build")
	.IsDependentOn("clean")
	.IsDependentOn("NuGet-Restore")
	.Does(() => 
{
	MSBuild(slnPath, settings =>
        settings.SetPlatformTarget(PlatformTarget.MSIL)
            .WithProperty("TreatWarningsAsErrors","false")
            .WithTarget("Build")
            .SetConfiguration(configuration));
});

Task("NuGet-Restore")
	.Does(() =>
{
	var sources = new[] {"https://api.nuget.org/v3/index.json" };

	DotNetCoreRestore(new DotNetCoreRestoreSettings { Sources = sources });

	NuGetRestore(slnPath, new NuGetRestoreSettings { Source = sources });
});

Task("Clean")
	.Does(() =>
{
	CleanDirectories(publishFolder);
	CleanDirectories(temporaryFolder);
	
	var directories = GetDirectories("./*");
	foreach(var directory in directories)
	{
		if (directory.GetDirectoryName() != "tools")
		{
			CleanDirectories(directory.FullPath + "/**/obj");
			CleanDirectories(directory.FullPath + "/**/bin");
			CleanDirectories(directory.FullPath + "/**/published-app");
		}
	}
});

#region Helper Methods

public string GetVersionWithBuildNumber(string assemblyPath, string buildNumber)
{
	var assemblyVersion = GetFullVersionNumber(assemblyPath);
	
	var versionTokens = assemblyVersion.Split('.');
	if (!string.IsNullOrEmpty(buildNumber))
	{
		versionTokens[3] = buildNumber;
	}
	var versionWithBuildNumber = String.Join(".", versionTokens);

	return versionWithBuildNumber;
}

#endregion


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Default")
	.IsDependentOn("Run-Integration-Tests")
	//.IsDependentOn("Run-Unit-Tests")
	//.IsDependentOn("Publish-WebApplication1.Api")	
	.Does(() =>
{
	
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
