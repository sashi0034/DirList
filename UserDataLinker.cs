using DirList.Configs;
using DirList.Views;
using System;
using System.Windows.Controls;

namespace DirList
{
	public record class MainWindowInfo(
		Configs.ConfigRecord ConfigRecord,
		DirListPanel DirListPanel)
	{ }

	public class UserDataLinker
	{
		private readonly MainWindowInfo _windowRef;
		private readonly UserData _userData;

		private event Action onSave;
        private event Action onLoad;

        public UserDataLinker()
		{}

		public static UserDataLinker LoadUserData(MainWindowInfo mainWindowRef)
		{
			var result = new UserDataLinker(mainWindowRef);
			result.onLoad();
			return result;
		}


		private UserDataLinker(MainWindowInfo mainWindowRef)
		{
            _windowRef = mainWindowRef;
            _userData = UserData.ReadFromFile();
            if (_userData == null) _userData = new UserData();

			init();
        }


        public void SaveUserData()
		{
			onSave();

			_userData.WriteSelf();
		}

        private void init()
        {
            // DirListPanel
            onLoad += () =>
			{
				_windowRef.ConfigRecord.DirListDataInstanceConfig.ResetDataInstanceList(_userData.DataInstanceList, _userData.DataInstanceSelectedIndex);
            };
			onSave += () => 
			{
				_windowRef.ConfigRecord.DirListDataInstanceConfig.ReadFromPanel();
                _userData.DataInstanceList = _windowRef.ConfigRecord.DirListDataInstanceConfig.DataInstanceList;
				_userData.DataInstanceSelectedIndex = _windowRef.ConfigRecord.DirListDataInstanceConfig.SelectedIndex;
            };

			// DirListSortKind
			onLoad += () =>
			{
				_windowRef.ConfigRecord.DirListSort.Selected = _userData.SortKind;
			};
			onSave += () =>
			{
				_userData.SortKind = _windowRef.ConfigRecord.DirListSort.Selected;
			};
        }
	}


}