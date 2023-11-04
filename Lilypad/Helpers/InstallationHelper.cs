namespace Lilypad.Helpers; 

public static class InstallationHelper { 
    public static Function GetInstallFunction(this Datapack datapack) {
        return datapack.Functions.GetOrCreate("install").SetLoad();
    }
    
    public static Function GetUninstallFunction(this Datapack datapack) {
        return datapack.Functions.GetOrCreate("uninstall");
    }
    
    public static void RegisterInstallation(this Datapack datapack, Action<Function> install, Action<Function> uninstall) {
        install(datapack.GetInstallFunction());
        uninstall(datapack.GetUninstallFunction());
    }
}