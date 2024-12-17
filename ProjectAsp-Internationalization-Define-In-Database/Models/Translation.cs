namespace MultiLanguageApp.Models
{
    public class Translation
    {
        public Guid Id { get; set; }
        public Guid TranslationKeyId { get; set; }
        public Guid LanguageId { get; set; }
        public string? TranslatedText { get; set; }
    }
}