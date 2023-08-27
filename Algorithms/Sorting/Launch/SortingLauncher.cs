using Common.Launch;
using Common.Locator;
using Sorting.Algorithms;

namespace Sorting.Launch;

internal class SortingLauncher : LauncherBase
{
    protected override void RegisterAlgs<T>(T[] array)
    {
        Locator.Instance.Register(new BubbleSort<T>(array));
        Locator.Instance.Register(new ShakerSort<T>(array));
        Locator.Instance.Register(new MergeSort<T>(array));
        Locator.Instance.Register(new QuickSort<T>(array));
        Locator.Instance.Register(new TreeSort<T>(array));
        Locator.Instance.Register(new HeapSort<T>(array));
    }
}
