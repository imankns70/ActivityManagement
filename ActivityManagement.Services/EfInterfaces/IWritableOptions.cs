using System;
using Microsoft.Extensions.Options;
namespace ActivityManagement.Services.EfInterfaces
{
    public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
    {
        new T Value { get;}
        void Update(Action<T> applyChanges);
    }
}
