using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiscUtil;
using System.IO;

/// <summary>
/// Collects statistics and saves it to a file
/// </summary>
public class StatisticsCollector : MonoBehaviour  {

	/// <summary>
	/// The internal count of statistics
	/// </summary>
	private static Counter<string> stats = new Counter<string>();

	/// <summary>
	/// The file name to save the sats under
	/// </summary>
	private static readonly string fileName = "/GameStats";

	/// <summary>
	/// The file type for the stats
	/// </summary>
	private static readonly string fileExtenstion = ".csv";

	/// <summary>
	/// Reports an occurance of a stat
	/// </summary>
	/// <param name="name"></param>
	/// <param name="amt"></param>
	public static void ReportInstance(string name, int amt = 1) {
		stats[name] += amt;
	}

	/// <summary>
	/// Clears the internal stat counter
	/// </summary>
	public static void ClearStats() {
		stats = new Counter<string>();
	}

	/// <summary>
	/// Saves the stats to a file
	/// </summary>
	public static void LogStats() {
		//Debug.Log(stats.ToString());

		//Count all the current stats files
		int num = 0;
		while (File.Exists(Application.persistentDataPath + fileName + num + fileExtenstion)) {
			num++;
		}

		//Prepare the file string
		string output = "";
		foreach (string key in stats.GetKeys()) {
			output += key + ", " + stats[key] + "\n";
		}

		//Only save the file if we have something to report.
		if (output != "")
			File.WriteAllText(Application.persistentDataPath + fileName + num + fileExtenstion, output);
	}
}
