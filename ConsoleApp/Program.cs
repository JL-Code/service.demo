using mecode.toolkit;
using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path = ConfigurationManager.AppSettings["dirPath"] ?? "C:\\1.0.1";
                var loop = Convert.ToInt32(ConfigurationManager.AppSettings["loop"] ?? "0");
                Console.WriteLine("path=>" + path);
                Console.WriteLine("loop=>" + loop);
                for (int i = 0; i < loop; i++)
                {
                    var temp = path + "\\" + i;
                    CompressUtil.DecompressFile($"{path}.zip", temp);
                    var stopiis = @"get-website 'Default Web Site' | where-object { if ($_.Name -ne ''){stop-website $_.Name; get-website $_.Name;}};";
                    var script = $"Copy-Item -Path \"{temp + "\\1.0.1"}\" -Destination \"{temp + "\\copy"}\" -Recurse -Force";
                    PowerShellUtil.RunScript(new List<string> { "Get-ExecutionPolicy | Where-Object { if ($_ -nq 'Unrestricted') { set-executionpolicy Unrestricted -confirm:$false -force } };", stopiis, script });
                }
                Console.WriteLine("执行完毕。");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
