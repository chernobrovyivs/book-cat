using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace BookCat
{
    public class CGenre : INotifyPropertyChanged
    {
        public override string ToString()
        {
            return Genre_guid.ToString();
        }

        public ObservableCollection<CGenre> parent;

            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Raises the <see cref="PropertyChanged"/> event for
            /// a given property.
            /// </summary>
            /// <param name="propertyName">The name of the changed property.</param>
            protected internal void OnPropertyChanged(string propertyName)
            {
                //validate the property name in debug builds
                VerifyProperty(propertyName);

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            /// <summary>
            /// Verifies whether the current class provides a property with a given
            /// name. This method is only invoked in debug builds, and results in
            /// a runtime exception if the <see cref="OnPropertyChanged"/> method
            /// is being invoked with an invalid property name. This may happen if
            /// a property's name was changed but not the parameter of the property's
            /// invocation of <see cref="OnPropertyChanged"/>.
            /// </summary>
            /// <param name="propertyName">The name of the changed property.</param>
            [Conditional("DEBUG")]
            private void VerifyProperty(string propertyName)
            {
                Type type = this.GetType();

                //look for a *public* property with the specified name
                PropertyInfo pi = type.GetProperty(propertyName);
                if (pi == null)
                {
                    //there is no matching property - notify the developer
                    string msg = "OnPropertyChanged was invoked with invalid property name {0}: ";
                    msg += "{0} is not a public property of {1}.";
                    msg = String.Format(msg, propertyName, type.FullName);
                    Debug.Fail(msg);
                }
            }
            #endregion

            public void CopyFrom(CGenre ci)
            {
                Genre_guid = ci.Genre_guid;
                Name = ci.Name;
                About = ci.About;
                Top_guid = ci.Top_guid;
                IsSelected = ci.IsSelected;
                IsExpanded = ci.IsExpanded;
                parent = ci.parent;
            }

            private string _about = "";
            public string About
            {
                get { return _about; }
                set
                {
                    _about = value;
                    OnPropertyChanged("About");
                }
            }

            private string _name = "";
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private bool _IsSelected;
            public bool IsSelected
            {
                get
                {
                    return _IsSelected;
                }
                set
                {
                    _IsSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }

            private bool _IsExpanded;
            public bool IsExpanded
            {
                get
                {
                    return _IsExpanded;
                }
                set
                {
                    _IsExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }

            private Guid _genre_guid;
            public Guid Genre_guid
            {
                get { return _genre_guid; }
                set
                {
                    _genre_guid = value;
                    OnPropertyChanged("Genre_guid");
                }
            }

            private Guid _top_guid;
            public Guid Top_guid
            {
                get { return _top_guid; }
                set
                {
                    _top_guid = value;
                    OnPropertyChanged("Top_guid");
                }
            }

            readonly List<CGenre> _children = new List<CGenre>();
            public IList<CGenre> Children
            {
                get { return _children; }
            }


            public ListCollectionView Childs
            {
                get
                {
                    ListCollectionView lcv = new ListCollectionView(parent);

                    lcv.Filter += delegate(object o)
                    {
                        if (o == null) return true;
                    
                        return (((CGenre)o).Top_guid == Genre_guid);
                    };

                    return lcv;
                }
            }
        }
}
