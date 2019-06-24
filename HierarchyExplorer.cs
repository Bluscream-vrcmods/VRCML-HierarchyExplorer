using System.Collections;
using UnityEngine;
using VRCModLoader;
using VRCTools;

namespace Mod
{
    [VRCModInfo("HierarchyExplorer", "1.2", "Slaynash, Bluscream", null, null)]
	public class Mod : VRCMod
	{
		private void OnApplicationStart()
		{
            Utils.Log("Starting");
			ModManager.StartCoroutine(PrintAll());
		}

		private IEnumerator PrintUIDetails()
		{
			yield return VRCUiManagerUtils.WaitForUiManagerInit();
			yield return new WaitForSeconds(10f);
			Canvas componentInChildren = VRCUiManagerUtils.GetVRCUiManager().transform.GetComponentInChildren<Canvas>();
			Utils.Log("Canvas: " + componentInChildren);
			Utils.Log(string.Concat(new object[]
			{
				"Canvas layer: ",
				componentInChildren.sortingLayerName,
				"(",
				componentInChildren.sortingLayerID,
				")"
			}));
			Utils.Log("Canvas tag: " + componentInChildren.tag);
			Utils.Log("Canvas rendermode: " + componentInChildren.renderMode);
			Utils.Log("-----------------------------------------------------");
			PrintHierarchy(VRCUiManagerUtils.GetVRCUiManager().transform.root, 0);
			yield break;
		}

		private IEnumerator PrintAll()
		{
			yield return VRCUiManagerUtils.WaitForUiManagerInit();
			foreach (GameObject gameObject in QuickMenuUtils.GetQuickMenuInstance().gameObject.scene.GetRootGameObjects())
			{
				if (gameObject != null)
				{
					PrintHierarchy(gameObject.transform, 0);
				}
			}
			yield break;
		}
		
		public static void PrintHierarchy(Transform transform, int depth)
		{
			string text = "";
			for (int i = 0; i < depth; i++)
			{
				text += "\t";
			}
			text = text + transform.name + " [";
			Component[] components = transform.GetComponents<Component>();
			for (int j = 0; j < components.Length; j++)
			{
				if (!(components[j] == null))
				{
					if (j == 0)
					{
						text += components[j].GetType();
					}
					else
					{
						text = text + ", " + components[j].GetType();
					}
				}
			}
			text += "]";
            Utils.LogToFile(text);
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				if (transform2 != null)
				{
					DebugUtils.PrintHierarchy(transform2, depth + 1);
				}
			}
		}
	}
}
