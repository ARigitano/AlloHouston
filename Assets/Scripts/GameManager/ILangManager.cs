using CRI.HelloHouston.Translation;

namespace CRI.HelloHouston.Experience
{
    public interface ILangManager
    {
        LangManager langManager { get; }
        TextManager textManager { get; }
    }
}