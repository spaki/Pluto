using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Commands.Common
{
    public abstract class Command : Message
    {
        public DateTime DateTime { get; private set; }

        protected Command()
        {
            DateTime = DateTime.Now;
        }
    }
}
