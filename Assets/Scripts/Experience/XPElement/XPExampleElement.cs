namespace CRI.HelloHouston.Experience
{
    public class XPExampleElement : XPElement
    {
        public override void OnHide()
        {
            base.OnHide();
            gameObject.SetActive(false);
        }

        public override void OnShow(int currentStep)
        {
            base.OnShow(currentStep);
            gameObject.SetActive(true);
        }
    }
}
