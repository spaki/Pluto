using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pluto.API.Auth;
using Pluto.API.Settings;
using Pluto.API.Setup;
using Pluto.Bus;
using Pluto.Domain.Bus;
using Pluto.Domain.Handlers;
using Pluto.Domain.Handlers.Events;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Repository.Repositories.Common;
using Pluto.Repository.UoW;

namespace Pluto.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDistributedMemoryCache()
                .AddSwagger()
                .AddMediatR()
                .AddCustomMvc()
                .AddSingleton(this.AppSettings)
                .AddSqlServerContexts(this.AppSettings)
                .AddJwtAuth(this.AppSettings)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IMediatorHandler, InMemoryBus>()
                .AddScopedByBaseType(typeof(CrudRepository<>)) // -> Repositories
                .AddScopedHandlers(typeof(INotificationHandler<>), typeof(UserEventHandler).Assembly) // -> Events
                .AddScopedHandlers(typeof(IRequestHandler<>), typeof(UserHandler).Assembly) // -> Commands
            ;

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) =>
            app
                .UseDeveloperExceptionPageIfDebug(env)
                .UseHttpsRedirection()
                .UseCustomCors()
                .UseSawaggerWithDocs()
                .UseDatabaseInitialization()
                .UseMvc()
            ;
    }
}
