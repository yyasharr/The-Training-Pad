using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class LogSystem
    {
        Dictionary<int, string> TimeStamps;
        Dictionary<int, List<int>> Year;
        Dictionary<int, List<int>> Month;
        Dictionary<int, List<int>> Day;
        Dictionary<int, List<int>> Hour;
        Dictionary<int, List<int>> Minute;
        Dictionary<int, List<int>> Second;



        public LogSystem()
        {
            TimeStamps = new Dictionary<int, string>();
            Year = new Dictionary<int, List<int>>();
            Month = new Dictionary<int, List<int>>();
            Day = new Dictionary<int, List<int>>();
            Hour = new Dictionary<int, List<int>>();
            Minute = new Dictionary<int, List<int>>();
            Second = new Dictionary<int, List<int>>();
        }

        public void Put(int id, string timestamp)
        {
            string[] split = timestamp.Split(':');
            TimeStamps.Add(id, timestamp);
            AddtoMap(Year, id, int.Parse(split[0]));
            AddtoMap(Month, id, int.Parse(split[1]));
            AddtoMap(Day, id, int.Parse(split[2]));
            AddtoMap(Hour, id, int.Parse(split[3]));
            AddtoMap(Minute, id, int.Parse(split[4]));
            AddtoMap(Second, id, int.Parse(split[5]));
        }
        void AddtoMap(Dictionary<int, List<int>> dict, int ID, int Value)
        {
            if (!dict.ContainsKey(Value))
            {
                dict[Value] = new List<int>();
            }
            dict[Value].Add(ID);
        }
        public IList<int> Retrieve(string s, string e, string gra)
        {
            string[] StartTime = s.Split(':');
            string[] EndTime = e.Split(':');

            int precision = GranularitytoInt(gra);
            HashSet<int> PreviousList = new HashSet<int>();

            for (int i = 0; i <= precision; i++)
            {
                Dictionary<int, List<int>> CurrentDict = GetDicfromInt(i);
                HashSet<int> CurrentList = new HashSet<int>();
                for (int j = int.Parse(StartTime[i]); j <= int.Parse(EndTime[i]); j++)
                {
                    if(CurrentDict.ContainsKey(j))
                    {
                        foreach(int id in CurrentDict[j])
                        {
                            if (i==0 || PreviousList.Contains(id))
                                CurrentList.Add(id);
                        }
                    }
                }
                PreviousList = CurrentList;
            }
            return new List<int>(PreviousList);
        }

        private Dictionary<int, List<int>> GetDicfromInt(int i)
        {
            switch (i)
            {
                case 0:
                    return Year;
                case 1:
                    return Month;
                case 2:
                    return Day;
                case 3:
                    return Hour;
                case 4:
                    return Minute;
                case 5:
                    return Second;
                default:
                    break;
            }
            return new Dictionary<int, List<int>>();
        }

        private int GranularitytoInt(string gra)
        {
            switch (gra)
            {
                case "Year":
                    return 0;
                case "Month":
                    return 1;
                case "Day":
                    return 2;
                case "Hour":
                    return 3;
                case "Minute":
                    return 4;
                case "Second":
                    return 5;
                default:
                    break;
            }
            return 0;
        }
    }
}
