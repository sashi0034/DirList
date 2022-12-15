using DirList.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DirList.Configs
{
    public enum ProgramForOpenKind
    {
        [Description("[ 以前の開き方 ]")]
        Default,

        [Description("Explorer")]
        Expolorer,
        
        [Description("Visual Studio Code")]
        Code,

        [Description("Visual Studio Code [ WSL ]")]
        CodeAsWsl,

    }

    public record struct ProgramForOpenProcessStartInfo(
        bool UseShellExecute,
        string FileName
        );

    public class ProgramForOpen
    {
        private readonly Views.ConfigComboBox _view;
        public ProgramForOpen(Views.ConfigComboBox view)
        {
            var descriptionList = UtilFunc.GetEnumDescriptionList<ProgramForOpenKind>();
            view.ResetComboItem(descriptionList);
            _view = view;
        }
        public ProgramForOpenKind Selected => _view.GetSelectedIndex<ProgramForOpenKind>();

    }
}
