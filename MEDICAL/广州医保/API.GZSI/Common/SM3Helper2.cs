using System;
using System.Data;
using System.IO;
using System.Text;

/**
 * SM3加密
 */
public static class SM3Utils {

    private static char[] chars = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
    public static readonly byte[] IV = {0x73, (byte) 0x80, 0x16, 0x6f, 0x49, 0x14, (byte) 0xb2, (byte) 0xb9, 0x17, 0x24, 0x42,
            (byte) 0xd7, (byte) 0xda, (byte) 0x8a, 0x06, 0x00, (byte) 0xa9, 0x6f, 0x30, (byte) 0xbc, (byte) 0x16, 0x31,
            0x38, (byte) 0xaa, (byte) 0xe3, (byte) 0x8d, (byte) 0xee, 0x4d, (byte) 0xb0, (byte) 0xfb, 0x0e, 0x4e};
    private static readonly int TJ_15 = Convert.ToInt32("79cc4519", 16);
    private static readonly int TJ_63 = Convert.ToInt32("7a879d8a", 16);
    private static readonly byte[] FirstPadding = {(byte) 0x80};
    private static readonly byte[] ZeroPadding = {(byte) 0x00};

    private static int T(int j) {
        if (j >= 0 && j <= 15) {
            return TJ_15;
        } else if (j >= 16 && j <= 63) {
            return TJ_63;
        } else {
            throw new FormatException("data invalid");
        }
    }

    private static int FF(int x, int y, int z, int j)
    {
        if (j >= 0 && j <= 15) {
            return (x ^ y ^ z);
        } else if (j >= 16 && j <= 63) {
            return ((x & y) | (x & z) | (y & z));
        } else {
            throw new FormatException("data invalid");
        }
    }

    private static int GG(int x, int y, int z, int j)
    {
        if (j >= 0 && j <= 15) {
            return (x ^ y ^ z);
        } else if (j >= 16 && j <= 63) {
            return ((x & y) | (~x & z));
        } else {
            throw new FormatException("data invalid");
        }
    }

    public static int RotateLeft(this int value, int count)
    {
        return (value << count) | (value >> (32 - count));
    }
    
    public static int RotateRight(this int value, int count)
    {
        return (value >> count) | (value << (32 - count));
    }

    private static int P0(int x) 
    {
        return (x ^ RotateLeft(x, 9) ^ RotateLeft(x, 17));
    }

    private static int P1(int x) {
        return (x ^ RotateLeft(x, 15) ^ RotateLeft(x, 23));
    }

