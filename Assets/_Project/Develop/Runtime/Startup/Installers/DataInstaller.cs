using System.Collections.Generic;
using _Project.Develop.Runtime.Data.Configs;
using _Project.Develop.Runtime.Domain.BusinessFeature.Models;

namespace _Project.Develop.Runtime.Startup.Installers
{
    public static class DataInstaller
    {
        public static BusinessInitData[] PrepareBusinessInitData(BusinessesDB businessesDB, BusinessNamesDB businessesNamesDB)
        {
            var list = new List<BusinessInitData>();

            for (int i = 0; i < businessesDB.Businesses.Length; i++)
            {
                var businessModel = businessesDB.Businesses[i];

                var upgrade1 = new UpgradeInitData
                {
                    UpgradeName = businessesNamesDB.GetUpgradeNameByKey(businessModel.Upgrade1.UpgradeKey),
                    Cost = businessModel.Upgrade1.Cost,
                    IncomeMultiplier = businessModel.Upgrade1.IncomeMultiplier
                };

                var upgrade2 = new UpgradeInitData
                {
                    UpgradeName = businessesNamesDB.GetUpgradeNameByKey(businessModel.Upgrade2.UpgradeKey),
                    Cost = businessModel.Upgrade2.Cost,
                    IncomeMultiplier = businessModel.Upgrade2.IncomeMultiplier
                };

                list.Add(new BusinessInitData
                {
                    BusinessName = businessesNamesDB.GetBusinessNameByKey(businessModel.BusinessKey),
                    BusinessId = i,
                    BaseIncome = businessModel.BaseIncome,
                    BaseCost = businessModel.BaseCost,
                    IncomeDelay = businessModel.IncomeDelay,
                    Upgrade1 = upgrade1,
                    Upgrade2 = upgrade2
                });
            }

            return list.ToArray();
        }
    }
}