namespace _Project.Develop.Runtime.UtilTools
{
    public static class Utils
    {
        public static float GetCurrentIncome(int level, float baseIncome, float upgrade1Multiplier, float upgrade2Multiplier)
        {
            // Формула из задания:
            // Доход = lvl * базовый_доход * (1 + множитель_от_улучшения_1 + множитель_от_улучшения_2)
            return level * baseIncome * (1f + upgrade1Multiplier + upgrade2Multiplier);
        }

        public static float GetNextLevelPrice(int level, float baseCost)
        {
            // Формула из задания:
            // Цена уровня = (lvl + 1) * базовая_стоимость
            return (level + 1) * baseCost;
        }
    }
}