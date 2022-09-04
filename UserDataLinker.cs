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
				foreach (var dir in _userData.DirPathList)
				{
					_windowRef.DirListPanel.AddDir(_windowRef.ConfigRecord, dir);
				}
			};
			onSave += () =>
			{
				_userData.DirPathList = _windowRef.DirListPanel.GetDirList();
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