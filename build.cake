///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var sln = "./IMC.sln";
///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean").Does(() => {
   CleanDirectory("./artifacts");
});

Task("Restore")
   .IsDependentOn("Clean")
   .Does(() => {
      DotNetCoreRestore(sln);
   });

Task("Build")
   .IsDependentOn("Restore")
   .Does(() => {
      DotNetCoreBuild(sln, new DotNetCoreBuildSettings {
          Configuration = configuration, 
          NoRestore = true  
         });
   });
   

Task("Test")
   .IsDependentOn("Build")
   .Does(() => {
      DotNetCoreTest(sln, new DotNetCoreTestSettings {
         Configuration = configuration,
         NoBuild = true,
         NoRestore = true      
      });
   });
   
Task("Publish")
   .IsDependentOn("Test").Does(() => {
   DotNetCorePublish("./src/IMC.Web/IMC.Web.csproj", new DotNetCorePublishSettings {
      Configuration = configuration, 
      NoBuild = true,
      NoRestore = true,
      OutputDirectory = "./artifacts"
   });

});

Task("Default").IsDependentOn("Publish");

RunTarget(target);