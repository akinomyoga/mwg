namespace mwg.Parse{
#if LaTeX
	public class LaTeX2HTML{
		/// <summary>
		/// ��͑Ώۂ̕���
		/// </summary>
		string text;
		/// <summary>
		/// ���݂̉�͈ʒu
		/// </summary>
		int index;
		/// <summary>
		/// �����̏W��
		/// </summary>
		System.Collections.Hashtable contexts;
		/// <summary>
		/// ���݂̕�����\�� key
		/// </summary>
		string crnContext;
		/// <summary>
		/// �o�͌���
		/// </summary>
		string output;
		/// <summary>
		/// ���O
		/// </summary>
		string log;
		/// <summary>
		/// �e�֐��̕ϐ��Ȃǂ����L
		/// </summary>
		System.Collections.ArrayList callInfo;
		System.Collections.Hashtable counters;
		System.Collections.Hashtable lengths;
	}
#endif
}