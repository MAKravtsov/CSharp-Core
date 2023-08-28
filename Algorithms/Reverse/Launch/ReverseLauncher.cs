using Common.Launch;
using Common.Locator;
using Reverse.Algorithms;

namespace Reverse.Launch;

internal class ReverseLauncher : LauncherBase
{
    protected override void RegisterAlgs<T>(T[] array)
    {
        Locator.Instance.Register(new Reverse<T>(array));
        Locator.Instance.Register(new SinglyLinkedListReverse<T>(array));
        Locator.Instance.Register(new LinkedListReverse<T>(array));
    }
}