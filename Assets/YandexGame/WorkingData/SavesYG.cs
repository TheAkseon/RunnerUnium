
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Coins;
        public int Score;
        public int CurrentLevel;
        public int FakeLevel;
        public bool muteMusic;
        public bool muteEffects;
        public int CostOfDamageImprovements;
        public int CostOfFiringRateImprovements;
        public int BaseDamage;
        public float BaseFiringRate;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            Coins = 0;
            CurrentLevel = 1; 
            FakeLevel = 1;
            muteMusic = false;
            muteEffects = false;
            CostOfDamageImprovements = 10;
            CostOfFiringRateImprovements = 20;
            BaseDamage = 1;
            BaseFiringRate = 1;
        }
    }
}
