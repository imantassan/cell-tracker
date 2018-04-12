using System;
using System.Linq;

namespace CellTracker.Helpers
{
    internal static class RandomDataGenerator
    {
        private static readonly Random NumberGenerator = new Random((int)DateTime.Now.Ticks);

        public static int GetNumber(int? min = null, int? max = null)
        {
            return NumberGenerator.Next(min ?? 0, max ?? 10);
        }

        public static double GetNumber(double? min = null, double? max = null)
        {
            var minValue = min ?? 0d;
            var maxValue = max ?? 1d;

            return minValue + NumberGenerator.NextDouble() * (maxValue - minValue);
        }

        public static DateTimeOffset GetDate(DateTimeOffset? min = null, DateTimeOffset? max = null)
        {
            var minDate = min ?? DateTimeOffset.Now.AddDays(-365);
            var maxDate = max ?? DateTimeOffset.Now;

            var diff = (maxDate - minDate).TotalSeconds;

            return minDate + TimeSpan.FromSeconds(GetNumber(0, diff));
        }

        public static string GetText(int length = 10)
        {
            const string alphabet = "aąbcčdeęėfghiįyjklmnoprsštuųūvzžAĄBCČDEĘĖFGHIĮYJKLMNOPRSŠTŲŪVZŽ";
            return string.Join("", Enumerable.Range(0, length).Select(_ => alphabet[GetNumber(0, alphabet.Length)]));
        }

        public static string GetNumberAsText(int length = 10)
        {
            return string.Join("", Enumerable.Range(0, length).Select(_ => GetNumber(0, 10)));
        }

        public static string GetPhoneNumber(string countryCode = "370")
        {
            return countryCode + "6" + GetNumberAsText(7);
        }
    }
}