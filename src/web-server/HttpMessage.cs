
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;


namespace P.Web.Server
{
    public class HttpMessage
    {
        public HttpMethod Method { get; }
        public Uri RessourceUri { get; set; }
        public IEnumerable<KeyValuePair<string,IEnumerable<string>>> Headers { get; }
        public string Body { get; }

        public HttpMessage(HttpMethod method, Uri ressourceUri, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers,
            string body)
        {
            Method = method;
            RessourceUri = ressourceUri;
            Headers = headers;
            Body = body;
        }
    }
}
