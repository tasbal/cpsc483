/* Copyright (c) 2007 Google Inc.
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
using System.Xml;
using Google.GData.Client;
using System.Text;

namespace Google.GData.Extensions.Apps
{
    /// <summary>
    /// All defined Google Apps extensions.
    /// </summary>
    public class GAppsExtensions
    {
        /// <summary>
        /// Declares provisioning extension elements for an atom base object.
        /// </summary>
        /// <param name="baseObject">the <code>AtomBase</code> object,
        /// e.g. <code>UserEntry</code> or <code>UserFeed</code></param>
        public static void AddProvisioningExtensions(AtomBase baseObject)
        {
            baseObject.AddExtension(new EmailListElement());
            baseObject.AddExtension(new LoginElement());
            baseObject.AddExtension(new NameElement());
            baseObject.AddExtension(new NicknameElement());
            baseObject.AddExtension(new QuotaElement());
        }

        /// <summary>
        /// Declares mail item extension elements for an atom base object.
        /// </summary>
        /// <param name="baseObject">the <code>AtomBase</code> object,
        /// e.g. <code>MailItemEntry</code> or <code>MailItemFeed</code></param>
        public static void AddMailItemExtensions(AtomBase baseObject)
        {
            baseObject.AddExtension(new LabelElement());
            baseObject.AddExtension(new MailItemPropertyElement());
            baseObject.AddExtension(new Rfc822MsgElement());
        }
    }

    /// <summary>
    /// Constants related to Google Apps extension elements.
    /// </summary>
    public class AppsNameTable
    {
        /// <summary>The Google Apps namespace.</summary>
        public const string AppsNamespace = "http://schemas.google.com/apps/2006";

        /// <summary>Prefix of Google Apps extension elements.</summary>
        public const string AppsPrefix = "apps";

        /// <summary>
        /// Google Apps user agent.
        /// </summary>
        public const string GAppsAgent = "GApps-CS/1.0.0";

        /// <summary>
        /// Identifier for Google Apps services.
        /// </summary>
        public const string GAppsService = "apps";

        /// <summary>
        /// Base feed URI for all Google Apps requests.
        /// </summary>
        public const string appsBaseFeedUri = "https://www.google.com/a/feeds/";

        /// <summary>
        /// Category term for a user account entry.
        /// </summary>
        public const string User = AppsNamespace + "#user";

        /// <summary>
        /// Category term for a nickname entry.
        /// </summary>
        public const string Nickname = AppsNamespace + "#nickname";

        /// <summary>
        /// Category term for an email list entry.
        /// </summary>
        public const string EmailList = AppsNamespace + "#emailList";

        /// <summary>
        /// Category term for an email list recipient entry.
        /// </summary>
        public const string EmailListRecipient = EmailList + ".recipient";

        /// <summary>
        /// XML element name for user login information.
        /// </summary>
        public const string AppsLogin = "login";

        /// <summary>
        /// XML attribute for the username of a login element.
        /// </summary>
        public const string AppsLoginUserName = "userName";

        /// <summary>
        /// XML attribute for the password of a login element.
        /// </summary>
        public const string AppsLoginPassword = "password";

        /// <summary>
        /// XML attribute for the suspended flag of a login element.
        /// </summary>
        public const string AppsLoginSuspended = "suspended";

        /// <summary>
        /// XML attribute for the ipWhitelisted flag of a login element.
        /// </summary>
        public const string AppsLoginIpWhitelisted = "ipWhitelisted";

        /// <summary>
        /// XML attribute for the hashFunctionName flag of a login element.
        /// </summary>
        public const string AppsLoginHashFunctionName = "hashFunctionName";

        /// <summary>
        /// XML attribute for the admin flag of a login element.
        /// </summary>
        public const string AppsLoginAdmin = "admin";

        /// <summary>
        /// XML attribute for the agreedToTerms flag of a login element.
        /// </summary>
        public const string AppsLoginAgreedToTerms = "agreedToTerms";

        /// <summary>
        /// XML attribute for the changePasswordAtNextLogin flag of a login element.
        /// </summary>
        public const string AppsLoginChangePasswordAtNextLogin = "changePasswordAtNextLogin";

        /// <summary>
        /// XML element name for email list data.
        /// </summary>
        public const string AppsEmailList = "emailList";

        /// <summary>
        /// XML attribute for the name of an email list.
        /// </summary>
        public const string AppsEmailListName = "name";

        /// <summary>
        /// XML element name for nickname data.
        /// </summary>
        public const string AppsNickname = "nickname";

        /// <summary>
        /// XML attribute for the "name" value of a nickname.
        /// </summary>
        public const string AppsNicknameName = "name";

        /// <summary>
        /// XML element name for specifying user quota.
        /// </summary>
        public const string AppsQuota = "quota";

        /// <summary>
        /// XML attribute for the quota limit, in megabytes.
        /// </summary>
        public const string AppsQuotaLimit = "limit";

        /// <summary>
        /// XML element name for specifying a user name.
        /// </summary>
        public const string AppsName = "name";

        /// <summary>
        /// XML attribute for the "familyName" value of a name.
        /// </summary>
        public const string AppsNameFamilyName = "familyName";

        /// <summary>
        /// XML attribute for the "givenName" value of a name.
        /// </summary>
        public const string AppsNameGivenName = "givenName";

        /// <summary>
        /// XML attribute for a Google Apps error.
        /// </summary>
        public const string AppsError = "error";

        /// <summary>
        /// XML attribute for the "errorCode" value of an error.
        /// </summary>
        public const string AppsErrorErrorCode = "errorCode";

        /// <summary>
        /// XML attribute for the "invalidInput" value of an error.
        /// </summary>
        public const string AppsErrorInvalidInput = "invalidInput";

        /// <summary>
        /// XML attribute for the "reason" value of an error.
        /// </summary>
        public const string AppsErrorReason = "reason";
    }

    /// <summary>
    /// Name table for Google Apps extensions specific to the Email Migration
    /// API.
    /// </summary>
    public class AppsMigrationNameTable : AppsNameTable
    {
        /// <summary>apps:label extension element</summary>
        public const string AppsLabel = "label";

        /// <summary>labelName attribute of apps:label extension</summary>
        public const string AppsLabelName = "labelName";

        /// <summary>apps:mailItemProperty extension element</summary>
        public const string AppsMailItemProperty = "mailItemProperty";

        /// <summary>apps:rfc822Msg extension element</summary>
        public const string AppsRfc822Msg = "rfc822Msg";

        /// <summary>
        /// Base feed URI for all Google Apps Migration requests.
        /// </summary>
        public const string AppsMigrationBaseFeedUri = "https://apps-apis.google.com/a/feeds/migration/2.0";

        /// <summary>
        /// Category term for a mail item entry.
        /// </summary>
        public const string MailItem = AppsNameTable.AppsNamespace + "#mailItem";    
    }


    /// <summary>
    /// Extension element sed to model a Google Apps email list.
    /// Has attribute "name".
    /// </summary>
    public class EmailListElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty EmailListElement instance.
        /// </summary>
        public EmailListElement()
            : base(AppsNameTable.AppsEmailList,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new EmailListElement instance with the specified value.
        /// </summary>
        /// <param name="name">the name attribute of this EmailListElement</param>
        public EmailListElement(string name)
            : base(AppsNameTable.AppsEmailList,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name property accessor.
        /// </summary>
        public string Name
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsEmailListName]); }
            set { this.getAttributes()[AppsNameTable.AppsEmailListName] = value; }
        }
    }

    /// <summary>
    /// Google Apps GData extension to model a user account.
    /// Has attributes: "userName", "password", "suspended",
    /// "ipWhitelisted", "admin", "agreedToTerms",
    /// "changePasswordAtNextLogin", and "hashFunctionName".
    /// </summary>
    public class LoginElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty LoginElement instance.
        /// </summary>
        public LoginElement()
            : base(AppsNameTable.AppsLogin,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified value.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        public LoginElement(string userName)
            : base(AppsNameTable.AppsLogin,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified values.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        /// <param name="password">The account's password.</param>
        /// <param name="suspended">True if the account has been suspended,
        /// false otherwise.</param>
        /// <param name="ipWhitelisted">True if the account has been IP whitelisted,
        /// false otherwise.</param>
        public LoginElement(string userName, string password, bool suspended, bool ipWhitelisted)
            : base(AppsNameTable.AppsLogin,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.UserName = userName;
            this.Password = password;
            this.Suspended = suspended;
            this.IpWhitelisted = ipWhitelisted;
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified values.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        /// <param name="password">The account's password.</param>
        /// <param name="suspended">True if the account has been suspended,
        /// false otherwise.</param>
        /// <param name="ipWhitelisted">True if the account has been IP whitelisted,
        /// false otherwise.</param>
        /// <param name="hashFunctionName">Hash function used to encode the password
        /// parameter.  Currently, only "SHA-1" is supported.</param>
        public LoginElement(string userName,
                            string password,
                            bool suspended,
                            bool ipWhitelisted,
                            string hashFunctionName)
            : base(AppsNameTable.AppsLogin,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.UserName = userName;
            this.Password = password;
            this.Suspended = suspended;
            this.IpWhitelisted = ipWhitelisted;
            this.HashFunctionName = hashFunctionName;
        }

        /// <summary>
        /// UserName property accessor
        /// </summary>
        public string UserName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsLoginUserName]); }
            set { getAttributes()[AppsNameTable.AppsLoginUserName] = value; }
        }

        /// <summary>
        /// Password property accessor
        /// </summary>
        public string Password
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsLoginPassword]); }
            set { getAttributes()[AppsNameTable.AppsLoginPassword] = value; }
        }

        /// <summary>
        /// Suspended property accessor
        /// </summary>
        public bool Suspended
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.AppsLoginSuspended]); }
            set { getAttributes()[AppsNameTable.AppsLoginSuspended] = value; }
        }

        /// <summary>
        /// IpWhitelisted property accessor
        /// </summary>
        public bool IpWhitelisted
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.AppsLoginIpWhitelisted]); }
            set { getAttributes()[AppsNameTable.AppsLoginIpWhitelisted] = value; }
        }

        /// <summary>
        /// HashFunctionName property accessor
        /// </summary>
        public string HashFunctionName
        {
            get
            {
                return Convert.ToString(getAttributes()[AppsNameTable.AppsLoginHashFunctionName]);
            }
            set
            {
                getAttributes()[AppsNameTable.AppsLoginHashFunctionName] = value;
            }
        }

        /// <summary>
        /// Admin property accessor.  The admin attribute is set to true if the user
        /// is an administrator and false if the user is not an administrator.
        /// </summary>
        public bool Admin
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.AppsLoginAdmin]); }
            set { getAttributes()[AppsNameTable.AppsLoginAdmin] = value; }
        }

        /// <summary>
        /// AgreedToTerms property accessor.  Read-only; true if the user has agreed
        /// to the terms of service.
        /// </summary>
        public bool AgreedToTerms
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.AppsLoginAgreedToTerms]); }
            set { getAttributes()[AppsNameTable.AppsLoginAgreedToTerms] = value; }
        }

        /// <summary>
        /// ChangePasswordAtNextLogin property accessor.  Optional; true if
        /// the user needs to change his or her password at next login.
        /// </summary>
        public bool ChangePasswordAtNextLogin
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.AppsLoginChangePasswordAtNextLogin]); }
            set { getAttributes()[AppsNameTable.AppsLoginChangePasswordAtNextLogin] = value; }
        }
    }

    /// <summary>
    /// Google Apps GData extension describing a name.
    /// Has attributes "familyName" and "givenName".
    /// </summary>
    public class NameElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty NameElement instance.
        /// </summary>
        public NameElement()
            : base(AppsNameTable.AppsName,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new NameElement instance with the specified values.
        /// </summary>
        /// <param name="familyName">Family name (surname).</param>
        /// <param name="givenName">Given name (first name).</param>
        public NameElement(string familyName, string givenName)
            : base(AppsNameTable.AppsName,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.FamilyName = familyName;
            this.GivenName = givenName;
        }

        /// <summary>
        /// FamilyName property accessor
        /// </summary>
        public string FamilyName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsNameFamilyName]); }
            set { getAttributes()[AppsNameTable.AppsNameFamilyName] = value; }
        }

        /// <summary>
        /// GivenName property accessor
        /// </summary>
        public string GivenName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsNameGivenName]); }
            set { getAttributes()[AppsNameTable.AppsNameGivenName] = value; }
        }
    }

    /// <summary>
    /// Extension element to model a Google Apps nickname.
    /// Has attribute "name".
    /// </summary>
    public class NicknameElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty <code>NicknameElement</code> instance.
        /// </summary>
        public NicknameElement()
            : base(AppsNameTable.AppsNickname,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new <code>NicknameElement</code> instance with the specified value.
        /// </summary>
        /// <param name="name">the name attribute of this <code>NicknameElement</code></param>
        public NicknameElement(string name)
            : base(AppsNameTable.AppsNickname,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name property accessor.
        /// </summary>
        public string Name
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.AppsNicknameName]); }
            set { getAttributes()[AppsNameTable.AppsNicknameName] = value; }
        }
    }

    /// <summary>
    /// Extension element to model a Google Apps account quota.
    /// Has attribute "limit".
    /// </summary>
    public class QuotaElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty QuotaElement instance.
        /// </summary>
        public QuotaElement()
            : base(AppsNameTable.AppsQuota,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new QuotaElement instance with the specified value.
        /// </summary>
        /// <param name="limit">the quota, in megabytes.</param>
        public QuotaElement(int limit)
            : base(AppsNameTable.AppsQuota,
                   AppsNameTable.AppsPrefix,
                   AppsNameTable.AppsNamespace)
        {
            this.Limit = limit;
        }

        /// <summary>
        /// Limit property accessor
        /// </summary>
        public int Limit
        {
            get { return Convert.ToInt32(getAttributes()[AppsNameTable.AppsQuotaLimit]); }
            set { getAttributes()[AppsNameTable.AppsQuotaLimit] = value; }
        }
    }

    /// <summary>
    /// Google Apps Data Migration API element describing a mail item's
    /// label.
    /// </summary>
    public class LabelElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty LabelElement instance.
        /// </summary>
        public LabelElement()
            : base(AppsMigrationNameTable.AppsLabel,
                    AppsMigrationNameTable.AppsPrefix,
                    AppsMigrationNameTable.AppsNamespace)
        {
            this.LabelName = null;
        }

        /// <summary>
        /// Constructs a new LabelElement instance with the specified value.
        /// </summary>
        /// <param name="labelName">the name of the mail item's label</param>
        public LabelElement(string labelName)
            : base(AppsMigrationNameTable.AppsLabel,
                    AppsMigrationNameTable.AppsPrefix,
                    AppsMigrationNameTable.AppsNamespace)
        {
            this.LabelName = labelName;
        }

        /// <summary>
        /// LabelName property accessor
        /// </summary>
        public string LabelName
        {
            get { return Convert.ToString(this.getAttributes()[AppsMigrationNameTable.AppsLabelName]); }
            set { this.getAttributes()[AppsMigrationNameTable.AppsLabelName] = value; }
        }
    }

    /// <summary>
    /// Google Apps Data Migration API element describing the RFC 822
    /// message of a mail item.
    /// </summary>
    public class Rfc822MsgElement : ExtensionBase
    {
        private byte[] value;

        // Default number of bytes to copy when writing as XML
        private const int defaultStepSize = 128 * 1024;

        /// <summary>
        /// Constructs a new <code>Rfc822Msg</code> element with no message.
        /// </summary>
        public Rfc822MsgElement()
            : base(AppsMigrationNameTable.AppsRfc822Msg,
                   AppsMigrationNameTable.AppsPrefix,
                   AppsMigrationNameTable.AppsNamespace)
        { }

        /// <summary>
        /// Constructs a new <code>Rfc822Msg</code> element with the specified message.
        /// </summary>
        /// <param name="initValue">the RFC 822 message in byte array form</param>
        public Rfc822MsgElement(byte[] initValue)
            : base(AppsMigrationNameTable.AppsRfc822Msg,
                   AppsMigrationNameTable.AppsPrefix,
                   AppsMigrationNameTable.AppsNamespace)
        {
            this.value = initValue;
        }

        /// <summary>
        /// Constructs a new <code>Rfc822MsgElement</code> element with the specified message.
        /// </summary>
        /// <param name="initValue">the RFC 822 message in string form</param>
        public Rfc822MsgElement(string initValue)
            : this(Encoding.ASCII.GetBytes(initValue))
        { }

        /// <summary>
        ///  Accessor method for the Rfc822 message in byte array form
        /// </summary>
        public byte[] Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #region overloaded for persistence

        /// <summary>
        /// Returns the actual Rfc822 message as a string.
        /// Useful for debugging, but use with caution if
        /// the message is very large.
        /// </summary>
        /// <returns>String representation of the Rfc822 message</returns>
        public override string ToString()
        {
            return Encoding.ASCII.GetString(value, 0, value.Length);
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

            Rfc822MsgElement e = null;

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
            e = this.MemberwiseClone() as Rfc822MsgElement;
            e.InitInstance(this);

            if (node != null)
            {
                e.Value = System.Text.Encoding.ASCII.GetBytes(node.InnerText);

                if (node.Attributes != null)
                {
                    e.ProcessAttributes(node);
                }
            }

            return e;
        }


        /// <summary>
        /// Saves the contents of this <code>Rfc822MsgElement</code> in XML
        /// </summary>
        /// <param name="writer">the <code>XmlWriter</code>to write to</param>
        public override void SaveInnerXml(XmlWriter writer)
        {
            if (this.value != null)
            {
                int end = this.value.Length;
                int curPos = 0;
                int copyLen = defaultStepSize;

                while (curPos < end)
                {
                    if (curPos + copyLen > end)
                    {
                        copyLen = end - curPos;
                    }
                    string str = System.Text.Encoding.ASCII.GetString(this.value, curPos, copyLen);
                    writer.WriteString(str);
                    curPos += copyLen;
                }
            }
        }
        #endregion
      }

    /// <summary>
    /// Google Apps Data Migration API element describing an enumerable
    /// property about a mail item.
    /// </summary>
    public class MailItemPropertyElement : ExtensionBase
    {
        /// <summary>
        /// Defined mail item properties.
        /// </summary>
        public enum MailItemProperty
        {
            /// <summary>Mark as Draft</summary>
            IS_DRAFT,
            
            /// <summary>Move to Inbox</summary>
            IS_INBOX,

            /// <summary>Move to Sent Mail</summary>
            IS_SENT,

            /// <summary>Mark as starred</summary>
            IS_STARRED,

            /// <summary>Move to Trash</summary>
            IS_TRASH,

            /// <summary>Mark as unread</summary>
            IS_UNREAD
        }

        /// <summary>Indicates that a mail item should be marked as a draft
        /// when inserted into GMail.</summary>
        public static readonly MailItemPropertyElement DRAFT =
            new MailItemPropertyElement(MailItemProperty.IS_DRAFT);

        /// <summary>Indicates that a mail item should be placed in the inbox
        /// when inserted into GMail.</summary>
        public static readonly MailItemPropertyElement INBOX =
            new MailItemPropertyElement(MailItemProperty.IS_INBOX);

        /// <summary>Indicates that a mail item should be marked as "Sent" when
        /// inserted into GMail.</summary>
        public static readonly MailItemPropertyElement SENT =
            new MailItemPropertyElement(MailItemProperty.IS_SENT);

        /// <summary>Indicates that a mail item should be starred when inserted
        /// into GMail.</summary>
        public static readonly MailItemPropertyElement STARRED =
            new MailItemPropertyElement(MailItemProperty.IS_STARRED);

        /// <summary>Indicates that a mail item should be placed in the trash
        /// when inserted into GMail.</summary>
        public static readonly MailItemPropertyElement TRASH =
            new MailItemPropertyElement(MailItemProperty.IS_TRASH);

        /// <summary>Indicates that a mail item should be marked as unread when
        /// inserted into GMail.</summary>
        public static readonly MailItemPropertyElement UNREAD =
            new MailItemPropertyElement(MailItemProperty.IS_UNREAD);

        /// <summary>
        /// Constructs a new <code>MailItemPropertyElement</code>.
        /// </summary>
        public MailItemPropertyElement()
            : base(AppsMigrationNameTable.AppsMailItemProperty,
                   AppsMigrationNameTable.AppsPrefix,
                   AppsMigrationNameTable.AppsNamespace)
        { }

        /// <summary>
        /// Constructs a new <code>MailItemPropertyElement</code> with the specified
        /// value.
        /// </summary>
        /// <param name="value">the <code>MailItemProperty</code> value for this element</param>
        public MailItemPropertyElement(MailItemProperty value)
            : base(AppsMigrationNameTable.AppsMailItemProperty,
                   AppsMigrationNameTable.AppsPrefix,
                   AppsMigrationNameTable.AppsNamespace)
        {
            this.Value = value;
        }

        /// <summary>
        /// Value property accessor
        /// </summary>
        public MailItemProperty Value
        {
            get
            {
                return (MailItemProperty) Enum.Parse(typeof(MailItemProperty),
                    Convert.ToString(getAttributes()[BaseNameTable.XmlValue]),
                    true);
            }
            set
            {
                getAttributes()[BaseNameTable.XmlValue] = value.ToString();
            }
        }
    }
}
