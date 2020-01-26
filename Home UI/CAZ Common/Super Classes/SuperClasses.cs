using System;
using System.Collections.Generic;
using System.Text;

namespace CAZ_Common.Super_Classes
{
    public abstract class CAZDBConnection
    {
        public CAZDBConnection()
        {

        }

        public string ConnectionString;


        public (int key, string val1, string val2) GetTable()
        {
            return (1, "1", "2");
        }

        public void WriteTo()
        {

        }

    }
}
