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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using System.Globalization;

namespace Google.GData.Extensions {

    /// <summary>
    /// Extensible enum type used in many places.
    /// compared to the base class, this one
    /// adds a default values
    /// </summary>
    public abstract class SimpleElement : ExtensionBase
    {

        private string value;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleElement(string name, string prefix, string ns)
                    :base(name, prefix,ns)
        {
        }



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        /// <param name="value">the intial value</param>
        protected SimpleElement(string name, string prefix, string ns, string value)
                    :base(name, prefix, ns)
        {
            this.value = value;
        }

       
        //////////////////////////////////////////////////////////////////////
        /// <summary>accesses the Attribute list. The keys are the attribute names
        /// the values the attribute values</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public SortedList Attributes
        {
            get 
            {
                return getAttributes();
            }
            set {this.attributes = value;}
        }
        // end of accessor public SortedList Attributes
 
        /// <summary>
        ///  Accessor Method for the value
        /// </summary>
        public virtual string Value
        {
            get { return value; }
            set { this.value = value;}
        }

        /// <summary>
        ///  Accessor Method for the value as integer
        /// </summary>
        public int IntegerValue
        {
            get { return Convert.ToInt32(value, CultureInfo.InvariantCulture); }
            set { this.value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        ///  Accessor Method for the value as integer
        /// </summary>
        public uint UnsignedIntegerValue
        {
            get { return Convert.ToUInt32(value, CultureInfo.InvariantCulture); }
            set { this.value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        ///  Accessor Method for the value as float
        /// </summary>
        public double FloatValue
        {
            get { return Convert.ToDouble(value, CultureInfo.InvariantCulture); }
            set { this.value = value.ToString(CultureInfo.InvariantCulture); }
        }

   
        #region overloaded for persistence

        /// <summary>
        /// debugging helper
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + " for: " + XmlNameSpace + "- " + XmlName;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Who object.</summary> 
        /// <param name="node">the xml parses node, can be NULL</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created SimpleElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public override IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser) 
        {
            Tracing.TraceCall();

            SimpleElement e = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                    node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }
            
            // memberwise close is fine here, as everything is identical beside the value
            e = this.MemberwiseClone() as SimpleElement;
            e.InitInstance(this);

            if (node != null)
            {
                e.Value = node.InnerText;

                if (node.Attributes != null)
                {
                    e.ProcessAttributes(node);
                }
            }

            return e;
        }

        
        /// <summary>
        /// saves out the value, get's called from Save
        /// </summary>
        /// <param name="writer"></param>
        public override void SaveInnerXml(XmlWriter writer)
        {
            if (this.value != null)
            {
                writer.WriteString(this.value);
            }
        }
        #endregion
    }




    /// <summary>
    /// a simple element with one attribute,called value that exposes 
    /// that value as the value property
    /// </summary>
    public class SimpleAttribute : SimpleElement
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleAttribute(string name, string prefix, string ns)
                    :base(name, prefix, ns)
        {
            this.Attributes.Add(BaseNameTable.XmlValue, null);
        }



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        /// <param name="value">the intial value</param>
        protected SimpleAttribute(string name, string prefix, string ns, string value)
                    :base(name, prefix, ns)
        {
            this.Attributes.Add(BaseNameTable.XmlValue, value);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor for "value" attribute.</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public override string Value
        {
            get {
                return this.Attributes[BaseNameTable.XmlValue] as string;
            }
            set
            {
                this.Attributes[BaseNameTable.XmlValue] = value;
            }
        }
        // end of accessor public string Value
    }

}  
