#if UNITY_EDITOR
using BASIC.UI.States;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum toolboxTypeParams
{
    All,
    Scene, 
    Item, 
    Optimization
}

public class BASICTOOLBOX_WINDOWSTATES_MAIN : IUIState
{
    private toolboxTypeParams currSearchParams;
    private Vector2 scrollInt;
    private string searchParams = "Search Packages..."; 

    public List<BASICPackage> cachePackageInfos = new();

    public BASICToolbox_PackageDatabase.RootObject packageDatabase { get; internal set; }

    void IUIState.RenderUI()
    {
        if (GUILayout.Button("Refresh Toolbox"))
        {
            cachePackageInfos = BASICToolbox_PackageDatabase.Instance.downloadAllPackages();
        }

        string[] searchTypeStrings = Enum.GetNames(typeof(toolboxTypeParams));
        int selectedIndex = (int)currSearchParams;

        searchParams = EditorGUILayout.TextField(searchParams); 

        currSearchParams = (toolboxTypeParams)EditorGUILayout.Popup("Search Type", selectedIndex, searchTypeStrings);

        scrollInt = EditorGUILayout.BeginScrollView(scrollInt);
        foreach (BASICPackage package in cachePackageInfos.Select(v => (BASICPackage)v))
        {
            if (package.packageType == currSearchParams.ToString() || currSearchParams == toolboxTypeParams.All)
            {
                if ((searchParams == string.Empty || searchParams == "Search Packages...") || package.packageName.Contains(searchParams))
                {
                    EditorGUILayout.BeginVertical("box");
                    GUILayout.Label(
                        $"Name: {package.packageName} \nBASIC Version: {package.verCreated} \nType: {package.packageType} \nDescription: {package.packageInfo}"
                    );

/*                    if (packageDatabase.packages.Contains(package.packageName) == true)
                        GUILayout.Label("Imported", EditorStyles.boldLabel);*/

                    if (package.packagePath == null)
                        EditorGUILayout.HelpBox("Package doesn't contain a .unitypackage", MessageType.Error);

                    if (package.packagePath != null && GUILayout.Button("Import"))
                    {
                        BASICToolbox_PackageDatabase.Instance.DownloadAndInstallPackage(package.packagePath, package);
                    }
                    EditorGUILayout.EndVertical();
                } else
                {
                    continue; 
                }
            }
            else
            {
                continue;
            }
        }
        EditorGUILayout.EndScrollView();
    }

}
#endif