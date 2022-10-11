using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptorsBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { } //işlem öncesi
        protected virtual void OnAfter(IInvocation invocation) { }//işlem Sonrası
        protected virtual void OnException(IInvocation invocation , Exception ex) { } //Hata Verdiğinde
        protected virtual void OnSuccess(IInvocation invocation) { }// işlem Tamamlandıgında


        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation,ex);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            } 
            
            OnAfter(invocation);
        }
    }
}
