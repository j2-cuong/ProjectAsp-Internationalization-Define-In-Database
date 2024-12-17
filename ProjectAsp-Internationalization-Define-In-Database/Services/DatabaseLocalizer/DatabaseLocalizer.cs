using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MultiLanguageApp.Models;

namespace MultiLanguageApp
{
    public class DatabaseLocalizer : IDatabaseLocalizer
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _cache;
        private static readonly string _cacheKeyPrefix = "Translations_";
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);
        private readonly ICookiesRepository _cookiesRepository;


        public DatabaseLocalizer(IMemoryCache cache, ICookiesRepository cookiesRepository)
        {
            _connectionString = $@"Server = Server\\SQLPublic; Database = tesst; User Id = tech; Password = password;Encrypt=True;TrustServerCertificate=True"; // Lấy từ config
            _cache = cache;
            _cookiesRepository = cookiesRepository;
        }

        private async Task<Dictionary<string, string>> LoadTranslationsFromDatabase(string languageCode, string keys)
        {
            var translations = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetTranslations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LanguageCode", languageCode);
                    cmd.Parameters.AddWithValue("@Keys", keys);

                    try
                    {
                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                translations.Add(
                                    reader.GetString(0), // KeyName
                                    reader.GetString(1)  // TranslatedText
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw; // Có thể log lỗi tại đây
                    }
                }
            }
            return translations;
        }

        public async Task<string> GetTranslation(string key)
        {
            var languageCode = _cookiesRepository.GetCurrentLanguage();
            var cacheKey = $"{_cacheKeyPrefix}{languageCode}";

            // Set mặc định nếu languageCode null hoặc rỗng
            languageCode = string.IsNullOrEmpty(languageCode) ? "vi_VN" : languageCode;

            // Kiểm tra xem cache có cần làm mới không
            bool registerCache = await CheckNewestData.CheckChanged(languageCode,_connectionString);
            if (registerCache)
            {
                // remove cache theo ngôn ngữ
                _cache.Remove(cacheKey);
                CheckNewestData.CompleteCheck(languageCode, _connectionString);
            }

            // Thử lấy từ cache
            if (_cache.TryGetValue(cacheKey, out Dictionary<string, string> translations))
            {
                // Cache tồn tại, kiểm tra key
                if (translations.TryGetValue(key, out string cachedValue))
                {
                    return cachedValue;
                }
            }

            // Cache không tồn tại hoặc đã bị clear, tải lại từ database
            translations = await LoadTranslationsFromDatabase(languageCode, key);

            // Cập nhật cache mới
            _cache.Set(cacheKey, translations, _cacheExpiration);

            // Lấy giá trị từ cache vừa cập nhật
            if (translations != null && translations.TryGetValue(key, out string newValue))
            {
                return newValue;
            }

            // Trả về key nếu không tìm thấy
            return key;
        }

        public async Task<List<LanguageConfig>> GetLanguageConfig()
        {
            var languages = new List<LanguageConfig>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, LanguageName, LanguageCode, LanguageIcon FROM LanguageConfig";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                languages.Add(new LanguageConfig
                                {
                                    Id = reader.GetGuid(0),
                                    LanguageName = reader.GetString(1),
                                    LanguageCode = reader.GetString(2),
                                    LanguageIcon = reader.GetString(3)
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return languages;
        }
    }
}