namespace SelectionSystem.Core.UI
{
    public interface IScreen
    {
        string ScreenId { get; }

        void Show();
        void Hide();
    }
}