﻿namespace UlalaAPI.Models
{
    public class StrategyModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public BossesZoneModel BossZone { get; set; }
        public CharactersConfigurationModel CharactersConfiguration { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public int Note { get; set; }
        public int Active { get; set; }
    }
}