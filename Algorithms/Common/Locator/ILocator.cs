using Common.Algorithm;

namespace Common.Locator;

public interface ILocator
{
    IAlgorithm? Get(Func<IAlgorithm, bool> predicate);

    IEnumerable<IAlgorithm> GetAll();

    void Register(IAlgorithm obj);
}
