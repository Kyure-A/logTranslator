using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace moe.kyre.logTranslator
{
    [System.Serializable]
    internal class ApiResponse
    {
        public int code;
        public string text;
    }
    
    public static class Translator
    {
        public readonly static string url = "https://script.google.com/macros/s/AKfycbwvoUGhYm0qEg-V4Na-6tCOl5XRKSErt_knOR1NqDwCkUPpusHO7Fp4qLRglwxGoDfFKQ/exec";
        
        public static string QueryBuilder (string text, string source, string target)
        {
            return $"{url}?text={text}&source={source}&target={target}";
        }
        
        public static async Task<string> Translate(string english)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(QueryBuilder(english, "en", "ja")))
            {
                var asyncOperation = webRequest.SendWebRequest();
                
                while (!asyncOperation.isDone)
                {
                    await Task.Yield();
                }
                
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = webRequest.downloadHandler.text;
                    ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
                    return response.text;
                }
                else
                {
                    return english;
                }
            }
        }
    }
}
