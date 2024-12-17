using Microsoft.Data.SqlClient;

namespace MultiLanguageApp
{
    public static class CheckNewestData
    {
        public async static Task<bool> CheckChanged(string language, string _conn)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string query = "SELECT IsChangeData FROM LanguageConfig where LanguageCode = @LanguageCode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LanguageCode", language);
                    try
                    {
                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();
                        if (result != null && bool.TryParse(result.ToString(), out bool isDataChanged))
                        {
                            return isDataChanged;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public async static void CompleteCheck(string language, string _conn)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string query = "UPDATE LanguageConfig SET IsChangeData = 0 where LanguageCode = @LanguageCode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LanguageCode", language);
                    try
                    {
                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }
    }
}
