using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_editor.Libraries
{
    
    public class StylesList
    {
        private readonly List<string> styles = new List<string> { "light", "dark" };

        public List<string> GetStyleList()
        {
            return styles.ToList();
        }

    }

}
