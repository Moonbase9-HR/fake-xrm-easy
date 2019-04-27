using System;
using Crm;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    /// <summary>
    /// This plugin adds 1 employee to the current nr of employees
    /// </summary>
    public class TraceImagesPlugin : IPlugin
    {
        public EntityImageCollection PreImages { get; set; }
        public EntityImageCollection PostImages { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context == null)
                throw new InvalidPluginExecutionException("Initialize IPluginExecutionContext fail.");

            if (!context.InputParameters.Contains("Target"))
            {
                throw new InvalidPluginExecutionException("Target not set.");
            }
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // pass back pre/post entity images in outputparameters
            foreach (var preImage in context.PreEntityImages)
            {
                tracingService.Trace($"PreImage Found: {preImage.Key}");
                foreach(var attribute in ((Entity)preImage.Value).Attributes)
                {
                    tracingService.Trace($"PreImage Attribute: {attribute.Key}:{attribute.Value}");
                }
            }
            foreach (var postImage in context.PostEntityImages)
            {
                tracingService.Trace($"PostImage Found: {postImage.Key}");
                foreach (var attribute in ((Entity)postImage.Value).Attributes)
                {
                    tracingService.Trace($"PostImage Attribute: {attribute.Key}:{attribute.Value}");
                }
            }
        }
    }
}
