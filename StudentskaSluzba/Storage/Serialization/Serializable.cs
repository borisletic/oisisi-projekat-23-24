using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.Storage.Serialization
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
