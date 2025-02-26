public static class InfinityListHelper
{
    public static int GetIndex(int index, int restartIndex, int totalCount)
    {
        if (index <= 0)
        {
            return 0;
        }
        if (index <= restartIndex) return index;
        index -= restartIndex;
        var repeatedLevelCount = totalCount - restartIndex;
        var levelRemainder = index % repeatedLevelCount;
        var levelIndex = levelRemainder + restartIndex;
        return levelIndex;
    }
}