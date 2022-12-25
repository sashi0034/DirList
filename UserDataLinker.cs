using DirList.Configs;
using DirList.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace DirList
{
	public record class MainWindowInfo(
		Configs.ConfigRecord ConfigRecord,
		DirListPanel DirListPanel,
		TabSwitchPanel TabSwitchPanel)
	{ }

	public class UserDataLinker
	{
		private readonly MainWindowInfo windowRef;
		private readonly UserData userData;

		private event Action onSave;
        private event Action onLoad;

        private const string dataPath = @"DirListData.xml";
        private const string dataPathBackup = @"DirListData1.xml";

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
            windowRef = mainWindowRef;
            userData = loadData();

			init();
        }

		private static UserData loadData() 
		{ 
			var data = UserData.ReadFromFile(dataPath);
			if (data != null) return data;

            var dataBackup = UserData.ReadFromFile(dataPathBackup);
            if (dataBackup != null) return dataBackup;

            return new UserData();
        }

        public void SaveUserData()
        {
            onSave();

            backupMainDataPath();

            userData.WriteSelf(dataPath);
        }

        private static void backupMainDataPath()
        {
            try
            {
				if (File.Exists(dataPath) == false) return;
                if (File.Exists(dataPathBackup) == true) File.Delete(dataPathBackup);
                File.Move(dataPath, dataPathBackup);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void init()
        {
            // DirListPanel
            onLoad += () =>
			{
				windowRef.ConfigRecord.DirListDataInstanceConfig.ResetDataInstanceList(userData.DataInstanceList, userData.DataInstanceSelectedIndex);
            };
			onSave += () => 
			{
				windowRef.ConfigRecord.DirListDataInstanceConfig.ReadFromPanel();
                userData.DataInstanceList = windowRef.ConfigRecord.DirListDataInstanceConfig.DataInstanceList;
				userData.DataInstanceSelectedIndex = windowRef.ConfigRecord.DirListDataInstanceConfig.SelectedIndex;
            };

			// DirListSortKind
			onLoad += () =>
			{
				windowRef.ConfigRecord.DirListSort.Selected = userData.SortKind;
			};
			onSave += () =>
			{
				userData.SortKind = windowRef.ConfigRecord.DirListSort.Selected;
			};

			// TabSwitchPanel
			onLoad += () =>
			{
				windowRef.TabSwitchPanel.Setup(userData.TabData, windowRef.ConfigRecord.DirListDataInstanceConfig);
			};
			onSave += () =>
			{
				userData.TabData = windowRef.TabSwitchPanel.Save();
			};
        }
	}


}