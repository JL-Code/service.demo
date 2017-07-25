using System.IO;
using System.Security.Cryptography;

namespace mecode.toolkit
{
    public class RSAUtil
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="withPrivate">私人</param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
            keyParameters = default(RSAParameters);
            if (!Directory.Exists(filePath))
                return false;
            keyParameters = JsonHelper.JSONToObject<RSAParameters>(File.ReadAllText(Path.Combine(filePath, filename)));
            return true;
        }
        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns></returns>
        public static RSAParameters GenerateAndSaveKey(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonHelper.ObjectToJSON(privateKeys));
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonHelper.ObjectToJSON(publicKeys));
            return privateKeys;
        }
    }
}
