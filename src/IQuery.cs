using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUp
{
    public interface IQuery<T>
    {
        SqlBuilder<T> Select();
        SqlBuilder<T> Update();
        SqlBuilder<T> Delete();
        SqlBuilder<T> Insert();
    }
}
