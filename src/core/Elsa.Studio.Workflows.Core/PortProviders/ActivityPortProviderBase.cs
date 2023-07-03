using Elsa.Api.Client.Activities;
using Elsa.Api.Client.Resources.ActivityDescriptors.Models;
using Elsa.Studio.Workflows.Contracts;
using Elsa.Studio.Workflows.Models;
using Humanizer;

namespace Elsa.Studio.Workflows.PortProviders;

public abstract class ActivityPortProviderBase : IActivityPortProvider
{
    public virtual double Priority => 0;
    public abstract bool GetSupportsActivityType(string activityType);

    public abstract IEnumerable<Port> GetPorts(PortProviderContext context);

    public virtual Activity? ResolvePort(string portName, PortProviderContext context)
    {
        var activity = context.Activity;
        var propName = portName.Camelize();
        return (Activity?)activity.GetValueOrDefault(propName);
    }

    public virtual void AssignPort(string portName, Activity? activity, PortProviderContext context)
    {
        var container = context.Activity;
        var propName = portName.Camelize();
        container[propName] = activity!;
    }
}