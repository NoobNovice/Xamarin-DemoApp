using System;
using SQLite;

namespace SaveMan.Models
{
    public class TransactionModel
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string Date { get; set; }

        public float TotalCost { get; set; }
    }
}
