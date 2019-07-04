using Forms=System.Windows.Forms;

namespace mwg.Controls{
	/// <summary>
	/// Window �̃��b�Z�[�W����������v���V�[�W����ێ����܂��B
	/// </summary>
	/// <param name="ctrl">���b�Z�[�W���󂯎���� Control ���w�肵�܂��B</param>
	/// <param name="msg">���b�Z�[�W�̓��e���w�肵�܂��B</param>
	/// <returns>
	/// �����������������ǂ�����Ԃ��܂��B
	/// true ��Ԃ����ꍇ�ɂ͎��̃n���h���͌Ăяo����܂���B
	/// false ��Ԃ����ꍇ�ɂ͈����������̃n���h�������s����܂��B
	/// </returns>
	public delegate bool WndProcEventHandler(Control ctrl,ref Forms::Message msg);

	/// <summary>
	/// �R���g���[���̊�{�N���X��񋟂��܂��B
	/// </summary>
	public class Control:Forms::ContainerControl{
		/// <summary>
		/// �R���g���[���̊���̃R���X�g���N�^�ł��B
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
		/// ���b�Z�[�W�̏������s���܂��B
		/// </summary>
		/// <param name="m">�R���g���[���ɓn���ꂽ���b�Z�[�W���w�肵�܂��B</param>
		/// <returns>
		/// ���������s���ꂽ�ꍇ�� true ��Ԃ��܂��B
		/// ����ȊO�̏ꍇ�� false ��Ԃ��܂��B
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
		/// ���̓��b�Z�[�W�̐�ǂ݂��s���܂��B
		/// </summary>
		/// <param name="msg">�R���g���[���ɓn���ꂽ���b�Z�[�W���w�肵�܂��B</param>
		/// <returns>
		/// ���������s���ꂽ�ꍇ�� true ��Ԃ��܂��B
		/// ����ȊO�̏ꍇ�� false ��Ԃ��܂��B
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