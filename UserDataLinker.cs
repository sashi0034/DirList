using DirList.Configs;
using DirList.Views;
using System;
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
            userData = UserData.ReadFromFile();
            if (userData == null) userData = new UserData();

			init();
        }


        public void SaveUserData()
		{
			onSave();

			userData.WriteSelf();
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