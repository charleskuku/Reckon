using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ReckonStringMatching.Services
{
    public static class StreamExtensions
    {
        public static T ReadAndDeserializeFromJson<T>(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead) throw new NotSupportedException("Can't read from this stream.");

            using(var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    JsonSerializer jsonSerializer = new();
                    return jsonSerializer.Deserialize<T>(jsonTextReader);

                }

            }
        }
    }
}
