using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Zotero
{
    public abstract class ZoteroObject : INotifyPropertyChanged
    {
        private string id;
        public string ID
        {
            get { return this.id; }
            set
            {
                this.id = value;
                OnPropertyChanged();
            }
        }

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

    public abstract class Container : ZoteroObject
    {
        public ObservableCollection<ZoteroObject> InnerObjects = new ObservableCollection<ZoteroObject>(); //TODO: Change to prevent storage of libraries
    }

    public abstract class Library : Container { }
    public class UserLibrary : Library { }
    public class GroupLibrary : Library { }

    public class Collection : Container { }
    
    public abstract class Item : ZoteroObject
    {
        public readonly ObservableCollection<Creator> Creators = new ObservableCollection<Creator>();

        private string title;
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                OnPropertyChanged();
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                this.date = value;
                OnPropertyChanged();
            }
        }

        private CultureInfo language;
        public CultureInfo Language
        {
            get { return this.language; }
            set
            {
                this.language = value;
                OnPropertyChanged();
            }
        }

        private string abstractNote;
        public string AbstractNote
        {
            get { return this.abstractNote; }
            set
            {
                this.abstractNote = value;
                OnPropertyChanged();
            }
        }

        private string shortTitle;
        public string ShortTitle
        {
            get { return this.shortTitle; }
            set
            {
                this.shortTitle = value;
                OnPropertyChanged();
            }
        }

        private DateTime accessDate;
        public DateTime AccessDate
        {
            get { return this.accessDate; }
            set
            {
                this.accessDate = value;
                OnPropertyChanged();
            }
        }

        private string rights;
        public string Rights
        {
            get { return this.rights; }
            set
            {
                this.rights = value;
                OnPropertyChanged();
            }
        }

        private string extra;
        public string Extra
        {
            get { return this.extra; }
            set
            {
                this.extra = value;
                OnPropertyChanged();
            }
        }

        private Uri url;
        public Uri URL
        {
            get { return this.url; }
            set
            {
                this.url = value;
                OnPropertyChanged();
            }
        }

        public readonly ObservableCollection<Tag> Tags = new ObservableCollection<Tag>();
    }

    public class ZoteroFieldAttribute : Attribute
    {
        public ZoteroFieldAttribute(string fieldName) { this.FieldName = fieldName; }
        public readonly string FieldName;
    }
}
