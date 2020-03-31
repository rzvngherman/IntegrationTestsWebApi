//	https://cakebuild.net/docs/fundamentals/args-and-environment-vars
// can test with:
//			.\test.ps1
//			.\test.ps1 -ScriptArgs '-my_setting_param="from PowerShell"'


//			$env:my_settingEnv = "from PowerShell"
//			.\test.ps1

var mySettingParam = Argument("my_setting_param", "default value");
var mySettingEnv = EnvironmentVariable("my_settingEnv") ?? "default value";


Task("Default")
    .Does(() =>
{
    Information("My setting is parameter: " + mySettingParam);
	Information("My setting from ENV: " + mySettingEnv);
});

RunTarget("Default");