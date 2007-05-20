namespace mwg.Parse{
#if LaTeX
	public class LaTeX2HTML{
		/// <summary>
		/// 解析対象の文章
		/// </summary>
		string text;
		/// <summary>
		/// 現在の解析位置
		/// </summary>
		int index;
		/// <summary>
		/// 文脈の集合
		/// </summary>
		System.Collections.Hashtable contexts;
		/// <summary>
		/// 現在の文脈を表す key
		/// </summary>
		string crnContext;
		/// <summary>
		/// 出力結果
		/// </summary>
		string output;
		/// <summary>
		/// ログ
		/// </summary>
		string log;
		/// <summary>
		/// 親関数の変数などを共有
		/// </summary>
		System.Collections.ArrayList callInfo;
		System.Collections.Hashtable counters;
		System.Collections.Hashtable lengths;
	}
#endif
}