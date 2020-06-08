using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace CustomMediaTypeFormatterWebAPI_Demo.CustomMediaTypeFormatter
{
    public class CustomXmlFormatter : XmlMediaTypeFormatter
    {
        public CustomXmlFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/xml");
        }
    }
}