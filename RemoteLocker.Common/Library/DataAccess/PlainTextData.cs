using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemoteLocker.Common.Library.DataAccess
{
    /// <summary>
    /// Proccess plain text data
    /// </summary>
    public class PlainTextData
    {
        /// <summary>
        /// Fetch data from file and extract data with specific Sperator
        /// </summary>
        /// <param name="Filename">File's path</param>
        /// <param name="Sperator">Sperator for extract data</param>
        /// <returns></returns>
        public static String[] FetchFrom(String Filename, Char Sperator)
        {
            String[] arrData = null;

            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sReader = new StreamReader(fs);
                    String tmpData = sReader.ReadLine();
                    arrData = tmpData.Split(Sperator);

                    sReader.Close();
                    sReader.Dispose();
                }
            }
            catch (IOException)
            {
                return null;
            }

            return arrData;
        }

        /// <summary>
        /// Save data (sperator by 'Sperator' character) to file
        /// </summary>
        /// <param name="Filename">File's path</param>
        /// <param name="Sperator">Sperator charactor</param>
        /// <param name="Values">Values for saving</param>
        /// <returns></returns>
        public static bool SaveTo(String Filename, Char Sperator, params String[] Values)
        {
            try
            {
                String data = Values[0];

                for (int i = 1; i < Values.Length; i++)
                    data += Sperator.ToString() + Values[i];

                using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter sWriter = new StreamWriter(fs);
                    sWriter.WriteLine(data);

                    sWriter.Close();
                    sWriter.Dispose();

                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
