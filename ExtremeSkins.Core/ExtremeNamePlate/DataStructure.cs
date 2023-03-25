using System.IO;

namespace ExtremeSkins.Core.ExtremeNamePlate;

public static class DataStructure
{
    public const string FolderName = "ExtremeNamePlate";
    public static string GetNamePlatePath(string hatParentPath, string namePlateName)
        => Path.Combine(hatParentPath, $"{namePlateName}.png");
}
