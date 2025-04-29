namespace _Project.Develop.Runtime.UtilTools
{
    public static class Utils
    {
        public static float GetCurrentIncome(int level, float baseIncome, float upgrade1Multiplier, float upgrade2Multiplier)
        {
            // ������� �� �������:
            // ����� = lvl * �������_����� * (1 + ���������_��_���������_1 + ���������_��_���������_2)
            return level * baseIncome * (1f + upgrade1Multiplier + upgrade2Multiplier);
        }

        public static float GetNextLevelPrice(int level, float baseCost)
        {
            // ������� �� �������:
            // ���� ������ = (lvl + 1) * �������_���������
            return (level + 1) * baseCost;
        }
    }
}