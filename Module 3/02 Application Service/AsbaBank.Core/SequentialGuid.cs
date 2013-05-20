using System;

namespace AsbaBank.Core
{
    public class SequentialGuid
    {
        Guid currentGuid;
        public Guid CurrentGuid
        {
            get { return currentGuid; }
        }

        public SequentialGuid()
        {
            currentGuid = Guid.NewGuid();
        }

        public SequentialGuid(Guid previousGuid)
        {
            currentGuid = previousGuid;
        }

        public static SequentialGuid operator ++(SequentialGuid sequentialGuid)
        {
            byte[] bytes = sequentialGuid.currentGuid.ToByteArray();
            for (int mapIndex = 0; mapIndex < 16; mapIndex++)
            {
                int bytesIndex = SqlOrderMap[mapIndex];
                bytes[bytesIndex]++;
                if (bytes[bytesIndex] != 0)
                {
                    break;
                }
            }
            sequentialGuid.currentGuid = new Guid(bytes);
            return sequentialGuid;
        }

        private static int[] sqlOrderMap;
        private static int[] SqlOrderMap
        {
            get { return sqlOrderMap ?? (sqlOrderMap = new[] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 }); }
        }
    }
}