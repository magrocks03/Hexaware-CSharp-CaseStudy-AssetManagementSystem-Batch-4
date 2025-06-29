using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.exception
{
    public class AssetNotMaintainException : Exception
    {
        public AssetNotMaintainException()
            : base("The asset has not been maintained in the last 2 years.") { }

        public AssetNotMaintainException(string message)
            : base(message) { }

        public AssetNotMaintainException(string message, Exception inner)
            : base(message, inner) { }
    }
}
