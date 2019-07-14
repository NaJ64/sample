namespace Sample.API.DependencyInjection
{
    public interface IOptions : Infrastructure.DependencyInjection.IOptions { }
    public class Options : Infrastructure.DependencyInjection.Options, IOptions { }
}