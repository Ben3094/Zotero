using System;
using System.Collections.Generic;
using System.Text;

namespace Zotero
{
    public class UnknownItem : Item { }

    public class Email : Item
    {
        private string subject;
        public string Subject
        {
            get { return this.subject; }
            set
            {
                this.subject = value;
                OnPropertyChanged();
            }
        }
    }

    public class Case : Item
    {
        private string caseName;
        public string CaseName
        {
            get { return this.caseName; }
            set
            {
                this.caseName = value;
                OnPropertyChanged();
            }
        }

        //TODO: create a specific model for authors based on ...
        private string reporter;
        public string Reporter
        {
            get { return this.reporter; }
            set
            {
                this.reporter = value;
                OnPropertyChanged();
            }
        }

        private string reporterVolume;
        public string ReporterVolume
        {
            get { return this.reporterVolume; }
            set
            {
                this.reporterVolume = value;
                OnPropertyChanged();
            }
        }

        //TODO: Create a model for places (with location)
        private string court;
        public string Court
        {
            get { return this.court; }
            set
            {
                this.court = value;
                OnPropertyChanged();
            }
        }

        //TODO: A model for docker number ?
        private string dockerNumber;
        public string DockerNumber
        {
            get { return this.dockerNumber; }
            set
            {
                this.dockerNumber = value;
                OnPropertyChanged();
            }
        }

        private string firstPage;
        public string FirstPage
        {
            get { return this.firstPage; }
            set
            {
                this.firstPage = value;
                OnPropertyChanged();
            }
        }

        private DateTime decidedDate;
        public DateTime DecidedDate
        {
            get { return this.decidedDate; }
            set
            {
                this.decidedDate = value;
                OnPropertyChanged();
            }
        }
    }

    public class Book : Item
    {
        private string edition;
        public string Edition
        {
            get { return this.edition; }
            set
            {
                this.edition = value;
                OnPropertyChanged();
            }
        }

        private string place;
        public string Place
        {
            get { return this.place; }
            set
            {
                this.place = value;
                OnPropertyChanged();
            }
        }

        private string publisher;
        public string Publisher
        {
            get { return this.publisher; }
            set
            {
                this.publisher = value;
                OnPropertyChanged();
            }
        }

        private string archive;
        public string Archive
        {
            get { return this.archive; }
            set
            {
                this.archive = value;
                OnPropertyChanged();
            }
        }

        private string archiveLocation;
        public string ArchiveLocation
        {
            get { return this.archiveLocation; }
            set
            {
                this.archiveLocation = value;
                OnPropertyChanged();
            }
        }

        private string libraryCatalog;
        public string LibraryCatalog
        {
            get { return this.libraryCatalog; }
            set
            {
                this.libraryCatalog = value;
                OnPropertyChanged();
            }
        }

        private string callNumber;
        public string CallNumber
        {
            get { return this.callNumber; }
            set
            {
                this.callNumber = value;
                OnPropertyChanged();
            }
        }

        private string isbn;
        public string ISBN
        {
            get { return this.isbn; }
            set
            {
                this.isbn = value;
                OnPropertyChanged();
            }
        }

        private int numPages;
        public int NumPages
        {
            get { return this.numPages; }
            set
            {
                this.numPages = value;
                OnPropertyChanged();
            }
        }

        private string series;
        public string Series
        {
            get { return this.series; }
            set
            {
                this.series = value;
                OnPropertyChanged();
            }
        }

        private int seriesNumber;
        public int SeriesNumber
        {
            get { return this.seriesNumber; }
            set
            {
                this.seriesNumber = value;
                OnPropertyChanged();
            }
        }

        private string volume;
        public string Volume
        {
            get { return this.volume; }
            set
            {
                this.volume = value;
                OnPropertyChanged();
            }
        }

        private int volumeNumber;
        public int VolumeNumber
        {
            get { return this.volumeNumber; }
            set
            {
                this.volumeNumber = value;
                OnPropertyChanged();
            }
        }
    }
}
