﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationFramework
{
    class Phone : DataType
    {
        private static Regex _regex = CreateRegEx();
        private const string _additionalPhoneNumberCharacters = "-.()";

        public Phone()
        {
            //
        }


        public string ErrorMessage()
        {
            return "must be a phone format.";
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString = value as string;

            if (_regex != null)
            {
                return valueAsString != null && _regex.Match(valueAsString).Length > 0;
            }
            else
            {
                if (valueAsString == null)
                {
                    return false;
                }

                valueAsString = valueAsString.Replace("+", string.Empty).TrimEnd();
                valueAsString = RemoveExtension(valueAsString);

                bool digitFound = false;
                foreach (char c in valueAsString)
                {
                    if (Char.IsDigit(c))
                    {
                        digitFound = true;
                        break;
                    }
                }

                if (!digitFound)
                {
                    return false;
                }

                foreach (char c in valueAsString)
                {
                    if (!(Char.IsDigit(c)
                        || Char.IsWhiteSpace(c)
                        || _additionalPhoneNumberCharacters.IndexOf(c) != -1))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private static Regex CreateRegEx()
        {
            const string pattern = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(pattern, options, matchTimeout);
                }
            }
            catch
            {
                // Fallback on error
            }

            return new Regex(pattern, options);
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            int lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("ext.", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 4);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("ext", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 3);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }


            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("x", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 1);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            return potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
            {
                return false;
            }

            foreach (char c in potentialExtension)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
