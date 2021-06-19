
namespace DependencyInjection.Abstractions
{
    public interface IWriter<T>
    {
        void Write(T data);
    }
}
