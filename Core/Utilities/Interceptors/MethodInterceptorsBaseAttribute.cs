using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method , AllowMultiple =true , Inherited =true)]
    public abstract class MethodInterceptorsBaseAttribute : Attribute, IInterceptor
    {

        public virtual void Intercept(IInvocation invocation)
        {


        }

    }
}
