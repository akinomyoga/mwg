using Forms=System.Windows.Forms;

namespace mwg.Controls{
	/// <summary>
	/// Window のメッセージを処理するプロシージャを保持します。
	/// </summary>
	/// <param name="ctrl">メッセージを受け取った Control を指定します。</param>
	/// <param name="msg">メッセージの内容を指定します。</param>
	/// <returns>
	/// 処理を完了したかどうかを返します。
	/// true を返した場合には次のハンドラは呼び出されません。
	/// false を返した場合には引き続き次のハンドラが実行されます。
	/// </returns>
	public delegate bool WndProcEventHandler(Control ctrl,ref Forms::Message msg);

	/// <summary>
	/// コントロールの基本クラスを提供します。
	/// </summary>
	public class Control:Forms::ContainerControl{
		/// <summary>
		/// コントロールの既定のコンストラクタです。
		/// </summary>
		public Control(){
			this.SetStyle(
				Forms::ControlStyles.AllPaintingInWmPaint|
				Forms::ControlStyles.DoubleBuffer|
				Forms::ControlStyles.UserPaint,
				true);
			this.UpdateStyles();
		}
		//============================================================
		//		event:WndProcess
		//============================================================
		public event WndProcEventHandler WndProcess;
		/// <summary>
		/// メッセージの処理を行います。
		/// </summary>
		/// <param name="m">コントロールに渡されたメッセージを指定します。</param>
		/// <returns>
		/// 処理が実行された場合に true を返します。
		/// それ以外の場合に false を返します。
		/// </returns>
		protected sealed override void WndProc(ref Forms::Message m){
			if(!this.OnWndProcess(ref m))base.WndProc(ref m);
		}
		private bool OnWndProcess(ref Forms::Message msg){
			if(this.WndProcess==null)return false;
			System.Delegate[] delegs=this.WndProcess.GetInvocationList();
			for(int i=delegs.Length-1;i>=0;i--){
				WndProcEventHandler proc=(WndProcEventHandler)delegs[i];
				if(proc(this,ref msg))return true;
			}
			return false;
		}
		//============================================================
		//		event:PreProcess
		//============================================================
		public event WndProcEventHandler PreProcess{
			add{this.Events.AddHandler(EV_PRE_PROC,value);}
			remove{this.Events.RemoveHandler(EV_PRE_PROC,value);}
		}
		private static object EV_PRE_PROC=new object();
		/// <summary>
		/// 入力メッセージの先読みを行います。
		/// </summary>
		/// <param name="msg">コントロールに渡されたメッセージを指定します。</param>
		/// <returns>
		/// 処理が実行された場合に true を返します。
		/// それ以外の場合に false を返します。
		/// </returns>
		public sealed override bool PreProcessMessage(ref Forms::Message msg){
			return this.OnPreProcess(ref msg)||base.PreProcessMessage(ref msg);
		}
		private bool OnPreProcess(ref Forms::Message msg){
			System.Delegate ev=base.Events[EV_PRE_PROC];
			if(ev==null)return false;
			System.Delegate[] delegs=ev.GetInvocationList();
			for(int i=delegs.Length-1;i>=0;i--){
				WndProcEventHandler proc=(WndProcEventHandler)delegs[i];
				if(proc(this,ref msg))return true;
			}
			return false;
		}
	}
}