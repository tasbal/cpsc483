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

#region Using directives
using System;
using System.Xml;
using System.IO;
using Google.GData.Client;
#endregion

//////////////////////////////////////////////////////////////////////
// <summary>GDataParserNameTable</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Extensions
{

    /// <summary>
    /// Subclass of the nametable, has the extensions for the GNamespace
    /// </summary>
    public class GDataParserNameTable : BaseNameTable
    {
        /// <summary>the google calendar namespace</summary>
        public const string NSGCal  = "http://schemas.google.com/gCal/2005"; 

        /// <summary>the google calendar prefix</summary>
        public const string gCalPrefix  = "gCal"; 


        /// <summary>the event prefix </summary>
        public static string Event = gNamespacePrefix + "event";

#region element strings
        /// <summary> timezone indicator on the feedlevel</summary>
        public const string XmlTimeZoneElement = "timezone"; 
        /// <summary>static string for parsing</summary> 
        public const string XmlWhenElement = "when";
        /// <summary>static string for parsing</summary> 
        public const string XmlWhereElement = "where";
        /// <summary>static string for parsing</summary> 
        public const string XmlWhoElement = "who";
        /// <summary>static string for parsing</summary> 
        public const string XmlEntryLinkElement = "entryLink";
        /// <summary>static string for parsing</summary> 
        public const string XmlFeedLinkElement = "feedLink";
        /// <summary>static string for parsing</summary> 
        public const string XmlEventStatusElement = "eventStatus";
        /// <summary>static string for parsing</summary> 
        public const string XmlVisibilityElement = "visibility";
        /// <summary>static string for parsing</summary> 
        public const string XmlTransparencyElement = "transparency";
        /// <summary>static string for parsing</summary>
        public const string XmlAttendeeTypeElement = "attendeeType";
        /// <summary>static string for parsing</summary>
        public const string XmlAttendeeStatusElement = "attendeeStatus";
        /// <summary>static string for parsing</summary>
        public const string XmlRecurrenceElement = "recurrence";
        /// <summary>static string for parsing</summary>
        public const string XmlRecurrenceExceptionElement = "recurrenceException";
        /// <summary>static string for parsing</summary>
        public const string XmlOriginalEventElement = "originalEvent";
        /// <summary>static string for parsing</summary>
        public const string XmlReminderElement = "reminder";
        /// <summary>static string for parsing</summary>
        public const string XmlCommentsElement = "comments";
        /// <summary>static string for parsing the color element in a calendar</summary>
        public const string XmlColorElement = "color";
        /// <summary>static string for parsing the selected element in a calendar</summary>
        public const string XmlSelectedElement = "selected";
        /// <summary>static string for parsing the ACL element in a calendar</summary>
        public const string XmlAccessLevelElement = "accesslevel";
        /// <summary>static string for parsing the hidden element in a calendar</summary>
        public const string XmlHiddenElement = "hidden";
        
#endregion

#region attribute strings

        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeStartTime = "startTime";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeEndTime = "endTime";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeValueString = "valueString";
        /// <summary>static string for parsing the email in gd:who</summary>    
        public const string XmlAttributeEmail = "email";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeRel = "rel";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeLabel = "label";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeHref = "href";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeCountHint = "countHint";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeReadOnly = "readOnly";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeId = "id";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeDays = "days";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeHours = "hours";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeMinutes = "minutes";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeAbsoluteTime = "absoluteTime";
        /// <summary>static string for parsing the specialized attribute on a RecurringException</summary>    
        public const string XmlAttributeSpecialized = "specialized";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeMethod= "method";
     

#endregion

#region Calendar specific (consider moving to seperate table)
        /// <summary>static string for parsing a webcontent element</summary>
        public const string XmlWebContentElement = "webContent"; 
        /// <summary>static string for parsing a webcontent element</summary>
        public const string XmlWebContentGadgetElement = "webContentGadgetPref"; 
        /// <summary>static string for parsing the extendedProperty element</summary>    
        public const string XmlExtendedPropertyElement = "extendedProperty";
        /// <summary>static string for the url attribute</summary>    
        public const string XmlAttributeUrl = "url";
        /// <summary>static string for the width attribute</summary>    
        public const string XmlAttributeWidth= "width";
        /// <summary>static string for the height attribute</summary>    
        public const string XmlAttributeHeight= "height";
        ///  <summary>static string for the sendEventNotifications element</summary>    
        public const string XmlSendNotificationsElement = "sendEventNotifications"; 
        ///  <summary>static string for the quickAdd element</summary>    
        public const string XmlQuickAddElement = "quickadd"; 
#endregion

   }
    /////////////////////////////////////////////////////////////////////////////

}
/////////////////////////////////////////////////////////////////////////////


