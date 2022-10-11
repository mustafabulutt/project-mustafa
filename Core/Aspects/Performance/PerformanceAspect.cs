using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopWatch;

        public PerformanceAspect()
        {
            _interval = 3;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();

        }
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopWatch.Start();

        }

        protected override void OnAfter(IInvocation invocation)
        {
            _stopWatch.Stop();
            if (_stopWatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performans raporu : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ==> {_stopWatch.Elapsed.TotalSeconds}");
            }
            _stopWatch.Reset();
        }


    }
}
