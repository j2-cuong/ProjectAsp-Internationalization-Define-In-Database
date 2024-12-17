namespace MultiLanguageApp
{
    public interface ICookiesRepository
    {
        void SetLanguage(string culture);
        string GetCurrentLanguage();
    }
}
