using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Glimpse.Core2.Extensibility
{
    public class TabContext:ITabContext
    {

        public TabContext(object requestContext, IDataStore pluginStore, DiscoverableCollection<IPipelineInspector> pipelineInspectors)
        {
            Contract.Requires<ArgumentNullException>(requestContext != null, "requestContext");
            Contract.Requires<ArgumentNullException>(pluginStore != null, "pluginStore");
            Contract.Requires<ArgumentNullException>(pipelineInspectors != null, "pipelineInspectors");

            RequestContext = requestContext;
            PluginStore = pluginStore;
            PipelineInspectors = pipelineInspectors;
        }

        private DiscoverableCollection<IPipelineInspector> PipelineInspectors { get; set; }

        public IDataStore PluginStore { get; private set; }

        private object RequestContext { get; set; }

        public T GetRequestContext<T>() where T : class
        {
            return RequestContext as T;
        }

        //TODO: Provide a non generic implementation? IE .GetPipelineInspector(Type type)?
        public T GetPipelineInspector<T>() where T : class, IPipelineInspector
        {
            return PipelineInspectors.FirstOrDefault(pm => pm.GetType() == typeof (T)) as T;
        }
    }
}