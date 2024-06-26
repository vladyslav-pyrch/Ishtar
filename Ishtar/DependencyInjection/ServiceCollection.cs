using System.Collections;
using Ishtar.DependencyInjection.Abstractions;

namespace Ishtar.DependencyInjection;

internal class ServiceCollection : IServiceCollection
{
    private readonly List<ServiceDescriptor> _serviceDescriptors = [];
    
    public IEnumerator<ServiceDescriptor> GetEnumerator()
    {
        return _serviceDescriptors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_serviceDescriptors).GetEnumerator();
    }

    public void Add(ServiceDescriptor item)
    {
        _serviceDescriptors.Add(item);
    }

    public void Clear()
    {
        _serviceDescriptors.Clear();
    }

    public bool Contains(ServiceDescriptor item)
    {
        return _serviceDescriptors.Contains(item);
    }

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
    {
        _serviceDescriptors.CopyTo(array, arrayIndex);
    }

    public bool Remove(ServiceDescriptor item)
    {
        return _serviceDescriptors.Remove(item);
    }

    public int Count => _serviceDescriptors.Count;

    public bool IsReadOnly => ((ICollection<ServiceDescriptor>)_serviceDescriptors).IsReadOnly;

    public int IndexOf(ServiceDescriptor item)
    {
        return _serviceDescriptors.IndexOf(item);
    }

    public void Insert(int index, ServiceDescriptor item)
    {
        _serviceDescriptors.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _serviceDescriptors.RemoveAt(index);
    }

    public ServiceDescriptor this[int index]
    {
        get => _serviceDescriptors[index];
        set => _serviceDescriptors[index] = value;
    }
}