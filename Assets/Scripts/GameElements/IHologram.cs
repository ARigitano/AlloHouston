namespace CRI.HelloHouston.GameElement
{
    internal interface IHologram
    {
        bool visible { get; }
        void ShowHologram();
        void HideHologram();
    }
}