using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public class MyTreeNode
    {
        public string name;
        public List<MyTreeNode> children;

        public MyTreeNode(string name,  List<MyTreeNode> children)
        {
            this.name = name;
            this.children = children;
        }
    }
}
