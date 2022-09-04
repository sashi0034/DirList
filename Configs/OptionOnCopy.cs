using DirList.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Configs
{
    public enum OptionOnCopyKind
    {
        [Description("通常")]
        Normal,
        [Description(@"'wslpath -u {PATH}'として")]
        WslPathCommand,
        [Description(@"'$(wslpath -u {PATH})'として")]
        WslPathAsString,
    }

    public class OptionOnCopy
    {
        private readonly Views.ConfigComboBox _view;
        public OptionOnCopy(Views.ConfigComboBox view)
        {
            var descriptionList = UtilFunc.GetEnumDescriptionList<OptionOnCopyKind>();
            view.ResetComboItem(descriptionList);
            _view = view;
        }
        public OptionOnCopyKind Selected => _view.SelectedIndex<OptionOnCopyKind>();

        public string GetFormatedPath(DirPath dir)
        {
            var baseText = dir.Path;

            switch (Selected)
            {
                case OptionOnCopyKind.WslPathCommand:
                    return $"wslpath -u {baseText}";
                case OptionOnCopyKind.WslPathAsString:
                    return $"$(wslpath -u {baseText})";
                default:
                    return baseText;
            }
        }
    }
}
