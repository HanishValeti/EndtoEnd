using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndtoEnd.MoqTests
{
    public interface IHttpClientWrapper
    {
        Uri BaseAddress { get; } 
    }
}
