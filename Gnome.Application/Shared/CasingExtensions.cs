using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public static class CasingExtensions
    {
        internal enum KebabCaseState
        {
            Start,
            Lower,
            Upper,
            NewWord
        }

        private static readonly char[] Delimeters = new char[3] { ' ', '-', '_' };

        private static char HYPHEN = '-';

        private static char UNDERSCORE = '_';

        //
        // Summary:
        //     Convert case of provided string to PascalCase
        //
        // Parameters:
        //   source:
        public static string ToPascalCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return SymbolsPipe(source, '\0', (char s, bool i) => new char[1] { char.ToUpperInvariant(s) });
        }

        //
        // Summary:
        //     Transforms string to Camel case
        //
        // Parameters:
        //   source:
        public static string ToCamelCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return SymbolsPipe(source, '\0', (char s, bool disableFrontDelimeter) => (!disableFrontDelimeter) ? new char[1] { char.ToUpperInvariant(s) } : new char[1] { char.ToLowerInvariant(s) });
        }

        //
        // Summary:
        //     Transforms string to Kebab case
        //
        // Parameters:
        //   source:
        public static string ToKebabCase(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            source = source.Replace(" ", "_");
            StringBuilder stringBuilder = new StringBuilder();
            KebabCaseState kebabCaseState = KebabCaseState.Start;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == ' ')
                {
                    if (kebabCaseState != 0)
                    {
                        kebabCaseState = KebabCaseState.NewWord;
                    }
                }
                else if (char.IsUpper(source[i]))
                {
                    switch (kebabCaseState)
                    {
                        case KebabCaseState.Upper:
                            {
                                bool flag = i + 1 < source.Length;
                                if (i > 0 && flag)
                                {
                                    char c = source[i + 1];
                                    if (!char.IsUpper(c) && c != UNDERSCORE && c != HYPHEN)
                                    {
                                        stringBuilder.Append(HYPHEN);
                                    }
                                }

                                break;
                            }
                        case KebabCaseState.Lower:
                        case KebabCaseState.NewWord:
                            if (i > 0)
                            {
                                stringBuilder.Append(HYPHEN);
                            }

                            break;
                    }
                    char value = char.ToLowerInvariant(source[i]);
                    stringBuilder.Append(value);
                    kebabCaseState = KebabCaseState.Upper;
                }
                else if (source[i] == UNDERSCORE || source[i] == HYPHEN)
                {
                    if (kebabCaseState != 0)
                    {
                        stringBuilder.Append(HYPHEN);
                        kebabCaseState = KebabCaseState.Start;
                    }
                }
                else
                {
                    if (kebabCaseState == KebabCaseState.NewWord)
                    {
                        stringBuilder.Append(HYPHEN);
                    }

                    stringBuilder.Append(source[i]);
                    kebabCaseState = KebabCaseState.Lower;
                }
            }

            return stringBuilder.ToString();
        }

        //
        // Summary:
        //     Converts string to Snake case
        //
        // Parameters:
        //   source:
        public static string ToSnakeCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return SymbolsPipe(source, '_', (char s, bool disableFrontDelimeter) => (!disableFrontDelimeter) ? new char[2]
            {
            '_',
            char.ToLowerInvariant(s)
            } : new char[1] { char.ToLowerInvariant(s) });
        }

        //
        // Summary:
        //     Converts string to Train case
        //
        // Parameters:
        //   source:
        public static string ToTrainCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return SymbolsPipe(source, '-', (char s, bool disableFrontDelimeter) => (!disableFrontDelimeter) ? new char[2]
            {
            '-',
            char.ToUpperInvariant(s)
            } : new char[1] { char.ToUpperInvariant(s) });
        }

        private static string SymbolsPipe(string source, char mainDelimeter, Func<char, bool, char[]> newWordSymbolHandler)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = true;
            bool arg = true;
            foreach (char c in source)
            {
                if (Delimeters.Contains(c))
                {
                    if (c == mainDelimeter)
                    {
                        stringBuilder.Append(c);
                        arg = true;
                    }

                    flag = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                    arg = true;
                    flag = true;
                }
                else if (flag || char.IsUpper(c))
            {
                    stringBuilder.Append(newWordSymbolHandler(c, arg));
                    arg = false;
                    flag = false;
                }
            else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