    private static byte[] padding(byte[] source)
    {
        if (source.Length >= 0x2000000000000000L) {
            throw new FormatException("src data invalid.");
        }
        long l = source.Length * 8;
        long k = 448 - (l + 1) % 512;
        if (k < 0) {
            k = k + 512;
        }
        try 
        {
            MemoryStream baos = new MemoryStream();
            baos.Write(source,0,source.Length);
            baos.Write(FirstPadding,0,FirstPadding.Length);
            long i = k - 7;
            while (i > 0) {
                baos.Write(ZeroPadding,0,ZeroPadding.Length);
                i -= 8;
            }
            byte[] longbyte = long2bytes(l);
            baos.Write(longbyte, 0, longbyte.Length);
            return baos.ToArray();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 无符号右移, 相当于java里的 value>>>pos
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static int RightMove(int value, int pos)
    {
        //移动 0 位时直接返回原值
        if (pos != 0)  
        {
            // int.MaxValue = 0x7FFFFFFF 整数最大值
            int mask = int.MaxValue;
            //无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
            value = value >> 1;
            //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
            value = value & mask;
            //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
            value = value >> pos - 1;     
        }

        return value;
    }

        /// <summary>
    /// 无符号右移, 相当于java里的 value>>>pos
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static long RightMove(long value, int pos)
    {
        //移动 0 位时直接返回原值
        if (pos != 0)  
        {
            // int.MaxValue = 0x7FFFFFFF 整数最大值
            int mask = int.MaxValue;
            //无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
            value = value >> 1;
            //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
            value = value & mask;
            //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
            value = value >> pos - 1;     
        }

        return value;
    }

    private static byte[] long2bytes(long l) {
        byte[] bytes = new byte[8];
        for (int i = 0; i < 8; i++) {
            //bytes[i] = (byte) (l >>> ((7 - i) * 8));
            bytes[i] = (byte) (RightMove(l,((7 - i) * 8)));
        }
        return bytes;
    }

    public static String encodeSM3(string source)  
    {

        try
        {
            byte[] b = encodeSM3(System.Text.Encoding.Default.GetBytes(source));
            return new String(encodeHex(b));
        }
        catch(Exception)
        {
            return "";
        }
    }

    public static byte[] encodeSM3(byte[] source) {
        byte[] m1 = padding(source);
        int n = m1.Length / (512 / 8);

        byte[] b = new byte[64];
        byte[] vi = (byte [])IV.Clone();
        byte[] vi1 = null;
        for (int i = 0; i < n; i++) {
            //b = Arrays.copyOfRange(m1, i * 64, (i + 1) * 64);
            System.Array.Copy(m1, i * 64, b, 0, 64);
            vi1 = CF(vi, b);
            vi = vi1;
        }
        return vi1;
    }

    private static byte[] CF(byte[] vi, byte[] bi) {
        int a, b, c, d, e, f, g, h;
        a = toInteger(vi, 0);
        b = toInteger(vi, 1);
        c = toInteger(vi, 2);
        d = toInteger(vi, 3);
        e = toInteger(vi, 4);
        f = toInteger(vi, 5);
        g = toInteger(vi, 6);
        h = toInteger(vi, 7);

        int[] w = new int[68];
        int[] w1 = new int[64];
        for (int i = 0; i < 16; i++) {
            w[i] = toInteger(bi, i);
        }
        for (int j = 16; j < 68; j++) {
            w[j] = P1(w[j - 16] ^ w[j - 9] ^ RotateLeft(w[j - 3], 15)) ^ RotateLeft(w[j - 13], 7)
                    ^ w[j - 6];
        }
        for (int j = 0; j < 64; j++) {
            w1[j] = w[j] ^ w[j + 4];
        }
        int ss1, ss2, tt1, tt2;
        for (int j = 0; j < 64; j++) {
            ss1 = RotateLeft(RotateLeft(a, 12) + e + RotateLeft(T(j), j), 7);
            ss2 = ss1 ^ RotateLeft(a, 12);
            tt1 = FF(a, b, c, j) + d + ss2 + w1[j];
            tt2 = GG(e, f, g, j) + h + ss1 + w[j];
            d = c;
            c = RotateLeft(b, 9);
            b = a;
            a = tt1;
            h = g;
            g = RotateLeft(f, 19);
            f = e;
            e = P0(tt2);
        }
        byte[] v = toByteArray(a, b, c, d, e, f, g, h);
        for (int i = 0; i < v.Length; i++) {
            v[i] = (byte) (v[i] ^ vi[i]);
        }
        return v;
    }

    private static int toInteger(byte[] source, int index) {
        StringBuilder valueStr = new StringBuilder("");
        for (int i = 0; i < 4; i++) {
            valueStr.Append(chars[(byte) ((source[index * 4 + i] & 0xF0) >> 4)]);
            valueStr.Append(chars[(byte)(source[index * 4 + i] & 0x0F)]);
        }
        return Convert.ToInt32(valueStr.ToString(), 16);

    }

    private static byte[] toByteArray(int a, int b, int c, int d, int e, int f, int g, int h) {
        try
        {
            MemoryStream baos = new MemoryStream(32);
            byte[] byteArray = toByteArray(a);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(b);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(c);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(d);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(e);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(f);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(g);
            baos.Write(byteArray, 0, byteArray.Length);
            byteArray = toByteArray(h);
            baos.Write(byteArray, 0, byteArray.Length);
            return baos.ToArray();
        }
        catch
        {
            return null;
        }
    }

    private static byte[] toByteArray(int i) {
        byte[] byteArray = new byte[4];
        byteArray[0] = (byte) (RightMove(i,24));
        byteArray[1] = (byte) (RightMove((i & 0xFFFFFF),16));
        byteArray[2] = (byte) (RightMove((i & 0xFFFF), 8));
        byteArray[3] = (byte) (i & 0xFF);
        return byteArray;
    }

    //private SM3Utils() {

    //}

    private static char[] encodeHex(byte[] data) {
        int len = data.Length;
        char[] outData = new char[len << 1];
        int i = 0;
        for(int var5 = 0; i < len; ++i) {
            outData[var5++] = chars[RightMove((240 & data[i]), 4)];
            outData[var5++] = chars[15 & data[i]];
        }
        return outData;
    }
}
