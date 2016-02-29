using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Kinect;

namespace Badminton_SW_v1._0
{
    class FileManipulation
    {
        static String fileLocation = "Recordings/skeletonData.mot1";
        static double angle;
        static string line = null;
        static string nextLine = null;

        public static void serialize(List<Skeleton> skel, Stream stream)
        {
            try
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                //stream = new FileStream(fileLocation, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                bFormatter.Serialize(stream, skel);

            }

            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

        }

        public static List<Skeleton> diserialise()
        {
            List<Skeleton> skeletonMotion = null;
            Stream stream = null;

            try
            {
                skeletonMotion = new List<Skeleton>();
                BinaryFormatter bFormatter = new BinaryFormatter();
                stream = File.Open(fileLocation, FileMode.Open);
                skeletonMotion = (List<Skeleton>)bFormatter.Deserialize(stream);
            }

            catch (Exception ex)
            {
                ex.ToString();
            }
            if (stream != null)
            {
                stream.Close();
            }
            return skeletonMotion;
        }

        public static void writeToTextFile(double angle, StreamWriter sw)
        {
            try
            {
                sw.WriteLine(angle);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }


        public static double readAgleFromFile(StreamReader str)
        {
            try
            {
                line = str.ReadLine();
                nextLine = str.ReadLine();

                while (line != null)
                {
                    line = nextLine;
                    angle = Double.Parse(line);
                    nextLine = str.ReadLine();
                }

            }
            catch (IOException exp)
            {
                exp.ToString();
            }

            return angle;
        }


    }
}