/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Calendar; 
using Google.GData.Extensions; 

using System.IO;
using System.Xml; 



namespace Google.GData.Client.UnitTests
{
    /// <summary>
    /// Summary description for objectmodelhelper.
    /// </summary>
    public class ObjectModelHelper
    {
        public static int DEFAULT_REMINDER_TIME = 30; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a new, in memory atom entry</summary> 
        /// <returns>the new AtomEntry </returns>
        //////////////////////////////////////////////////////////////////////
        public static AtomEntry CreateAtomEntry(int iCount)
        {
            AtomEntry entry = new AtomEntry();
            // some unicode chars
            Char[] chars = new Char[] {
            '\u0023', // #
            '\u0025', // %
            '\u03a0', // Pi
            '\u03a3',  // Sigma
            '\u03d1', // beta
            '&',
            };

            AtomPerson author = new AtomPerson(AtomPersonType.Author);
            author.Name = "John Doe" + chars[0] + chars[1] + chars[2] + chars[3] + chars[4] + chars[5]; 
            author.Email = "JohnDoe@example.com";
            entry.Authors.Add(author);
    
            AtomCategory cat = new AtomCategory();
    
            cat.Label = "Default";
            cat.Term = "Default" + chars[4] + " Term";
            entry.Categories.Add(cat);
    
            entry.Content.Content = "this is the default text & entry";
            entry.Content.Type = "html"; 
            entry.Published = new DateTime(2001, 11, 20, 22, 30, 0);  
            entry.Title.Text = "This is a entry number: " + iCount;
            entry.Updated = DateTime.Now; 

    
            return entry;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a new, in memory atom entry for google base</summary> 
        /// <returns>the new AtomEntry </returns>
        //////////////////////////////////////////////////////////////////////
        public static AtomEntry CreateGoogleBaseEntry(int iCount)
        {
            AtomEntry entry = CreateAtomEntry(iCount); 

            // now add some base specific nodes. This should later be replaced by 
            // the GoogleBase classes
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<gb:label xmlns:gb='http://base.google.com/ns/1.0'>Computer</gb:label>");
            XmlNode gbaseNode1 = doc.DocumentElement;
            doc.LoadXml("<gb:item_type xmlns:gb='http://base.google.com/ns/1.0'>products</gb:item_type>");
            XmlNode gbaseNode2 = doc.DocumentElement;

            entry.ExtensionElements.Add(gbaseNode1);
            entry.ExtensionElements.Add(gbaseNode2);

            return entry;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a new, in memory atom entry</summary> 
        /// <returns>the new AtomEntry </returns>
        //////////////////////////////////////////////////////////////////////
        public static EventEntry CreateEventEntry(int iCount)
        {
            EventEntry entry = new EventEntry();
            // some unicode chars
            Char[] chars = new Char[] {
                                          '\u0023', // #
                                          '\u0025', // %
                                          '\u03a0', // Pi
                                          '\u03a3',  // Sigma
                                          '\u03d1', // beta
            };

            // if unicode needs to be disabled for testing, just uncomment this line
            // chars = new Char[] { 'a', 'b', 'c', 'd', 'e'}; 

    
    
            AtomPerson author = new AtomPerson(AtomPersonType.Author);
            author.Name = "John Doe" + chars[0] + chars[1] + chars[2] + chars[3]; 
            author.Email = "JohnDoe@example.com";
            entry.Authors.Add(author);
    
            AtomCategory cat = new AtomCategory();
    
            cat.Label = "Default";
            cat.Term = "Default" + chars[4] + " Term";
            entry.Categories.Add(cat);
    
            entry.Content.Content = "this is the default text entry";
            entry.Published = new DateTime(2001, 11, 20, 22, 30, 0);  
            entry.Title.Text = "This is a entry number: " + iCount;
            entry.Updated = DateTime.Now; 

            When newTime = new When();
            newTime.StartTime = DateTime.Today.AddDays(-3);
            newTime.EndTime = DateTime.Today.AddDays(1);
            entry.Times.Add(newTime);


            entry.Reminder = new Reminder();
            entry.Reminder.Minutes = DEFAULT_REMINDER_TIME; 

            Who someone = new Who();
            someone.ValueString = "test.fmantek@gmail.com";
            Who.AttendeeStatus status = new Who.AttendeeStatus();
            status.Value = "event.accepted"; 
            someone.Attendee_Status = status;
            someone.Rel = "http://schemas.google.com/g/2005#event.organizer";

            entry.Participants.Add(someone);


            Where newPlace = new Where();
            newPlace.ValueString = "A really nice place";
            entry.Locations.Add(newPlace);
            newPlace = new Where();
            newPlace.ValueString = "Another really nice place";
            newPlace.Rel = Where.RelType.EVENT_ALTERNATE;
            entry.Locations.Add(newPlace);
            return entry;
        }
        /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
       /// <summary>compares two atomEntrys to see if they are identical objects</summary> 
       /// <param name="theOne">the first AtomEntry </param>
       /// <param name="theOther">the other AtomEntry to compare with </param> 
       /// <returns>true if equal </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsEntryIdentical(AtomEntry theOne, AtomEntry theOther)
       {
           Tracing.TraceMsg("Entering IsEntryIdentical"); 

           if (theOne == null || theOther == null)
           {
               return theOne == theOther; 
           }

           Tracing.TraceMsg("Comparing AuthorCollection"); 
           if (ObjectModelHelper.IsPersonCollectionIdentical(theOne.Authors, theOther.Authors)==false)
           {
               return false;
           }
           Tracing.TraceMsg("Comparing ContributorCollection"); 
           if (ObjectModelHelper.IsPersonCollectionIdentical(theOne.Contributors, theOther.Contributors)==false)
           {
               return false;
           }
           Tracing.TraceMsg("Comparing CategoryCollection"); 
           if (ObjectModelHelper.IsCategoryCollectionIdentical(theOne.Categories, theOther.Categories)==false)
           {
               return false;
           }
           Tracing.TraceMsg("Comparing LinkCollection"); 
           if (ObjectModelHelper.IsLinkCollectionIdentical(theOne.Links, theOther.Links)==false)
           {
               return false;
           }

           Tracing.TraceMsg("Comparing Content"); 
           if (ObjectModelHelper.IsContentIdentical(theOne.Content, theOther.Content)==false)
           {
               return false;
           }

           Tracing.TraceMsg("Comparing Source"); 
           if (ObjectModelHelper.IsSourceIdentical(theOne.Source, theOther.Source)==false)
           {
               return false;
           }

           Tracing.TraceMsg("Comparing Summary"); 
           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Summary, theOther.Summary)==false)
           {
               return false;
           }

           Tracing.TraceMsg("Comparing Title"); 
           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Title, theOther.Title)==false)
           {
               return false;
           }
           Tracing.TraceMsg("Comparing Rights"); 
           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Rights, theOther.Rights)==false)
           {
               return false;
           }

           Tracing.TraceMsg("Comparing BaseLink"); 
           if (ObjectModelHelper.IsBaseLinkIdentical(theOne.Id, theOther.Id)==false)
           {
               return false;
           }
           /*
           if (System.DateTime.Compare(theOne.Published, theOther.Published) != 0)
           {
               return false;
           }
           if (System.DateTime.Compare(theOne.Updated,theOther.Updated) != 0)
           {
               return false;
           }
           */

           Tracing.TraceMsg("Exiting IsEntryIdentical"); 


           return true;
       }
       /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
       /// <summary>public static bool IsPersonCollectionIdentical(AtomPersonCollection theOne, AtomPersonCollection theOther)</summary> 
       /// <param name="theOne">the One Collection </param>
       /// <param name="theOther">the Other Collection </param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsPersonCollectionIdentical(AtomPersonCollection theOne, AtomPersonCollection theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (theOne.Count != theOther.Count)
           {
               return false;
           }
           for (int i=0; i< theOne.Count; i++)
           {
               if (IsPersonIdentical(theOne[i], theOther[i])==false)
               {
                   return false;
               }
               
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
       /// <summary>verifies the categroy collection</summary> 
       /// <param name="theOne">the One Collection </param>
       /// <param name="theOther">the Other Collection </param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsCategoryCollectionIdentical(AtomCategoryCollection theOne, AtomCategoryCollection theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (theOne.Count != theOther.Count)
           {
               return false;
           }
           for (int i=0; i< theOne.Count; i++)
           {
               if (IsCategoryIdentical(theOne[i], theOther[i])==false)
               {
                   return false;
               }

           }


           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>verifies the categroy collection</summary> 
       /// <param name="theOne">the One Collection </param>
       /// <param name="theOther">the Other Collection </param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsLinkCollectionIdentical(AtomLinkCollection theOne, AtomLinkCollection theOther)
       {

           if (theOne == null && theOther == null)
           {
               return true;
           }
           if (theOne.Count != theOther.Count)
           {
               return false;
           }
           for (int i=0; i< theOne.Count; i++)
           {
               if (IsLinkIdentical(theOne[i], theOther[i])==false)
               {
                   return false;
               }

           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////



       //////////////////////////////////////////////////////////////////////
       /// <summary>public static bool IsBaseIdentical(AtomBase base, AtomBase base2)</summary> 
       /// <param name="theOne">the One base </param>
       /// <param name="theOther">the Other base</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsBaseIdentical(AtomBase theOne, AtomBase theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (AtomUri.Compare(theOne.Base, theOther.Base)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.Language, theOther.Language)!= 0)
           {
               return false;
           }
           return true;
       }
       /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
       /// <summary>public static bool IsPersonIdentical(AtomPerson theOne, AtomPerson theOther)</summary> 
       /// <param name="theOne">the One Person </param>
       /// <param name="theOther">the Other Person</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsPersonIdentical(AtomPerson theOne, AtomPerson theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               Tracing.TraceInfo("IsPersonIdentical: comparing  base failed");
               return false;
           }
           Tracing.TraceInfo("IsPersonIdentical: comparing  Name " + theOne.Name + " " + theOther.Name);

           if (String.Compare(theOne.Email, theOther.Email)!=0)
           {
               Tracing.TraceInfo("IsPersonIdentical: comparing  email failed" + theOne.Email + " " + theOther.Email);
               return false;
           }
           if (String.Compare(theOne.Name, theOther.Name)!=0)
           {
               Tracing.TraceInfo("IsPersonIdentical: comparing  Name failed" + theOne.Name + " " + theOther.Name);
               return false;
           }
           if (AtomUri.Compare(theOne.Uri, theOther.Uri) != 0)
           {
               Tracing.TraceInfo("IsPersonIdentical: comparing  URI failed - " + theOne.Uri.ToString() + " " + theOther.Uri.ToString());
               return false;
           }
           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares a category</summary> 
       /// <param name="theOne">the One category </param>
       /// <param name="theOther">the Other category</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsCategoryIdentical(AtomCategory theOne, AtomCategory theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }


           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }

           if (String.Compare(theOne.Label, theOther.Label)!=0)
           {
               return false;
           }
           if (String.Compare(theOne.Term, theOther.Term) != 0)
           {
               return false;
           }
           if (AtomUri.Compare(theOne.Scheme, theOther.Scheme)!= 0)
           {
               return false;
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 content objects</summary> 
       /// <param name="theOne">the One content </param>
       /// <param name="theOther">the Other content</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsContentIdentical(AtomContent theOne, AtomContent theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }


           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }
           if (String.Compare(theOne.Type, theOther.Type)!= 0)
           {
               return false;
           }
           if (AtomUri.Compare(theOther.Src, theOther.Src)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.Content, theOther.Content)!= 0)
           {
               return false;
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 source objects</summary> 
       /// <param name="theOne">the One source</param>
       /// <param name="theOther">the Other source</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsSourceIdentical(AtomSource theOne, AtomSource theOther)
       {

           Tracing.TraceInfo("Comparing source objects"); 
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }

           Tracing.TraceInfo("Source: comparing Authors collections"); 
           if (ObjectModelHelper.IsPersonCollectionIdentical(theOne.Authors, theOther.Authors)==false)
           {
               return false;
           }

           Tracing.TraceInfo("Source: comparing Contributors collections"); 
           if (ObjectModelHelper.IsPersonCollectionIdentical(theOne.Contributors, theOther.Contributors)==false)
           {
               return false;
           }
           Tracing.TraceInfo("Source: comparing categories collections"); 
           if (ObjectModelHelper.IsCategoryCollectionIdentical(theOne.Categories, theOther.Categories)==false)
           {
               return false;
           }
           Tracing.TraceInfo("Source: comparing links collections"); 
           if (ObjectModelHelper.IsLinkCollectionIdentical(theOne.Links, theOther.Links)==false)
           {
               return false;
           }

           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Title, theOther.Title)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Rights, theOther.Rights)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsTextConstructIdentical(theOne.Subtitle, theOther.Subtitle)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsBaseLinkIdentical(theOne.Id, theOther.Id)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsGeneratorIdentical(theOne.Generator, theOther.Generator)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsBaseLinkIdentical(theOne.Icon, theOther.Icon)==false)
           {
               return false;
           }
           if (ObjectModelHelper.IsBaseLinkIdentical(theOne.Logo, theOther.Logo)==false)
           {
               return false;
           }
  


           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 text construct objects</summary> 
       /// <param name="theOne">the One</param>
       /// <param name="theOther">the Other</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsTextConstructIdentical(AtomTextConstruct theOne, AtomTextConstruct theOther)
       {
           if (theOne==null && theOther == null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }

           if (theOne.Type != theOther.Type)
           {
               return false;
           }
           if (String.Compare(theOne.Text,theOther.Text)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.XmlName, theOther.XmlName)!=0)
           {
               return false;
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 Generator objects</summary> 
       /// <param name="theOne">the One</param>
       /// <param name="theOther">the Other</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsGeneratorIdentical(AtomGenerator theOne, AtomGenerator theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }

            if (String.Compare(theOne.Text, theOther.Text)!= 0)
            {
                return false;
            }
            if (String.Compare(theOne.Version, theOther.Version)!=0)
            {
                return false;
            }
            if (AtomUri.Compare(theOne.Uri, theOther.Uri)!=0)
            {
                return false;
            }


           return true;
       }
       /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 IDs</summary> 
       /// <param name="theOne">the One </param>
       /// <param name="theOther">the Other</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsBaseLinkIdentical(AtomBaseLink theOne, AtomBaseLink theOther)
       {
           if (theOne==null && theOther==null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }
           if (AtomUri.Compare(theOne.Uri, theOther.Uri)!=0)
           {
               return false;
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
       /// <summary>compares 2 link objects</summary> 
       /// <param name="theOne">the One link</param>
       /// <param name="theOther">the Other link</param>
       /// <returns>true if identical </returns>
       //////////////////////////////////////////////////////////////////////
       public static bool IsLinkIdentical(AtomLink theOne, AtomLink theOther)
       {
           if (theOne == null && theOther == null)
           {
               return true;
           }

           if (ObjectModelHelper.IsBaseIdentical(theOne, theOther)==false)
           {
               return false;
           }

           if (AtomUri.Compare(theOne.HRef, theOther.HRef)!=0)
           {
               return false;
           }

           if (theOne.Length != theOther.Length)
           {
               return false;
           }

           if (String.Compare(theOne.Rel, theOther.Rel)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.Type, theOther.Type)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.HRefLang, theOther.HRefLang)!= 0)
           {
               return false;
           }
           if (String.Compare(theOne.Title, theOther.Title)!= 0)
           {
               return false;
           }

           return true;
       }
       /////////////////////////////////////////////////////////////////////////////




       //////////////////////////////////////////////////////////////////////
       /// <summary>dump feeds</summary> 
       /// <param name="theOne">the filenam</param>
       //////////////////////////////////////////////////////////////////////
       public static void DumpAtomObject(AtomBase atom, string baseName)
       {
           if (atom != null)
           {
               StreamWriter stream = new StreamWriter(baseName, false, System.Text.Encoding.UTF8); 
               XmlTextWriter writer =  new XmlTextWriter(stream);
               writer.Formatting = Formatting.Indented;
               writer.WriteStartDocument(false);
               atom.SaveToXml(writer);
               writer.Flush();
               writer.Close();
               stream.Close();
           }
       }
       /////////////////////////////////////////////////////////////////////////////
    }
}
