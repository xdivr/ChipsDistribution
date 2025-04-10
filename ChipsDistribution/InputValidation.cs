namespace ChipsDistribution
{
    public class InputValidation
    {
        private const int MIN_PLAYERS_CHIPS_COUNT = 0;
        private const int MIN_PLAYERS_COUNT = 2;

        public static IEnumerable<int> GetValidatedInput()
        {
            var result = new List<int>();

            bool isValidated = false;

            while (!isValidated)
            {
                result.Clear();

                Console.WriteLine("Enter chips count for players separated by commas (e.g. 0,1,2,4...)");
                string? userText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userText))
                {
                    Console.WriteLine("You entered an empty input");
                    continue;
                }

                var separatedChipsCounts = userText.Split(Constants.NUMBERS_SEPARATOR).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

                if (separatedChipsCounts.Count < MIN_PLAYERS_COUNT)
                {
                    Console.WriteLine("Count of players must be more than 1");
                    continue;
                }

                foreach (var chipsCount in separatedChipsCounts)
                {
                    if (Int32.TryParse(chipsCount, null, out int resultChipsCount))
                    {
                        if (resultChipsCount < MIN_PLAYERS_CHIPS_COUNT)
                        {
                            Console.WriteLine("Chips count can't be negative for a player");
                            break;
                        }

                        result.Add(resultChipsCount);
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect chips count entered: {chipsCount}");
                        break;
                    }
                }

                if (separatedChipsCounts.Count == result.Count)
                {
                    isValidated = true;
                    continue;
                }
            }

            return result;
        }
    }
}
