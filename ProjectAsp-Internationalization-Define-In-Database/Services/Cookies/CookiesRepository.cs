using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MultiLanguageApp
{
    public class CookiesRepository : ICookiesRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookiesRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetLanguage(string culture)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                httpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
        }

        public string GetCurrentLanguage()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.Request.Cookies.ContainsKey(CookieRequestCultureProvider.DefaultCookieName))
            {
                var cultureCookie = httpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
                var requestCulture = CookieRequestCultureProvider.ParseCookieValue(cultureCookie);
                return requestCulture.Cultures.FirstOrDefault().Value ?? CultureInfo.CurrentCulture.Name;
            }

            return "vi_VN"; // Trả về ngôn ngữ mặc định nếu không có cookie
        }
    }
}
