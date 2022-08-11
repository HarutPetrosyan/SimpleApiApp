using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TestApi.Models.Cryptography
{
    public class JWT
    {

        public JWT(string EncryptionKey)
        {
            _EncryptionKey = EncryptionKey;
        }
        private string _EncryptionKey;
        
        public string SetJWTToken(object JWTObject)
        {
            var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_EncryptionKey));
            var _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);
            var _header = new JwtHeader(_credentials);
            var _payload = new JwtPayload { { "JwtTokenObject", JWTObject } };
            var _secToken = new JwtSecurityToken(_header, _payload);
            var _handler = new JwtSecurityTokenHandler();
            return _handler.WriteToken(_secToken);
        }
        public string SetJWTToken(string JWTObjectName, object JWTObject)
        {
            var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_EncryptionKey));
            var _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);
            var _header = new JwtHeader(_credentials);
            var _payload = new JwtPayload { { JWTObjectName, JWTObject } };
            var _secToken = new JwtSecurityToken(_header, _payload);
            var _handler = new JwtSecurityTokenHandler();
            return _handler.WriteToken(_secToken);
        }

        public string ReadJWTToken(string _Token)
        {

            var _handler = new JwtSecurityTokenHandler();
            var _token = _handler.ReadJwtToken(_Token);
            return _token.Payload.SerializeToJson();
        }
       


    }
}
