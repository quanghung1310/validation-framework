using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationFramework
{
    class RegularExpression : DataType
    {
        private int _matchTimeoutInMilliseconds;
        private bool _matchTimeoutSet;

        public int MatchTimeoutInMilliseconds
        {
            get
            {
                return _matchTimeoutInMilliseconds;
            }
            set
            {
                _matchTimeoutInMilliseconds = value;
                _matchTimeoutSet = true;
            }
        }

        public string Pattern { get; private set; }
        private Regex Regex { get; set; }

        public RegularExpression(string pattern)
        {
            this.Pattern = pattern;
        }

        public string ErrorMessage()
        {
            return "format is invalid.";
        }

        public bool IsValid(object value)
        {
            this.SetupRegex();

            string stringValue = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (String.IsNullOrEmpty(stringValue))
            {
                return true;
            }

            Match m = this.Regex.Match(stringValue);

            return (m.Success && m.Index == 0 && m.Length == stringValue.Length);
        }

        private void SetupRegex()
        {
            if (this.Regex == null)
            {
                if (string.IsNullOrEmpty(this.Pattern))
                {
                    throw new InvalidOperationException("Empty pattern");
                }

                if (!_matchTimeoutSet)
                {
                    MatchTimeoutInMilliseconds = GetDefaultTimeout();
                }

                Regex = MatchTimeoutInMilliseconds == -1
                    ? new Regex(Pattern)
                    : Regex = new Regex(Pattern, default(RegexOptions), TimeSpan.FromMilliseconds((double)MatchTimeoutInMilliseconds));
            }
        }

        private static int GetDefaultTimeout()
        {
            return 2000;
        }
    }
}
