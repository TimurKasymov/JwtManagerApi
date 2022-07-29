using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace jwtapi
{
       /// <summary>
       /// Handles requests relating to JWT
       /// </summary>
       public class JwtManager
       {
              private readonly IOptions<JwtSettings> jwtSettings;
              private readonly ILogger<JwtManager> _logger;

              public JwtManager(IOptions<JwtSettings> jwtSettings, ILogger<JwtManager> logger)
              {
                     this._logger = logger;
                     this.jwtSettings = jwtSettings;
              }

              private SymmetricSecurityKey GetSecretKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Value.Secret));
              
              /// <summary>
              /// Generates JWT token based on received data
              /// </summary>
              /// <param name="payload"></param>
              /// <param name="expiresInDays"></param>
              /// <returns></returns>
              public string Generate(Dictionary<string, string> payload, int expiresInDays)
              {
                     var tokenHandler = new JwtSecurityTokenHandler();
                     Claim[] claims = new Claim[payload.Count()];
                     foreach (var data in payload)
                            claims.Append(new Claim(data.Key, data.Value));

                     var tokenDescriptor = new SecurityTokenDescriptor()
                     {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddDays(expiresInDays),
                            Issuer = jwtSettings.Value.Issuer,
                            Audience = jwtSettings.Value.Audience,
                            SigningCredentials = new SigningCredentials(GetSecretKey(), SecurityAlgorithms.HmacSha256Signature)
                     };

                     var token = tokenHandler.CreateToken(tokenDescriptor);
                     return tokenHandler.WriteToken(token);
              }

              /// <summary>
              /// trys to validate given token
              /// </summary>
              /// <param name="token"></param>
              /// <returns></returns>
              public bool ValidateToken(string token)
              {
                     var tokenHandler = new JwtSecurityTokenHandler();
                     try
                     {
                            tokenHandler.ValidateToken(token, new TokenValidationParameters
                            {
                                   ValidateIssuerSigningKey = true,
                                   ValidateIssuer = true,
                                   ValidateAudience = true,
                                   ValidIssuer = jwtSettings.Value.Issuer,
                                   ValidAudience = jwtSettings.Value.Audience,
                                   IssuerSigningKey = GetSecretKey()
                            }, out SecurityToken validatedToken);
                     }
                     catch
                     {
                            _logger.LogInformation("Given token failed JWT validation");
                            return false;
                     }

                     return true;
              }

              /// <summary>
              /// Extracts claims data from token
              /// </summary>
              /// <param name="token"></param>
              /// <returns></returns>
              public List<ClaimProperty>? GetTokenClaims(string token)
              {
                     var tokenHandler = new JwtSecurityTokenHandler();
                     var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                     if (securityToken == null)
                            return null;
                     var stringClaimValue = securityToken.Claims.Select(s => new ClaimProperty(s.Type, s.Value)).ToList();
                     return stringClaimValue;
              }
       }
}