﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIKitTutorials.Model.Items
{
    public class VKItemsResponse<T>
    {
        public int count { get; set; }
        public Array items { get; set; }
    }
}
