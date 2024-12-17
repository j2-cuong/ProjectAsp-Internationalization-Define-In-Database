
namespace MultiLanguageApp.Models
{
    public class TranslationKey
    {
        public Guid Id { get; set; }
        public string? KeyName { get; set; }
        public string? KeyGroup { get; set; }
        public string? Description { get; set; }
    }
}