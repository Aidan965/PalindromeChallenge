using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PalindromeChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cachedPalindromes = CreateCacheFromFile();

            string exit = "";
            while(exit != "1")
            {
                Console.WriteLine("Please enter your name:");
                string username = Console.ReadLine();
                Console.WriteLine("Please a word you want to check:");
                string word = Console.ReadLine();

                IsPalindrome(username, word, cachedPalindromes);

                Console.WriteLine("Please input 1 to exit, or anything else to continue:");
                exit = Console.ReadLine();
            }
            Console.WriteLine("Application ended by user input.");
        }

        public static bool IsPalindrome(string username, string word, List<string> cache, bool expected = false)
        {
            word = word.ToLower();
            var reversedWord = ReverseWord(word);
            var palindromeWords = cache;

            if (palindromeWords.Contains(word))
            {
                Console.WriteLine($"{username} input {word} is a palindrome, and exists in the cache.");
                return true;
            }
            else if (!word.Any(c => char.IsLetter(c)))
            {
                Console.WriteLine($"{username} input {word} is not a palindrome - must only contain letters, no spaces or punctuation allowed.");
                return false;
            }
            else if (word == reversedWord)
            {
                Console.WriteLine($"{word} is a palindrome.");
                
                if (!palindromeWords.Contains(word))
                {
                    Console.WriteLine($"Adding {word} to cache.");
                    palindromeWords.Add(word);
                }
                if (!WordFoundInFile(word))
                {
                    Console.WriteLine($"Adding {word} to file.");
                    File.AppendAllText("words.txt", "\n");
                    File.AppendAllText("words.txt", word);
                }
                return true;
            }
            else
            {
                Console.WriteLine($"{username} input {word} is not a palindrome.");
                return false;
            }
        }

        private static bool WordFoundInFile(string palindrome)
        {
            using var fileStream = File.OpenRead("words.txt");
            using var streamReader = new StreamReader(fileStream);
            string word;

            while ((word = streamReader.ReadLine()) != null)
            {
                word = word.Trim();

                if (word == palindrome)
                {
                    return true;
                }
            }
            return false;
        }

        private static string ReverseWord(string word)
        {
            var wordLetters = word.ToCharArray();
            Array.Reverse(wordLetters);
            return new string(wordLetters);
        }

        private static List<string> CreateCacheFromFile()
        {
            var wordsCache = new List<string>();
            using var fileStream = File.OpenRead("words.txt");
            using var streamReader = new StreamReader(fileStream);
            string word;

            while ((word = streamReader.ReadLine()) != null)
            {
                word = word.Trim();
                wordsCache.Add(word);
            }
            return wordsCache;
        }
    }
}
