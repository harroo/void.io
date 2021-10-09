
using System;

using System.IO;
using System.IO.Compression;

using System.Collections.Generic;

namespace CompressionUtility {

    public static class RLECompression {

        public static UInt16[] Compress (UInt16[] data) {
            List<UInt16> compressedData = new List<UInt16>();
            UInt16 currentType = data[0];
            UInt16 currentCount = 1;
            for (int i = 1; i < data.Length; i++) {
                if (data[i] == currentType) {
                    currentCount++;
                } else {
                    compressedData.Add(currentType);
                    compressedData.Add(currentCount);
                    currentType = data[i];
                    currentCount = 1;
                }
            }
            compressedData.Add(currentType);
            compressedData.Add(currentCount);
            return compressedData.ToArray();
        }

        public static UInt16[] Uncompress (UInt16[] data) {
            List<UInt16> uncompressedData = new List<UInt16>();
            for (int i = 0; i < data.Length; i += 2) {
                UInt16 type = data[i];
                UInt16 count = data[i + 1];
                for (int x = 0; x < count; x++) {
                    uncompressedData.Add(type);
                }
            }
            return uncompressedData.ToArray();
        }
    }

    public static class GZipCompression {

        public static byte[] Compress (byte[] input) {

            using (MemoryStream memory = new MemoryStream()) {

                using (GZipStream zipStream = new GZipStream(memory, CompressionMode.Compress, true)) {

                    zipStream.Write(input, 0, input.Length);
                }

                return memory.ToArray();
            }
        }

        public static byte[] Decompress (byte[] input) {

            using (GZipStream zipStream = new GZipStream(new MemoryStream(input), CompressionMode.Decompress)) {

                const int size = 4096;
                byte[] buffer = new byte[size];

                using (MemoryStream memory = new MemoryStream()) {

                    int count = 0;

                    do {

                        count = zipStream.Read(buffer, 0, size);

                        if (count > 0) {

                            memory.Write(buffer, 0, count);
                        }

                    } while (count > 0);

                    return memory.ToArray();
                }
            }
        }
    }
}
