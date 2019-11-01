using System;

using SQLite;

namespace SaveMan.Models
{
    public class HistoryMountModel
    {
        [PrimaryKey]
        public string HistoryID { get; set; }

        public string Mount { get; set; }

        public string Year { get; set; }

        public float Payment { get; set; }

        public float Income { get; set; }

        public float costTag1 { get; set; }

        public float costTag2 { get; set; }

        public float costTag3 { get; set; }

        public float costTag4 { get; set; }

        public float costTag5 { get; set; }

        public float costTag6 { get; set; }

        public float costTag7 { get; set; }

        public float costTag8 { get; set; }

        public float costTag9 { get; set; }

        public HistoryMountModel()
        {
            HistoryID = DateTime.Now.ToString("MMyyyy");
            Mount = DateTime.Now.ToString("MMM");
            Year = DateTime.Now.ToString("yyyy");
            Payment = 0;
            Income = 0;
            costTag1 = 0;
            costTag2 = 0;
            costTag3 = 0;
            costTag4 = 0;
            costTag5 = 0;
            costTag6 = 0;
            costTag7 = 0;
            costTag8 = 0;
            costTag9 = 0;
        }

        public HistoryMountModel(string historyID, string mountStr, string yearStr)
        {
            HistoryID = historyID;
            Mount = mountStr;
            Year = yearStr;
            Payment = 0;
            Income = 0;
            costTag1 = 0;
            costTag2 = 0;
            costTag3 = 0;
            costTag4 = 0;
            costTag5 = 0;
            costTag6 = 0;
            costTag7 = 0;
            costTag8 = 0;
            costTag9 = 0;
        }
    }
}
