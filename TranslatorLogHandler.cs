using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace moe.kyre.logTranslator
{
    public class TranslatorLogHandler : ILogHandler
    {
        private ILogHandler _handler;
        
        // constractor
        public TranslatorLogHandler (ILogHandler handler)
        {
            _handler = handler;
        }

        public async void LogFormat (LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            string result = await Translator.Translate(format);
            _handler.LogFormat(logType, context, result, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            _handler.LogException(exception, context);
        }
    }
}
