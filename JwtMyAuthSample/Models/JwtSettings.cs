using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtMyAuthSample.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// token谁颁发的
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// token可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 用于加密的key
        /// </summary>
        public string SecretKey { get; set; }
    }
}
