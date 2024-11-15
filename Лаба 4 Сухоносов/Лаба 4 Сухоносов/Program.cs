using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class Tasks
{
    // Задача 1: Проверка наличия хотя бы двух одинаковых элементов в списке
    public static bool HasDuplicates(List<int> list)
    {
        HashSet<int> set = new HashSet<int>();
        foreach (int item in list)
        {
            if (!set.Add(item))
            {
                return true;
            }
        }
        return false;
    }

    // Задача 2: Удаление первого вхождения заданного элемента в списке
    public static void RemoveFirstOccurrence(LinkedList<int> list, int element)
    {
        var node = list.First;
        while (node != null)
        {
            if (node.Value == element)
            {
                list.Remove(node);
                break;
            }
            node = node.Next;
        }
    }

    // Задача 3: Определение популярности музыкальных произведений
    public static void MusicPreferences(Dictionary<string, HashSet<string>> preferences)
    {
        HashSet<string> allLiked = new HashSet<string>();
        HashSet<string> someLiked = new HashSet<string>();
        HashSet<string> noneLiked = new HashSet<string>();

        // Найдем все песни, которые нравятся хотя бы одному меломану
        HashSet<string> allSongs = new HashSet<string>();
        foreach (var entry in preferences)
        {
            allSongs.UnionWith(entry.Value);
        }

        // Проверим, какие песни нравятся всем, некоторым и никому
        foreach (var song in allSongs)
        {
            bool likedByAll = true;
            bool likedBySome = false;

            foreach (var entry in preferences)
            {
                if (entry.Value.Contains(song))
                {
                    likedBySome = true;
                }
                else
                {
                    likedByAll = false;
                }
            }

            if (likedByAll)
            {
                allLiked.Add(song);
            }
            else if (likedBySome)
            {
                someLiked.Add(song);
            }
        }

        // Найдем все возможные песни
        HashSet<string> allPossibleSongs = new HashSet<string> { "Song1", "Song2", "Song3", "Song4", "Song5", "Song6", "Song7", "Song8", "Song9", "Song10" };

        // Определим песни, которые не нравятся никому
        noneLiked = new HashSet<string>(allPossibleSongs);
        noneLiked.ExceptWith(allSongs);

        Console.WriteLine("Нравится всем: " + string.Join(", ", allLiked));
        Console.WriteLine("Нравится некоторым: " + string.Join(", ", someLiked));
        Console.WriteLine("Нравится никому: " + string.Join(", ", noneLiked));
    }

    // Задача 4: Напечатать все гласные буквы, которые не входят более чем в одно слово
    public static void PrintUniqueVowels(string text)
    {
        HashSet<char> vowels = new HashSet<char> { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
        Dictionary<char, int> vowelCount = new Dictionary<char, int>();

        // Преобразование текста в нижний регистр
        text = text.ToLower();

        foreach (var word in text.Split(' ', '\n', '\r', '\t'))
        {
            HashSet<char> wordVowels = new HashSet<char>(word.Where(c => vowels.Contains(c)));
            foreach (var vowel in wordVowels)
            {
                if (vowelCount.ContainsKey(vowel))
                {
                    vowelCount[vowel]++;
                }
                else
                {
                    vowelCount[vowel] = 1;
                }
            }
        }

        var uniqueVowels = vowelCount.Where(pair => pair.Value == 1).Select(pair => pair.Key).OrderBy(v => v);
        Console.WriteLine("уникальные гласные: " + string.Join(", ", uniqueVowels));
    }


    // Задача 5: Определение лучшего участника олимпиады
    public static void BestOlympiadParticipant(string filePath)
    {
        var participants = ReadParticipantsFromFile(filePath);
        var sortedParticipants = new SortedList<int, List<Participant>>();

        foreach (var participant in participants)
        {
            if (!sortedParticipants.ContainsKey(participant.Score))
            {
                sortedParticipants[participant.Score] = new List<Participant>();
            }
            sortedParticipants[participant.Score].Add(participant);
        }

        int maxScore = sortedParticipants.Keys.Last();
        int winnerCount = sortedParticipants[maxScore].Count;

        if (maxScore > 200 && winnerCount <= participants.Count * 0.2)
        {
            // Найти лучшего участника, который не стал победителем
            var bestNonWinners = sortedParticipants.Where(p => p.Key < maxScore).SelectMany(p => p.Value).ToList();
            if (bestNonWinners.Count == 1)
            {
                Console.WriteLine($"{bestNonWinners[0].LastName} {bestNonWinners[0].FirstName}");
            }
            else
            {
                int bestNonWinnerScore = sortedParticipants.Keys.Reverse().Skip(1).First();
                int bestNonWinnerCount = sortedParticipants[bestNonWinnerScore].Count;
                Console.WriteLine(bestNonWinnerCount);
            }
        }
        else
        {
            // Найти лучших участников, если победителей нет
            int bestScore = sortedParticipants.Keys.Last();
            int bestCount = sortedParticipants[bestScore].Count;
            if (bestCount == 1)
            {
                Console.WriteLine($"{sortedParticipants[bestScore][0].LastName} {sortedParticipants[bestScore][0].FirstName}");
            }
            else
            {
                Console.WriteLine(bestCount);
            }
        }
    }

    public static void WriteParticipantsToFile(string filePath, List<Participant> participants)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Participant>));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, participants);
        }
    }

    public static List<Participant> ReadParticipantsFromFile(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Participant>));
        using (StreamReader reader = new StreamReader(filePath))
        {
            return (List<Participant>)serializer.Deserialize(reader);
        }
    }
}

