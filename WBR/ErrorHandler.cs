using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBR
{
    static public class ErrorHandler
    {
        private const string FileName = "errors.txt";

        public static TResult Try<T1, TResult>(Func<T1, TResult> function, T1 parameter1)
        {
            try
            {
                return function(parameter1);
            }
            catch (Exception ex)
            {
                NewError(ex);
                return default;
            }
        }

        public static TResult Try<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 parameter1, T2 parameter2)
        {
            try
            {
                return function(parameter1, parameter2);
            }
            catch (Exception ex)
            {
                NewError(ex);
                return default;
            }
        }

        public static TResult Try<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            try
            {
                return function(parameter1, parameter2, parameter3);
            }
            catch (Exception ex)
            {
                NewError(ex);
                return default;
            }
        }

        public static void NewError(string error)
        {
            var utcNow = DateTime.UtcNow;
            Console.WriteLine(utcNow.ToString());
            string s = $"[{utcNow}]: {error}";
             
            FileHandler.AddToAppData(FileName, s);
            
        }
        public static void NewError(Exception ex)
        {
            NewError(ex.Message + Environment.NewLine + ex.StackTrace);
        }
    }
}
