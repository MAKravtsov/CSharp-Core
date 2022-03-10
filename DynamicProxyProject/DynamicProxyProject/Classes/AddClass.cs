namespace DynamicProxyProject.Classes
{
    /// <summary>
    /// Класс, который что-то делает
    /// </summary>
    public class AddClass : IAddInterface
    {
        /*
         * При использовании перехватчиков с DI от Microsoft
         */
        [Decorate(typeof(AddDecorator))]
        [Decorate(typeof(SubstractDecorator))]
        public void Add()
        {
            Constants.Number += 2;
        }
    }

    /// <summary>
    /// Интерфейс, который реализует класс, который что-то делает
    /// </summary>
    public interface IAddInterface
    {
        public void Add();
    }
}
