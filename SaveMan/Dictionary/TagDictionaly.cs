using System;
using System.Collections.Generic;

namespace SaveMan.Dictionary
{
    public static class TagDictionaly
    {
        private static Dictionary<string, int> _tagDictionary;

        public static Dictionary<string, int> GetDic()
        {
            _tagDictionary = new Dictionary<string, int>();
            _tagDictionary.Add("Food", 1);
            _tagDictionary.Add("Transport", 2);
            _tagDictionary.Add("Shopping", 3);
            _tagDictionary.Add("Entertainment", 4);
            _tagDictionary.Add("Travel", 5);
            _tagDictionary.Add("Learning", 6);
            _tagDictionary.Add("Home/Apartment", 7);
            _tagDictionary.Add("Insurance", 8);
            _tagDictionary.Add("Medical", 9);
            _tagDictionary.Add("Income", 10);
            return _tagDictionary;
        }
    }
}
