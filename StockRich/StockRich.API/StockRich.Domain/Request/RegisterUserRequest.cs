using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockRich.Domain.Request;

public class RegisterUserRequest
{
    /// <summary>
    /// 帳號
    /// </summary>
    [Required]
    [RegularExpression("[a-zA-Z0-9]{4,9}")]
    [JsonPropertyName("account")]
    public string Account { get; set; }
    
    /// <summary>
    /// 密碼
    /// </summary>
    [Required]
    [StringLength(8)]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    [StringLength(20)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}