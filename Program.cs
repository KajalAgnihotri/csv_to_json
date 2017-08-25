using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace population
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str;
            int a = 0, flag1 = 0, flag2 = 0, flag3 = 0, flag4 = 0;
            double sum = 0, sum1 = 0, sum2 = 0, sum3 = 0,sum4=0,sum5=0, gdp = 0, gdp1 = 0, gdp2 = 0, gdp3 = 0,gdp4=0,gdp5=0, pop_growth = 0, pow_growth = 0;
            double[] pop = new double[20];

            Dictionary<string, double> dic = new Dictionary<string, double>();
            Dictionary<string, double> dic1 = new Dictionary<string, double>();
            Dictionary<string, double> dic2 = new Dictionary<string, double>();
            Dictionary<string, Tuple<double, double>> continent = new Dictionary<string, Tuple<double, double>>();
            Dictionary<string, Tuple<double, double>> growth = new Dictionary<string, Tuple<double, double>>();

            FileStream fs = new FileStream("D://datafile.csv", FileMode.Open, FileAccess.Read);
            FileStream fs1 = new FileStream("D://Population1.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream fs2 = new FileStream("D://Purchase1.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream fs3 = new FileStream("D://GDP1.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream fs4 = new FileStream("D://Growth1.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream fs5 = new FileStream("D://Aggregate1.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamReader sr = new StreamReader(fs);
            StreamWriter sw1 = new StreamWriter(fs1);
            StreamWriter sw2 = new StreamWriter(fs2);
            StreamWriter sw3 = new StreamWriter(fs3);
            StreamWriter sw4 = new StreamWriter(fs4);
            StreamWriter sw5 = new StreamWriter(fs5);

            var head = sr.ReadLine().Split(',');
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine().Split(',');
                if (str[0].Equals("European Union")) { }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        str[i]=str[i].Replace("\"", "");
                    }
                    double element = Double.Parse(str[5]);
                    double element1 = Double.Parse(str[2]);
                    double element4 = Double.Parse(str[9]);
                    double element2 = Double.Parse(str[14]);
                    double element3 = Double.Parse(str[17]);
                    double element5 = Double.Parse(str[6]);
                    double element6 = Double.Parse(str[7]);
                    double element7 = Double.Parse(str[8]);
                    double element8 = Double.Parse(str[3]);
                    double element9 = Double.Parse(str[4]);

                    pop_growth = Math.Round(element - element1, 2);
                    pow_growth = Math.Round(element3 - element2, 2);
                    a++;
                    dic.Add(str[0], element);
                    dic1.Add(str[0], element4);
                    dic2.Add(str[0], element3);
                    growth.Add(str[0], Tuple.Create(pop_growth, pow_growth));

                    if (str[0].Equals("Argentina") || str[0].Equals("Brazil"))
                    {
                        sum = sum +( element+element1+element8+element9)/4;
                        gdp = gdp +( element4+element5+element6+element7)/4;
                        
                        flag1++;
                        if (flag1 == 2)
                            continent.Add("South America", Tuple.Create(sum, gdp));
                    }

                    if (str[0].Equals("United Kingdom") || str[0].Equals("France") || str[0].Equals("Russia") || str[0].Equals("Turkey") || str[0].Equals("Germany") || str[0].Equals("Italy"))
                    {
                        sum1 = sum1 +( element+element1 + element8 + element9)/4;
                        gdp1 = gdp1 + (element4+ element5 + element6 + element7)/4;
                        flag2++;
                        if (flag2 == 6)
                            continent.Add("Europe", Tuple.Create(sum1, element4));
                    }

                    if (str[0].Equals("China") || str[0].Equals("India") || str[0].Equals("Indonesia") || str[0].Equals("Saudi Arabia") || str[0].Equals("Republic of Korea") || str[0].Equals("Japan"))
                    {
                        sum2 = sum2 +( element+ element1 + element8 + element9)/4;
                        gdp2 = gdp2 + (element4+ element5 + element6 + element7)/4;
                        flag3++;
                        if (flag3 == 6)
                            continent.Add("Asia", Tuple.Create(sum2, gdp2));
                    }
                    if (str[0].Equals("Mexico") || str[0].Equals("Canada") || str[0].Equals("USA"))
                    {
                        sum3 = sum3 + (element+ element1 + element8 + element9)/4;
                        gdp3 = gdp3 + (element4 + element5 + element6 + element7)/4;
                        flag4++;
                        if (flag4 == 3)
                        {
                            continent.Add("North America", Tuple.Create(sum3, gdp3));
                        }

                    }
                    if (str[0].Equals("South Africa"))
                    {
                        sum4 = sum4 + (element + element1 + element8 + element9)/4;
                        gdp4 = gdp4 + (element4 + element5 + element6 + element7)/4;
                        continent.Add("Africa", Tuple.Create(sum4, gdp4));
                    }
                    if (str[0].Equals("Australia"))
                    {
                        sum5 = sum5 + (element + element1 + element8 + element9)/4;
                        gdp5 = gdp5 + (element4 + element5 + element6 + element7)/4;
                        continent.Add("Australia", Tuple.Create(sum5, gdp5));
                    }
                }
            }
            var myList = dic.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();

            var myList1 = dic1.ToList();
            myList1.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList1.Reverse();

            var myList2 = dic2.ToList();
            myList2.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList2.Reverse();

            var myList3 = growth.ToList();
            var myList4 = continent.ToList();
            sw1.WriteLine("[");
            sw2.WriteLine("[");
            sw3.WriteLine("[");
            sw4.WriteLine("[");

            for (int i = 0; i < myList.Count; i++)
            {
                var b = ",";
                if (i == myList.Count - 1)
                {
                    b = "";
                }
                sw1.WriteLine("\t" + "\n" + "{" + "\n" + "\t" + head[0] + ": " + "\"" + myList[i].Key + "\"" + "," + "\n" + "\t" + "Population2013" + ": " +  myList[i].Value + "\n" + "}" + b);
                sw2.WriteLine("\t" + "\n" + "{" + "\n" + "\t" + head[0] + ": " + "\"" + myList1[i].Key + "\"" + "," + "\n" + "\t" +"PurchasingPower2013"+ ": " + "\"" + myList1[i].Value + "\"" + "\n" + "}" + b);
                sw3.WriteLine("\t" + "\n" + "{" + "\n" + "\t" + head[0] + ": " + "\"" + myList2[i].Key + "\"" + "," + "\n" + "\t" + "GDP2013"+ ": " + "\"" + myList2[i].Value + "\"" + "\n" + "}" + b);
                sw4.WriteLine("\t" + "\n" + "{" + "\n" + "\t" + head[0] + ": " + "\"" + myList3[i].Key + "\"" + "," + "\n" + "\t" + "\"GrowthInPopulation\"" + ": " + "\"" + myList3[i].Value.Item1 + "\"" + "," + "\n" + "\t" + "\"GrowthInPurchasingPower\"" + ": " + "\"" + myList3[i].Value.Item2 + "\"" + "\n" + "}" + b);
            }
            sw1.WriteLine("]");
            sw1.Flush();
            sw2.WriteLine("]");
            sw2.Flush();
            sw3.WriteLine("]");
            sw3.Flush();
            sw4.WriteLine("]");
            sw4.Flush();
            sw5.WriteLine("[");
            for (int i = 0; i < myList4.Count; i++)
            {
                var b = ",";
                if (i == myList4.Count - 1)
                {
                    b = "";
                }
                sw5.WriteLine("\t" + "\n" + "{" + "\n" + "\t" + "\"continent\"" + ": " + "\"" + myList4[i].Key + "\"" + "," + "\n" + "\t" + "\"PopulationAggreggate\"" + ": " + "\"" + myList4[i].Value.Item1 + "\"" + "," + "\n" + "\t" + "\"GDPAggregate\"" + ": " + "\"" + myList4[i].Value.Item2 + "\"" + "\n" + "}" + b);
            }
            sw5.WriteLine("]");
            sw5.Flush();
        }
    }
}


