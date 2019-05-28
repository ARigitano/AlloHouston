namespace CRI.HelloHouston.GameElements
{
    internal interface IHologram
    {
        bool visible { get; set; }
        void ShowHologram();
        void HideHologram();
    }
}