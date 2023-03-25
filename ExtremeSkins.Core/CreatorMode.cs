using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExtremeSkins.Core;

public static class CreatorMode
{
    public const string CreatorModeFolder = "CreatorMode";
    public const string TranslationCsvFile = "translation.csv";
    public const string Comma = ",";

    private const string creatorModePlaceHolder = "CreatorMode = {0}";

    public enum SupportedLangs
    {
        English,
        Latam,
        Brazilian,
        Portuguese,
        Korean,
        Russian,
        Dutch,
        Filipino,
        French,
        German,
        Italian,
        Japanese,
        Spanish,
        SChinese,
        TChinese,
        Irish
    }


    public static StreamWriter GetTranslationWriter(string amongUsPath)
    {
        CreateCreatorModeFolder(amongUsPath);
        string csvFile = Path.Combine(amongUsPath, CreatorModeFolder, TranslationCsvFile);

        bool isFileExist = File.Exists(csvFile);

        using StreamWriter transCsv = new StreamWriter(
            csvFile, isFileExist, new UTF8Encoding(true));

        if (!isFileExist)
        {
            List<string> langList = new List<string>();

            foreach (SupportedLangs enumValue in Enum.GetValues(typeof(SupportedLangs)))
            {
                langList.Add(enumValue.ToString());
            }

            transCsv.WriteLine(
                string.Format(
                    "{1}{0}{2}",
                    Comma,
                    "TransKey",
                    string.Join(Comma, langList)));
        }

        return transCsv;
    }

    public static void SetCreatorMode(string amongUsPath, bool active)
    {
        string cfgPath = Path.Combine(amongUsPath, Config.Path);
        string text;

        using (var cfg = new StreamReader(cfgPath, new UTF8Encoding(true)))
        {
            text = cfg.ReadToEnd();
        }
        text = text.Replace(
            string.Format(creatorModePlaceHolder, !active),
            string.Format(creatorModePlaceHolder, active));

        using var newCfg = new StreamWriter(cfgPath, false, new UTF8Encoding(true));
        newCfg.Write(text);
    }

    private static void CreateCreatorModeFolder(string targetPath)
    {
        string csvFolder = Path.Combine(targetPath, CreatorModeFolder);

        if (!Directory.Exists(csvFolder))
        {
            Directory.CreateDirectory(csvFolder);
        }
    }
}
