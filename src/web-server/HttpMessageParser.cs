using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace P.Web.Server
{
    public sealed class HttpMessageParser
    {
        private static readonly Regex StartLineHeadersRegEx = new Regex("(GET|POST|PUT|PATCH|DELETE|HEAD|CONNECT|OPTIONS|TRACE) (.+)(HTTP/1.1)|(.+):\\s(.+) |$");
        public static HttpMessage Parse(string message)
        {
            var startLineHeadersMatch = StartLineHeadersRegEx.Match(message);

            if (startLineHeadersMatch.Success)
            {
                var method = Enum.Parse<HttpMethod>(startLineHeadersMatch.Groups[1].Value);
                var ressource = new Uri(new Uri("popo:://pserver"), startLineHeadersMatch.Groups[2].Value);
                var headers = FilterHeaders(startLineHeadersMatch.Groups);

                var body = message.Substring(message.IndexOf("\r\n\r\n", StringComparison.OrdinalIgnoreCase)+4);
                return new HttpMessage(method, ressource, headers, body);
            }
            return null;
        }

        private static IEnumerable<KeyValuePair<string, IEnumerable<string>>> FilterHeaders(GroupCollection groups)
        {
            return null;
        }
    }
}
