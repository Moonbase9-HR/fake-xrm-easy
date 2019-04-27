using System;
using Crm;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    /// <summary>
    /// This plugin adds 1 employee to the current nr of employees
    /// </summary>
    public class ImagesAccountPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            var factory = (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(context.UserId);

            var account = (Entity)context.InputParameters["Target"] as Account;
            var preimage = (Entity)context.PreEntityImages["PreImage"] as Account;

            account.NumberOfEmployees = preimage.NumberOfEmployees + 1;

            context.OutputParameters["Target"] = account;
        }
    }
}
