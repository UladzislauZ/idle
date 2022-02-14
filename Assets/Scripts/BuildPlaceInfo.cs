using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [Serializable]
    public class BuildPlaceInfo
    {
        private int levelBuild;

        private int levelCountGive;

        private int levelCostUpgrade;

        public int GetlevelBuild() => levelBuild;

        public int GetlevelCountGive() => levelCountGive;

        public int GetlevelCostUpgrade() => levelCostUpgrade;

        public BuildPlaceInfo()
        {
            levelBuild = 0;
            levelCountGive = 5;
            levelCostUpgrade = 20;
        }

        public void SetInfo(int levelBuild, int levelCountGive, int levelCostUpgrade)
        {
            this.levelBuild = levelBuild;
            this.levelCountGive = levelCountGive;
            this.levelCostUpgrade = levelCostUpgrade;
        }

        public void Upgrade()
        {
            levelBuild++;
            levelCountGive += levelCountGive;
            levelCostUpgrade *= levelCostUpgrade;
            //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.ParameterLevel);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("Upgrade build");
        }
    }
}
