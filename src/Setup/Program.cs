using System;
using System.Text;
using WixSharp;
using WixSharp.CommonTasks;

namespace RevitDevX.Setup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Compiler.WixLocation = @"..\packages\WixSharp.wix.bin.3.14.0\tools\bin";
            RevitDevXSetup.Build();
        }
    }

    public static class RevitDevXSetup
    {
        private static readonly Version Version =
            new Version(
                BuildInfo.MAJOR_BUILD_VERSION_ID,
                BuildInfo.MINOR_BUILD_VERSION_ID,
                BuildInfo.BUILD_ID
            );

        public const string COMPANY_NAME = "a-severin";

        public static void Build()
        {
            var project = new Project("RevitDevX")
            {
                Version = Version,
                GUID = new Guid("A4EE65F0-97B7-4227-BB60-D75443308982"),
                OutFileName = $"Install RevitDevX {Version} EN",
                OutDir = @"..\MSI",
                Language = "en-US",
                Codepage = "1251",
                Encoding = Encoding.UTF8,
                MajorUpgradeStrategy = MajorUpgradeStrategy.Default,
                UI = WUI.WixUI_InstallDir,
                ControlPanelInfo = {
                    Manufacturer = COMPANY_NAME
                },
                Package =
                {
                    AttributesDefinition =
                        "AdminImage=yes;InstallerVersion=500;InstallScope=perMachine;InstallPrivileges=elevated"
                }
            };
            project.MajorUpgradeStrategy.RemoveExistingProductAfter = Step.InstallInitialize;
            project.AddProperty(new Property("MSIUSEREALADMINDETECTION", "1"));

            var plugins = @"%CommonAppDataFolder%\Autodesk\Revit\Addins\2023";
            var addin = @"RevitDevX";
            project.AddDirs(
                new Dir(
                    plugins,
                    new Dir(
                        addin,
                        new DirFiles(@"..\Build\*dll"),
                        new DirFiles(@"..\Build\*.pdb"),
                        new DirFiles(@"..\Build\*.config")
                        ),
                    new File(@"..\Build\RevitDevX.addin")
                )
            );

            Compiler.BuildMsi(project);
        }

    }
}
