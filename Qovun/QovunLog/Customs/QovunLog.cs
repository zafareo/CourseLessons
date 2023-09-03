using QovunLog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QovunLog.Customs
{
    public class QovunLog
    {
        private Loglevel _level;
        public QovunLog(Loglevel level)
        {
            _level = level;
        }

        public void Log(Loglevel level, string message)
        {
            if (level >= _level)
            {
                string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] - {message}";
                Console.WriteLine(formattedMessage);
            }
        }

        public void Debug(string message)
        {
            Log(Loglevel.Debug, message);
        }

        public void Info(string message)
        {
            Log(Loglevel.Info, message);
        }

        public void Warning(string message)
        {
            Log(Loglevel.Warning, message);
        }

        public void Error(string message)
        {
            Log(Loglevel.Error, message);
        }
    }
}
