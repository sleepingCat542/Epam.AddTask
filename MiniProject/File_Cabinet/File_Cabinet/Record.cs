using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Cabinet
{
    /// <summary>
    /// Class that provides record
    /// </summary>
    class Record
    {
        /// <summary>
        /// Variable for auto increase index
        /// </summary>
        static int IndexNumber=1;

        /// <summary>
        /// Array with free index. Free index is formed after deleting a record
        /// </summary>
        List<int> _arrayOfFreeIndices = new List<int>();


        int _index;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        DateTime _dateOfBirth;

        /// <summary>
        /// Property for _index <see cref="_index"/>
        /// </summary>
        public int Index
        {
            get => _index;
            set
            {
                if (_arrayOfFreeIndices.Count!=0)
                foreach(int freeIndex in _arrayOfFreeIndices)
                {
                        _index = freeIndex;
                }
                else
                    _index = IndexNumber++;
            }
        }


        /// <summary>
        /// Property for _dateOfBirth <see cref="_dateOfBirth"/>
        /// </summary>
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Today)
                    throw new Exception("Incorrect date of birth");
                else
                    _dateOfBirth = value;
            }
        }

        /// <summary>
        /// Provides the instance of class <see cref="Record"/>
        /// </summary>
        /// <param name="fName">First name</param>
        /// <param name="lName">Last name</param>
        /// <param name="dateBirth">Date of birth</param>
        public Record(string fName, string lName, DateTime dateBirth)
        {
            _index = Index;
            this.FirstName = fName;
            this.LastName = lName;
            DateOfBirth = dateBirth;
        }

        /// <summary>
        /// Provides the instance of class <see cref="Record"/>
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="fName">First name</param>
        /// <param name="lName">Last name</param>
        /// <param name="dateBirth">Date of birth</param>
        public Record(int index, string fName, string lName, DateTime dateBirth)
        {
            this.Index=index;
            this.FirstName = fName;
            this.LastName = lName;
            DateOfBirth = dateBirth;
        }

    }

}
