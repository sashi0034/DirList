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

		public UserDataLinker()
		{}

		public static UserDataLinker LoadUserData(MainWindowInfo mainWindowRef)
		{
			var result = new UserDataLinker(mainWindowRef);
			result.load();
			return result;
		}


		private UserDataLinker(MainWindowInfo mainWindowRef)
		{
            _windowRef = mainWindowRef;
            _userData = UserData.ReadFromFile();
            if (_userData == null) _userData = new UserData();
        }


        public void SaveUserData()
		{
			save();

			_userData.WriteSelf();
		}

        private void load()
        {
            foreach (var dir in _userData.DirPathList)
            {
                _windowRef.DirListPanel.AddDir(_windowRef.ConfigRecord, dir);
            }
        }

        private void save()
		{
			_userData.DirPathList = _windowRef.DirListPanel.GetDirList();
		}
	}


}