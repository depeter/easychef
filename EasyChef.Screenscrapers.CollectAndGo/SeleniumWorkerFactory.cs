using EasyChef.Shared.Infrastructure;
using System.Threading.Tasks;
using System;
using System.Reflection;
using System.Linq;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public static class SeleniumWorkerFactory
    {
        internal static Task<TRequest> StartWorker<TRequest>(TRequest message) where TRequest : MessageBusMessage
        {
            // find the seleniumworker that handles this requesttype
            Type type = typeof(SeleniumWorker<TRequest>);
            var workerType = Assembly.GetAssembly(type).GetTypes().Where(myType => myType.IsClass && 
                                                                                   !myType.IsAbstract && 
                                                                                   myType.IsSubclassOf(type) && 
                                                                                   myType.BaseType.GenericTypeArguments.Any(x => x.IsAssignableFrom(typeof(TRequest)))
                                                                        ).FirstOrDefault();
            
            if (workerType == null)
                throw new ApplicationException($"Could not create SeleniumWorker: No suitable worker found for type '{typeof(TRequest).Name}'.");

            // create an instance of it
            var instance = Activator.CreateInstance(workerType);

            // start the worker
            return Task.FromResult((instance as SeleniumWorker<TRequest>).Start(message));
        }
    }
}