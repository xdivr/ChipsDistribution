namespace ChipsDistribution
{
    public class Distrubutor
    {
        public static int DistributeChips(List<int> chipsCounts)
        {
            var averageCount = chipsCounts.Average();

            var iterations = 0;
            while (true)
            {
                var totalSmallestNumberIndex = IterateDistribution(chipsCounts);
                iterations++;
                Console.WriteLine(string.Join(Constants.NUMBERS_SEPARATOR, chipsCounts) + " | " + $"Iteration: {iterations}");

                if (chipsCounts.All(a => a == averageCount))
                {
                    break;
                }

                IterateDistribution(chipsCounts, totalSmallestNumberIndex);
                iterations++;
                Console.WriteLine(string.Join(Constants.NUMBERS_SEPARATOR, chipsCounts) + " | " + $"Iteration: {iterations}");

                if (chipsCounts.All(a => a == averageCount))
                {
                    break;
                }
            }

            return iterations;
        }

        /// <summary>
        /// Do 1 step of distribution
        /// </summary>
        /// <param name="chipsCounts">Current chips counts list</param>
        /// <param name="ignoreIndex">Ignore smallest number index from previous iteration</param>
        /// <returns>Smallest number index</returns>
        private static int IterateDistribution(List<int> chipsCounts, int? ignoreIndex = null)
        {
            int smallestNumber = -1;
            int smallestNumberIndex = -1;

            if (!ignoreIndex.HasValue)
            {
                smallestNumber = chipsCounts.Min();
                smallestNumberIndex = chipsCounts.FindIndex(f => f == smallestNumber);
            }
            else
            {
                var restChipsCounts = chipsCounts.Where((v, i) => i != ignoreIndex).ToList();
                smallestNumber = restChipsCounts.Min();
                smallestNumberIndex = chipsCounts.FindIndex(f => f == smallestNumber);
            }

            var biggestNeighbourIndex = GetBiggestNeighbourIndex(chipsCounts, smallestNumberIndex);
            chipsCounts[smallestNumberIndex]++;
            chipsCounts[biggestNeighbourIndex]--;
            
            return smallestNumberIndex;
        }

        private static int GetBiggestNeighbourIndex(List<int> chipsCounts, int sourceIndex)
        {
            int foundIndex = -1;

            if (chipsCounts.Count == 2)
            {
                switch (sourceIndex)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 0;
                }
            }

            int leftNeighbourIndex = sourceIndex - 1;
            int rightNeighbourIndex = sourceIndex + 1;
            if (sourceIndex == 0)
            {
                leftNeighbourIndex = chipsCounts.Count - 1;
                rightNeighbourIndex = sourceIndex + 1;
            }

            if (sourceIndex == chipsCounts.Count - 1)
            {
                rightNeighbourIndex = sourceIndex - 1;
                leftNeighbourIndex = 0;
            }

            if (chipsCounts[leftNeighbourIndex] > chipsCounts[rightNeighbourIndex])
            {
                foundIndex = leftNeighbourIndex;
            }

            if (chipsCounts[leftNeighbourIndex] < chipsCounts[rightNeighbourIndex])
            {
                foundIndex = rightNeighbourIndex;
            }

            if (chipsCounts[leftNeighbourIndex] == chipsCounts[rightNeighbourIndex])
            {
                var nextLeftNeighbourIndex = GetBiggestNeighbourIndex(chipsCounts, leftNeighbourIndex);
                var nextRightNeighbourIndex = GetBiggestNeighbourIndex(chipsCounts, rightNeighbourIndex);

                if (chipsCounts[nextLeftNeighbourIndex] > chipsCounts[nextRightNeighbourIndex])
                {
                    foundIndex = leftNeighbourIndex;
                }

                if (chipsCounts[nextLeftNeighbourIndex] < chipsCounts[nextRightNeighbourIndex])
                {
                    foundIndex = rightNeighbourIndex;
                }

                if (chipsCounts[nextLeftNeighbourIndex] == chipsCounts[nextRightNeighbourIndex])
                {
                    foundIndex = rightNeighbourIndex;
                }
            }

            return foundIndex;
        }
    }
}
