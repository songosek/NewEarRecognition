using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace EarRecognition
{
    static class DataAccessLayer
    {
        public static string databasePath;
        private const string defaultDatabasePath = @"defaultDatabase.xml";
        private const string template = @"template.xml";

        public static bool AddPerson(Person person)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(databasePath ?? defaultDatabasePath);
                var rootNode = xDoc.GetElementsByTagName("people")[0];
                var nav = rootNode.CreateNavigator();
                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                using (var writer = nav.AppendChild())
                {
                    var serializer = new XmlSerializer(person.GetType());
                    writer.WriteWhitespace("");
                    serializer.Serialize(writer, person, emptyNamepsaces);
                    writer.Close();

                }
                xDoc.Save(databasePath ?? defaultDatabasePath);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public static Person FindPerson(Person person)
        {
            People people; 
            using (StreamReader reader = new StreamReader(databasePath ?? defaultDatabasePath))
            {                XmlSerializer deserializer = new XmlSerializer(typeof(People));
                people = deserializer.Deserialize(reader) as People;
            }
            if (people.people.Count < 1)
                throw new ArgumentException();

            List<double> errors = new List<double>();
            for(int i = 0; i < people.people.Count; i++)
            {
                errors.Add(Math.Sqrt(Math.Pow((people.people[i].earHeight - person.earHeight), 2)
                    + Math.Pow((people.people[i].earWidth - person.earWidth), 2)
                    + Math.Pow((people.people[i].darkPixelsCount - person.darkPixelsCount), 2)));
            }

            double minError = double.MaxValue;
            int minErrorIndex = -1;
            for(int i = 0; i < errors.Count; i++)
            {
                if (errors[i] < minError)
                {
                    minErrorIndex = i;
                    minError = errors[i];
                }
            }
            person.name = people.people[minErrorIndex].name;
            person.surname = people.people[minErrorIndex].surname;

            return person;
        }

        public static void CreateNewDatabase(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(template);

            XmlTextWriter writer = new XmlTextWriter(path, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
        }
    }
}
