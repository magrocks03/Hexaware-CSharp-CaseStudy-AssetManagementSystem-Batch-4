using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.exception
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException()
            : base("Asset not found in the system.") { }

        public AssetNotFoundException(string message)
            : base(message) { }

        public AssetNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
