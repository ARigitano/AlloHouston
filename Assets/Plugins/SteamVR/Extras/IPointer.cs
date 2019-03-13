using Valve.VR.Extras;

public interface IPointer
{
    event PointerEventHandler PointerIn;
    event PointerEventHandler PointerOut;
}