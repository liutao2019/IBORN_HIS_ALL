using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAPICOM;

namespace FoShanYDSI
{
    public class CapicomTool
    {
        static private String storeName = "My";
        static StoreClass oStore;
        static Certificates oCerts;

        /// <summary>
        /// 获取数字签名(SHA1摘要)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetSignBySHA1(string data)
        {
            oStore = new StoreClass();
            oStore.Open(
              CAPICOM_STORE_LOCATION.CAPICOM_CURRENT_USER_STORE,
              storeName, CAPICOM_STORE_OPEN_MODE.CAPICOM_STORE_OPEN_READ_ONLY);
            oCerts = (Certificates)oStore.Certificates;
            oCerts = (Certificates)oCerts.Find(CAPICOM_CERTIFICATE_FIND_TYPE.CAPICOM_CERTIFICATE_FIND_KEY_USAGE, CAPICOM_KEY_USAGE.CAPICOM_DIGITAL_SIGNATURE_KEY_USAGE, true)//CAPICOM_KEY_USAGE.CAPICOM_DIGITAL_SIGNATURE_KEY_USAGE
                .Find(CAPICOM_CERTIFICATE_FIND_TYPE.CAPICOM_CERTIFICATE_FIND_TIME_VALID, 0, true)
                .Find(CAPICOM_CERTIFICATE_FIND_TYPE.CAPICOM_CERTIFICATE_FIND_EXTENDED_PROPERTY, CAPICOM_PROPID.CAPICOM_PROPID_KEY_SPEC, true);
            if (oCerts == null || oCerts.Count == 0)
            {
                return "-1";
            }
            CAPICOM.SignedData signedData = new SignedDataClass();
            signedData.Content = SHA.hex_sha1(data);
            CAPICOM.Signer signer = new CAPICOM.SignerClass();
            if (oCerts.Count > 1)
            {
                signer.Certificate = (Certificate)(oCerts.Select("选择签名证书", "请选中你的签名证书，按确定", false))[1];
            }
            else
            {
                signer.Certificate = (Certificate)oCerts[1];
            }
            signer.Options = CAPICOM_CERTIFICATE_INCLUDE_OPTION.CAPICOM_CERTIFICATE_INCLUDE_END_ENTITY_ONLY;
            string ret = signedData.Sign(signer, false, CAPICOM_ENCODING_TYPE.CAPICOM_ENCODE_BASE64);

            return ret.Replace("\r\n", "");
        }
    }

    class SHA
    {
        private static bool hexcase = false;
        private static int chrsz = 8;

        public static String hex_sha1(String s) { s = (s == null) ? "" : s; return binb2hex(core_sha1(str2binb(s), s.Length * chrsz)); }

        private static String binb2hex(int[] binarray)
        {
            String hex_tab = hexcase ? "0123456789ABCDEF" : "0123456789abcdef";
            String str = "";
            for (int i = 0; i < binarray.Length * 4; i++)
            {
                char a = (char)hex_tab[(binarray[i >> 2] >> ((3 - i % 4) * 8 + 4)) & 0xF];
                char b = (char)hex_tab[(binarray[i >> 2] >> ((3 - i % 4) * 8)) & 0xF];
                str += (a.ToString() + b.ToString());
            }
            return str;
        }

        private static int[] complete216(int[] oldbin)
        {
            if (oldbin.Length >= 16) return oldbin;
            int[] newbin = new int[16 - oldbin.Length];
            for (int i = 0; i < newbin.Length; newbin[i] = 0, i++) ;
            return concat(oldbin, newbin);
        }

        private static int[] concat(int[] oldbin, int[] newbin)
        {
            int[] retval = new int[oldbin.Length + newbin.Length];
            for (int i = 0; i < (oldbin.Length + newbin.Length); i++)
            {
                if (i < oldbin.Length) retval[i] = oldbin[i];
                else retval[i] = newbin[i - oldbin.Length];
            }
            return retval;
        }

        private static int[] core_hmac_sha1(String key, String data)
        {
            key = (key == null) ? "" : key;
            data = (data == null) ? "" : data;
            int[] bkey = complete216(str2binb(key));
            if (bkey.Length > 16) bkey = core_sha1(bkey, key.Length * chrsz);
            int[] ipad = new int[16];
            int[] opad = new int[16];
            for (int i = 0; i < 16; ipad[i] = 0, opad[i] = 0, i++) ;
            for (int i = 0; i < 16; i++)
            {
                ipad[i] = bkey[i] ^ 0x36363636;
                opad[i] = bkey[i] ^ 0x5C5C5C5C;
            }
            int[] hash = core_sha1(concat(ipad, str2binb(data)), 512 + data.Length * chrsz);
            return core_sha1(concat(opad, hash), 512 + 160);
        }

