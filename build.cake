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

/*
https://daveaglick.com/posts/publishing-to-azure-using-cake-and-web-deploy

https://stackoverflow.com/questions/45228119/can-cake-of-http-cakebuild-net-be-used-for-deploying-azure-webapps
*/

Task("Default").IsDependentOn("Publish");

RunTarget(target);