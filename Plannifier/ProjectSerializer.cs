
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Plannifier.Components.MSProject;

namespace Plannifier
{
    public class ProjectSerializer
    {
        /// <summary>
        /// Loads projects
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Components.Workspace Load(String FileName)
        {
            Components.Workspace TheWorkspace = null;

            XmlSerializer Serializer = new XmlSerializer(typeof(Components.Workspace));

            if (File.Exists(FileName))
            {
                TextReader Reader = new StreamReader(FileName);
                TheWorkspace = (Components.Workspace)Serializer.Deserialize(Reader);
                Reader.Close();
            }

            return TheWorkspace;
        }

        /// <summary>
        /// Saves projects.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public static void Save(String FileName, Components.Workspace TheWorkspace)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(Components.Workspace));

            XmlTextWriter xmlTextWriter = new XmlTextWriter(FileName, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;

            Serializer.Serialize(xmlTextWriter, TheWorkspace);
            xmlTextWriter.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TheWorkspace"></param>
        public static void Export(String FileName, Components.Workspace TheWorkspace)
        {
            if (FileName.ToLower().Contains("xml"))
            {
                ExportMPP(FileName, TheWorkspace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TheWorkspace"></param>
        public static void ExportMPP(String FileName, Components.Workspace TheWorkspace)
        {
            MPPWorkspace MPP = new MPPWorkspace(TheWorkspace);

            XmlSerializer Serializer = new XmlSerializer(typeof(MPPWorkspace));

            XmlTextWriter xmlTextWriter = new XmlTextWriter(FileName, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;

            Serializer.Serialize(xmlTextWriter, MPP);
            xmlTextWriter.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static String ReadFileAsString(String FileName)
        {
            try
            {
                System.IO.StreamReader TheFile = new System.IO.StreamReader(FileName);

                if (TheFile != null)
                {
                    String Str = TheFile.ReadToEnd();
                    TheFile.Close();
                    return Str;
                }
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }
    }
}
