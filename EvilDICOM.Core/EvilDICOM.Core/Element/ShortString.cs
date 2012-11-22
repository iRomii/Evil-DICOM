<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public sealed class ShortString : AbstractElement<string>
    {
        public override VR VR
        {
            get { return VR.ShortString; }
        }

        public override string Data
        {
            get { return _data; }
            set { _data = DataRestriction.EnforceLengthRestriction(16, value); }
        }

        public ShortString() { }

        public ShortString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class ShortString : AbstractElement<string>
    {
        public new string Data
        {
            get { return base.Data; }
            set { base.Data = DataRestriction.EnforceLengthRestriction(16, value); }
        }

        public ShortString() { }

        public ShortString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.ShortString;
        }
    }
}
>>>>>>> upstream/master
