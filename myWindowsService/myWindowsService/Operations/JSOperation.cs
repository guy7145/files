using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noesis.Javascript;

namespace MyBot.Operations
{
    class JSOperation
    {
        public static object Operate(string script)
        {
            return Operate(script, 0);
        }
        public static object Operate(string script, object answer)
        {
            object ans;
            using (JavascriptContext context = new JavascriptContext())
            {
                context.SetParameter("answer", answer);
                context.Run(script);
                ans = context.GetParameter("answer");
            }
            return ans;
        }
        public static object OperateScript(string url)
        {
            return OperateScript(url, 0);
        }
        public static object OperateScript(string url, object answer)
        {
            string script = Actions.ReadFile(url);
            return Operate(script, answer);
        }
    }
}