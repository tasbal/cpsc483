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

#define USE_TRACING
#define USE_LOGGING

using System;
using System.Xml; 
using System.Net;
using System.Diagnostics;
#if WindowsCE || PocketPC
#else 
using System.Runtime.Serialization;
#endif
using System.Security.Permissions;
using System.IO;
using System.Text; 



#endregion


//////////////////////////////////////////////////////////////////////
// <summary>custom exceptions</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>standard exception class to be used when authentication 
    /// using Google Client Login fails
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
#if WindowsCE || PocketPC
#else 
    [Serializable]
#endif
    public class AuthenticationException : LoggedException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public AuthenticationException() {}

        /// <summary>
        /// base constructor, takes a message text
        /// </summary> 
        /// <param name="msg"></param>
        public AuthenticationException(String msg) :  base(msg) {} 
     }

    /// <summary>thrown when the credentials are wrong</summary> 
    public class InvalidCredentialsException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public InvalidCredentialsException() {}
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public InvalidCredentialsException(String msg) :  base(msg) {} 
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>thrown when the account was deleted
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
     public class AccountDeletedException : AuthenticationException
     {
            //////////////////////////////////////////////////////////////////////
            /// <summary>default constructor so that FxCop does not complain</summary> 
            //////////////////////////////////////////////////////////////////////
            public AccountDeletedException() {}
            //////////////////////////////////////////////////////////////////////
            /// <summary>constructor taking a descriptive string</summary> 
            //////////////////////////////////////////////////////////////////////
            public AccountDeletedException(String msg) :  base(msg) {} 
     }

     //////////////////////////////////////////////////////////////////////
     /// <summary>thrown when the account was disabled
     /// </summary> 
     //////////////////////////////////////////////////////////////////////
      public class AccountDisabledException : AuthenticationException
      {
             //////////////////////////////////////////////////////////////////////
             /// <summary>default constructor so that FxCop does not complain</summary> 
             //////////////////////////////////////////////////////////////////////
             public AccountDisabledException() {}
            //////////////////////////////////////////////////////////////////////
            /// <summary>constructor taking a descriptive string</summary> 
            //////////////////////////////////////////////////////////////////////
            public AccountDisabledException(String msg) :  base(msg) {} 
      }

      //////////////////////////////////////////////////////////////////////
      /// <summary>the account hoder was not verified
      /// </summary> 
      //////////////////////////////////////////////////////////////////////
       public class NotVerifiedException : AuthenticationException
       {
            //////////////////////////////////////////////////////////////////////
            /// <summary>default constructor so that FxCop does not complain</summary> 
            //////////////////////////////////////////////////////////////////////
            public NotVerifiedException() {}
            //////////////////////////////////////////////////////////////////////
            /// <summary>constructor taking a descriptive string</summary> 
            //////////////////////////////////////////////////////////////////////
            public NotVerifiedException(String msg) :  base(msg) {} 
       }

       //////////////////////////////////////////////////////////////////////
       /// <summary>The Terms were not agreed with..
       /// </summary> 
       //////////////////////////////////////////////////////////////////////
        public class TermsNotAgreedException : AuthenticationException
        {
            //////////////////////////////////////////////////////////////////////
            /// <summary>default constructor so that FxCop does not complain</summary> 
            //////////////////////////////////////////////////////////////////////
            public TermsNotAgreedException() {}
            //////////////////////////////////////////////////////////////////////
            /// <summary>constructor taking a descriptive string</summary> 
            //////////////////////////////////////////////////////////////////////
            public TermsNotAgreedException(String msg) :  base(msg) {} 
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>The service is current not available
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
         public class ServiceUnavailableException : AuthenticationException
         {
            //////////////////////////////////////////////////////////////////////
            /// <summary>default constructor so that FxCop does not complain</summary> 
            //////////////////////////////////////////////////////////////////////
            public ServiceUnavailableException() {}
            //////////////////////////////////////////////////////////////////////
            /// <summary>constructor taking a descriptive string</summary> 
            //////////////////////////////////////////////////////////////////////
            public ServiceUnavailableException(String msg) :  base(msg) {} 
         }

         //////////////////////////////////////////////////////////////////////
         /// <summary>many unsuccessfull logins might create this...
         /// </summary> 
         //////////////////////////////////////////////////////////////////////
          public class CaptchaRequiredException : AuthenticationException
          {
             private string captchaUrl;
             private string captchaToken;

             //////////////////////////////////////////////////////////////////////
             /// <summary>default constructor so that FxCop does not complain</summary> 
             //////////////////////////////////////////////////////////////////////
             public CaptchaRequiredException() {}
  
             //////////////////////////////////////////////////////////////////////
             /// <summary>constructor taking a descriptive string</summary> 
             //////////////////////////////////////////////////////////////////////
             public CaptchaRequiredException(String msg, String url, String token) :  base(msg)
             {
                 this.captchaUrl = url;
                 this.captchaToken = token;
             }


              //////////////////////////////////////////////////////////////////////
              /// <summary>Read only accessor for captchaUrl</summary> 
              //////////////////////////////////////////////////////////////////////
              public string Url
              {
                  get {return this.captchaUrl;}
              }
              // end of accessor for captchaUrl
    
              //////////////////////////////////////////////////////////////////////
               /// <summary>Read only accessor for captchaToken</summary> 
              //////////////////////////////////////////////////////////////////////
              public string Token
              {
                  get {return this.captchaToken;}
              }
              // end of accessor for captchaToken
        }

} //end of file //////////////////////////////////////////////////////////////////////////
