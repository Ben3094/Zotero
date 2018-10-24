using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using SQLite.Net.Platform.Generic;

namespace Zotero
{
    public class OnlineUserConnection : Connection
    {
        public OnlineUserConnection(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                OnPropertyChanged();
            }
        }

        #region CONNECTION
        protected override void ConnectionProcedure()
        {
            throw new NotImplementedException();
        }

        protected override void DisconnectionProcedure()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LIBRARY MANAGEMENT
        public override void Add(ZoteroObject objectToAdd)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string IDToDelete)
        {
            throw new NotImplementedException();
        }

        public override Library[] Dump()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class OnlineLibraryConnection : Connection
    {
        public static string OnlineURIScheme = "http";
        public static string OnlineURIHost = "api.zotero.org";
        private string IDPrefix;
        public static UriBuilder LocalBaseURI = new UriBuilder(OnlineURIScheme, OnlineURIHost);
        private HttpClient HttpClient = new HttpClient();
        private KeyValuePair<string, string> AuthenticationQuery;
        private const string AUTHENTICATION_QUERY_FIELD = "key";

        public OnlineLibraryConnection(LibraryTypeEnum libraryType, string libraryID, string key)
        {
            this.LibraryID = libraryID;
            this.LibraryType = libraryType;

            switch (libraryType)
            {
                case LibraryTypeEnum.Group:
                    this.IDPrefix = "groups/" + this.LibraryID;
                    break;
                case LibraryTypeEnum.User:
                    this.IDPrefix = "users/" + this.LibraryID;
                    break;
            }

            this.AuthenticationQuery = new KeyValuePair<string, string>(AUTHENTICATION_QUERY_FIELD, key);
        }

        private string libraryID;
        public string LibraryID
        {
            get { return this.libraryID; }
            private set { this.libraryID = value; }
        }

        public enum LibraryTypeEnum { User, Group }
        private LibraryTypeEnum libraryType;
        public LibraryTypeEnum LibraryType
        {
            get { return this.libraryType; }
            private set { this.libraryType = value; }
        }

        private const string KeyParameter = "key";

        private string key;
        /// <summary>
        /// Key to access the online Zotero library with the API (you can create a public key for a library in https://www.zotero.org/settings/keys)
        /// </summary>
        public string Key
        {
            get { return this.key; }
            private set { this.key = value; }
        }

        #region CONNECTION
        protected override void ConnectionProcedure()
        {
            throw new NotImplementedException();
        }

        protected override void DisconnectionProcedure()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LIBRARY MANAGEMENT
        public override void Add(ZoteroObject objectToAdd)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string IDToDelete)
        {
            throw new NotImplementedException();
        }

        private const string LibraryItemsPath = "items";

        public override Library[] Dump()
        {
            Library dumpedLibrary = new UserLibrary();

            UriBuilder dumpUriBuilder = LocalBaseURI;
            dumpUriBuilder.Path = string.Join("/", this.IDPrefix, LibraryItemsPath);
            dumpUriBuilder.Query = ComposeURIQuery(AuthenticationQuery);

            Task<HttpResponseMessage> dumpRequestTask = HttpClient.GetAsync(dumpUriBuilder.ToString());
            dumpRequestTask.Wait();
            HttpResponseMessage dumpRequestResponse = dumpRequestTask.Result;

            if (dumpRequestResponse.IsSuccessStatusCode)
            {
                Task<string> readJSONDumpResponse = dumpRequestResponse.Content.ReadAsStringAsync();
                readJSONDumpResponse.Wait();
                string JSONDumpResponse = readJSONDumpResponse.Result;
                JSONNode node = new JSONNode(JSONDumpResponse);
                Newtonsoft.Json.Linq.JObject mainNode = Newtonsoft.Json.Linq.JObject.Parse(JSONDumpResponse);
            }

            return new Library[] { dumpedLibrary };
        }
        #endregion

        #region HELPERS
        public static string ComposeURIQuery(params KeyValuePair<string, string>[] queries)
        {
            string completeQuery = "";

            foreach (KeyValuePair<string, string> query in queries)
                completeQuery += query.Key + '=' + query.Value + '&';

            return completeQuery.TrimEnd('&'); 
        }
        #endregion
    }

    public class LocalLibraryConnection : Connection
    {
        public static string LocalURIScheme = "http";
        public static string LocalURIHost = IPAddress.Loopback.ToString();
        public static int LocalURIPort = 23119;
        public static string ConnectorPath = "connector";
        public static UriBuilder LocalBaseURI = new UriBuilder(LocalURIScheme, LocalURIHost, LocalURIPort, ConnectorPath);
        private HttpClient HttpClient = new HttpClient();

        public static string htmlPageRoot = "html";
        public static string htmlPageHeader = "head";
        public static string htmlPageTitle = "title";

        #region CONNECTION
        public static string PingCommmandPath = "ping";

        public static string pingHtmlPageTitle = "Zotero Connector Server is Available";

        protected override void ConnectionProcedure()
        {
            //Ping local server
            UriBuilder localPingURLBuilder = LocalBaseURI;
            LocalBaseURI.Path = string.Join("/", ConnectorPath, PingCommmandPath);
            HttpClient.DefaultRequestHeaders.Host = localPingURLBuilder.Host;
            Task<HttpResponseMessage> pingTask = HttpClient.GetAsync(localPingURLBuilder.ToString());
            pingTask.Wait();
            HttpResponseMessage pingResponse = pingTask.Result;

            //Response parsing
            XmlDocument htmlResponsePage = new XmlDocument();
            Task<string> getHTMLResponsePageTask = pingResponse.Content.ReadAsStringAsync();
            htmlResponsePage.LoadXml(getHTMLResponsePageTask.Result.Remove(0, 15));
            XmlNode titleNode = htmlResponsePage.SelectSingleNode(string.Join("/", htmlPageRoot, htmlPageHeader, htmlPageTitle));

            //Check response
            if (titleNode.InnerText != pingHtmlPageTitle)
                throw new IOException("Zotero connector server is not available");
            else
                this.Status = StatusEnum.Connected;
        }

        protected override void DisconnectionProcedure()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LIBRARY MANAGEMENT
        public override void Add(ZoteroObject objectToAdd)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string IDToDelete)
        {
            throw new NotImplementedException();
        }

        public override Library[] Dump()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region HELPERS
        private async Task<HttpResponseMessage> HttpPost(Uri address, string contentType, string content)
        {
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            HttpClient.DefaultRequestHeaders.Host = address.Host;

            StringContent httpContent = new StringContent(content, System.Text.Encoding.UTF8, contentType);

            return await HttpClient.PostAsync(address, httpContent);
        }
        #endregion
    }


    public abstract class Connection : INotifyPropertyChanged
    {
        #region CONNECTION
        public enum StatusEnum { Unknown, Connecting, Connected, Disconnected, Error }
        public StatusEnum Status;
        public void Connect()
        {
            try
            {
                this.Status = StatusEnum.Connecting;
                this.ConnectionProcedure();
                this.Status = StatusEnum.Connected;
            }
            catch (Exception) { this.Status = StatusEnum.Error; }
        }
        protected abstract void ConnectionProcedure();
        public void Disconnect()
        {
            try
            {
                this.DisconnectionProcedure();
                this.Status = StatusEnum.Disconnected;
            }
            catch (Exception) { this.Status = StatusEnum.Error; }
        }
        protected abstract void DisconnectionProcedure();
        #endregion

        #region LIBRARY MANAGEMENT
        public abstract void Add(ZoteroObject objectToAdd);
        public void Remove(ZoteroObject objectToDelete)
        {
            this.Remove(objectToDelete.ID);
        }
        public abstract void Remove(string IDToDelete);

        public abstract Library[] Dump();
        #endregion

        /// <summary>
        /// Fired when a property is modified in this class
        /// </summary>
        /// <remarks>Do not only work with "magic", you need to call "OnPropertyChanged()" method in inner properties "set" methods.</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fire "PropertyChanged" event with automatic property name completion when called from properties "set" methods
        /// </summary>
        /// <param name="propertyName">Modified property name</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
