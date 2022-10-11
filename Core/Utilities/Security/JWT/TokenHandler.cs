using Core.Extensions;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration Configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            
            Token token = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            //şifreleme türü
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //token geçerlilik süresi
            token.Expiration = DateTime.Now.AddDays(60);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:token.Expiration,
                notBefore:DateTime.Now,
                claims:SetClaims(user,operationClaims),
                signingCredentials:signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();


            //token oluşturuldu
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            //refresh token üretiliyor

            token.RefreshToken = CreateRefreshToken();

            return token;

        }


        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random =RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }


        }

        private IEnumerable<Claim> SetClaims(User user , List<OperationClaim> operationClaims)
        {
            string[] defRole = new string[] { "m1b2" };
            var claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddUserId(user.Id);

            claims.AddRoles(operationClaims.Select(p=>p.Name).ToArray());
            claims.AddRoles(defRole);
            return claims;

        }
    }
}
