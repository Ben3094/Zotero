using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Zotero
{
    public enum CreatorType
    {
        Author = 1,
        Contributor = 2,
        Editor = 3,
        Translator = 4,
        SeriesEditor = 5,
        Interviewee = 6,
        Interviewer = 7,
        Director = 8,
        Scriptwriter = 9,
        Producer = 10,
        CastMember = 11,
        Sponsor = 12,
        Counsel = 13,
        Inventor = 14,
        AttorneyAgent = 15,
        Recipient = 16,
        Performer = 17,
        Composer = 18,
        WordsBy = 19,
        Cartographer = 20,
        Programmer = 21,
        Artist = 22,
        Commenter = 23,
        Presenter = 24,
        Guest = 25,
        Podcaster = 26,
        ReviewedAuthor = 27,
        Cosponsor = 28,
        BookAuthor = 29
    }
    public class Creator : ZoteroObject
    {
        public Creator(CreatorType creatorType, string firstName, string lastName) { this.firstName = firstName; this.lastName = lastName; }
        private CreatorType type;
        public CreatorType Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                OnPropertyChanged();
            }
        }
        private string firstName = "";
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                this.firstName = value;
                OnPropertyChanged();
            }
        }
        private string lastName = "";
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                this.lastName = value;
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
}
