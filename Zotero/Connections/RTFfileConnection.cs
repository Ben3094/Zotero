using System;
using System.Collections.Generic;
using System.Text;
using Zotero;

namespace Zotero.Connections
{
    public class RDFfileConnection : Connection
    {
        public override void Add(ZoteroObject objectToAdd)
        {
            throw new NotImplementedException();
        }

        public override Library[] Dump()
        {
            throw new NotImplementedException();
        }

        public override void Remove(string IDToDelete)
        {
            throw new NotImplementedException();
        }

        protected override void ConnectionProcedure()
        {
            throw new NotImplementedException();
        }

        protected override void DisconnectionProcedure()
        {
            throw new NotImplementedException();
        }
    }
}
