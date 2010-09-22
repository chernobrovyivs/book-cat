using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookCat
{
    class GenreViewModel : INotifyPropertyChanged 
    {
        // все объекты
        readonly ObservableCollection<CGenre> _allGenres = new ObservableCollection<CGenre>();

        // ссылка на родителя, в самом родителе null
        readonly GenreViewModel _parentModel;

        // наш оборачиваемый объект
        readonly CGenre _genre;
        public CGenre Genre
        {
            get
            {
                return _genre;
            }
        }


        // детки
        readonly ReadOnlyCollection<GenreViewModel> _children;
        public ReadOnlyCollection<GenreViewModel> Children
        {
            get { return _children; }
        }

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

        #region IsSelected, IsExpanded
        bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }

                // Разворачиваем все элементы до корня
                if (_isExpanded && _parentModel != null)
                    _parentModel.IsExpanded = true;
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        // конструктор корня
        public GenreViewModel(ObservableCollection<CGenre> genres)
        {
            // в корневой модели най объект пустой, его не показываем нигде, но у него есть детки
            // _genre = new CGenre();

            _allGenres = genres;

            _children = new ReadOnlyCollection<GenreViewModel>(
                                (from child in _allGenres
                                 where child.Top_guid == Guid.Empty
                                 select new GenreViewModel(child, this)
                                 )
                                 .ToList());
        }

        // конструктор дочек
        private GenreViewModel(CGenre genre, GenreViewModel parentModel)
        {
            _genre = genre;

            _parentModel = parentModel;

            _allGenres = _parentModel._allGenres;

            _children = new ReadOnlyCollection<GenreViewModel>(
                    (from child in _allGenres
                     where child.Top_guid == _genre.Genre_guid
                     select new GenreViewModel(child, this)
                     )
                     .ToList());
        }

        public string Name
        {
            get { return _genre.Name; }
        }

        public Guid Genre_guid
        {
            get { return _genre.Genre_guid; }
        }

        public Guid Top_guid
        {
            get { return _genre.Top_guid; }
        }

    }
}
