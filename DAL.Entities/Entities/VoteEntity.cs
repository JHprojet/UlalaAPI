﻿namespace DAL.Entities
{
    public class VoteEntity
    {
        public int Id { get; set; }
        public int StrategyId { get; set; }
        public int UserId { get; set; }
        public int Vote { get; set; }
        public int Active { get; set; }
    }
}