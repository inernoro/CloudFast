using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

/*
	ParseMaster, version 1.0 (pre-release) (2005/02/01) x4
	Copyright 2005, Dean Edwards
	Web: http://dean.edwards.name/

	This software is licensed under the CC-GNU LGPL
	Web: http://creativecommons.org/licenses/LGPL/2.1/

    Ported to C# by Jesse Hansen, twindagger2k@msn.com
*/

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    /// <summary>
    /// a multi-pattern parser
    /// </summary>
    public class ParseMaster
    {
        // used to determine nesting levels
        private readonly Regex _groups = new Regex("\\(");

        private readonly Regex _subReplace = new Regex("\\$");

        private readonly Regex _indexed = new Regex("^\\$\\d+$");

        private readonly Regex _escape = new Regex("\\\\.");

        private readonly Regex _deleted = new Regex("\\x01[^\\x01]*\\x01");

        /// <summary>
        /// Delegate to call when a regular expression is found.
        /// Use match.Groups[offset + &lt;group number&gt;].Value to get
        /// the correct subexpression
        /// </summary>
        public delegate string MatchGroupEvaluator(Match match, int offset);

        private static string Delete(Match match, int offset)
        {
            return "\x01" + match.Groups[offset].Value + "\x01";
        }

        /// <summary>
        /// Ignore Case?
        /// </summary>
        public bool IgnoreCase { get; set; } = false;

        /// <summary>
        /// Escape Character to use
        /// </summary>
        public char EscapeChar { get; set; } = '\0';

        /// <summary>
        /// Add an expression to be deleted
        /// </summary>
        /// <param name="expression">Regular Expression String</param>
        public void Add(string expression)
        {
            Add(expression, string.Empty);
        }

        /// <summary>
        /// Add an expression to be replaced with the replacement string
        /// </summary>
        /// <param name="expression">Regular Expression String</param>
        /// <param name="replacement">Replacement String. Use $1, $2, etc. for groups</param>
        public void Add(string expression, string replacement)
        {
            if (replacement == string.Empty)
                Addexpress(expression, new MatchGroupEvaluator(Delete));

            Addexpress(expression, replacement);
        }

        /// <summary>
        /// Add an expression to be replaced using a callback function
        /// </summary>
        /// <param name="expression">Regular expression string</param>
        /// <param name="replacement">Callback function</param>
        public void Add(string expression, MatchGroupEvaluator replacement)
        {
            Addexpress(expression, replacement);
        }

        /// <summary>
        /// Executes the parser
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>parsed string</returns>
        public string Exec(string input)
        {
            return _deleted.Replace(Unescape(GetPatterns().Replace(Escape(input), Replacement)), string.Empty);
            //long way for debugging
            /*input = escape(input);
            Regex patterns = getPatterns();
            input = patterns.Replace(input, new MatchEvaluator(replacement));
            input = DELETED.Replace(input, string.Empty);
            return input;*/
        }

        private readonly ArrayList _patterns = new ArrayList();

        public void Addexpress(string expression, object replacement)
        {
            var pattern = new Pattern
            {
                Expression = expression,
                Replacement = replacement,
                Length = _groups.Matches(InternalEscape(expression)).Count + 1
            };
            //count the number of sub-expressions
            // - add 1 because each group is itself a sub-expression

            //does the pattern deal with sup-expressions?
            var s = replacement as string;
            if (s != null && _subReplace.IsMatch(s))
            {
                var sreplacement = s;
                // a simple lookup (e.g. $2)
                if (_indexed.IsMatch(sreplacement))
                {
                    pattern.Replacement = int.Parse(sreplacement.Substring(1)) - 1;
                }
            }

            _patterns.Add(pattern);
        }

        /// <summary>
        /// builds the patterns into a single regular expression
        /// </summary>
        /// <returns></returns>
        private Regex GetPatterns()
        {
            var rtrn = new StringBuilder(string.Empty);
            foreach (var pattern in _patterns)
            {
                rtrn.Append(((Pattern)pattern) + "|");
            }
            rtrn.Remove(rtrn.Length - 1, 1);
            return new Regex(rtrn.ToString(), IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// Global replacement function. Called once for each match found
        /// </summary>
        /// <param name="match">Match found</param>
        private string Replacement(Match match)
        {
            int i = 1, j = 0;
            Pattern pattern;
            //loop through the patterns
            while ((pattern = (Pattern)_patterns[j++]) != null)
            {
                //do we have a result?
                if (match.Groups[i].Value != string.Empty)
                {
                    var replacement = pattern.Replacement;
                    var evaluator = replacement as MatchGroupEvaluator;
                    if (evaluator != null)
                    {
                        return evaluator(match, i);
                    }
                    else if (replacement is int)
                    {
                        return match.Groups[(int)replacement + i].Value;
                    }
                    else
                    {
                        //string, send to interpreter
                        return ReplacementString(match, i, (string)replacement, pattern.Length);
                    }
                }
                else //skip over references to sub-expressions
                    i += pattern.Length;
            }
            return match.Value; //should never be hit, but you never know
        }

        /// <summary>
        /// Replacement function for complicated lookups (e.g. Hello $3 $2)
        /// </summary>
        private static string ReplacementString(Match match, int offset, string replacement, int length)
        {
            while (length > 0)
            {
                replacement = replacement.Replace("$" + length--, match.Groups[offset + length].Value);
            }
            return replacement;
        }

        private readonly StringCollection _escaped = new StringCollection();

        //encode escaped characters
        private string Escape(string str)
        {
            if (EscapeChar == '\0')
                return str;
            var escaping = new Regex("\\\\(.)");
            return escaping.Replace(str, EscapeMatch);
        }

        private string EscapeMatch(Match match)
        {
            _escaped.Add(match.Groups[1].Value);
            return "\\";
        }

        //decode escaped characters
        private int _unescapeIndex;

        public ParseMaster( )
        { 
        }

        private string Unescape(string str)
        {
            if (EscapeChar == '\0')
                return str;
            var unescaping = new Regex("\\" + EscapeChar);
            return unescaping.Replace(str, UnescapeMatch);
        }

        private string UnescapeMatch(Match match)
        {
            return "\\" + _escaped[_unescapeIndex++];
        }

        private string InternalEscape(string str)
        {
            return _escape.Replace(str, "");
        }

        //subclass for each pattern
        private class Pattern
        {
            public string Expression;
            public object Replacement;
            public int Length;

            public override string ToString()
            {
                return "(" + Expression + ")";
            }
        }
    }
}
