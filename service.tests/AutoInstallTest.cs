using mecode.toolkit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace service.tests
{
    [TestClass]
    public class AutoInstallTest
    {
        static readonly string _rootPath = @"E:\";
        [TestInitialize]
        public void Initialize()
        {

        }
        [TestCategory("自动升级_文件操作")]
        [TestMethod]
        public void MoveFile_Null_Success()
        {
            //将Test02中的文件 复制替换到Test01中
            var test2Path = Path.Combine(_rootPath, "Test02");
            var test1Path = Path.Combine(_rootPath, "Test01");

            try
            {
                Directory.Move(test2Path, test1Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }
        [TestCategory("自动升级_PowerShell脚本执行")]
        [TestMethod]
        public void RunPowerShell()
        {
            var scripts = new List<string>
            {
                //"Get-IISSite -Name 'Default Web Site'"
                "Stop-IISSite -Name 'Default Web Site' -Confirm:$false"
            };
            PowerShellUtil.RunScript(scripts);
        }

        [TestCategory("自动升级_PowerShell脚本执行")]
        [TestMethod]
        public void StartIISSite()
        {
            var scripts = new List<string>
            {
                "Start-IISSite -Name 'Default Web Site'"
            };
            PowerShellUtil.RunScript(scripts);
        }

        [TestCategory("自动升级_PowerShell脚本执行")]
        [TestMethod]
        public void LoadFileRunScript()
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\IISSiteManager.ps1";
            var result = PowerShellUtil.RunFileScript(path);
            Console.WriteLine(result);
        }

        [TestCategory("自动升级_PowerShell脚本执行")]
        [TestMethod]
        public void LoadRunScript()
        {
            var result = PowerShellUtil.RunScript("Get-IISSite;");
            Console.WriteLine(result);
        }
    }
}
