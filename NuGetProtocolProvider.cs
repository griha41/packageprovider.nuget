// 
//  Copyright (c) Microsoft Corporation. All rights reserved. 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace OneGet.ProtocolProvider.NuGet {
    using System.Collections;
    using System.Collections.Generic;
    using Utility;
    using Callback = System.Func<string, System.Collections.Generic.IEnumerable<object>, object>;

    public class NuGetProtocolProvider {
        #region implement ProtocolProvider-interface
/// <summary>
        ///     Returns the name of the Provider. Doesn't need callback .
        /// </summary>
        /// <returns></returns>
        public string GetProtocolProviderName() {
            return "NuGet";
        }
        public void InitializeProvider(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'InitializeProvider'" );
            }

        }
        public void Query(string name, string requiredVersion, string minimumVersion, string maximumVersion, Hashtable options,  Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'Query'" );
            }

        }
        public void GetItemMetadata(string item, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetItemMetadata'" );
            }

        }
        public void GetItemDetails(string item, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetItemDetails'" );
            }

        }
        public void DownloadItem(string item, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'DownloadItem'" );
            }

        }
        public void UnpackItem(string item, string folder, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'UnpackItem'" );
            }

        }
        public void InstallItem(string item, string folder, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'InstallItem'" );
            }

        }
        public bool IsValidPackageLocation(string location, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'IsValidPackageLocation'" );
            }

            return  default(bool);
        }
        public bool IdentifyFileAsItem(string filepath, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'IdentifyFileAsItem'" );
            }

            return  default(bool);
        }
        public bool IdentifyUnpackedLocationAsItem(string folder, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'IdentifyUnpackedLocationAsItem'" );
            }

            return  default(bool);
        }
        public IEnumerable<string> SupportedFileExtensions(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'SupportedFileExtensions'" );
            }

            return  default(IEnumerable<string>);
        }
        public IEnumerable<string> SupportedUriSchemes(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'SupportedUriSchemes'" );
            }

            return  default(IEnumerable<string>);
        }
        public bool GetOptionNames(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetOptionNames'" );
            }

            return  default(bool);
        }
        public bool GetOptionDefinition(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetOptionDefinition'" );
            }

            return  default(bool);
        }

                //
        public void Find(string location, string name, string requiredVersion, string minimumVersion, string maximumVersion, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'Find'" );
            }

        }

        //
        public bool GetOptionDefinitions(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetOptionDefinitions'" );
            }

            return  default(bool);
        }

        #endregion

    }
}