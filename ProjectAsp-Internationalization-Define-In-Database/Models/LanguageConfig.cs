
namespace MultiLanguageApp.Models
{
    public class LanguageConfig
    {
        public Guid Id { get; set; }
        public string? LanguageCode { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageIcon { get; set; }
        public bool IsActive { get; set; } = true;
        public int? DisplayOrder { get; set; }

    }
}