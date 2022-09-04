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
        [Description("Explorer")]
        Expolorer,
        [Description("Visual Studio Code")]
        Code,
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

        public ProgramForOpenProcessStartInfo GetInfoToExecute()
        {
            switch (Selected)
            {
                case ProgramForOpenKind.Expolorer: return new(false, "explorer.exe");
                case ProgramForOpenKind.Code: return new(true, "code");
            }
            throw new NotImplementedException();
        }

    }
}
