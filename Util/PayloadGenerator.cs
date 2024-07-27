using System;
using System.Linq;

namespace Automation_task.Util
{
    public static class PayloadGenerator
    {
        public static string GenerateEmail()
        {
            string[] domains = { "example.com", "test.com", "sample.org" };
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            string username = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            string domain = domains[random.Next(domains.Length)];

            return $"{username}@{domain}";
        }
    }
}