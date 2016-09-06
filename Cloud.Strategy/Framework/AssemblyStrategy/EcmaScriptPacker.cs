using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using Cloud.Framework.Assembly;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    public class EcmaScriptPacker : StrategyBase<string>, IEcmaScriptPacker
    {
        public enum PackerEncoding { None = 0, Numeric = 10, Mid = 36, Normal = 62, HighAscii = 95 };

        private const string Ignore = "$1";

        public PackerEncoding Encoding { get; set; }

        public bool FastDecode { get; set; }

        public bool SpecialChars { get; set; }

        public bool Enabled { get; set; } = true;

        public EcmaScriptPacker()
        {
            Encoding = PackerEncoding.Normal;
            FastDecode = true;
            SpecialChars = false;
        }

        public EcmaScriptPacker(PackerEncoding encoding, bool fastDecode, bool specialChars)
        {
            Encoding = encoding;
            FastDecode = fastDecode;
            SpecialChars = specialChars;
        }

        public string Pack(string script)
        {
            if (!Enabled) return script;
            script += "\n";
            script = BasicCompression(script);
            if (SpecialChars)
                script = EncodeSpecialChars(script);
            if (Encoding != PackerEncoding.None)
                script = EncodeKeywords(script);
            return script;
        }

        private string BasicCompression(string script)
        {
            var parser = new ParseMaster { EscapeChar = '\\' };
            // make safe
            // protect strings
            parser.Add("'[^'\\n\\r]*'", Ignore);
            parser.Add("\"[^\"\\n\\r]*\"", Ignore);
            // remove comments
            parser.Add("\\/\\/[^\\n\\r]*[\\n\\r]");
            parser.Add("\\/\\*[^*]*\\*+([^\\/][^*]*\\*+)*\\/");
            // protect regular expressions
            parser.Add("\\s+(\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?)", "$2");
            parser.Add("[^\\w\\$\\/'\"*)\\?:]\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?", Ignore);
            // remove: ;;; doSomething();
            if (SpecialChars)
                parser.Add(";;[^\\n\\r]+[\\n\\r]");
            // remove redundant semi-colons
            parser.Add(";+\\s*([};])", "$2");
            // remove white-space
            parser.Add("(\\b|\\$)\\s+(\\b|\\$)", "$2 $3");
            parser.Add("([+\\-])\\s+([+\\-])", "$2 $3");
            parser.Add("\\s+");
            // done
            return parser.Exec(script);
        }

        private WordList _encodingLookup;
        private string EncodeSpecialChars(string script)
        {
            var parser = new ParseMaster();
            parser.Add("((\\$+)([a-zA-Z\\$_]+))(\\d*)",
                EncodeLocalVars);
            var regex = new Regex("\\b_[A-Za-z\\d]\\w*");
            _encodingLookup = Analyze(script, regex, EncodePrivate);
            parser.Add("\\b_[A-Za-z\\d]\\w*", EncodeWithLookup);
            script = parser.Exec(script);
            return script;
        }

        private string EncodeKeywords(string script)
        {
            if (Encoding == PackerEncoding.HighAscii) script = Escape95(script);
            var parser = new ParseMaster();
            var encode = GetEncoder(Encoding);
            var regex = new Regex(
                    (Encoding == PackerEncoding.HighAscii) ? "\\w\\w+" : "\\w+"
                );
            _encodingLookup = Analyze(script, regex, encode);
            parser.Add((Encoding == PackerEncoding.HighAscii) ? "\\w\\w+" : "\\w+",
                EncodeWithLookup);
            return (script == string.Empty) ? "" : BootStrap(parser.Exec(script), _encodingLookup);
        }

        private string BootStrap(string packed, WordList keywords)
        {
            packed = "'" + Escape(packed) + "'";
            var ascii = Math.Min(keywords.Sorted.Count, (int)Encoding);
            if (ascii == 0)
                ascii = 1;
            var count = keywords.Sorted.Count;
            foreach (var key in keywords.Protected.Keys)
            {
                keywords.Sorted[(int)key] = "";
            }
            var sbKeywords = new StringBuilder("'");
            foreach (var word in keywords.Sorted)
                sbKeywords.Append(word + "|");
            sbKeywords.Remove(sbKeywords.Length - 1, 1);
            var keywordsout = sbKeywords + "'.split('|')";

            string encode = null;
            var inline = "c";

            switch (Encoding)
            {
                case PackerEncoding.Mid:
                    encode = "function(c){return c.toString(36)}";
                    inline += ".toString(a)";
                    break;
                case PackerEncoding.Normal:
                    encode = "function(c){return(c<a?\"\":e(parseInt(c/a)))+" +
                        "((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))}";
                    inline += ".toString(a)";
                    break;
                case PackerEncoding.HighAscii:
                    encode = "function(c){return(c<a?\"\":e(c/a))+" +
                        "String.fromCharCode(c%a+161)}";
                    inline += ".toString(a)";
                    break;
                case PackerEncoding.None:
                    break;
                case PackerEncoding.Numeric:
                    break;
                default:
                    encode = "function(c){return c}";
                    break;
            }

            // decode: code snippet to speed up decoding
            var decode = "";
            if (FastDecode)
            {
                decode = "if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1;}";
                switch (Encoding)
                {
                    case PackerEncoding.HighAscii:
                        decode = decode.Replace("\\\\w", "[\\xa1-\\xff]");
                        break;
                    case PackerEncoding.Numeric:
                        decode = decode.Replace("e(c)", inline);
                        break;
                    case PackerEncoding.None:
                        break;
                    case PackerEncoding.Mid:
                        break;
                    case PackerEncoding.Normal:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (count == 0)
                    decode = decode.Replace("c=1", "c=0");
            }

            var unpack = "function(p,a,c,k,e,d){while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p;}";
            Regex r;
            if (FastDecode)
            {
                r = new Regex("\\{");
                unpack = r.Replace(unpack, "{" + decode + ";", 1);
            }

            if (Encoding == PackerEncoding.HighAscii)
            {
                r = new Regex("'\\\\\\\\b'\\s*\\+|\\+\\s*'\\\\\\\\b'");
                unpack = r.Replace(unpack, "");
            }
            if (Encoding == PackerEncoding.HighAscii || ascii > (int)PackerEncoding.Normal || FastDecode)
            {
                r = new Regex("\\{");
                unpack = r.Replace(unpack, "{e=" + encode + ";", 1);
            }
            else
            {
                r = new Regex("e\\(c\\)");
                unpack = r.Replace(unpack, inline);
            }
            var _params = "" + packed + "," + ascii + "," + count + "," + keywordsout;
            if (FastDecode)
            {
                _params += ",0,{}";
            }
            return "eval(" + unpack + "(" + _params + "))\n";
        }

        private string Escape(string input)
        {
            var r = new Regex("([\\\\'])");
            return r.Replace(input, "\\$1");
        }

        private static EncodeMethod GetEncoder(PackerEncoding encoding)
        {
            switch (encoding)
            {
                case PackerEncoding.Mid:
                    return Encode36;
                case PackerEncoding.Normal:
                    return Encode62;
                case PackerEncoding.HighAscii:
                    return Encode95;
                default:
                    return Encode10;
            }
        }

        private static string Encode10(int code)
        {
            return code.ToString();
        }

        private static readonly string _lookup36 = "0123456789abcdefghijklmnopqrstuvwxyz";

        private static string Encode36(int code)
        {
            var encoded = "";
            var i = 0;
            do
            {
                var digit = (code / (int)Math.Pow(36, i)) % 36;
                encoded = _lookup36[digit] + encoded;
                code -= digit * (int)Math.Pow(36, i++);
            } while (code > 0);
            return encoded;
        }

        private static readonly string Lookup62 = _lookup36 + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string Encode62(int code)
        {
            var encoded = "";
            var i = 0;
            do
            {
                var digit = (code / (int)Math.Pow(62, i)) % 62;
                encoded = Lookup62[digit] + encoded;
                code -= digit * (int)Math.Pow(62, i++);
            } while (code > 0);
            return encoded;
        }

        private static readonly string _lookup95 = "、￥ウЖ┆辈炒刀犯购患骄坷谅媚牌侨墒颂臀闲岩釉罩棕仝圮蒉哙徕沅彐玷殛腱眍镳耱篝貊鼬";

        private static string Encode95(int code)
        {
            var encoded = "";
            var i = 0;
            do
            {
                var digit = (code / (int)Math.Pow(95, i)) % 95;
                encoded = _lookup95[digit] + encoded;
                code -= digit * (int)Math.Pow(95, i++);
            } while (code > 0);
            return encoded;
        }

        private string Escape95(string input)
        {
            var r = new Regex("[\xa1-\xff]");
            return r.Replace(input, Escape95Eval);
        }

        private string Escape95Eval(Match match)
        {
            return "\\x" + ((int)match.Value[0]).ToString("x");
        }

        private static string EncodeLocalVars(Match match, int offset)
        {
            var length = match.Groups[offset + 2].Length;
            var start = length - Math.Max(length - match.Groups[offset + 3].Length, 0);
            return match.Groups[offset + 1].Value.Substring(start, length) + match.Groups[offset + 4].Value;
        }

        private string EncodeWithLookup(Match match, int offset)
        {
            return (string)_encodingLookup.Encoded[match.Groups[offset].Value];
        }

        private delegate string EncodeMethod(int code);

        private static string EncodePrivate(int code)
        {
            return "_" + code;
        }

        private static WordList Analyze(string input, Regex regex, EncodeMethod encodeMethod)
        {
            // analyse
            // retreive all words in the script
            var all = regex.Matches(input);
            WordList rtrn;
            rtrn.Sorted = new StringCollection();
            rtrn.Protected = new HybridDictionary();
            rtrn.Encoded = new HybridDictionary();
            if (all.Count <= 0) return rtrn;
            var unsorted = new StringCollection();
            var Protected = new HybridDictionary();
            var values = new HybridDictionary();
            var count = new HybridDictionary();
            int i = all.Count, j = 0;
            string word;
            // count the occurrences - used for sorting later
            do
            {
                word = "$" + all[--i].Value;
                if (count[word] == null)
                {
                    count[word] = 0;
                    unsorted.Add(word);
                    Protected["$" + (values[j] = encodeMethod(j))] = j++;
                }
                // increment the word counter
                count[word] = (int)count[word] + 1;
            } while (i > 0);
            i = unsorted.Count;
            var sortedarr = new string[unsorted.Count];
            do
            {
                word = unsorted[--i];
                if (Protected[word] != null)
                {
                    sortedarr[(int)Protected[word]] = word.Substring(1);
                    rtrn.Protected[(int)Protected[word]] = true;
                    count[word] = 0;
                }
            } while (i > 0);
            var unsortedarr = new string[unsorted.Count];
            unsorted.CopyTo(unsortedarr, 0);
            // sort the words by frequency
            Array.Sort(unsortedarr, new CountComparer(count));
            j = 0;
            do
            {
                if (sortedarr[i] == null)
                    sortedarr[i] = unsortedarr[j++].Substring(1);
                rtrn.Encoded[sortedarr[i]] = values[i];
            } while (++i < unsortedarr.Length);
            rtrn.Sorted.AddRange(sortedarr);
            return rtrn;
        }

        private struct WordList
        {
            public StringCollection Sorted;
            public HybridDictionary Encoded;
            public HybridDictionary Protected;
        }

        private class CountComparer : IComparer
        {
            private readonly HybridDictionary _count;

            public CountComparer(HybridDictionary count)
            {
                _count = count;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                return (int)_count[y] - (int)_count[x];
            }

            #endregion
        }

        #region IHttpHandler Members

        #endregion

        public override string[] Declare { get; }
    }
}
