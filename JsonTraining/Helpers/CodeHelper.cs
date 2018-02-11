using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonTraining.Helpers
{
    public static class CodeHelper
    {
        public static object CodeToObject(string code)
        {
            // 1.CSharpCodePrivoder  
            var objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.ICodeComplier  
            var objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            // 3.CompilerParameters  
            var objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            // 4.CompilerResults  
            var cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GenerateCode(code));

            if (cr.Errors.HasErrors)
            {
                return new object();
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance("CodeHelper.CodeHelper");
                MethodInfo objMI = objHelloWorld.GetType().GetMethod("OutPut");
                var obj = objMI.Invoke(objHelloWorld, null);
                return objMI.Invoke(objHelloWorld, null);
            }
        }

        private static string GenerateCode(string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace CodeHelper");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("      public class CodeHelper");
            sb.Append(Environment.NewLine);
            sb.Append("      {");
            sb.Append(Environment.NewLine);
            sb.Append("          public object OutPut()");
            sb.Append(Environment.NewLine);
            sb.Append("          {");
            sb.Append(Environment.NewLine);
            sb.Append(code);
            sb.Append(Environment.NewLine);
            sb.Append("          }");
            sb.Append(Environment.NewLine);
            sb.Append("      }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
