using System;
using SQLite;

namespace SaveMan.Models
{
    public class TransactionLogModel
    {
        [PrimaryKey]
        public string LogID { get; set; }

        public float Cost { get; set; }

        public string ShortNote { get; set; }

        public string ReferenceDate { get; set; }

        public int TagID { get; set; }

        //public TransactionLogModel(string logID, float cost, string shortNote, string refDate, int tagID)
        //{
        //    LogID = logID;
        //    Cost = cost;
        //    ShortNote = shortNote;
        //    ReferenceDate = refDate;
        //    TagID = tagID;
        //}
    }
}
