using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }
        // Получаем слово из пользовательского ввода
        Console.Write("Введите слово: ");
        string? input = Console.ReadLine()?.ToLower();
        // Загружаем словарь из файла
        List<string> dictionary = File.ReadAllLines("russian_nouns.txt").ToList();
        var stopwatch = new Stopwatch();
        // Начинаем отсчет времени выполнения
        stopwatch.Start();
        // Находим слова из словаря, которые можно составить из букв данного слова
        List<string> matchingWords = new();
        foreach (string word in dictionary)
        {
            if (CanFormWord(input, word))
            {
                matchingWords.Add(word);
            }
        }
        // Сортируем найденные слова по убыванию длины
        matchingWords.Sort((a, b) => b.Length - a.Length);
        // Стоп отсчет времени выполнения
        stopwatch.Stop();
        // Выводим найденные слова
        Console.WriteLine("Найденные слова:");
        foreach (string word in matchingWords)
        {
            Console.WriteLine(word);
        }
        // Выводим время выполнения
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} миллисекунд");
    }
    // Проверяем можно ли составить слово word из букв слова letters
    static bool CanFormWord(string letters, string word)
    {
        foreach (char c in word)
        {
            int index = letters.IndexOf(c);
            if (index == -1)
            {
                return false;
            }
            letters = letters.Remove(index, 1);
        }
        return true;
    }
}