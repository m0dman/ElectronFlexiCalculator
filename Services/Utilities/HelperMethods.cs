using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace RandoxITUtility.Utilities
{
    public static class HelperMethods
    {

        /// <summary>
        /// Static operation to get the name of the method calling this method
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The name of the method that is currently being called</returns>
        public static string GetCallerMemberName([CallerMemberName] string name = "")
        {
            return name;
        }


        /// <summary>
        /// Return a provide date formatted to a string for usage with SQL query parameters
        /// </summary>
        /// <param name="date">Date in question to format</param>
        /// <returns>A date string formatted to yyyy-MM-dd HH:mm:ss</returns>
        public static string GenerateSQLFormattedDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Used to check if the result is positive. Used for checking if the font color needs to be changed.
        /// </summary>
        /// <param name="value">The result that needs to be checked.</param>
        /// <returns>Returns return if the value is positive.</returns>
        public static bool IsResultPositive(string value)
        {
            return value.Trim().ToLower() == "positive" || value.Trim().ToLower() == "borderline";
        }

        /// <summary>
        /// Used to ensure that the provided value is converted to upper, trimmed and also any whitespace in the value is removed.
        /// </summary>
        /// <param name="value">The value to be affected.</param>
        /// <returns>Returns the value after it has been updated.</returns>
        public static string ToUpperAndTrimAndRemoveWhiteSpace(this string value)
        {
            return value.ToUpper().Trim().Replace(" ", string.Empty);
        }

        public static string GetCronFromDateTime(DateTime dateTime) => $"{dateTime.Minute} {dateTime.Hour} {dateTime.Day} {dateTime.Month} *";

        public static string GetJobString<T>(string uniqueID)
        {
            switch (typeof(T).Name)
            {
                case "TestRegistration":
                    return $"TestRegistration-{uniqueID}";
                default:
                    return $"Process{typeof(T).Name}-{uniqueID}";
            }
        }
        /// <summary>
        /// Chunks the list
        /// </summary>
        /// <typeparam name="T">List object type</typeparam>
        /// <param name="list">List to be chunked</param>
        /// <param name="nSize">Size of a single chunk</param>
        /// <returns></returns>
        public static IEnumerable<List<T>> SplitList<T>(List<T> list, int nSize = 30)
        {
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }

        /// <summary>
        /// Convers an RGB color to hex representation
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToHex(this Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        /// <summary>
        /// Returns the name of the enum variable based on the index and returns it.
        /// </summary>
        /// <typeparam name="T">The enum that the variable is a part of.</typeparam>
        /// <param name="enumIndex">The index that the variable is at in the enum.</param>
        /// <returns></returns>
        public static string GetEnumVariableName<T>(T enumIndex)
        {
            return Enum.GetName(typeof(T), enumIndex).Replace("_", " ");
        }

        public static string RemoveComma(this string str)
        {
            return str.Replace(",", " ");
        }

        /// <summary>
        /// Removes the symbols from the string leaving: chars,numbers,slash,colon,dot,space,at sign,hyphen
        /// </summary>
        /// <param name="str">String to be processed</param>
        /// <returns></returns>
        public static string RemoveInvalidCsvSymbols(this string str)
        {
            return Regex.Replace(str, @"[^\w\d\s\.@:\/-]", "");
        }

        /// <summary>
        /// Extracts the location based on the user's email extention
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        public static string ParseEmailAsLocation(this string email)
        {
            if (email != "" && email != null)
            {
                var extractedText = Regex.Match(email, @"@(\w+).").Groups[1].Value.ToLower();
                if (extractedText.StartsWith("randox"))
                    return "RANDOX";
                else if (extractedText == "qnostics" || extractedText == "qcmd")
                    return "QNOSTICS";
                else
                    return extractedText;
            }
            else
                return "";
        }

        /// <summary>
        /// Checks if the strings passed through match.
        /// </summary>
        /// <param name="val1">First string to be checked.</param>
        /// <param name="val2">Second string to be checked.</param>
        /// <returns>A boolean that states whether the strings match.</returns>
        public static bool Matches(this string val1, string val2)
        {
            return val1.ToUpper().Trim() == val2.ToUpper().Trim();
        }

        /// <summary>
        /// Checks if the strings passed through match.
        /// </summary>
        /// <param name="val1">First string to be checked.</param>
        /// <param name="val2">Second string to be checked.</param>
        /// <returns>A boolean that states whether the strings match.</returns>
        public static bool StartMatches(this string val1, string val2)
        {
            return val1.ToUpper().Trim().StartsWith(val2.ToUpper().Trim());
        }

        //Checks if the given barcode is in the right format for a universal barcode.
        public static bool IsUniversalBarcode(string barcode)
        {
            int barcodeTotalLength = 11;
            int barcodeCharactersLength = 3;
            int barcodeDigitsLength = barcodeTotalLength - barcodeCharactersLength;

            //If greater than the max length for the universal barcode.
            if (barcode.Length != barcodeTotalLength)
                return false;

            //Check each character to ensure that that there is not a number.
            string characters = barcode.Substring(0, barcodeCharactersLength);
            MatchCollection characterMatches = Regex.Matches(characters, "[a-zA-Z]");

            if (characterMatches == null)
                return false;

            if (characterMatches.Count != barcodeCharactersLength)
                return false;

            //Check that all of the characters entered are numbers.
            string digits = barcode.Substring(barcodeCharactersLength, barcodeDigitsLength);
            MatchCollection digitMatches = Regex.Matches(digits, "[0-9]");

            if (digitMatches == null)
                return false;

            if (digitMatches.Count != barcodeDigitsLength)
                return false;

            return true;
        }
    }
}