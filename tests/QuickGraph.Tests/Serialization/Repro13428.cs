    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Serialization
{

    #region Enumerations
    /// 
    /// Enumeration of the person's gender
    /// 
    public enum Gender
    {
        /// 
        /// Male gender.
        /// 
        Male,

        /// 
        /// Female gender.
        /// 
        Female
    }

    /// 
    /// Enumeration of the person's age group
    /// 
    public enum AgeGroup
    {
        /// 
        /// Unknown age group.
        /// 
        Unknown,

        /// 
        /// 0 to 20 age group.
        /// 
        Youth,

        /// 
        /// 20 to 40 age group.
        /// 
        Adult,

        /// 
        /// 40 to 65 age group.
        /// 
        MiddleAge,

        /// 
        /// Over 65 age group.
        /// 
        Senior
    }

    #endregion

    /// 
    /// Representation for a single serializable Person.
    /// INotifyPropertyChanged allows properties of the Person class to
    /// participate as source in data bindings.
    /// 
    [Serializable]
    public class Person : INotifyPropertyChanged, IEquatable<Person>, IDataErrorInfo
    {
        #region Fields and Constants

        private const string DefaultFirstName = "Unknown";
        private string id;
        private string firstName;
        private string lastName;
        private string middleName;
        private string suffix;
        private string nickName;
        private string maidenName;
        private Gender gender;
        private DateTime? birthDate;
        private string birthPlace;
        private DateTime? deathDate;
        private string deathPlace;
        private bool isLiving;

        #endregion

        #region Constructors

        /// 
        /// Initializes a new instance of the Person class.
        /// Each new instance will be given a unique identifier.
        /// This parameterless constructor is also required for serialization.
        /// 
        public Person()
        {
            this.id = Guid.NewGuid().ToString();
            this.firstName = DefaultFirstName;
            this.isLiving = true;
        }

        /// 
        /// Initializes a new instance of the person class with the firstname and the lastname.
        /// 
        /// <param name="firstName" />First name.
        /// <param name="lastName" />Last name.
        public Person(string firstName, string lastName)
            : this()
        {
            // Use the first name if specified, if not, the default first name is used.
            if (!string.IsNullOrEmpty(firstName))
            {
                this.firstName = firstName;
            }

            this.lastName = lastName;
        }

        /// 
        /// Initializes a new instance of the person class with the firstname, the lastname, and gender
        /// 
        /// <param name="firstName" />First name.
        /// <param name="lastName" />Last name.
        /// <param name="gender" />Gender of the person.
        public Person(string firstName, string lastName, Gender gender)
            : this(firstName, lastName)
        {
            this.gender = gender;
        }

        #endregion

        #region Properties

        /// 
        /// Gets or sets the unique identifier for each person.
        /// 
        [XmlAttribute]
        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        /// 
        /// Gets or sets the name that occurs first in a given name
        /// 
        [XmlElement]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged("FirstName");
                    this.OnPropertyChanged("Name");
                    this.OnPropertyChanged("FullName");
                }
            }
        }

        /// 
        /// Gets or sets the part of a given name that indicates what family the person belongs to. 
        /// 
        [XmlElement]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.OnPropertyChanged("LastName");
                    this.OnPropertyChanged("Name");
                    this.OnPropertyChanged("FullName");
                }
            }
        }

        /// 
        /// Gets or sets the name that occurs between the first and last name.
        /// 
        [XmlElement]
        public string MiddleName
        {
            get
            {
                return this.middleName;
            }

            set
            {
                if (this.middleName != value)
                {
                    this.middleName = value;
                    this.OnPropertyChanged("MiddleName");
                    this.OnPropertyChanged("FullName");
                }
            }
        }

        /// 
        /// Gets the person's name in the format FirstName LastName.
        /// 
        [XmlIgnore]
        public string Name
        {
            get
            {
                string name = String.Empty;
                if (!string.IsNullOrEmpty(this.firstName))
                {
                    name += this.firstName;
                }

                if (!string.IsNullOrEmpty(this.lastName))
                {
                    name += " " + this.lastName;
                }

                return name;
            }
        }

        /// 
        /// Gets the person's fully qualified name: Firstname MiddleName LastName Suffix
        /// 
        [XmlIgnore]
        public string FullName
        {
            get
            {
                string fullName = String.Empty;
                if (!string.IsNullOrEmpty(this.firstName))
                {
                    fullName += this.firstName;
                }

                if (!string.IsNullOrEmpty(this.middleName))
                {
                    fullName += " " + this.middleName;
                }

                if (!string.IsNullOrEmpty(this.lastName))
                {
                    fullName += " " + this.lastName;
                }

                if (!string.IsNullOrEmpty(this.suffix))
                {
                    fullName += " " + this.suffix;
                }

                return fullName;
            }
        }

        /// 
        /// Gets or sets the text that appear behind the last name providing additional information about the person.
        /// 
        [XmlElement]
        public string Suffix
        {

            get
            {
                return this.suffix;
            }

            set
            {
                if (this.suffix != value)
                {
                    this.suffix = value;
                    this.OnPropertyChanged("Suffix");
                    this.OnPropertyChanged("FullName");
                }
            }
        }

        /// 
        /// Gets or sets the person's familiar or shortened name
        /// 
        [XmlElement]
        public string NickName
        {
            get
            {
                return this.nickName;
            }

            set
            {
                if (this.nickName != value)
                {
                    this.nickName = value;
                    this.OnPropertyChanged("NickName");
                }
            }
        }

        /// 
        /// Gets or sets the person's name carried before marriage
        /// 
        [XmlElement]
        public string MaidenName
        {
            get
            {
                return this.maidenName;
            }

            set
            {
                if (this.maidenName != value)
                {
                    this.maidenName = value;
                    this.OnPropertyChanged("MaidenName");
                }
            }
        }

        /// 
        /// Gets or sets the person's gender
        /// 
        [XmlElement]
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                if (this.gender != value)
                {
                    this.gender = value;
                    this.OnPropertyChanged("Gender");
                }
            }
        }

        /// 
        /// Gets the age of the person.
        /// 
        [XmlIgnore]
        public int? Age
        {
            get
            {
                if (this.BirthDate == null)
                {
                    return null;
                }

                // Determine the age of the person based on just the year.
                DateTime startDate = this.BirthDate.Value;
                DateTime endDate = (this.IsLiving || this.DeathDate == null) ? DateTime.Now : this.DeathDate.Value;
                int age = endDate.Year - startDate.Year;

                // Compensate for the month and day of month (if they have not had a birthday this year).
                if (endDate.Month < startDate.Month || (endDate.Month == startDate.Month && endDate.Day < startDate.Day))
                {
                    age--;
                }

                return Math.Max(0, age);
            }
        }

        /// 
        /// Gets the age of the person.
        /// 
        [XmlIgnore]
        public AgeGroup AgeGroup
        {
            get
            {
                AgeGroup ageGroup = AgeGroup.Unknown;

                if (this.Age.HasValue)
                {
                    // The AgeGroup enumeration is defined later in this file. It is up to the Person
                    // class to define the ages that fall into the particular age groups
                    if (this.Age >= 0 && this.Age < 20)
                    {
                        ageGroup = AgeGroup.Youth;
                    }
                    else if (this.Age >= 20 && this.Age < 40)
                    {
                        ageGroup = AgeGroup.Adult;
                    }
                    else if (this.Age >= 40 && this.Age < 65)
                    {
                        ageGroup = AgeGroup.MiddleAge;
                    }
                    else
                    {
                        ageGroup = AgeGroup.Senior;
                    }
                }

                return ageGroup;
            }
        }

        /// 
        /// Gets the year the person was born
        /// 
        [XmlIgnore]
        public string YearOfBirth
        {
            get
            {
                if (this.birthDate.HasValue)
                {
                    return this.birthDate.Value.Year.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    return "-";
                }
            }
        }

        /// 
        /// Gets the year the person died
        /// 
        [XmlIgnore]
        public string YearOfDeath
        {
            get
            {
                if (this.deathDate.HasValue && !this.isLiving)
                {
                    return this.deathDate.Value.Year.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    return "-";
                }
            }
        }

        /// 
        /// Gets or sets the person's birth date.  This property can be null.
        /// 
        [XmlElement]
        public DateTime? BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (this.birthDate == null || this.birthDate != value)
                {
                    this.birthDate = value;
                    this.OnPropertyChanged("BirthDate");
                    this.OnPropertyChanged("Age");
                    this.OnPropertyChanged("AgeGroup");
                    this.OnPropertyChanged("YearOfBirth");
                    this.OnPropertyChanged("BirthMonthAndDay");
                    this.OnPropertyChanged("BirthDateAndPlace");
                }
            }
        }

        /// 
        /// Gets or sets the person's place of birth
        /// 
        [XmlElement]
        public string BirthPlace
        {
            get
            {
                return this.birthPlace;
            }

            set
            {
                if (this.birthPlace != value)
                {
                    this.birthPlace = value;
                    this.OnPropertyChanged("BirthPlace");
                    this.OnPropertyChanged("BirthDateAndPlace");
                }
            }
        }

        /// 
        /// Gets the month and day the person was born in. This property can be null.
        /// 
        [XmlIgnore]
        public string BirthMonthAndDay
        {
            get
            {
                if (this.birthDate == null)
                {
                    return null;
                }
                else
                {
                    return this.birthDate.Value.ToString(
                        DateTimeFormatInfo.CurrentInfo.MonthDayPattern,
                        CultureInfo.CurrentCulture);
                }
            }
        }

        /// 
        /// Gets a friendly string for BirthDate and Place
        /// 
        [XmlIgnore]
        public string BirthDateAndPlace
        {
            get
            {
                if (this.birthDate == null)
                {
                    return null;
                }
                else
                {
                    StringBuilder returnValue = new StringBuilder();
                    returnValue.Append("Born ");
                    returnValue.Append(
                        this.birthDate.Value.ToString(
                            DateTimeFormatInfo.CurrentInfo.ShortDatePattern,
                            CultureInfo.CurrentCulture));

                    if (!string.IsNullOrEmpty(this.birthPlace))
                    {
                        returnValue.Append(", ");
                        returnValue.Append(this.birthPlace);
                    }

                    return returnValue.ToString();
                }
            }
        }

        /// 
        /// Gets or sets the person's death of death.  This property can be null.
        /// 
        [XmlElement]
        public DateTime? DeathDate
        {
            get
            {
                return this.deathDate;
            }

            set
            {
                if (this.deathDate == null || this.deathDate != value)
                {
                    this.IsLiving = false;
                    this.deathDate = value;
                    this.OnPropertyChanged("DeathDate");
                    this.OnPropertyChanged("Age");
                    this.OnPropertyChanged("YearOfDeath");
                }
            }
        }

        /// 
        /// Gets or sets the person's place of death
        /// 
        [XmlElement]
        public string DeathPlace
        {
            get
            {
                return this.deathPlace;
            }

            set
            {
                if (this.deathPlace != value)
                {
                    this.IsLiving = false;
                    this.deathPlace = value;
                    this.OnPropertyChanged("DeathPlace");
                }
            }
        }

        /// 
        /// Gets or sets a value indicating whether the person is still alive or deceased.
        /// 
        [XmlElement]
        public bool IsLiving
        {
            get
            {
                return this.isLiving;
            }

            set
            {
                if (this.isLiving != value)
                {
                    this.isLiving = value;
                    this.OnPropertyChanged("IsLiving");
                }
            }
        }

        /// 
        /// Gets a string that describes this person to their parents.
        /// 
        [XmlIgnore]
        public string ParentRelationshipText
        {
            get
            {
                if (this.gender == Gender.Male)
                {
                    return "Son";
                }
                else
                {
                    return "Daughter";
                }
            }
        }

        /// 
        /// Gets a string that describes this person to their siblings.
        /// 
        [XmlIgnore]
        public string SiblingRelationshipText
        {
            get
            {
                if (this.gender == Gender.Male)
                {
                    return "Brother";
                }
                else
                {
                    return "Sister";
                }
            }
        }

        /// 
        /// Gets a string that describes this person to their spouses.
        /// 
        [XmlIgnore]
        public string SpouseRelationshipText
        {
            get
            {
                if (this.gender == Gender.Male)
                {
                    return "Husband";
                }
                else
                {
                    return "Wife";
                }
            }
        }

        /// 
        /// Gets a string that describes this person to their children.
        /// 
        [XmlIgnore]
        public string ChildRelationshipText
        {
            get
            {
                if (this.gender == Gender.Male)
                {
                    return "Father";
                }
                else
                {
                    return "Mother";
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// 
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// 
        public event PropertyChangedEventHandler PropertyChanged;

        /// 
        /// Fires the event for the property when it changes.
        /// 
        /// <param name="propertyName" />Property name.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IEquatable Members

        /// 
        /// Determine equality between two person classes
        /// 
        /// <param name="other" />An object to compare with this object.
        /// true if the current object is equal to the other parameter; otherwise, false. 
        public bool Equals(Person other)
        {
            return this.Id == other.Id;
        }

        #endregion

        #region Methods

        /// 
        /// Returns a String that represents the current Object.
        /// 
        /// A String that represents the current Object.
        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region IDataErrorInfo Members

        /// 
        /// Gets an error message indicating what is wrong with this object.
        /// 
        public string Error
        {
            get { return null; }
        }

        /// 
        /// Gets the error message for the property with the given name.
        /// 
        /// <param name="columnName" />The name of the property whose error message to get.
        /// The error message for the property. The default is an empty string ("").
        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                if (columnName == "BirthDate")
                {
                    if (this.BirthDate == DateTime.MinValue)
                    {
                        result = "This does not appear to be a valid date.";
                    }
                }

                if (columnName == "DeathDate")
                {
                    if (this.DeathDate == DateTime.MinValue)
                    {
                        result = "This does not appear to be a valid date.";
                    }
                }

                return result;
            }
        }

        #endregion
    }

    [TestClass]
    public class Repro13482Test
    {
        [TestMethod]
        public void Repro13482()
        {
            var graph = new AdjacencyGraph<Person, TaggedEdge<Person, string>>();

            Person jacob = new Person("Jacob", "Hochstetler")
            {
                BirthDate = new DateTime(1712, 01, 01),
                BirthPlace = "Alsace, France",
                DeathDate = new DateTime(1776, 01, 01),
                DeathPlace = "Pennsylvania, USA",
                Gender = Gender.Male
            };

            Person john = new Person("John", "Hochstetler")
            {
                BirthDate = new DateTime(1735, 01, 01),
                BirthPlace = "Alsace, France",
                DeathDate = new DateTime(1805, 04, 15),
                DeathPlace = "Summit Mills, PA",
                Gender = Gender.Male
            };

            Person jonathon = new Person("Jonathon", "Hochstetler")
            {
                BirthPlace = "Pennsylvania",
                DeathDate = new DateTime(1823, 05, 08),
                Gender = Gender.Male,
            };

            Person emanuel = new Person("Emanuel", "Hochstedler")
            {
                BirthDate = new DateTime(1855, 01, 01),
                DeathDate = new DateTime(1900, 01, 01),
                Gender = Gender.Male
            };

            graph.AddVerticesAndEdge(new TaggedEdge<Person, string>(jacob, john, jacob.ChildRelationshipText));
            graph.AddVerticesAndEdge(new TaggedEdge<Person, string>(john, jonathon, john.ChildRelationshipText));
            graph.AddVerticesAndEdge(new TaggedEdge<Person, string>(jonathon, emanuel, jonathon.ChildRelationshipText));

            var settings = new XmlWriterSettings() { Indent = true, IndentChars = @"    " };
            using (var writer = XmlWriter.Create(Console.Out, settings))
            {
                SerializationExtensions.SerializeToXml(
                    graph,
                    writer,
                    v => v.Id,
                    AlgorithmExtensions.GetEdgeIdentity(graph),
                    "graph",
                    "person",
                    "relationship",
                    "");
            }
        }
    }
}
