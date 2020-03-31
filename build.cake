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
var publishFolder = "./publish";
var slnPath = "WebApplicationIntegrationTesting.sln";
var temporaryFolder = "./temp";




//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

///define all tasks
Task("Publish-WebApplication1.Api")
	.IsDependentOn("Build")
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


task("unit-tests")
	.IsDependentOn("Build")
	.Does(() => 
{
     var settings = new DotNetCoreTestSettings
     {
         Configuration = configuration,
		 NoBuild = true
     };

     var projectFiles = GetFiles("./**/*.Tests.csproj");
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
	CleanDirectories(".sonarqube");

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
	//.IsDependentOn("Unit-Tests")
	.IsDependentOn("Publish-WebApplication1.Api")
	//.IsDependentOn("Publish-IMSPA.APIMsi")
	.Does(() =>
{
	
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
