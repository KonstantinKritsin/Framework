using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Project.Common.Extensions
{
    public static class CommonExtensions
    {
        public static string NameOf<T1, T2>(Expression<Func<T1, T2>> exp)
        {
            var body = exp.Body as MemberExpression;
            if (body == null)
            {
                var ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            Debug.Assert(body != null, "body != null");
            return body.Member.Name;
        }

        public static byte[] ToBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetPrincipalNameWithoutDomain(string principal)
        {
            if (string.IsNullOrEmpty(principal))
                return string.Empty;
            return Regex.Match(principal.Trim(), "(i:)?0e.t.*\\|(?<userName>[\\d\\w_&\\.\\s-]+)@[\\w\\d\\._]|(c:)?0-.t.*\\|(?<userName>[\\d\\w_&\\.\\s-]+)$|^(?<userName>[\\d\\w_&\\.\\s-]+)@[\\w\\d\\._]|^(?<userName>[\\d\\w_&\\.\\s-]+)$|[\\w]+\\\\(?<userName>[\\d\\w_&\\.\\s-]+)$").Groups["userName"].Value;
        }
    }
}
