﻿using Confluent.Kafka;
using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace NSE.MessageBus.Serializador
{
    internal class DeserializerNSE<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using var memoryStream = new MemoryStream(data.ToArray());
            using var zip = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            string stringZip = zip.ToString();

            return JsonSerializer.Deserialize<T>(stringZip);
        }
    }
}