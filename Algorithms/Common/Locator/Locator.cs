using Common.Algorithm;

namespace Common.Locator;

public class Locator : ILocator
{
    private readonly List<IAlgorithm> _services;

    private static Locator? _instance;

    public static Locator Instance => _instance ??= new Locator();

    private Locator()
    {
        _services = new();
    }

    public IAlgorithm? Get(Func<IAlgorithm, bool> predicate)
    {
        return _services.FirstOrDefault(y => predicate(y));
    }

    public IEnumerable<IAlgorithm> GetAll()
    {
        return _services;
    }

    public void Register(IAlgorithm obj)
    {
        _services.Add(obj);
    }
}
