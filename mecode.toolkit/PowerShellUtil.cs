using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace mecode.toolkit
{
    public class PowerShellUtil
    {
        /// <summary>
        /// 执行powershell脚本命令
        /// </summary>
        /// <param name="scriptText"></param>
        /// <returns></returns>
        public static string RunScript(string scriptText)
        {

            // create Powershell runspace  

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it  

            runspace.Open();

            // create a pipeline and feed it the script text  

            Pipeline pipeline = runspace.CreatePipeline();

            pipeline.Commands.AddScript(scriptText);

            pipeline.Commands.Add("Out-String");

            // execute the script  

            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace  

            runspace.Close();

            // convert the script result into a single string  

            var stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 执行powershell脚本命令
        /// </summary>
        /// <param name="path">脚本路径</param>
        /// <returns></returns>
        public static string RunFileScript(string path)
        {
            var scriptText = GetFileContent(path);
            // create Powershell runspace  

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it  

            runspace.Open();

            // create a pipeline and feed it the script text  

            Pipeline pipeline = runspace.CreatePipeline();

            pipeline.Commands.AddScript(scriptText);

            pipeline.Commands.Add("Out-String");

            // execute the script  

            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace  

            runspace.Close();

            // convert the script result into a single string  

            StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 执行powershell脚本命令
        /// </summary>
        /// <param name="scripts">脚本文件</param>
        public static void RunScript(List<string> scripts)
        {
            Runspace runspace = null;
            try
            {
                runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                foreach (var scr in scripts)
                {
                    pipeline.Commands.AddScript(scr);
                }
                //返回结果     
                var results = pipeline.Invoke();
                runspace.Close();
            }
            catch (Exception ex)
            {
                runspace?.Close();
                Logger.Error($"执行ps命令异常：{ex.Message}", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 执行自定义函数
        /// </summary>
        /// <param name="script">包含函数代码的ps脚本</param>
        /// <param name="funcName">函数名</param>
        /// <param name="parameters">函数参数</param>
        /// <returns></returns>
        public static string Run(string script, string command, Dictionary<string, object> parameters)
        {
            var stringBuilder = new StringBuilder();
            using (var runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                var ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddScript(script);
                ps.Invoke();
                ps.AddCommand(command).AddParameters(parameters);
                foreach (PSObject obj in ps.Invoke())
                {
                    stringBuilder.AppendLine(obj.ToString());
                }
            }
            return stringBuilder.ToString();
        }
        public static string Run(string command, Dictionary<string, object> parameters)
        {
            var stringBuilder = new StringBuilder();
            using (var runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                var ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddCommand(command).AddParameters(parameters);
                foreach (PSObject obj in ps.Invoke())
                {
                    stringBuilder.AppendLine(obj.ToString());
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 将文本文件转换为string字符串
        /// </summary>
        /// <param name="path">文件路径</param>
        static string GetFileContent(string path)
        {
            string scriptStr;
            if (!File.Exists(path))
                throw new Exception("当前文件不存在");
            using (var fs = new FileStream(path, FileMode.Open))
            {
                if (!fs.CanRead)
                    throw new Exception("当前文件不不可读");
                var reader = new StreamReader(fs);
                scriptStr = reader.ReadToEnd();
                reader.Close();
            }
            return scriptStr;
        }
    }
}
