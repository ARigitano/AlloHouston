namespace CRI.HelloHouston.GameElement
{
    internal interface IHologram
    {
        bool visible { get; set; }
        void ShowHologram();
        void HideHologram();
    }
}