using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Interceptors;

public class AspectInterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
            (true).ToList();
        var methodAttributes = (type.GetMethod(method.Name) ?? throw new InvalidOperationException())
            .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        classAttributes.AddRange(methodAttributes);
        // ReSharper disable once CoVariantArrayConversion
        return classAttributes.OrderBy(x => x.Priority).ToArray();
    }
}