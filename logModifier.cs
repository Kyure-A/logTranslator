using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace moe.kyre.logTranslator
{
    #if UNITY_EDITOR
    [InitializeOnLoad]
    #endif
    public static class LogModifier
    {
        private static ILogHandler _custom;

        static LogModifier()
        {
            _custom = new TranslatorLogHandler(Debug.unityLogger.logHandler);
            Apply();
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Apply();
        }
        
        private static void Apply()
        {
            _custom = new TranslatorLogHandler(Debug.unityLogger.logHandler);
            Debug.unityLogger.logHandler = _custom;
        }
    }
}
