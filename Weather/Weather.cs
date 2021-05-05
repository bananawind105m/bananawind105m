// ***********************************************************************
// Assembly         : Weather
// Author           : Anthony Cox
// Created          : 05-05-2021
//
// Last Modified By : Anthony Cox
// Last Modified On : 05-05-2021
// ***********************************************************************
// <copyright file="Weather.cs" company="TLC Software.net, LLC">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
//Challenge 1
//PART ONE: Weather Data
//In the attached weather.dat file, you’ll find daily weather data for Morristown, NJ for June 2002.
//Save this text file, then write a program to output the day number (found in column one)
//with the smallest temperature spread (the maximum temperature is the second column, the minimum temperature is the third column).
namespace Weather
{
    /// <summary>
    /// Class Weather.
    /// </summary>
    public class Weather
    {
        int dayNum, holdDay;
        double Max, Min, spread, holdMax, holdMin;
        const string dayStr = "Day", maxStr = "Max", minStr = "Min", dataFile = @".\Data\Weather.txt", notNumeric = "[^0-9]";

        /// <summary>
        /// Initializes a new instance of the <see cref="Weather" /> class.
        /// </summary>
        public Weather()
        {
            dayNum = holdDay = 0;
            Max = Min = holdMax = holdMin = 0.0;
            //spread is initialized to 10000 to make sure spreadtmp is < spread initially
            spread = 10000.0;
            Console.WriteLine(getWeatherOutput());
        }

        /// <summary>
        /// Gets the weather output.
        /// </summary>
        /// <returns>System.String.</returns>
        string getWeatherOutput()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                //read in the file
                StreamReader sr = new StreamReader(dataFile);
                String WeatherDat = sr.ReadToEnd();
                sr.Close();

                //split the file into lines
                string[] WeatherSplit = WeatherDat.Split('\n');

#if DEBUG
                sb.AppendFormat("{0,5} {1,5} {2,5} {3,8}{4}", dayStr, maxStr, minStr, "Spread", System.Environment.NewLine);
#endif

                foreach (string weatherLine in WeatherSplit)
                {
                    if (string.IsNullOrEmpty(weatherLine.Trim()))
                    {
#if DEBUG
                        sb.AppendFormat($"Skipping empty line...{System.Environment.NewLine}");
#endif
                        continue;
                    }

                    string[] weatherLineSplit = Regex.Replace(weatherLine.Trim(), "\\s+", " ").Split(' ');
                    if (int.TryParse(weatherLineSplit[0].Trim(), out dayNum))
                    {
                        //true we have a #
                        //not a header or blank line
                        if (!double.TryParse(Regex.Replace(weatherLineSplit[1].Trim(), notNumeric, ""), out Max))
                        {
                            //error weatherLineSplit[1] is ! a #
                            sb.AppendFormat($"Error Max Temp '{weatherLineSplit[1]}' is not a number{System.Environment.NewLine}");
                        }

                        if (!double.TryParse(Regex.Replace(weatherLineSplit[2].Trim(), notNumeric, ""), out Min))
                        {
                            //error weatherLineSplit[2] is ! a #
                            sb.AppendFormat($"Error Min temp '{weatherLineSplit[2]}' is not a number{System.Environment.NewLine}");
                        }

                        //ok we have a max and a min
                        double spreadTmp = Math.Abs(Max - Min);
                        if (spreadTmp < spread)
                        {
                            spread = spreadTmp;
                            holdDay = dayNum;
                            holdMax = Max;
                            holdMin = Min;
                        }

#if DEBUG
                        sb.AppendFormat("{0,5} {1,5} {2,5} {3,8}{4}", dayNum, Max, Min, spreadTmp, System.Environment.NewLine);
#endif
                    }

                }

#if DEBUG
                sb.AppendFormat("{0}{0}", System.Environment.NewLine);
#endif

                sb.AppendFormat("{0,5} {1,5} {2,5} {3, 12}{4}{5,5} {6,5} {7,5} {8,12}",
                    dayStr, maxStr, minStr, "Min Spread", System.Environment.NewLine, holdDay, holdMax, holdMin, spread);
            }
            catch (Exception ex)
            {
                sb.AppendFormat($"Error, ex.message = {ex.Message}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the minimum spread.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double getMinSpread()
        {
            return spread;
        }
    }
}
