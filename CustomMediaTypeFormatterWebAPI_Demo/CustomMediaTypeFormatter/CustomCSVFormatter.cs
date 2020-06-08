using CustomMediaTypeFormatterWebAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace CustomMediaTypeFormatterWebAPI_Demo.CustomMediaTypeFormatter
{
    public class CustomCSVFormatter : BufferedMediaTypeFormatter
    {
        public CustomCSVFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));

            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        //override the CanReadType method to indicate which types the formatter can deserialize.
        //In this example, the formatter does not support deserialization, so the method simply returns false.
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Customer))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Customer>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);

            using (var writer = new StreamWriter(writeStream))
            {
                var customers = value as IEnumerable<Customer>;
                if (customers != null)
                {
                    foreach (var customer in customers)
                    {
                        WriteItem(customer, writer);
                    }
                }
                else
                {
                    Customer singleCustomer = value as Customer;
                    if (singleCustomer == null)
                    {
                        throw new InvalidOperationException("Cannot serialize type");
                    }
                    WriteItem(singleCustomer, writer);
                }
            }
        }

        // Helper methods for serializing Products to CSV format.
        private void WriteItem(Customer customer, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3}", Escape(customer.Id),
            Escape(customer.Name), Escape(customer.PhoneNumber), Escape(customer.Email), Escape(customer.Address));
        }

        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };
        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                // Delimit the entire field with quotes and replace embedded quotes with "".
                return string.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }
    }
}