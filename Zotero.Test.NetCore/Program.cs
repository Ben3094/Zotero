using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Zotero;
using Zotero.Connections;

namespace Zotero.Test.NetCore
{
    class Program
    {
        //public class CreatorLink
        //{
        //    public Creator creator1;
        //    public Creator creator2;

        //    public int Occurences = 1;

        //    public bool Contains(Creator creator)
        //    {
        //        return (creator == creator1) || (creator == creator2);
        //    }
        //}

        const string DEFAULT_ZOTERO_SQLITE_STORAGE_PATH = @"C:\Users\benja\Zotero\zotero.sqlite";
        const string SENSING_TARGET_SUFFIX = "sector";
        static void Main(string[] args)
        {
            ZoteroDatabaseConnection connection = new ZoteroDatabaseConnection(DEFAULT_ZOTERO_SQLITE_STORAGE_PATH);
            connection.Connect();
            Library[] libraries = connection.Dump();


            //void WriteCollectionStructure(Container targetContainer, ref string output, int reccursiveLevel, bool isReccursive = true)
            //{
            //    foreach (Container subContainer in targetContainer.InnerObjects.Where(obj => obj is Container))
            //    {
            //        output += '\n' + 
            //    }
            //}

            IEnumerable<Item> items = ListItems((Container)((Container)libraries[1].InnerObjects.First(obj => (obj is Container) && (obj.ID == "3"))).InnerObjects.First(obj => (obj is Container) && (obj.ID == "5")));
            //.InnerObjects.First(obj => (obj is Container) && (obj.ID == "3"))
            //.InnerObjects.First(obj => (obj is Container) && (obj.ID == "5"))
            IEnumerable<Tag> sensingTargetTags = new List<Tag>();
            ////List<CreatorLink> links = new List<CreatorLink>();
            foreach (Item item in items)
            {
                sensingTargetTags = sensingTargetTags.Union(item.Tags.Where(tag => tag.Name.EndsWith(SENSING_TARGET_SUFFIX)));
                Debug.WriteLine(String.Format("There are {0} sensing target tags in this library", sensingTargetTags.Count()));

                //for (int creator1Index = 0; creator1Index < item.Creators.Count; creator1Index++)
                //{
                //    for (int creator2Index = creator1Index + 1; creator2Index < item.Creators.Count; creator2Index++)
                //    {
                //        try
                //        {
                //            links.First(link => link.Contains(item.Creators[creator1Index]) || link.Contains(item.Creators[creator2Index])).Occurences++;
                //        }
                //        catch (InvalidOperationException)
                //        {
                //            links.Add(new CreatorLink() { creator1 = item.Creators[creator1Index], creator2 = item.Creators[creator2Index] });
                //        }
                //    }
                //}
            }
            string stringPartToRemove = ' ' + SENSING_TARGET_SUFFIX;
            using (FileStream outputFile = File.Open(@"C:\Users\benja\Desktop\output.csv", FileMode.CreateNew))
            using (StreamWriter outputFileWriter = new StreamWriter(outputFile))
            {
                foreach (Tag targetedSectorTag in sensingTargetTags)
                {
                    int startIndex = targetedSectorTag.Name.IndexOf(stringPartToRemove);
                    string targetedSector = targetedSectorTag.Name.Remove(startIndex, stringPartToRemove.Length);
                    int sensingTargetTagImplementation = items.Count(item => item.Tags.Contains(targetedSectorTag));
                    outputFileWriter.WriteLine(targetedSector + ',' + sensingTargetTagImplementation);
                }
            }

            IEnumerable<Item> ListItems(Container target, bool recursive = true)
                {
                    List<Item> results = new List<Item>();
                    foreach (ZoteroObject innerObject in target.InnerObjects)
                    {
                        if (innerObject is Item)
                            results.Add((Item)innerObject);
                        else if (recursive)
                            if (innerObject is Container)
                                results.AddRange(ListItems((Container)innerObject));
                    }
                    return results;
                }
        }
    }
}
