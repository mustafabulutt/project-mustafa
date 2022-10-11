using Castle.DynamicProxy;
using Core.Contans;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspects.Secured
{
    public class SecuredAspect  : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;


        public SecuredAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_roles != null && _httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {

                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimsRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }

                }
                throw new Exception(Messages.AuthorizationError);
            }
            else
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                if (claims.Count()>0)
                {
                    return;

                }
                else
                {
                    throw new Exception(Messages.NullTokenError);
                }
            }
           
        }
    }
}
