using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Repositories.MovieNoteRepository;
using Business.Repositories.MovieScoreRepository;
using Business.Utilities.ApiRequest;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Repositories.MovieNoteRepository;
using DataAccess.Repositories.MovieRepository;
using DataAccess.Repositories.MovieScoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<EfMovieDal>().As<IMovieDal>();
            builder.RegisterType<ApiRequestManager>().As<IApiRequest>();



            builder.RegisterType<MovieNoteManager>().As<IMovieNoteService>();
            builder.RegisterType<EfMovieNoteDal>().As<IMovieNoteDal>();


            builder.RegisterType<MovieScoreManager>().As<IMovieScoreService>();
            builder.RegisterType<EfMovieScoreDal>().As<IMovieScoreDal>();







            builder.RegisterType<ApiRequestManager>().As<IApiRequest>().InstancePerDependency().EnableInterfaceInterceptors();


            #region Fluent Validation işlemlerimiz Oldugunu Ve Attr ile çalıştıgımızı Belirttiğimiz alan
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {

                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

            #endregion

        }

    }
}
