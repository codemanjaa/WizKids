using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProWizKids
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("WizKids Test loading....");
            Console.Write("1. Enter the text for the Palindrome: ");
            string testString = Console.ReadLine().ToLower();

            if (checkPalindrome(testString))
            {
                Console.WriteLine("This is a palindrome");
            }
            else
            {
                Console.WriteLine("Ohh Palindrome test failed.");
            }
            pressContinue();

            Console.WriteLine("2. ");
            Console.WriteLine("-------------Print Method call -------------- ");
            printNumber();
            pressContinue();


            Console.WriteLine("3. ");
            Console.WriteLine("----------------Email finding and replacements------------------");
            string textEmail = "Christian has the email address christian+123@gmail.com." +
                                "Christian's friend, John Cave-Brown, has the email address john.cave-brown@gmail.com." +
                                "John's daughter Kira studies at Oxford University and has the email adress Kira123@oxford.co.uk." +
                                "Her Twitter handle is @kira.cavebrown.";


            List<string> extractedEmailList = ExtractEmails(textEmail);
            if(extractedEmailList != null)
            {
                // extractedEmailList.ForEach(Console.WriteLine);

               
                for (int i = 0; i < extractedEmailList.Count; i++)
                {
                    Console.WriteLine(extractedEmailList[i]);

                    if (IsValidEmail(extractedEmailList[i]))
                    {
                        Console.WriteLine("Can be replaced.");
                    }
                }

            }

            pressContinue();
        }







        // Press continue alert

        static void pressContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }


        // Palindrome checker
        static bool checkPalindrome(string myString)
        {
            string first = myString.Substring(0, myString.Length / 2);
            char[] arr = myString.ToCharArray();

            Array.Reverse(arr);

            string temp = new string(arr);
            string second = temp.Substring(0, temp.Length / 2);

            return first.Equals(second);
        }

        // print number
        static void printNumber()
        {

            int i = 1;

            for (i = 1; i<= 100; i++)
            {
                
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FooBar");
                }

                else if(i%3 == 0)
                {
                    Console.WriteLine("Foo");

                }

                else if (i % 5 == 0)
                {
                    Console.WriteLine("Bar");
                }

                else
                {
                    Console.WriteLine(i);
                }
               
            }
        }


        // Email validation and replace - Microsoft Documentation

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }


        // Extract the email from the text

        static List<string> ExtractEmails(string textToScrape)
        {
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match match;

            List<string> results = new List<string>();
            for (match = reg.Match(textToScrape); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }

            return results;
        }

        // Replace the email with new one
        static void replaceEmail(string original, string replace)
        {
            

        }

        
    }
}
