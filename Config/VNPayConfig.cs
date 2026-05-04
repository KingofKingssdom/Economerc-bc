using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Config
{
    public class VNPayConfig
    {
        public string vnp_PayUrl { get; set; } = string.Empty;
        public string vnp_Returnurl { get; set; } = string.Empty;
        public string vnp_TmnCode { get; set; } = string.Empty;
        public string vnp_HashSecret { get; set; } = string.Empty;
        public string vnp_apiUrl { get; set; } = string.Empty;

        public string HashAllFields(Dictionary<string, string> fields)
        {
           
            var fieldNames = fields.Keys.ToList();
            fieldNames.Sort();

            var sb = new StringBuilder();
            var first = true;

            foreach (string fieldName in fieldNames)
            {
                string fieldValue = fields[fieldName];
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    // Thêm dấu "&" giữa các tham số (ngoại trừ tham số đầu tiên)
                    if (!first)
                    {
                        sb.Append("&");
                    }

                    sb.Append(fieldName);
                    sb.Append("=");
                    sb.Append(fieldValue);

                    first = false;
                }
            }

            return HmacSHA512(vnp_HashSecret, sb.ToString());
        }

        public static string HmacSHA512(string key, string data)
        {
            if (key == null || data == null) throw new ArgumentNullException();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);

                
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
