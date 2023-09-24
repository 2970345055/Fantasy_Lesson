using Fantasy.Core;

namespace Fantasy
{
	public partial class Login : UI
	{
		public override string AssetName { get; protected set; } = "Login";
		public override string BundleName { get; protected set; } = "loginui";
		public override UILayer Layer { get; protected set; } = UILayer.BaseRoot;

		public UnityEngine.UI.Button LoginButton;
		public UnityEngine.UI.InputField PassWord;
		public UnityEngine.UI.InputField UserName;

		public override void Initialize()
		{
			var referenceComponent = GameObject.GetComponent<FantasyUI>();
			LoginButton = referenceComponent.GetReference<UnityEngine.UI.Button>("LoginButton");
			PassWord = referenceComponent.GetReference<UnityEngine.UI.InputField>("PassWord");
			UserName = referenceComponent.GetReference<UnityEngine.UI.InputField>("UserName");
		}
	}
}
