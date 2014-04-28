using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MusicOre.Model;

namespace MusicOre
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			AppDomain.CurrentDomain.SetData("DataDirectory", ConfigurationManager.AppSettings["LibraryPath"]);

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Migrations.Configuration>());
			
			LibraryOperations.ScanDirectory(@"D:\MegaSync\Music");
			LibraryOperations.CurrentDeviceMediaEntries.ToList();
		}
	}
}
