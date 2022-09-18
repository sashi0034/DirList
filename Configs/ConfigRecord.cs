using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Configs
{

    public record ConfigRecord(
        OptionOnCopy OptionOnCopy,
        ProgramForOpen ProgramForOpen,
        DirListSort DirListSort,
        DirListDataInstanceConfig DirListDataInstanceConfig
        )
    {}
}