        private static int[] core_sha1(int[] x, int len)
        {
            int size = (len >> 5);
            x = strechBinArray(x, size);
            x[len >> 5] |= 0x80 << (24 - len % 32);
            size = ((len + 64 >> 9) << 4) + 15;
            x = strechBinArray(x, size);
            x[((len + 64 >> 9) << 4) + 15] = len;
            int[] w = new int[80];
            int a = 1732584193;
            int b = -271733879;
            int c = -1732584194;
            int d = 271733878;
            int e = -1009589776;
            for (int i = 0; i < x.Length; i += 16)
            {
                int olda = a;
                int oldb = b;
                int oldc = c;
                int oldd = d;
                int olde = e;
                for (int j = 0; j < 80; j++)
                {
                    if (j < 16) w[j] = x[i + j];
                    else w[j] = rol(w[j - 3] ^ w[j - 8] ^ w[j - 14] ^ w[j - 16], 1);
                    int t = safe_add(safe_add(rol(a, 5), sha1_ft(j, b, c, d)), safe_add(safe_add(e, w[j]), sha1_kt(j)));
                    e = d;
                    d = c;
                    c = rol(b, 30);
                    b = a;
                    a = t;
                }
                a = safe_add(a, olda);
                b = safe_add(b, oldb);
                c = safe_add(c, oldc);
                d = safe_add(d, oldd);
                e = safe_add(e, olde);
            }
            int[] retval = new int[5];
            retval[0] = a;
            retval[1] = b;
            retval[2] = c;
            retval[3] = d;
            retval[4] = e;
            return retval;
        }

        public static String hex_hmac_sha1(String key, String data) { return binb2hex(core_hmac_sha1(key, data)); }
        //private static int rol(int num, int cnt) { return (num << cnt) | (num >>> (32 - cnt)); }
        private static int rol(int num, int cnt) { return (num << cnt) | (MoveByte(num,32-cnt)); }
        private static int MoveByte(int value, int pos)
        {
            if (value < 0)
            {
                string s = Convert.ToString(value, 2);    // 转换为二进制
                for (int i = 0; i < pos; i++)
                {
                    s = "0" + s.Substring(0, 31);
                }
                return Convert.ToInt32(s, 2);            // 将二进制数字转换为数字
            }
            else
            {
                return value >> pos;
            }
        }

        private static int safe_add(int x, int y)
        {
            int lsw = (int)(x & 0xFFFF) + (int)(y & 0xFFFF);
            int msw = (x >> 16) + (y >> 16) + (lsw >> 16);
            return (msw << 16) | (lsw & 0xFFFF);
        }

        private static int sha1_ft(int t, int b, int c, int d)
        {
            if (t < 20) return (b & c) | ((~b) & d);
            if (t < 40) return b ^ c ^ d;
            if (t < 60) return (b & c) | (b & d) | (c & d);
            return b ^ c ^ d;
        }

        private static int sha1_kt(int t) { return (t < 20) ? 1518500249 : (t < 40) ? 1859775393 : (t < 60) ? -1894007588 : -899497514; }

        private static int[] str2binb(String str)
        {
            str = (str == null) ? "" : str;
            int[] tmp = new int[str.Length * chrsz];
            int mask = (1 << chrsz) - 1;
            for (int i = 0; i < str.Length * chrsz; i += chrsz) tmp[i >> 5] |= ((int)(str[i / chrsz]) & mask) << (24 - i % 32);
            int len = 0;
            for (int i = 0; i < tmp.Length && tmp[i] != 0; i++, len++) ;
            int[] bin = new int[len];
            for (int i = 0; i < len; i++) bin[i] = tmp[i];
            return bin;
        }

        private static int[] strechBinArray(int[] oldbin, int size)
        {
            int currlen = oldbin.Length;
            if (currlen >= size + 1) return oldbin;
            int[] newbin = new int[size + 1];
            for (int i = 0; i < size; newbin[i] = 0, i++) ;
            for (int i = 0; i < currlen; i++) newbin[i] = oldbin[i];
            return newbin;
        }
    }
}
