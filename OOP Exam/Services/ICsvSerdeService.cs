using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Services
{
    interface ICsvSerdeService
    {
        IEnumerable<T> Deserialize<T>(string path, char delimiter);
        void Serialize<T>(string path, char delimiter, IEnumerable<T> value);
    }
}