[XmlRoot("Participants")]
public class Participant
{
    [XmlElement("LastName")]
    public string LastName { get; set; }

    [XmlElement("FirstName")]
    public string FirstName { get; set; }

    [XmlElement("Grade")]
    public int Grade { get; set; }

    [XmlElement("Score")]
    public int Score { get; set; }

    public Participant() { }

    public Participant(string lastName, string firstName, int grade, int score)
    {
        LastName = lastName;
        FirstName = firstName;
        Grade = grade;
        Score = score;
    }

    public static List<Participant> GenerateRandomParticipants(int count)
    {
        List<Participant> participants = new List<Participant>();
        Random random = new Random();
        string[] lastNames = { "Семенов", "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Попов", "Соколов" };
        string[] firstNames = { "Егор", "Иван", "Петр", "Сидор", "Алексей", "Дмитрий", "Сергей", "Андрей" };

        for (int i = 0; i < count; i++)
        {
            string lastName = lastNames[random.Next(lastNames.Length)];
            string firstName = firstNames[random.Next(firstNames.Length)];
            int grade = random.Next(1, 12); // Генерация класса от 1 до 11
            int score = random.Next(100, 301); // Генерация баллов от 100 до 300
            participants.Add(new Participant(lastName, firstName, grade, score));
        }

        return participants;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Задача 1
        Console.WriteLine("1 задание");
        Console.Write("Введите количество элементов в списке: ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            Random random = new Random();
            List<int> list = new List<int>();

            // Заполнение списка случайными числами
            for (int i = 0; i < count; i++)
            {
                list.Add(random.Next(1, 11)); // Генерация чисел от 1 до 10
            }

            Console.WriteLine("Список: " + string.Join(", ", list));
            Console.WriteLine("Есть дубликаты: " + Tasks.HasDuplicates(list));
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
        }
        Console.WriteLine();

        // Задача 2
        Console.WriteLine("2 задание");
        Console.Write("Введите количество элементов в списке: ");
        if (int.TryParse(Console.ReadLine(), out int c) && c > 0)
        {
            Random random = new Random();
            LinkedList<int> linkedList = new LinkedList<int>();

            // Заполнение списка случайными числами
            for (int i = 0; i < c; i++)
            {
                linkedList.AddLast(random.Next(1, 11)); // Генерация чисел от 1 до 10
            }

            Console.WriteLine("Исходный список: " + string.Join(", ", linkedList));

            Console.Write("Введите значение для удаления первого вхождения: ");
            if (int.TryParse(Console.ReadLine(), out int valueToRemove))
            {
                Tasks.RemoveFirstOccurrence(linkedList, valueToRemove);
                Console.WriteLine("Список после удаления: " + string.Join(", ", linkedList));
            }
            else
            {
                Console.WriteLine("Некорректный ввод значения для удаления.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
        }
        Console.WriteLine();

        // Задача 3
        Console.WriteLine("3 задание");
        Random random1 = new Random();
        Dictionary<string, HashSet<string>> preferences = new Dictionary<string, HashSet<string>>();

        // Список возможных имен меломанов и песен
        string[] melomanNames = { "Meloman1", "Meloman2" };
        string[] songNames = { "Song1", "Song2", "Song3", "Song4", "Song5", "Song6", "Song7", "Song8", "Song9", "Song10" };

        // Заполнение словаря случайными данными
        for (int i = 0; i < melomanNames.Length; i++)
        {
            HashSet<string> songs = new HashSet<string>();
            int numberOfSongs = random1.Next(1, songNames.Length + 1); // Количество песен для каждого меломана

            for (int j = 0; j < numberOfSongs; j++)
            {
                string randomSong = songNames[random1.Next(songNames.Length)];
                songs.Add(randomSong);
            }

            preferences[melomanNames[i]] = songs;
        }

        Console.WriteLine("Музыкальные предпочтения:");
        Tasks.MusicPreferences(preferences);
        Console.WriteLine();

        // Задача 4
        Console.WriteLine("4 задание");

        string filename = "text.txt";
        string text = "";

        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                text += line + Environment.NewLine;
            }
        }

        Tasks.PrintUniqueVowels(text);
        Console.WriteLine();

        // Задача 5
        Console.WriteLine("5 задание");
        string filePath = "participants.xml";
        List<Participant> participants = new List<Participant>
        {
            new Participant("Семенов", "Егор", 11, 225),
            new Participant("Иванов", "Иван", 10, 230),
            new Participant("Петров", "Петр", 9, 210),
            new Participant("Сидоров", "Сидор", 11, 225)
        };
        Tasks.WriteParticipantsToFile(filePath, participants);
        Tasks.BestOlympiadParticipant(filePath);

        Console.ReadKey();
    }

    private static void OpenRead(string v)
    {
        throw new NotImplementedException();
    }
}
