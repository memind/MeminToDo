using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Microsoft.Extensions.Configuration;
using Common.Logging.Sinks;

namespace Common.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               //var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
               configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Sink(new CustomLogSink())
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    //.WriteTo.Elasticsearch(
                    //    new ElasticsearchSinkOptions(new Uri(elasticUri))
                    //    {
                    //        IndexFormat = $"memintodo-logs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    //        AutoRegisterTemplate = true,
                    //        DetectElasticsearchVersion = false,
                    //        NumberOfShards = 2,
                    //        NumberOfReplicas = 1
                    //    }).MinimumLevel.Verbose()
                    //.WriteTo.Seq(context.Configuration["Seq:ServerUrl"])
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .ReadFrom.Configuration(context.Configuration);
           };
    }
}
