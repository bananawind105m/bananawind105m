using System;
using System.IO;
using System.Text.RegularExpressions;
//Challenge 1
//PART ONE: Weather Data
//In the attached weather.dat file, you’ll find daily weather data for Morristown, NJ for June 2002.
//Save this text file, then write a program to output the day number (found in column one)
//with the smallest temperature spread (the maximum temperature is the second column, the minimum temperature is the third column).
namespace Weather
{
    public class Weather
    {
        public Weather()
        {
            int dayNum = 0, holdDay = 0;
            //spread is initialized to 10000 to make sure spreadtmp is < spread initially
            double Max = 0.0, Min = 0.0, spread = 10000.0, holdMax = 0.0, holdMin = 0.0;
            string dayStr = "Day", maxStr = "Max", minStr = "Min";

            try 
            {
                //read in the file
                StreamReader sr = new StreamReader(@".\Data\Weather.txt");
                String WeatherDat = sr.ReadToEnd();

                string[] WeatherSplit = WeatherDat.Split('\n');

#if DEBUG
                Console.WriteLine("{0,5} {1,5} {2,5} {3,8}", dayStr, maxStr, minStr, "Spread");
#endif

                foreach(string weatherLine in WeatherSplit)
                {
                    if(string.IsNullOrEmpty(weatherLine.Trim()))
                    {
#if DEBUG
                        Console.WriteLine("Skipping empty line...");
#endif
                        continue;
                    }

                    string [] weatherLineSplit = Regex.Replace(weatherLine.Trim(), "\\s+", " ").Split(' ');
                    if(int.TryParse(weatherLineSplit[0].Trim(),out dayNum))
                    {
                        //true we have a #
                        //not a header or blank line
                        if(!double.TryParse(Regex.Replace(weatherLineSplit[1].Trim(), "[^0-9]", ""), out Max))
                        {
                            //error weatherLineSplit[1] is ! a #
                            Console.WriteLine($"Error Max Temp '{weatherLineSplit[1]}' is not a number");
                        }

                        if(!double.TryParse(Regex.Replace(weatherLineSplit[2].Trim(), "[^0-9]", ""), out Min))
                        {
                            //error weatherLineSplit[2] is ! a #
                            Console.WriteLine($"Error Min temp '{weatherLineSplit[2]}' is not a number"); 
                        }

                        //ok we have a max and a min
                        double spreadTmp = Math.Abs(Max - Min);
                        if(spreadTmp<spread)
                        {
                            spread = spreadTmp;
                            holdDay = dayNum;
                            holdMax = Max;
                            holdMin = Min;
                        }

#if DEBUG
                        Console.WriteLine("{0,5} {1,5} {2,5} {3,8}", dayNum, Max, Min, spreadTmp);
#endif
                    }

                }

#if DEBUG
                Console.WriteLine("{0}{0}", System.Environment.NewLine);
#endif

                Console.WriteLine("{0,5} {1,5} {2,5} {3, 12}{4}{5,5} {6,5} {7,5} {8,12}",
                    dayStr, maxStr, minStr, "Min Spread", System.Environment.NewLine, holdDay, holdMax, holdMin, spread);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error, ex.message = {ex.Message}");
            }
        }
    }
}
