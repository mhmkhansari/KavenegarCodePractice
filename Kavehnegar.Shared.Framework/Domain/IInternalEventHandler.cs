﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Shared.Framework.Domain
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
